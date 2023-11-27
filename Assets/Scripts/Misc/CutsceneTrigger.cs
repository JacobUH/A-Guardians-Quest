using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public int videoIndex;
    public bool triggerCutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerCoroutine());
        }
    }

    public IEnumerator TriggerCoroutine()
    {
        InputReader.Instance.DisableFreelookInputReader();
        float fadeDuration = FadeScreen.Instance.GetFadeDuration();

        FadeScreen.Instance.FadeOut();
        yield return new WaitForSeconds(fadeDuration);

        FadeScreen.Instance.FadeIn();
        VideoManager.Instance.PlayVideo(videoIndex);
        if(!triggerCutscene) transform.parent.gameObject.SetActive(false);
    }
}
