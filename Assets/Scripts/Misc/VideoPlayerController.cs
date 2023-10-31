using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public GameObject canvasUI;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        AudioManager.Instance.MuteBGM();
        InputReader.Instance.DisableFreelookInputReader();
        canvasUI.SetActive(false);
        Time.timeScale = 0.0f;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    private void OnEnable()
    {
        AudioManager.Instance.MuteBGM();
        InputReader.Instance.DisableFreelookInputReader();
        canvasUI.SetActive(false);
        Time.timeScale = 0.0f;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.startButton.IsPressed())
            {
                OnVideoFinished(videoPlayer);
            }
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            OnVideoFinished(videoPlayer);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Stop();
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        canvasUI.SetActive(true);
        InputReader.Instance.EnableFreelookInputReader();
        AudioManager.Instance.UnmuteBGM();
    }
}
