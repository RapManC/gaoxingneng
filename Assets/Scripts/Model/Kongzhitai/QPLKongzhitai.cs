using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QPLKongzhitai : MonoBehaviour
{
    [HideInInspector] public Transform Canvas_3D;
    [HideInInspector] public Transform Qingporu_ButtonPather;
    QingporuTestManlag qingporuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("QingporuParent").GetComponent<QingporuTestManlag>(); } }
    public void Awake()
    {
        Canvas_3D = transform.Find("Canvas_3D");
        Qingporu_ButtonPather = Canvas_3D.Find("Button_UI");
    }
    private void Start()
    {
        UIManage_3D._Instance.KongzhitaiPingmuList.Add(gameObject);
        foreach (Transform tran in transform.Find("Canvas_3D/Content/Input_UI"))
        {
            if (tran.GetComponent<ParameterControl>())
            {
                UIManage_3D._Instance.ParameterControlList.Add(tran.GetComponent<ParameterControl>());
            }
        }
    }
    private void OnDestroy()
    {
        UIManage_3D._Instance.KongzhitaiPingmuList.Remove(gameObject);

        foreach (Transform tran in transform.Find("Canvas_3D/Content/Input_UI"))
        {
            if (tran.GetComponent<ParameterControl>())
            {
                UIManage_3D._Instance.ParameterControlList.Remove(tran.GetComponent<ParameterControl>());
            }
        }
    }
public void ShowCanvas()
    {
        GetManager.Instance.ControlFlow.Jingxingzong(4);
        Canvas_3D.gameObject.SetActive(true);
        Canvas3Dto2D.Instance.SetCameraTrans(1);
    }
    #region 氢破炉按键操作

    /// <summary>
    /// 氢破炉升温
    /// </summary>
    public void OnQPShengwen()
    {
        MainSceneGuide.Instance.StopAutoMove();
        qingporuTestManlag.Shengwen();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Shengwen").GetComponent<Button>(), false);
    }
    /// <summary>
    /// 氢破炉抽真空
    /// </summary>
    public void OnQPCouzhengkong()
    {
        qingporuTestManlag.Chouzhenkong();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Chouzhengkong").GetComponent<Button>(), false);
    }
    /// <summary>
    /// 氢破炉加压
    /// </summary>
    public void OnQPJiaya()
    {
        qingporuTestManlag.Jiaya();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Jiaya").GetComponent<Button>(), false);
    }
    /// <summary>
    /// 氢破炉破碎
    /// </summary>
    public void OnQPPosui()
    {
        qingporuTestManlag.Poshui();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Posui").GetComponent<Button>(), false);
    }
    /// <summary>
    /// 氢破炉吸氢
    /// </summary>
    public void OnQPXiqing()
    {
        qingporuTestManlag.Xiqing();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Xiqing").GetComponent<Button>(), false);
    }
    /// <summary>
    /// 氢破炉降温
    /// </summary>
    public void OnQPJiangwen()
    {
        qingporuTestManlag.Jiangwen();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Jiangwen").GetComponent<Button>(), false);
    }
    #endregion
}
