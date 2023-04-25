using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class PlayMp4 : MonoBehaviour
{
    public string VideoName;
    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponentInChildren<VideoPlayer>();
        string mPath = Path.Combine(Application.streamingAssetsPath, VideoName);
        mPath = mPath.Replace("/file:/", "file://");
        video.url = mPath;
    }

    private void OnEnable()
    {
        video.Play();
    }
}
