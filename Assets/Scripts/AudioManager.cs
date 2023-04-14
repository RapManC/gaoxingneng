using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager 
{
    public static AudioSource BGAudio { get { return GameObject.Find("Root").GetComponent<AudioSource>(); } }
    public static AudioSource CanvasAudio { get { return GetManager.Instance.Root.Find("Canvas").GetComponent<AudioSource>(); } }
    public static AudioSource QingporuAudio { get { return GetManager.Instance.QingporuParent.GetComponent<AudioSource>(); } }
    public static AudioSource ShuningluAudio { get { return GetManager.Instance.ShuningLuParent.GetComponent<AudioSource>(); } }

    public static void SetAudio( AudioSource source ,string audioiName)
    {
       AudioClip clip= Resources.Load<AudioClip>("Audio/" + audioiName);
        if (source != null && clip != null)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            if (source == null)
            {
                Debug.LogWarning("没有找到声音源,音频文件"+ audioiName);
            }
            else
            {
                Debug.LogWarning("没有找到音频:" + audioiName);
            }
        }
    }
}
