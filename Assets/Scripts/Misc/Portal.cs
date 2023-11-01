using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
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
        FadeScreen.Instance.FadeOut();
        float fadeDuration = FadeScreen.Instance.GetFadeDuration();
        InputReader.Instance.DisableFreelookInputReader();
        CharacterController controller = other.GetComponent<CharacterController>();

        yield return new WaitForSeconds(fadeDuration);

        controller.enabled = false;
        other.gameObject.transform.position = destination.position;
        other.gameObject.transform.rotation = destination.rotation;
        controller.enabled = true;

        yield return new WaitForSeconds(1f);

        FadeScreen.Instance.FadeIn();
        yield return new WaitForSeconds(fadeDuration);

        InputReader.Instance.EnableFreelookInputReader();
        if (bgmIndex != -1) AudioManager.Instance.ChangeBMG(bgmIndex);
        yield break;
    }
}
