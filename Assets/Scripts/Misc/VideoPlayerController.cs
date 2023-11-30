using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    public GameObject canvasUI;
    public VideoPlayer videoPlayer;
    public GameObject skipTooltip;
    public PauseMenu pauseMenu;
    public string loadSceneAfterFinish;


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
        pauseMenu.videoPlaying = true;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Stop();
        Time.timeScale = 1.0f;
        canvasUI.SetActive(true);
        InputReader.Instance.EnableFreelookInputReader();
        AudioManager.Instance.UnmuteBGM();
        videoPlayer.loopPointReached -= OnVideoFinished;
        skipTooltip.SetActive(false);
        pauseMenu.videoPlaying = false;
        if (!string.IsNullOrEmpty(loadSceneAfterFinish))
        {
            SceneManager.LoadSceneAsync(loadSceneAfterFinish, LoadSceneMode.Single);
        }
        this.gameObject.SetActive(false);
    }
}
