using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : SingletonMonobehaviour<VideoManager>
{
    public List<GameObject> videos = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        for(int i = 0; i< videos.Count; i++)
        {
            videos[i].GetComponent<VideoPlayer>().Prepare();
        }
    }

    public void PlayVideo(int index)
    {
        videos[index].GetComponent<VideoPlayerController>().PlayVideo();
    }
}
