using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public GameObject canvasUI;
    public VideoPlayer videoPlayer;
    public GameObject skipTooltip;

    private void Update()
    {
        if (!videoPlayer.isPlaying) return;
        if (Gamepad.current != null)
        {
            if (Gamepad.current.selectButton.IsPressed())
            {
                OnVideoFinished(videoPlayer);
            }
        }
        if(Input.GetKeyUp(KeyCode.X))
        {
            OnVideoFinished(videoPlayer);
        }
    }

    public void PlayVideo()
    {
        AudioManager.Instance.MuteBGM();
        InputReader.Instance.DisableFreelookInputReader();
        canvasUI.SetActive(false);
        Time.timeScale = 0.0f;
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("CutsceneAudio"));
        skipTooltip.SetActive(true);
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Stop();
        Time.timeScale = 1.0f;
        canvasUI.SetActive(true);
        InputReader.Instance.EnableFreelookInputReader();
        AudioManager.Instance.UnmuteBGM();
        videoPlayer.loopPointReached -= OnVideoFinished;
        this.gameObject.SetActive(false);
        skipTooltip.SetActive(false);
    }
}
