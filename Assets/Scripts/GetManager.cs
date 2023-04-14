using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManager :Base<GetManager>
{
    public Transform Root { get {return GameObject.Find("Root").transform; } }
    public Transform Canvas { get { return GameObject.Find("Root").transform.Find("Canvas"); } }
    public Transform Cameras { get { return Root.Find("Cameras"); } }
    public Transform InteractiveModel { get { return Root.Find("InteractiveModel"); } }
    public ControlFlow ControlFlow { get { return Canvas.Find("Shiyan_UI/CaozhuoLiuchen_UI").GetComponent<ControlFlow>(); } }

    public Transform ShuningLuParent { get { return InteractiveModel.Find("ShuningLuParent"); } }
    public Transform QingporuParent { get { return InteractiveModel.Find("QingporuParent"); } }
    public Transform QilimoParent { get { return InteractiveModel.Find("QilimoParent"); } }
    public Transform ShaojierLuParent { get { return InteractiveModel.Find("ShaojierLuParent"); } }

    public SNLKongzhitai SNLKongzhitai { get { return ShuningLuParent.Find("SNLKongzhitai").GetComponent<SNLKongzhitai>(); } }
    public QPLKongzhitai QPLKongzhitai { get { return QingporuParent.Find("QPLKongzhitai").GetComponent<QPLKongzhitai>(); } }
    public QLMKongzhiitai QLMKongzhiitai { get { return QilimoParent.Find("QLMKongzhitai").GetComponent<QLMKongzhiitai>(); } }
    public SJLKongzhiitai SJLKongzhiitai { get { return ShaojierLuParent.Find("Saojielu_Kongzhitai").GetComponent<SJLKongzhiitai>(); } }

    /// <summary>
    /// 关闭所有控制台
    /// </summary>
    public void ColseAllKongzhitai()
    {
        List<GameObject> KongzhitaiList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Kongzhitai"));
        foreach(var v in KongzhitaiList)
        {
            v.transform.Find("Canvas_3D").gameObject.SetActive(false);
        }
    }
}
