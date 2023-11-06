using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : SingletonMonobehaviour<FadeScreen>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration;

    protected override void Awake()
    {
        base.Awake();
        fadeImage.gameObject.SetActive(true);
        FadeIn();
    }

    private IEnumerator FadeCoroutine(float waitTime)
    {
        FadeOut();
        yield return new WaitForSeconds(waitTime + fadeDuration);
        FadeIn();
    }

    public void Fade(float waitTime)
    {
        StartCoroutine(FadeCoroutine(waitTime));
    }

    public void FadeIn()
    {
        Color newColor = new Color(0, 0, 0, 0);
        fadeImage.CrossFadeColor(newColor, fadeDuration, true, true);
    }

    public void FadeOut()
    {
        Color newColor = new Color(0, 0, 0, 1f);
        fadeImage.CrossFadeColor(newColor, fadeDuration, true, true);
    }

    public float GetFadeDuration()
    {
        return fadeDuration;
    }
}
