using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QingporuTestManlag :MonoBehaviour
{
    public float ChuozhenkongTime =8;
    public float JiayaTime = 8;
    public float PoshuiTime = 25;
    public float GuntongRotSpeed = 15;
    bool IsGuntongRot;
    [Header("透明")]
    [HideInInspector] public Transform ToumingParent;
    PipeFlow couzhenkongPipeFlow;
    PipeFlow jiayaPipeFlow;
    Transform QPTuoche;
    List<GameObject> biaopianList = new List<GameObject>();
    float time;
    int xiaoshiSpeed;
    [HideInInspector] public Transform Guntong;
    Transform tuiche ;
    private void Awake()
    {
        ToumingParent = transform.Find("Qingporu/Qingporu_touming").transform;
        couzhenkongPipeFlow = transform.Find("Qingporu/Qilius/Couzhenkong").GetComponent<PipeFlow>();
        jiayaPipeFlow = transform.Find("Qingporu/Qilius/Jiaya").GetComponent<PipeFlow>();
        Guntong = transform.Find("Qingporu/Qingporu_touming/GunrunPather/Guntong");
        QPTuoche = transform.Find("QPTuoChe");
        foreach (Transform baopian in Guntong.Find("Baopiao"))
        {
            biaopianList.Add(baopian.gameObject);
        }
        xiaoshiSpeed = (biaopianList.Count / 20);
        tuiche = transform.Find("StartTuoChe");
        Material material = Resources.Load<Material>("213309cwekez2ke9yx6pu6");
        material.SetColor("_Color", new Color(113,30,30,255)/255);
    }
    private void Start()
    {
        //ParticleSystem particle = Guntong.Find("PoshuiTexiao").GetComponent<ParticleSystem>();
        //var v = particle.shape;
        //Material material = v.meshRenderer.material;
        //material.SetColor("_Color", Color.black);
        //particle.gameObject.SetActive(true);
        //StartTest();
    }
    public void StartTest()
    {
        GetManager.Instance.ControlFlow.Jingxingzong(4);
        UIManage.Instance.SetHint("当前进行氢破炉工段");
        tuiche.gameObject.SetActive(true);
        StartCoroutine(UIManage.Instance.enumerator(5.5f, () =>
        {
            GetManager.Instance.QPLKongzhitai.ShowCanvas();
            UIManage.Instance.SetTishiPos("SNLBaopian");
            MainSceneGuide.Instance.AutoMoveByIndex(2);
            UIManage.Instance.SetHint("前往提示地点，点击物料框进行加料操作");
            AudioManage.Instance.PlayMusicSource("前往提示地点，点击物料框进行加料操作", 0.5f);
            tuiche.transform.Find("Kuangkuang/QPLKuan").GetComponent<ModelClick>().SetMayClick();

        }));
        GetManager.Instance.ControlFlow.SetTestProgress(0);

        Step step = ScoreManager._Instance.GetStep(StepType.氢破炉生产能耗);
        step.startTime = ScoreManager.GetCurveTimeLong();
    }
    private void FixedUpdate()
    {
        if (IsGuntongRot)
        {
            Guntong.Rotate(Vector3.up, GuntongRotSpeed);
            time += Time.deltaTime;
            if (time >= 1)
            {
                if (biaopianList.Count > 0)
                {
                    time = 0;
                    for (int i = 0; i <= xiaoshiSpeed; i++)
                    {
                        int y = Random.Range(0, biaopianList.Count);
                        biaopianList[y].SetActive(false);
                        biaopianList.Remove(biaopianList[y]);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 开盖和倒料
    /// </summary>
    public void KaigaiAnddaoliao()
    {
        UIManage.Instance.SetHint("正在倒料");
        AudioManage.Instance.PlayMusicSource("正在倒料", 0.5f);
        transform.Find("Qingporu/Qingporu_Men").GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(5.5f, () =>
        {
            Animator tuicheAnim = tuiche.GetComponent<Animator>();
            tuicheAnim.Play("TuicheQingdao");
            tuicheAnim.GetComponent<Animator>().enabled = true;
        }));
    }
    /// <summary>
    /// 升温操作
    /// </summary>
    public void Shengwen()
    {
        GetManager.Instance.ControlFlow.SetTestProgress(1);
        SetTouming(true);
        Guntong.Find("Baopiao").gameObject.SetActive(true);
        UIManage.Instance.SetHint("请在控制台页面调节氢破炉温度参数");
        AudioManage.Instance.PlayMusicSource("请在控制台页面调节氢破炉温度参数", 0.5f);
        //InputManage._Instance.SetInputUI(GongduanType.氢破炉, "温度", "请调节氢破温度", 400, 700, "°C", (float value1) =>
        // {
        //     UIManage.Instance.SetHint("设置温度为：" + value1 + "°C，正在进行加热。");
        //     AudioManager.SetAudio(AudioManager.QingporuAudio, "加热");
        //     Transform lights = transform.Find("Qingporu/Lights").transform;
        //     lights.Find("Light1").GetComponent<Light>().DOIntensity(4, 10);
        //     lights.Find("Light2").GetComponent<Light>().DOIntensity(4, 10).OnComplete(() =>
        //     {
        //         AudioManager.QingporuAudio.Pause();
        //         UIManage.Instance.SetHint("升温完成，前往控制台点击抽真空按键。");
        //         UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Chouzhengkong").GetComponent<Button>(), true);
        //         GetManager.Instance.ControlFlow.SetTestProgress(2);
        //     });
        // });

        Step step = ScoreManager._Instance.GetStep(StepType.氢破炉调节反应温度);
        step.startTime = ScoreManager.GetCurveTimeLong();

        UIManage_3D._Instance.StartInput(GongduanType.氢破炉, "温度", () => {
            UIManage.Instance.SetHint("设置温度为：" + UIManage_3D._Instance.GetNowValue() + "°C，正在进行加热。");
            AudioManage.Instance.PlayMusicSource("设置温度为：365°C，正在进行加热。", 0.5f);
            AudioManager.SetAudio(AudioManager.QingporuAudio, "加热");
            Transform lights = transform.Find("Qingporu/Lights").transform;
            lights.Find("Light1").GetComponent<Light>().DOIntensity(4, 10);
            lights.Find("Light2").GetComponent<Light>().DOIntensity(4, 10).OnComplete(() =>
            {
                AudioManager.QingporuAudio.Pause();
                UIManage.Instance.SetHint("升温完成，前往控制台点击抽真空按键。");
                AudioManage.Instance.PlayMusicSource("升温完成，前往控制台点击抽真空按键。",0.5f);
                UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Chouzhengkong").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(2);
            });

            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.氢破炉工段, "温度");
            step.score = 25 * score;
            step.repeatCount++;
        });
    }
    /// <summary>
    /// 抽真空
    /// </summary>
    public void Chouzhenkong()
    {
        Canvas3Dto2D.Instance.SetCanvasActive(false);
        AudioManager.SetAudio(AudioManager.QingporuAudio, "抽真空");
        couzhenkongPipeFlow.Flow(4, null);
        UIManage.Instance.changeDataManager.StartChange(101.33f, 0, ChuozhenkongTime, "当前压强", "Pa", () =>
        {
            UIManage.Instance.SetHint("抽真空完成，前往控制台点击加压按键");
            AudioManage.Instance.PlayMusicSource("抽真空完成，前往控制台点击加压按键", 0.5f);
            UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Jiaya").GetComponent<Button>(), true);
            AudioManager.QingporuAudio.Pause();
            couzhenkongPipeFlow.Stop();
            GetManager.Instance.ControlFlow.SetTestProgress(3);
            Canvas3Dto2D.Instance.SetCanvasActive(true);
        });
    }
    /// <summary>
    /// 加压
    /// </summary>
    public void Jiaya()
    {
        //InputManage._Instance.SetInputUI(GongduanType.氢破炉, "压力", "请调节氢破压力", 0, 130, "Pa", (float value2) =>
        //{
        //    UIManage.Instance.SetHint("设置压力为：" + value2 + "Pa，正在进行充氢。");
        //    AudioManager.SetAudio(AudioManager.QingporuAudio, "充气");
        //    jiayaPipeFlow.Flow(JiayaTime / 3, null);
        //    StartCoroutine(UIManage.Instance.enumerator(JiayaTime, () =>
        //    {
        //        AudioManager.QingporuAudio.Pause();
        //        jiayaPipeFlow.Stop();
        //        UIManage.Instance.SetHint("充氢结束，前往控制台点击破碎按键");
        //        UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Posui").GetComponent<Button>(), true);
        //        GetManager.Instance.ControlFlow.SetTestProgress(4);
        //    }));
        //});
        Step step = ScoreManager._Instance.GetStep(StepType.氢破炉调节反应压力);
        step.startTime = ScoreManager.GetCurveTimeLong();
        UIManage.Instance.SetHint("请在控制台页面调节氢破炉压力参数");
        AudioManage.Instance.PlayMusicSource("请在控制台页面调节氢破炉压力参数", 0.5f);
        UIManage_3D._Instance.StartInput(GongduanType.氢破炉, "压力", () => {
            UIManage.Instance.SetHint("设置压力为：" + UIManage_3D._Instance.GetNowValue() + "Pa，正在进行充氢。");
            AudioManage.Instance.PlayMusicSource("设置压力，正在进行充氢。", 0.5f);
            AudioManager.SetAudio(AudioManager.QingporuAudio, "充气");
            jiayaPipeFlow.Flow(JiayaTime / 3, null);

            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.氢破炉工段, "压力");
            step.score = 25 * score;
            step.repeatCount++;

            StartCoroutine(UIManage.Instance.enumerator(JiayaTime, () =>
            {
                AudioManager.QingporuAudio.Pause();
                jiayaPipeFlow.Stop();
                UIManage.Instance.SetHint("充氢结束，前往控制台点击破碎按键");
                AudioManage.Instance.PlayMusicSource("充氢结束，前往控制台点击破碎按键", 0.5f);
                UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Posui").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(4);
            }));
        });
    }

    /// <summary>
    /// 破碎
    /// </summary>
    public void Poshui()
    {
        //InputManage._Instance.SetInputUI(GongduanType.氢破炉, "时间", "请调节氢破时间", 8, 24, "H", (float value3) =>
        //{
        //    UIManage.Instance.SetHint("设置破碎时间为：" + value3 + "h，正在开始破碎，请前往查看。");
        //    IsGuntongRot = true;
        //    Guntong.Find("PoshuiTexiao").gameObject.SetActive(true);
        //    AudioManager.SetAudio(AudioManager.QingporuAudio, "破碎");
        //    StartCoroutine(UIManage.Instance.enumerator(25, () =>
        //    {
        //        IsGuntongRot = false;
        //        AudioManager.QingporuAudio.Pause();
        //        UIManage.Instance.SetHint("破碎结束，前往控制台点击吸氢按键");
        //        UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Xiqing").GetComponent<Button>(), true);
        //        GetManager.Instance.ControlFlow.SetTestProgress(5);
        //    }));
        //});

        Step step = ScoreManager._Instance.GetStep(StepType.氢破炉调节破碎时间);
        step.startTime = ScoreManager.GetCurveTimeLong();
        UIManage.Instance.SetHint("请在控制台页面调节氢破时间。");
        AudioManage.Instance.PlayMusicSource("请在控制台页面调节氢破时间。", 0.5f);
        UIManage_3D._Instance.StartInput(GongduanType.氢破炉, "时间", () =>
        {
            UIManage.Instance.SetHint("设置破碎时间为：" + UIManage_3D._Instance.GetNowValue() + "h，正在开始破碎，请前往查看。");
            AudioManage.Instance.PlayMusicSource("设置破碎时间，正在开始破碎，请前往查看。", 0.5f);

            IsGuntongRot = true;
            Guntong.Find("PoshuiTexiao").gameObject.SetActive(true);
            AudioManager.SetAudio(AudioManager.QingporuAudio, "破碎");

            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.氢破炉工段, "时间");
            step.score = 25 * score;
            step.repeatCount++;
            StartCoroutine(UIManage.Instance.enumerator(25, () =>
            {
                IsGuntongRot = false;
                AudioManager.QingporuAudio.Pause();
                Guntong.Find("PoshuiTexiao").GetComponent<ParticleSystem>().Pause();
                UIManage.Instance.SetHint("破碎结束，前往控制台点击吸氢按键");
                AudioManage.Instance.PlayMusicSource("破碎结束，前往控制台点击吸氢按键", 0.5f);
                UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Xiqing").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(5);
            }));
        });
    }
    /// <summary>
    /// 吸氢
    /// </summary>
    public void Xiqing()
    {
        UIManage.Instance.SetHint("正在吸氢，请稍等");
        AudioManage.Instance.PlayMusicSource("正在吸氢，请稍等", 0.5f);
        AudioManager.SetAudio(AudioManager.CanvasAudio, "抽真空");
        couzhenkongPipeFlow.Flow(ChuozhenkongTime / 3, null);
        StartCoroutine(UIManage.Instance.enumerator(ChuozhenkongTime, () =>
        {
            UIManage.Instance.SetHint("抽氢结束，前往控制台点击降温按键");
            AudioManage.Instance.PlayMusicSource("抽氢结束，前往控制台点击降温按键", 0.5f);
            couzhenkongPipeFlow.Stop();
            AudioManager.CanvasAudio.Pause();
            UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Jiangwen").GetComponent<Button>(), true);
            GetManager.Instance.ControlFlow.SetTestProgress(6);
        }));
    }
    /// <summary>
    /// 降温
    /// </summary>
    public void Jiangwen()
    {
        Canvas3Dto2D.Instance.SetCanvasActive(false);
        UIManage.Instance.SetHint("冷却已启动，等待冷却中..");
        AudioManage.Instance.PlayMusicSource("冷却已启动，等待冷却中..", 0.5f);
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(true);
        }
        else
        {
            UIManage.Instance.changeDataManager.StartChange(0, 240, 9, "冷却时间", "min", ()=> {
                Material material = Resources.Load<Material>("213309cwekez2ke9yx6pu6");
                material.SetColor("_Color", Color.white);
            });
        }

        Transform lights = transform.Find("Qingporu/Lights").transform;
        lights.Find("Light1").GetComponent<Light>().DOIntensity(0, 10);
        lights.Find("Light2").GetComponent<Light>().DOIntensity(0, 10).OnComplete(() =>
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(false);
            UIManage.Instance.SetHint("冷却完毕，前往出料口点击出料门进行出料操作");
            AudioManage.Instance.PlayMusicSource("冷却完毕，前往出料口点击出料门进行出料操作", 0.5f);
            UIManage.Instance.SetTishiPos("QPLShangliao");
            MainSceneGuide.Instance.AutoMoveByIndex(4);
            transform.Find("Qingporu/Qingporu_Men/QPLMen").GetComponent<ModelClick>().SetMayClick();
            GetManager.Instance.ControlFlow.SetTestProgress(7);
        });
    }
    /// <summary>
    /// 装运
    /// </summary>
    public void Zhuangyun()
    {
        SetTouming(false);
        QPTuoche.gameObject.SetActive(true);
        Animator menAnim = transform.Find("Qingporu/Qingporu_Men").GetComponent<Animator>();
        menAnim.Play("KaimenAnimation");
        menAnim.enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(5.5f, () =>
        {
            menAnim.enabled = false;
            QPTuoche.GetComponent<Animator>().enabled = true;
        }));
    }
    void SetTouming(bool istoming)
    {
        foreach (Touming tran in ToumingParent.GetComponentsInChildren<Touming>())
        {
            if (istoming)
                tran.SetToumingColor();
            else
                tran.SetZhencangColor();
        }
    }
}
