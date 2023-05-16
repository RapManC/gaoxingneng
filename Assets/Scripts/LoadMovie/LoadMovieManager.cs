using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class LoadMovieManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    public GameObject QuitButton;
    private float showTime;
    public float ButtonShowTime = 5f;

    private void Start()
    {
        ToEndVideo();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!QuitButton.activeSelf)
            {
                QuitButton.SetActive(true);
            }
            showTime = ButtonShowTime;
        }
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;
            if(showTime <=0)
                QuitButton.SetActive(false);
        }
    }

    void ToEndVideo()
    {
        videoPlayer.loopPointReached += EndWithVideoPlay;
    }

    /// <summary>
    /// 播放结束逻辑
    /// </summary>
    /// <param name="thisPlay"></param>
    void EndWithVideoPlay(VideoPlayer thisPlay)
    {
        Debug.Log("视频播放完毕动作！");
        StartCoroutine(LoadScene());
    }

    public void OnCloseMovie() {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("honggui");
        yield return ao;
    }
}
