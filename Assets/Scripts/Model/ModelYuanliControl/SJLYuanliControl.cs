using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SJLYuanliControl : MonoBehaviour
{
    System.Action StartAction;
    System.Action EndAction;
    public string YuanliText;
    private void Awake()
    {

        StartAction = SJLYuanli;
        EndAction = SJLJeshu;
    }
    private void OnEnable()
    {
        StartAction?.Invoke();
        UIManage.Instance.SetModelYuanliJiesao(YuanliText);
    }
    private void OnDisable()
    {
        EndAction?.Invoke();
    }
    void SJLYuanli()
    {
        UIManage.Instance.SetXuexiHide("正在播放" + name + "工作状态动画");
    }
    void SJLJeshu()
    {

    }
}
