using UnityEngine;
using UnityEngine.UI;

public class SNLKongzhitai : MonoBehaviour
{
    [HideInInspector] public Transform Canvas_3D;
    [HideInInspector] public Transform Shuningru_ButtonPather;
    private void Awake()
    {
        Canvas_3D = transform.Find("Canvas_3D");
        Shuningru_ButtonPather = Canvas_3D.Find("Button_UI");
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
    public void Kaiji()
    {
        MainSceneGuide.Instance.StopAutoMove();
        transform.Find("Canvas_3D").gameObject.SetActive(true);
        AudioManager.SetAudio(AudioManager.ShuningluAudio, "轰鸣声");
        StartCoroutine(UIManage.Instance.enumerator(3, () => {AudioManager.ShuningluAudio.Pause(); }));
        ShowCanvas();
        GetManager.Instance.ControlFlow.SetTestProgress(1);
        UIManage.Instance.SetHint("设备启动完成，点击控制台屏幕'自动放料'按键按键。");
        AudioManage.Instance.PlayMusicSource("设备启动完成，点击控制台屏幕'自动放料'按键。", 0.5f);
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Zhidongfangliao").GetComponent<Button>(), true);

        Canvas3Dto2D.Instance.SetCameraTrans(0);
        MainSceneGuide.Instance.AutoMoveByIndex(18, 0);
        MainSceneGuide.Instance.AutoMoveDontNeedPause();
    }
    /// <summary>
    /// 设置速凝炉按键
    /// </summary>
    public void ShowCanvas()
    {
        Canvas_3D.gameObject.SetActive(true);
    }
    #region 速凝炉按键操作
    /// <summary>
    /// 点击自动放料按键
    /// </summary>
    public void OnzhidongFangliao()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Zhidongfangliao").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.KaigaiQidong();
        StartCoroutine(UIManage.Instance.enumerator(20, ShuningruTestManlag.Instance.HegaiQidong));
    }
    /// <summary>
    /// 点击材料熔炼
    /// </summary>
    public void OnCailiaoLonglian()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Cailiaolonglian").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.JaireQidong();
        ShuningruTestManlag.Instance.Toming();
    }
    /// <summary>
    /// 点击包加热
    /// </summary>
    public void OnBaojiare()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Baojiare").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.Baojiarekaiguan();
    }
    /// <summary>
    /// 自动浇铸
    /// </summary>
    public void OnZidongjiaozhu()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Zidongjiaozhu").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.Zhidongjiaozhu();
    }
    /// <summary>
    /// 自动充氢
    /// </summary>
    public void OnZidongcongqing()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Zidongcongqing").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.Congqingqiqidong();
    }

    /// <summary>
    /// 自动破碎
    /// </summary>
    [System.Obsolete]
    public void OnZidongposhui()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Zidongposhui").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.Poshuiqidong();
    }
    /// <summary>
    /// 自动风冷
    /// </summary>
    public void OnZidongfenglen()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Zidongfenglen").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.Fenglenqidong();
    }
    /// <summary>
    /// 气动板阀开关
    /// </summary>
    public void OnQidongbankaigaun()
    {
        UIManage.Instance.SetButtonIntera(Shuningru_ButtonPather.Find("Qidongbankaigaun").GetComponent<Button>(), false);
        ShuningruTestManlag.Instance.Qidongban();
        Canvas3Dto2D.Instance.SetCanvasActive(false);
    }
    #endregion
}