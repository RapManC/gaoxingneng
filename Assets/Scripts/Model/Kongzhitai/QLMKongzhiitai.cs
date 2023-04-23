using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QLMKongzhiitai : MonoBehaviour
{
    [HideInInspector] public Transform Canvas_3D;
    [HideInInspector] public Transform Qingporu_ButtonPather;
    QiliumoTestManlag qiliumoTestManlag { get { return GetManager.Instance.InteractiveModel.Find("QilimoParent").GetComponent<QiliumoTestManlag>(); } }
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
        Canvas_3D.gameObject.SetActive(true);
        Canvas3Dto2D.Instance.SetCameraTrans(2);
    }
    /// <summary>
    /// 加氮气
    /// </summary>
    public void Onjiadanqi()
    {
        MainSceneGuide.Instance.StopAutoMove();
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Jiadanqi").GetComponent<Button>(), false);
        qiliumoTestManlag.Jiadanqi();
    }
    /// <summary>
    /// 碰撞破碎
    /// </summary>
    public void OnPengzhuanposhui()
    {
        UIManage.Instance.SetButtonIntera(Qingporu_ButtonPather.Find("Pengzhuangposhui").GetComponent<Button>(), false);
        qiliumoTestManlag.Pengzhuangposhui();
    }
}
