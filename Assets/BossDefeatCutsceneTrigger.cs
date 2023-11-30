using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeatCutsceneTrigger : MonoBehaviour
{
    public Character character;
    public int videoIndex;
    public bool triggerCutscene;

    private void Start()
    {
        character = GetComponent<Character>();
        character.DieEvent += CutsceneTrigger;
    }

    public void CutsceneTrigger(GameObject thisObject)
    {
        if (gameObject != thisObject) return;
        StartCoroutine(TriggerCoroutine());
    }

    public IEnumerator TriggerCoroutine()
    {
        InputReader.Instance.DisableFreelookInputReader();
        float fadeDuration = FadeScreen.Instance.GetFadeDuration();

        FadeScreen.Instance.FadeOut();
        yield return new WaitForSeconds(fadeDuration);

        FadeScreen.Instance.FadeIn();
        VideoManager.Instance.PlayVideo(videoIndex);
        if (!triggerCutscene) this.gameObject.SetActive(false);
    }
}
