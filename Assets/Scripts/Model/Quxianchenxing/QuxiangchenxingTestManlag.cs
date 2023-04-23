using DG.Tweening;
using UnityEngine;

public class QuxiangchenxingTestManlag :MonoBehaviour
{
    Transform tuoche;
    ParticleSystem texiao;
    Transform fendui;
    QXCXInputControl inputControl;
    [HideInInspector]public Transform jiaohuModel;
    private void Awake()
    {
        tuoche = transform.Find("TuoChe");
        texiao = transform.Find("Texiao").GetComponent<ParticleSystem>();
        fendui = transform.Find("Fendui");
        jiaohuModel = transform.Find("JiaohuModel");
        inputControl = transform.Find("Xianshiping").GetComponent<QXCXInputControl>();
    }
    private void Start()
    {
        //StartTest();
    }
    public void StartTest()
    {
        tuoche.gameObject.SetActive(true);
        GetManager.Instance.ControlFlow.Jingxingzong(6);
        UIManage.Instance.SetHint("前往取向成型工段点击真空瓶进行上料。");
        transform.Find("TuoChe/QiGuan1/QiGuan1_Pingshen").GetComponent<ModelClick>().SetMayClick();
        UIManage.Instance.SetTishiPos("QXCXShangliao");
        MainSceneGuide.Instance.AutoMoveByIndex(7);
        GetManager.Instance.ControlFlow.SetTestProgress(0);

        Step step = ScoreManager._Instance.GetStep(StepType.取向成型生产能耗);
        step.startTime = ScoreManager.GetCurveTimeLong();
    }
    /// <summary>
    /// 上料
    /// </summary>
    public void Sangliao()
    {
        UIManage.Instance.SetHint("正在播放上料，前往观察");
        AudioManage.Instance.PlayMusicSource("正在播放上料，前往观察", 0.5f);
        SetTouming(true);
        tuoche.GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(6.5f, () =>
        {
            tuoche.GetComponent<Animator>().enabled = false;
            tuoche.gameObject.SetActive(false);
            transform.Find("Zhenkongqing").gameObject.SetActive(true);
            StartCoroutine(UIManage.Instance.enumerator(2, () =>
            {
                texiao.gameObject.SetActive(true);
                StartCoroutine(UIManage.Instance.enumerator(15, () =>
                {
                    texiao.Pause();
                    fendui.gameObject.SetActive(true);
                    UIManage.Instance.SetHint("上料结束，点击粉末传送至设备内部");
                    AudioManage.Instance.PlayMusicSource("上料结束，点击粉末传送至设备内部", 0.5f);
                    fendui.GetComponent<ModelClick>().SetMayClick();
                    GetManager.Instance.ControlFlow.SetTestProgress(1);
                }));
            }));
        }));
    }
    /// <summary>
    /// 设备内运输
    /// </summary>
    public void Shebeileiyunshu()
    {
        UIManage.Instance.SetHint("正在播放设备内运输动画");
        AudioManage.Instance.PlayMusicSource("正在播放设备内运输动画", 0.5f);
        fendui.GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(5f, () =>
        {
            UIManage.Instance.SetHint("设备内运输完成,进行称重操作");
            UIManage.Instance.SetHint("前往窗口点击提示开关,开始称重");
            UIManage.Instance.SetTishiPos("QXCXChuangkou");
            MainSceneGuide.Instance.AutoMoveByIndex(8);
            jiaohuModel.Find("Buttons/Yunshong").GetComponent<QXCX_ButtonControl>().SetIsMayClick();
        }));
    }
    /// <summary>
    /// 称重
    /// </summary>
    public void Chengzhong()
    {
        UIManage.Instance.SetHint("正在进行称重");
        AudioManage.Instance.PlayMusicSource("正在称重", 0.5f);
        fendui.GetComponent<Animator>().Play("FenduiAnimation2");
        StartCoroutine(UIManage.Instance.enumerator(7, () =>
        {
            transform.Find("Chen/polySurface1").gameObject.SetActive(true);
            StartCoroutine(UIManage.Instance.enumerator(4, () =>
            {
                Debug.Log("称重结束");
                //设置输入取向成型数据
                //InputManage._Instance.SetInputUI(GongduanType.取向成型, "磁场强度", "请设置磁场强度", 0.8f, 3, "T", (a) =>
                //{
                //    StartCoroutine(UIManage.Instance.enumerator(0,() => { SetTEnd(a); }));
                //    SetTEnd(a);
                //});
                UIManage.Instance.SetTishiPos("QXCXChuangkou");
                MainSceneGuide.Instance.AutoMoveByIndex(9);

                Step step = ScoreManager._Instance.GetStep(StepType.取向成型调节压制压力);
                step.startTime = ScoreManager.GetCurveTimeLong();
                Step step2 = ScoreManager._Instance.GetStep(StepType.取向成型调节磁场强度);
                step2.startTime = ScoreManager.GetCurveTimeLong();

                UIManage.Instance.SetHint("前往取向成型仪器窗口调剂上方参数磁场强度参数和压力参数。");
                AudioManage.Instance.PlayMusicSource("前往取向成型仪器窗口调剂上方参数磁场强度参数和压力参数。", 0.5f);
                inputControl.StartTiaojie();
            }));
        }));
        //void SetTEnd(float value)
        //{
        //    UIManage.Instance.SetHint("你设置的磁场强度为：" + value + "T,继续设置压力值。");
        //    InputManage._Instance.SetInputUI(GongduanType.取向成型, "压力", "请设置压力值", 2f, 80, "Pa", (p) =>
        //    {
        //        UIManage.Instance.SetHint("你设置的压力值为：" + p + "Pa,点击提示按键进行压制成型操作");
        //        jiaohuModel.Find("Buttons/Yazi").GetComponent<QXCX_ButtonControl>().SetIsMayClick();
        //        GetManager.Instance.ControlFlow.SetTestProgress(2);
        //    });
        //}
    }
    /// <summary>
    /// 压制成型
    /// </summary>
    public void Yuazhichengxing()
    {
        UIManage.Instance.SetHint("正在压制...");
        AudioManage.Instance.PlayMusicSource("正在压制...", 0.5f);
        //保存用户输入数据
        QXCXInputControl qXCXInputControl = transform.Find("Xianshiping").GetComponent<QXCXInputControl>();
        qXCXInputControl.FoundScore(false);
        qXCXInputControl.FoundScore(true);
        qXCXInputControl.FoundEnergyScore();
        inputControl.IsStartTiaojie = false;
        fendui.GetComponent<Animator>().Play("FenduiAnimation3");
        
        StartCoroutine(UIManage.Instance.enumerator(0,()=> {
            Step step = ScoreManager._Instance.GetStep(StepType.取向成型调节压制压力);
            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.取向成型工段, "压力");
            step.score = 25 * score;
            step.repeatCount++;

            Step step2 = ScoreManager._Instance.GetStep(StepType.取向成型调节磁场强度);
            step2.endTime = ScoreManager.GetCurveTimeLong();
            int score2 = ScoreManager._Instance.GetParameterDataScore(link.取向成型工段, "磁场");
            step2.score = 25 * score2;
            step2.repeatCount++;
        }));
        StartCoroutine(UIManage.Instance.enumerator(6, () =>
        {
            jiaohuModel.Find("Ya").DOLocalMoveY(-0.624f, 4).OnComplete(() =>
            {
                StartCoroutine(UIManage.Instance.enumerator(3, () =>
                {
                    fendui.gameObject.SetActive(false);
                    transform.Find("Kuai").gameObject.SetActive(true);
                    jiaohuModel.Find("Ya").DOLocalMoveY(-0.245f, 4).OnComplete(() =>
                    {
                        StartCoroutine(UIManage.Instance.enumerator(3, () =>
                        {
                            Debug.Log("压制完成");
                            UIManage.Instance.SetHint("压制完成，点击抽真空按钮");
                            AudioManage.Instance.PlayMusicSource("压制完成，点击抽真空按钮", 0.5f);
                            jiaohuModel.Find("Buttons/Zhengkong").GetComponent<QXCX_ButtonControl>().SetIsMayClick();
                            GetManager.Instance.ControlFlow.SetTestProgress(3);
                        }));
                    });
                }));
            });
        }));
    }
    /// <summary>
    /// 抽真空包装
    /// </summary>
    public void Couzhenkongbaozhuang()
    {
        Transform Kuai = transform.Find("Kuai");
        UIManage.Instance.SetHint("正在进行抽真空包装");
        AudioManage.Instance.PlayMusicSource("正在进行抽真空包装", 0.5f);
        Kuai.GetComponent<Animator>().Play("KuaiAnimation2");
        StartCoroutine(UIManage.Instance.enumerator(4.5f, () =>
        {
            Kuai.GetComponent<Animator>().enabled=false;
            Kuai.gameObject.AddComponent<Rigidbody>();
        }));
        StartCoroutine(UIManage.Instance.enumerator(7f, () =>
        {
            QXCXKuaifasheqi kuaifasheqi= transform.Find("KuaiFashenqi").GetComponent<QXCXKuaifasheqi>();
            kuaifasheqi.IsShengcheng(true);
            Rigidbody rigidbody = Kuai.GetComponent<Rigidbody>();
            Destroy(rigidbody);
            StartCoroutine(UIManage.Instance.enumerator(10f, () =>
            {
                kuaifasheqi.IsShengcheng(false);
                UIManage.Instance.SetHint("抽真空完成，点击右侧门板进行成品运输");
                AudioManage.Instance.PlayMusicSource("抽真空完成，点击右侧门板进行成品运输", 0.5f);
                UIManage.Instance.SetTishiPos("QXCXYunshu");
                MainSceneGuide.Instance.AutoMoveByIndex(10);
                transform.Find("JiaohuModel/Men").GetComponent<ModelClick>().SetMayClick();
                GetManager.Instance.ControlFlow.SetTestProgress(4);
            }));
        }));
    }
    /// <summary>
    /// 运输
    /// </summary>
    public void Yunshu()
    {
        Transform endTuoche = transform.Find("EndTuoChe");
        Transform men = transform.Find("JiaohuModel/Men");
        men.DOLocalRotate(new Vector3(0, -138, 0), 1f).OnComplete(()=>{
            endTuoche.gameObject.SetActive(true);
            SetTouming(false);
        });
        StartCoroutine(UIManage.Instance.enumerator(32, () => {
            endTuoche.GetComponent<Animator>().enabled = false;
            endTuoche.gameObject.SetActive(false);
            UIManage.Instance.SetHint("取向成型工段结束");
            GetManager.Instance.InteractiveModel.Find("ShaojierLuParent").GetComponent<ShaojierLuTestManlag>().StartTest();

            Step step = ScoreManager._Instance.GetStep(StepType.取向成型生产能耗);
            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.取向成型工段, "生产能耗");
            step.score = 33 * score;
            step.repeatCount++;
        }));
    }
    void SetTouming(bool isToming)
    {
        foreach(Transform tran in transform.Find("Waikes"))
        {
            if (tran.GetComponent<Touming>())
            {
                if (isToming)
                    tran.GetComponent<Touming>().SetToumingColor();
                else
                    tran.GetComponent<Touming>().SetZhencangColor();
            }
        }
    }
}
