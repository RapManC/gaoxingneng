using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QXCXYuanliControl : MonoBehaviour
{
    System.Action StartAction;
    System.Action EndAction;
    QXCXKuaifasheqi kuaifasheqi;
    public string YuanliText;
    int count;
    private void Awake()
    {
        StartAction = QXCXYuanli;
        EndAction = QXCXJeshu;
        kuaifasheqi = transform.Find("KuaiFashenqi").GetComponent<QXCXKuaifasheqi>();
    }
    private void OnEnable()
    {
        count = 0;
        StartAction?.Invoke();
        UIManage.Instance.SetModelYuanliJiesao(YuanliText);
    }
    private void OnDisable()
    {
        EndAction?.Invoke();
    }
    /// <summary>
    /// 取向成型
    /// </summary>
    void QXCXYuanli()
    {
        UIManage.Instance.SetXuexiHide("正在播放" + name + "工作状态动画");
        GetComponent<Animator>().enabled = true;
    }
    public void AnimEnd()
    {
        kuaifasheqi.Shengcheng();
        GetComponent<Animator>().Play("YuanliAnimation2");
    }
    void QXCXJeshu()
    {
        transform.Find("Kuai").localEulerAngles = new Vector3(0, 90, 0);
        foreach (Transform tran in transform.Find("KuaiFashenqi"))
        {
            if (tran.name != "TargetKuai")
                Destroy(tran.gameObject);
        }
    }
    void Anim2End()
    {
        kuaifasheqi.Shengcheng();
        count++;
        if (count > 14)
            GetComponent<Animator>().enabled = false;
    }
}
