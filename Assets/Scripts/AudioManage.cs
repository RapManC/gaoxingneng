using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音乐管理器
/// </summary>
public class AudioManage : MonoBehaviour
{
    private static AudioManage instance;
    public static AudioManage Instance => instance;
    
    //背景音乐播放器
    private AudioSource musicSource;
    //音效播放器
    private AudioSource soundSource;
    //默认播放的背景音乐
    private AudioClip   BGClip;
    //默认播放器的背景音乐大小
    private float mousicSize;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }

        //给播放器添加组件
        musicSource = this.gameObject.AddComponent<AudioSource>();
        soundSource = this.gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 初始化背景音乐
    /// </summary>
    /// <param name="path">播放音乐的位置</param>
    /// <param name="size">播放音乐的大小</param>
    public void PlayMusicSource(string path,float size)
    {
        AudioClip clip1 = Resources.Load<AudioClip>("Audio/第二次/"+path);
        BGClip = clip1;
        mousicSize = size;
        musicSource.clip = BGClip;
        musicSource.volume = mousicSize;
        musicSource.Play();
    }
    /// <summary>
    /// 重载播放，无须位置,无需声音大小
    /// </summary>
    public void PlayMusicSource(float size)
    {
        musicSource.clip = BGClip;
        mousicSize = size;
        musicSource.volume = mousicSize;
        musicSource.Play();
    }
    //改变大小
    public void AlterMusic(float size)
    {
        musicSource.volume = size;
    }
    /// <summary>
    /// 重载播放，无须位置,无需声音大小
    /// </summary>
    public void PlayMusicSource()
    {
        musicSource.clip = BGClip;
        musicSource.Play();
    }
    /// <summary>
    /// 关闭背景音乐
    /// </summary>
    public void IsCloseMusicSource(bool isClose)
    {
        if (isClose)
        {
            musicSource.Play();
        }
        else
            musicSource.Pause();
    }

     //音效播放器
    public void PlaySoundSource(string path,float size)
    {
        AudioClip clip1 = Resources.Load<AudioClip>("Audio/第二次/" + path);
        Debug.Log("Play："+clip1 == null );
        
        soundSource.clip = clip1;
        soundSource.volume = size;
        soundSource.Play();
    }

}
