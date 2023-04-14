using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class JiancheTestManlag : MonoBehaviour
{
    Material material;
    Transform clickModels;
    Transform lanzi;
    Transform famengan;
    Transform kaiguan;

    Dictionary<string, string> TestResults = new Dictionary<string, string>();
    private void Awake()
    {
        material = Resources.Load<Material>("Material/Kuai");
        clickModels = transform.Find("ClickModels");
        lanzi = clickModels.Find("Lanzi");
        famengan = clickModels.Find("Famengan");
        kaiguan = clickModels.Find("Kiaguan");
    }
    private void Start()
    {
    }
    public void StartTest()
    {
        SetMaterial(0.7f);
        transform.Find("Tuoche").gameObject.SetActive(true);
        lanzi.gameObject.SetActive(true);
        GetManager.Instance.ControlFlow.Jingxingzong(9);
        UIManage.Instance.SetTishiPos("Jianche");
        UIManage.Instance.SetHint("现在进行检测操作，点击物料放入检测机中。");
        AudioManage.Instance.PlayMusicSource("现在进行检测操作，点击物料放入检测机中。", 0.5f);
        lanzi.GetComponent<JiancheModelClickControl>().SetClick();
        GetManager.Instance.ControlFlow.SetTestProgress(0);

        Step step = ScoreManager._Instance.GetStep(StepType.性能检测);
        step.startTime = ScoreManager.GetCurveTimeLong();
    }
    /// <summary>
    /// 添加检测料
    /// </summary>
    public void Jialiao()
    {
        UIManage.Instance.SetHint("正在将物料放入检测机中。");
        AudioManage.Instance.PlayMusicSource("正在将物料放入检测机中。", 0.5f);
        lanzi.GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(4, () => {
            lanzi.GetComponent<Animator>().enabled = false;
            UIManage.Instance.SetHint("物料放置完成，旋转阀门杆固定物料。");
            AudioManage.Instance.PlayMusicSource("物料放置完成，旋转阀门杆固定物料。", 0.5f);
            famengan.GetComponent<JiancheModelClickControl>().SetClick();
            GetManager.Instance.ControlFlow.SetTestProgress(1);
        }));
    }
    /// <summary>
    /// 旋转阀门杆
    /// </summary>
    public void Xuanzhuanfamen()
    {
        famengan.DOLocalRotate(new Vector3(0, -179, 0), 2f);
        transform.Find("Mdoels/Yagan").DOLocalMoveY(0.0841f, 2).OnComplete(() =>
        {
            UIManage.Instance.SetHint("固定物料完成，点击提示按键开启设备。");
            kaiguan.GetComponent<JiancheModelClickControl>().SetClick();
            GetManager.Instance.ControlFlow.SetTestProgress(2);
        });
    }
    /// <summary>
    /// 开启设备
    /// </summary>
    public void Kaiqishebei()
    {
        InitTestResults();
        AudioManager.SetAudio(AudioManager.CanvasAudio, "滴滴声");
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            SetTestResultsUI(false);
            transform.Find("ClickModels/Diannao_pingmu/Canvas").gameObject.SetActive(true);
        }));
        UIManage.Instance.SetHint("设备已开启，点击仪器获取检测结果。");
        AudioManage.Instance.PlayMusicSource("设备已开启，点击仪器获取检测结果。", 0.5f);
        clickModels.Find("Diannao_pingmu").GetComponent<JiancheModelClickControl>().SetClick();
        GetManager.Instance.ControlFlow.SetTestProgress(3);
    }

    void InitTestResults()
    {
        int _brValue = Random.Range(1290, 1325);
        string _brValueStr = (_brValue / 1000).ToString("F2");
        TestResults.Add("Br", _brValueStr + "T");
        int _hcjValue = Random.Range(789, 953);
        TestResults.Add("Hcj", _hcjValue + "KA/m");
        int _bhValue = Random.Range(325, 486);
        TestResults.Add("(BH)max", _bhValue + "KA/m");
        foreach (var v in DataManager.Instance.YuanshuZanbi)
        {
            TestResults.Add(v.Key, v.Value.ToString() + "%");
        }
        TestResults.Add("Nd2Fe14B", DataManager.Instance.Xitupeizhi + "wt.%");
        Debug.Log(TestResults.Count);
    }
    void SetTestResultsUI(bool isPingmu)
    {
        Transform bgImage;
        if (isPingmu)
        {
            bgImage = UIManage.Instance.popout_UI.Find("BG/Jianchejieguo/Image");
            foreach(Transform tran in bgImage.Find("Canshubaio"))
            {
                if(tran.name!= "Text"&& tran.name != "Xiangmu_Template")
                {
                    Destroy(tran.gameObject);
                }
            }
        }
        else
            bgImage = transform.Find("ClickModels/Diannao_pingmu/Canvas/Image");
        Transform taegtTran = bgImage.Find("Canshubaio");
        GameObject taegtGo = taegtTran.Find("Xiangmu_Template").gameObject;
        string tubiaoName = "";
        switch (DataManager.Instance.PaihaoXaunzhe)
        {
            case 0:
                tubiaoName = "img_N38sh";
                break;
            case 1:
                tubiaoName = "img_N38";
                break;
            case 2:
                tubiaoName = "img_N35";
                break;
        }
        Transform tubiao = bgImage.Find("Quxiantu");
        tubiao.GetComponent<Image>().GetComponent<Image>().sprite = Resources.Load<Sprite>("Quxiantu/" + tubiaoName);
        tubiao.Find("Text").GetComponent<Text>().text = TestResults["Br"];
        tubiao.Find("Text (1)").GetComponent<Text>().text = "-" + TestResults["Br"];
        tubiao.Find("Text (2)").GetComponent<Text>().text = TestResults["Hcj"];
        tubiao.Find("Text (3)").GetComponent<Text>().text = "-" + TestResults["Hcj"];
        foreach (var v in TestResults)
        {
            GameObject go = Instantiate(taegtGo, taegtTran);
            go.transform.Find("Text").GetComponent<Text>().text = v.Key;
            go.transform.Find("Image/Text").GetComponent<Text>().text = v.Value;
            go.SetActive(true);
            Debug.Log(v.Key);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(taegtTran as RectTransform);

    }
    /// <summary>
    /// 获取结果
    /// </summary>
    public void Huodejieguo()
    {
        SetTestResultsUI(true);
        UIManage.Instance.ShowPopout("Jianchejieguo");
        string value = string.Format("Br:{0}、Hcl:{1}、(BH)max:{2}", TestResults["Br"], TestResults["Hcj"], TestResults["(BH)max"]);
        ParameterData parameterData1 = new ParameterData(link.性能检测, "性能参数", value, 5, Random.Range(4, 5), "开始检测时间", "结束检测时间");
        Step step = ScoreManager._Instance.GetStep(StepType.性能检测);
        step.endTime = ScoreManager.GetCurveTimeLong();
        int score = ScoreManager._Instance.GetParameterDataScore(link.性能检测, "性能参数");
        step.score = 5 * score;
        step.repeatCount++;
        ScoreManager._Instance._experimentalData.status = 1;
        EndExperiment();
    }
    void EndExperiment()
    {
        Debug.Log("实验结束");
        UIManage.Instance.SetHint("实验流程以全部结束，你可点击报表查看数据，也可重复此前阶段实验已获得更好的实验结果。");
        AudioManage.Instance.PlayMusicSource("实验流程以全部结束，你可点击报表查看数据，也可重复此前阶段实验已获得更好的实验结果。", 0.5f);
        GetManager.Instance.Canvas.Find("BaoBiao_UI/Scroll View/Viewport/Content/BGImage/Button").GetComponent<Button>().interactable = true;
    }

    void SetMaterial(float vlaue)
    {
        material.SetFloat("_Metallic", vlaue);
        material.SetFloat("_Glossiness", vlaue);
    }

}
