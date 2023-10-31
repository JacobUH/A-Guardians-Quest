using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public GameObject canvasUI;
    public VideoPlayer videoPlayer;

    private void OnEnable()
    {
        InputReader.Instance.DisableFreelookInputReader();
        canvasUI.SetActive(false);
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void Update()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.IsPressed())
            {
                OnVideoFinished(videoPlayer);
            }
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoPlayer.Stop();
        this.gameObject.SetActive(false);
        canvasUI.SetActive(true);
        InputReader.Instance.EnableFreelookInputReader();
    }
}
