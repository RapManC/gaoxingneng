using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayMp4 : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(UIManage.Instance.enumerator(0, () => {
            transform.Find("MP4RawImage").GetComponent<VideoPlayer>().Play();
        }));
    }
}
