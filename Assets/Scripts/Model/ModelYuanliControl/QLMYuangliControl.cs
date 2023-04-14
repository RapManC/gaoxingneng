using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLMYuangliControl : MonoBehaviour
{
    System.Action StartAction;
    System.Action EndAction;
    public string YuanliText;
    ParticleSystem jingliao;
    ParticleSystem poshui;
    ParticleSystem chuliao;


    private void Awake()
    {
        jingliao = transform.Find("Jingliao").GetComponent<ParticleSystem>();
        poshui = transform.Find("Poshui").GetComponent<ParticleSystem>();
        chuliao = transform.Find("Chuliao").GetComponent<ParticleSystem>();
        StartAction = SeartYuanli;
        EndAction = EndYuanli;
    }
    void SeartYuanli()
    {

        UIManage.Instance.SetModelYuanliJiesao(YuanliText);
        UIManage.Instance.SetXuexiHide("正在播放" + name + "工作状态动画");
        jingliao.gameObject.SetActive(true);
        StartCoroutine(UIManage.Instance.enumerator(20, () => {
            jingliao.gameObject.SetActive(false);
            poshui.gameObject.SetActive(true);
            StartCoroutine(UIManage.Instance.enumerator(7, () => {
                chuliao.gameObject.SetActive(true);
            }));
        }));
    }
    void EndYuanli()
    {
        jingliao.gameObject.SetActive(false);
        poshui.gameObject.SetActive(false);
        chuliao.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartAction?.Invoke();
    }
    private void OnDisable()
    {
        EndAction?.Invoke();
    }
}
