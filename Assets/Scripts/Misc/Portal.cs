using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string loadSceneName;
    public string unloadSceneName;
    public Transform destination;
    public int bgmIndex = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (destination == null) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportCoroutine(other));
        }
    }

    public IEnumerator TeleportCoroutine(Collider other)
    {
        InputReader.Instance.DisableFreelookInputReader();
        CharacterController controller = other.GetComponent<CharacterController>();

        FadeScreen.Instance.FadeOut();
        float fadeDuration = FadeScreen.Instance.GetFadeDuration();
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        controller.enabled = false;
        other.gameObject.transform.position = destination.position;
        other.gameObject.transform.rotation = destination.rotation;
        controller.enabled = true;

        yield return new WaitForSeconds(2f);

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(unloadSceneName);
        while (!unloadOperation.isDone)
        {
            yield return null;
        }

        if (bgmIndex != -1) AudioManager.Instance.ChangeBMG(bgmIndex);
        FadeScreen.Instance.FadeIn();
        yield return new WaitForSeconds(fadeDuration);

        InputReader.Instance.EnableFreelookInputReader();
        yield break;
    }
}
