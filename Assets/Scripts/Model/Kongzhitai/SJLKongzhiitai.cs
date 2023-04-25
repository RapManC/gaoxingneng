using UnityEngine;
using UnityEngine.UI;

public class SJLKongzhiitai : MonoBehaviour
{
    [HideInInspector] public Transform Canvas_3D;
    [HideInInspector] public Transform Saojielu_ButtonPather;
    ShaojierLuTestManlag shaojierLuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("ShaojierLuParent").GetComponent<ShaojierLuTestManlag>(); } }
    public void Awake()
    {
        Canvas_3D = transform.Find("Canvas_3D");
        Saojielu_ButtonPather = Canvas_3D.Find("Button_UI");
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
    }
    /// <summary>
    /// 抽真空
    /// </summary>
    public void OnChouzhenkong()
    {
        MainSceneGuide.Instance.StopAutoMove();
        UIManage.Instance.SetButtonIntera(Saojielu_ButtonPather.Find("Chouzhenkong").GetComponent<Button>(), false);
        shaojierLuTestManlag.Chouzhenkong();
    }
    /// <summary>
    /// 充氮气
    /// </summary>
    public void OnChongdanqi()
    {
        UIManage.Instance.SetButtonIntera(Saojielu_ButtonPather.Find("Chongdanqi").GetComponent<Button>(), false);
        shaojierLuTestManlag.Chongdianqi();
    }
    /// <summary>
    /// 设备内运输
    /// </summary>
    public void OnShebeileiyunshu()
    {
        MainSceneGuide.Instance.StopAutoMove();
        UIManage.Instance.SetButtonIntera(Saojielu_ButtonPather.Find("Shebeileiyunshu").GetComponent<Button>(), false);
        shaojierLuTestManlag.Shebeileiyunshu();
    }
    /// <summary>
    /// 烧结
    /// </summary>
    public void OnShaojie()
    {
        UIManage.Instance.SetButtonIntera(Saojielu_ButtonPather.Find("Shaojie").GetComponent<Button>(), false);
        shaojierLuTestManlag.Shaojie();
    }
    /// <summary>
    /// 降温
    /// </summary>
    public void OnJiangwen()
    {
        UIManage.Instance.SetButtonIntera(Saojielu_ButtonPather.Find("Jiangwen").GetComponent<Button>(), false);
        shaojierLuTestManlag.Jiangwen();
    }
}
