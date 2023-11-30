using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject quitButton;

    private void OnEnable()
    {
        GameManager.Instance.SetInMenuBool(true);
        retryButton.SetActive(true);
        quitButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(retryButton);
    }

    public void OnRetry()
    {
        StartCoroutine(RetryCoroutine(GameObject.FindGameObjectWithTag("Player")));
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator RetryCoroutine(GameObject player)
    {
        InputReader.Instance.DisableFreelookInputReader();
        CharacterController controller = player.GetComponent<CharacterController>();
        PlayerStateMachine psm = player.GetComponent<PlayerStateMachine>();
        Character character = player.GetComponent<Character>();

        FadeScreen.Instance.FadeOut();
        float fadeDuration = FadeScreen.Instance.GetFadeDuration();
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(psm.saveScene);
        while (!unloadOperation.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(psm.saveScene, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        controller.enabled = false;
        player.transform.position = psm.savePosition;
        player.transform.rotation = psm.saveRotation;
        controller.enabled = true;
        psm.forceReceiver.enabled = true;
        character.Revive();
        psm.targetManager.ClearTargetList();
        psm.SwitchState(new PlayerFreeLookState(psm));

        retryButton.SetActive(false);
        quitButton.SetActive(false);

        FadeScreen.Instance.FadeIn();
        yield return new WaitForSeconds(fadeDuration);

        InputReader.Instance.EnableFreelookInputReader();
        GameManager.Instance.SetInMenuBool(false);
        this.gameObject.SetActive(false);
        yield break;
    }
}
