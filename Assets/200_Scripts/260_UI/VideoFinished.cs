using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFinished : MonoBehaviour
{
    [SerializeField]
    VideoPlayer myVideoPlayer;
    public GameObject selfVideo;
    void Start()
    {
        myVideoPlayer.loopPointReached += DoSomethingWhenVideoFinished;
    }

    void DoSomethingWhenVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video is finished");
        selfVideo.SetActive(false);
    }
}
