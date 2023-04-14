using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class XPVideo : MonoBehaviour
{

    RawImage rawImage;
    VideoPlayer videoPlayer;

    void OnEnable()
    {
        rawImage = this.GetComponent<RawImage>();
        videoPlayer = this.GetComponent<VideoPlayer>();

        if (videoPlayer.source == VideoSource.VideoClip)
        {
            if (videoPlayer.clip == null)
            {
                Debug.Log("视频为空");
                videoPlayer.clip = Resources.Load<VideoClip>("chazhi");
            }
        }
        else
        {
            Debug.Log(videoPlayer);
            Debug.Log("URL前:"+videoPlayer.url);
            videoPlayer.url =  Application.streamingAssetsPath + "/Chrome.ogv";
            Debug.Log("URL后:" + videoPlayer.url);
        }
        videoPlayer.Play();
        this.GetComponent<Button>().onClick.RemoveAllListeners();
        this.GetComponent<Button>().onClick.AddListener(()=> {
            if (videoPlayer.isPlaying)
                videoPlayer.Pause();
            else
                videoPlayer.Play();
        });
       // videoPlayer.Play();

    }

    private void OnDisable()
    {
        videoPlayer.Pause();
    }

    // Update is called once per frame
    void Update()
    {
      // rawImage.texture = videoPlayer.texture;
        
    }
}
