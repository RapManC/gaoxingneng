using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HighlightPlus;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class ShuningruTestManlag : MonoBehaviour
{
    //盒子加热操作数据
    public float HezhiBiansheTime;
    float timeLog;
    bool isHeziJiare;
    bool isHeziJinagwen;
    float color_r;
    private static ShuningruTestManlag instance;

    private ParticleSystem particle1;
    private ParticleSystem particle2;
    private ParticleSystem particle3;
    Transform shunilu { get {return GetManager.Instance.ShuningLuParent.Find("ShuningLu"); } }
    Material heziMaterial { get { return GameObject.Find("Hezi").GetComponent<MeshRenderer>().materials[0]; } }
    Transform SuningruButtons { get { return GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather; } }

    public static ShuningruTestManlag Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
        Transform texiao = transform.Find("ShuningLu/Panzi/Texiao");
        particle1 = texiao.GetChild(1).GetComponent<ParticleSystem>();
        particle2 = texiao.GetChild(0).GetComponent<ParticleSystem>();
        particle3 = texiao.GetChild(2).GetComponent<ParticleSystem>();
    }
    private void Start()
    {
    }
    public void StartTest()
    {
        UIManage.Instance.SetHint("现在进行速凝炉操作,前往控制台点击高亮控制台电源，开启速凝炉控制台");
        AudioManage.Instance.PlayMusicSource("现在进行速凝炉操作,前往控制台点击高亮控制台电源，开启速凝炉控制台", 0.5f);
        Transform anjian= transform.Find("SNLKongzhitai/Guizi/AnNiu/an1");
        anjian.GetComponent<HighlightEffect>().highlighted = true;
        anjian.GetComponent<NewButtonManlag>().IsClick = true;
        GetManager.Instance.ControlFlow.Jingxingzong(3);
        GetManager.Instance.ControlFlow.SetTestProgress(0);
        UIManage.Instance.SetTishiPos("SNLKongzhitai");
        MainSceneGuide.Instance.AutoMoveByIndex(0);


        Step step = ScoreManager._Instance.GetStep(StepType.真空速凝炉生产能耗);
        step.startTime = ScoreManager.GetCurveTimeLong();
    }
    private void Update()
    {
        //盒子加热
        if (isHeziJiare)
        {
            timeLog += Time.deltaTime;
            color_r = timeLog / HezhiBiansheTime * 50;
            color_r = Mathf.Clamp(color_r, 0, 100);
            heziMaterial.SetColor("_EmissionColor", new Color(color_r, 0, 0) / 255);
            if (color_r == 100)
            {
                isHeziJiare = false;
            }
        }
        if (isHeziJinagwen)
        {
            timeLog -= Time.deltaTime;
            color_r = timeLog / HezhiBiansheTime * 50;
            color_r = Mathf.Clamp(color_r, 0, 50);
            heziMaterial.SetColor("_EmissionColor", new Color(color_r, 0, 0) / 255);
            if (color_r == 0)
            {
                isHeziJinagwen = false;
            }
        }
    }
    //开盖启动
    public void KaigaiQidong()
    {
        Canvas3Dto2D.Instance.SetCanvasActive(false);
        Transform dianji = GetManager.Instance.ShuningLuParent.Find("Diaoji");
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            UIManage.Instance.SetHint("正在开盖...");
            AudioManager.SetAudio(AudioManager.ShuningluAudio, "轰鸣声");
            GetManager.Instance.Cameras.transform.Find("ShunigruCamera").gameObject.SetActive(true);
            GetManager.Instance.Canvas.Find("Shiyan_UI/MiniCanmera_UI").gameObject.SetActive(true);
            dianji.DOLocalMoveX(-7.072f, 4).OnComplete(() =>
            {
                UIManage.Instance.SetHint("机械臂正在添加原料，请稍等...");
                AudioManager.SetAudio(AudioManager.CanvasAudio, "机械臂");
                AudioManage.Instance.PlayMusicSource("机械臂正在添加原料，请稍等.", 0.5f);

                dianji.GetComponent<Animator>().enabled = true;
                dianji.transform.Find("材料").gameObject.SetActive(true);
            });
        }));
        StartCoroutine(UIManage.Instance.enumerator(9.5f, () =>
        {
            AudioManager.SetAudio(AudioManager.CanvasAudio, "机械臂");
        }));
            StartCoroutine(UIManage.Instance.enumerator(16f, () => {  dianji.Find("材料").GetComponent<Animator>().enabled = true; }));
        StartCoroutine(UIManage.Instance.enumerator(19.87f, () => { 
            dianji.GetComponent<Animator>().enabled = false;

            GameObject cailiao = transform.Find("Diaoji/材料").gameObject;
            cailiao.GetComponent<Animator>().enabled = false;
            List<GameObject> goList = new List<GameObject>();
            foreach (Transform temp in cailiao.transform)
            {
                goList.Add(temp.gameObject);
            }
            foreach (GameObject temp in goList)
            {
                temp.transform.parent = GetManager.Instance.ShuningLuParent.Find("CailiaoLonghua").transform;
                temp.GetComponent<Rigidbody>().isKinematic = false;
            }
            cailiao.SetActive(false);
            GetManager.Instance.ShuningLuParent.Find("CailiaoLonghua").gameObject.SetActive(true);
        }));
    }
    //合盖启动
    public void HegaiQidong()
    {
        StartCoroutine(UIManage.Instance.enumerator(0, () => { UIManage.Instance.SetHint("机械臂正在合盖，请稍等..."); }));
        AudioManage.Instance.PlayMusicSource("机械臂正在合盖，请稍等...", 0.5f);
        AudioManager.SetAudio(AudioManager.CanvasAudio, "机械臂");
        GameObject.Find("Diaoji").transform.DOLocalMoveX(-4.22f, 3);
        StartCoroutine(UIManage.Instance.enumerator(6, () =>
        {
            UIManage.Instance.SetHint("投料操作完成，点击屏幕“材料熔炼”按钮。");
            AudioManage.Instance.PlayMusicSource("投料操作完成，点击屏幕“材料熔炼”按钮。",0.5f);
            GetManager.Instance.Cameras.Find("ShunigruCamera").gameObject.SetActive(false);
            GetManager.Instance.Canvas.Find("Shiyan_UI/MiniCanmera_UI").gameObject.SetActive(false);
            UIManage.Instance.SetButtonIntera(SuningruButtons.Find("Cailiaolonglian").GetComponent<Button>(), true);
            Canvas3Dto2D.Instance.SetCanvasActive(true);
        }));
    }
    //加热启动
    public void JaireQidong()
    {
        foreach (Transform tran in GameObject.Find("CailiaoLonghua").transform)
        {
            tran.gameObject.SetActive(true);
        }
        GetManager.Instance.ControlFlow.SetTestProgress(2);
        UIManage.Instance.SetHint("加热启动前请先在控制台屏幕调节温度。");
        AudioManage.Instance.PlayMusicSource("加热启动前请先在控制台屏幕调节温度。", 0.5f);
        Step step = ScoreManager._Instance.GetStep(StepType.真空速凝炉调节加热温度);
        step.startTime = ScoreManager.GetCurveTimeLong();

        UIManage_3D._Instance.StartInput(GongduanType.真空速凝炉, "温度", () =>
        {
            OnQuerenWendu(UIManage_3D._Instance.GetNowValue());
            GetManager.Instance.ControlFlow.SetTestProgress(3);

            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.真空速凝炉工段, "温度");
            step.score = 25 * score;
            step.repeatCount++;
        });
    }
    //透明事件
    public void Toming()
    {
        shunilu.Find("waibu").GetComponent<Touming>().SetToumingColor();
        shunilu.Find("Waike").GetComponent<Touming>().SetToumingColor();
        shunilu.Find("Men").GetComponent<Touming>().SetToumingColor();
        shunilu.Find("Longqi").GetComponent<Touming>().SetToumingColor();
        shunilu.Find("Zhuzi").GetComponent<Touming>().SetToumingColor();
    }
    //恢复
    public void Huhu()
    {
        shunilu.Find("waibu").GetComponent<Touming>().SetZhencangColor();
        shunilu.Find("Waike").GetComponent<Touming>().SetZhencangColor();
        shunilu.Find("Men").GetComponent<Touming>().SetZhencangColor();
        shunilu.Find("Longqi").GetComponent<Touming>().SetZhencangColor();
        shunilu.Find("Zhuzi").GetComponent<Touming>().SetZhencangColor();
    }
    //加热停止
    public void JaireStop()
    {
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            UIManage.Instance.SetHint("加热已停止，请打开包加热开关");
        }));
    }
    //包加热开关
    public void Baojiarekaiguan()
    {
        isHeziJiare = true;
        StartCoroutine(UIManage.Instance.enumerator(0, () => { UIManage.Instance.SetHint("请前往观察熔炼状态后，再进行后续操作。"); }));
        AudioManage.Instance.PlayMusicSource("请前往观察熔炼状态后，再进行后续操作。", 0.5f);
        StartCoroutine(UIManage.Instance.enumerator(HezhiBiansheTime, () =>
        {
            UIManage.Instance.SetHint("包升温完成，请在控制台调整转速。");
            AudioManage.Instance.PlayMusicSource("包升温完成，请在控制台调整转速。", 0.5f);
            Step step = ScoreManager._Instance.GetStep(StepType.真空速凝炉调节转速);
            step.startTime = ScoreManager.GetCurveTimeLong();
            GetManager.Instance.ControlFlow.SetTestProgress(5);
            UIManage_3D._Instance.StartInput(GongduanType.真空速凝炉, "转速", () =>
            {
                ShuningruTestManlag.Instance.OnQuerenZhuanshu(UIManage_3D._Instance.GetNowValue());

                step.endTime = ScoreManager.GetCurveTimeLong();
                int score = ScoreManager._Instance.GetParameterDataScore(link.真空速凝炉工段, "转速");
                step.score = 25 * score;
                step.repeatCount++;
            });
        }));
    }
    //自动浇铸
    public void Zhidongjiaozhu()
    {
        GetManager.Instance.ControlFlow.SetTestProgress(6);
        StartCoroutine(UIManage.Instance.enumerator(0, () => { UIManage.Instance.SetHint("正在自动浇铸中，请稍等..."); }));
        AudioManage.Instance.PlayMusicSource("正在自动浇铸中，请稍等...", 0.5f);
        transform.Find("ShuningLu/Longqi").GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(5.5f, () => { GameObject.Find("Hezi").GetComponent<Animator>().enabled = true; }));
        StartCoroutine(UIManage.Instance.enumerator(12, () =>
        {
            shunilu.Find("Poshui1").GetComponent<ModelRotControl>().isRot = true;
            shunilu.Find("Particle System").gameObject.SetActive(true);
            GameObject.FindObjectOfType<BopianGenerateManlag>().IsStartGenerate = true;
            transform.Find("ShuningLu/Longqi/Yemian/zhuti").transform.DOScaleY(0, 12).OnComplete(()=> transform.Find("ShuningLu/Longqi/Yemian/zhuti").gameObject.SetActive(false)) ;
        }));
        StartCoroutine(UIManage.Instance.enumerator(23, () => {
            UIManage.Instance.SetHint("自动浇铸完成，请点击“自动充氢气”按键");
            AudioManage.Instance.PlayMusicSource("自动浇铸完成，请点击“自动充氢气”按键", 0.5f);
            GetManager.Instance.ControlFlow.SetTestProgress(7);
            transform.Find("ShuningLu/Longqi").GetComponent<Animator>().Play("Qingdao Animation_Dao");
            isHeziJinagwen = true;
            StartCoroutine(UIManage.Instance.enumerator(10f, () => {
                transform.Find("ShuningLu/Hezi").GetComponent<Animator>().Play("Hezi Animation_Dao");
                GameObject.FindObjectOfType<BopianGenerateManlag>().IsStartGenerate = false;
                shunilu.Find("Particle System").gameObject.SetActive(false);
            }));
            UIManage.Instance.SetButtonIntera(GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather.Find("Zidongcongqing").GetComponent<Button>(), true);
        }));
    }
    //自动浇铸后暂停
    public void ZhidongjiaozhuStop()
    {
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            UIManage.Instance.SetHint("自动浇铸已暂停，请点击充氢气启动");
        }));
    }
    public void Congqingqiqidong()
    {
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            GameObject.FindObjectOfType<BopianGenerateManlag>().IsStartGenerate = false;
            shunilu.Find("Particle System").gameObject.SetActive(false);
            UIManage.Instance.SetHint("充氢气已启动，请点击“自动破碎”按键。");
            AudioManage.Instance.PlayMusicSource("充氢气已启动，请点击“自动破碎”按键。", 0.5f);
            UIManage.Instance.SetButtonIntera(GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather.Find("Zidongposhui").GetComponent<Button>(), true);
            GetManager.Instance.ControlFlow.SetTestProgress(8);
        }));
    }

    [System.Obsolete]
    public void Poshuiqidong()
    {
        Step step = ScoreManager._Instance.GetStep(StepType.真空速凝炉薄片厚度);
        step.startTime = ScoreManager.GetCurveTimeLong();
        int score = ScoreManager._Instance.GetParameterDataScore(link.真空速凝炉工段, "生产能耗");
        step.score = 33 * score;
        step.repeatCount++;

        particle1.startSize = 0.09f;
        particle1.maxParticles = 700;
        particle1.gameObject.SetActive(false);
        particle1.gameObject.SetActive(true);
        particle1.transform.DOScale(0.7f, 20f);
        particle3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        particle3.transform.DOScale(0.6f, 20f);

        AudioManager.SetAudio(AudioManager.ShuningluAudio, "搅拌");
        shunilu.Find("Poshui2").GetComponent<ModelRotControl>().isRot = true;
        shunilu.Find("Poshui3").GetComponent<ModelRotControl>().isRot = true;
        shunilu.Find("Poshui2 (1)").GetComponent<ModeRot>().isRot = true;
        shunilu.Find("Poshui3 (1)").GetComponent<ModeRot>().isRot = true;
        GetManager.Instance.ShuningLuParent.Find("PingpangMoveBiaopianCollde").GetComponent<PingpangMoveColldeControl>().isMove = true;
        StartCoroutine(UIManage.Instance.enumerator(0, () => { 
            UIManage.Instance.SetHint("破碎机已启动，正在破碎中...");
            AudioManage.Instance.PlayMusicSource("破碎机已启动，正在破碎中...", 0.5f);
            transform.Find("ShuningLu/Panzi/Texiao").gameObject.SetActive(true);
        }));
        StartCoroutine(UIManage.Instance.enumerator(5, () =>
        {
            GameObject.Find("Bopianfasheqi").transform.Find("baopian2").GetComponent<BiaoPian>().IsLKikePoshui = true;
            GameObject.Find("Bopianfasheqi").transform.Find("biaopian1").GetComponent<BiaoPian>().IsLKikePoshui = true;
            foreach (BiaoPian b in GameObject.FindObjectsOfType<BiaoPian>())
            {
                b.IsLKikePoshui = true;
            }
        }));
        StartCoroutine(UIManage.Instance.enumerator(10, () => {
            particle2.maxParticles = 500;
            particle2.Play();
        }));
        StartCoroutine(UIManage.Instance.enumerator(20, () => { PoshuiStop(); }));
    }
    public void PoshuiStop()
    {
        particle1.Pause();
        particle2.Pause();
        particle3.Pause();
        AudioManager.ShuningluAudio.Pause();
        shunilu.Find("Poshui1").GetComponent<ModelRotControl>().isRot = false;
        shunilu.Find("Poshui2").GetComponent<ModelRotControl>().isRot = false;
        shunilu.Find("Poshui3").GetComponent<ModelRotControl>().isRot = false;
        shunilu.Find("Poshui2 (1)").GetComponent<ModeRot>().isRot = false;
        shunilu.Find("Poshui3 (1)").GetComponent<ModeRot>().isRot = false;
        shunilu.Find("Particle System").gameObject.SetActive(false);
        GetManager.Instance.ShuningLuParent.Find("PingpangMoveBiaopianCollde").GetComponent<PingpangMoveColldeControl>().isMove = false;
        GameObject.FindObjectOfType<BopianGenerateManlag>().IsStartGenerate = false;
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            UIManage.Instance.SetHint("破碎机已停止，下一步点击“自动风冷开启”按钮");
            AudioManage.Instance.PlayMusicSource("破碎机已停止，下一步点击“自动风冷开启”按钮",0.5f);
            UIManage.Instance.SetButtonIntera(GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather.Find("Zidongfenglen").GetComponent<Button>(), true);
            GetManager.Instance.ControlFlow.SetTestProgress(9);
        }));
    }
    public void Fenglenqidong()
    {
        Canvas3Dto2D.Instance.SetCanvasActive(false);
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            UIManage.Instance.SetHint("风冷启动已启动，等待冷却中..");
            AudioManage.Instance.PlayMusicSource("风冷启动已启动，等待冷却中..",0.5f);
            Resources.Load<Material>("Material/SuiXie 1").SetColor("_EmissionColor", new Color(73, 73, 73) / 255);
            Resources.Load<Material>("Material/Xiaobaopian").SetColor("_EmissionColor", new Color(73, 73, 73) / 255);
            //if (Application.platform != RuntimePlatform.WebGLPlayer)
            //{
            //    UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(true);
            //    StartCoroutine(UIManage.Instance.enumerator(7, () =>
            //    {
            //        UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(false);
            //        FenglenStop();
            //    }));
            //}
            //else
            //{
            //    UIManage.Instance.changeDataManager.StartChange(0, 120, 7, "冷却时间", "min", () =>
            //    {
            //        FenglenStop();
            //    });
            //}

            UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(true);
            StartCoroutine(UIManage.Instance.enumerator(7, () =>
            {
                UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(false);
                FenglenStop();
            }));
        }));
    }
    public void FenglenStop()
    {
        UIManage.Instance.SetHint("冷却完毕，点击屏幕开启“气动板阀开关。");
        AudioManage.Instance.PlayMusicSource("冷却完毕，点击屏幕开启“气动板阀开关”。", 0.5f);
        UIManage.Instance.SetButtonIntera(GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather.Find("Qidongbankaigaun").GetComponent<Button>(), true);
        GetManager.Instance.ControlFlow.SetTestProgress(10);
        Canvas3Dto2D.Instance.SetCanvasActive(true);

    }
    public void Qidongban()
    {
        shunilu.Find("Men").transform.DORotate(new Vector3(-90, 0, 0), 1f);//-115.344
        UIManage.Instance.SetHint("阀门已开启，前往楼下点击托盘查看成品,");
        AudioManage.Instance.PlayMusicSource("阀门已开启，前往楼下点击托盘查看成品,", 0.5f);
        Panzi panzi= GetManager.Instance.InteractiveModel.Find("ShuningLuParent/ShuningLu/Panzi").GetComponent<Panzi>();
        panzi.SetClick();
        AudioManager.SetAudio(AudioManager.ShuningluAudio, "开门声");
        GetManager.Instance.ControlFlow.SetTestProgress(11);
        UIManage.Instance.SetTishiPos("SNLBaopian");
        MainSceneGuide.Instance.AutoMoveByIndex(1);
    }
    
    /// <summary>
    /// 控制开关是否能够点击
    /// </summary>
    public void KaigaunKuangtai( GameObject go ,bool zhauntai)
    {
        if (go != null)
        {
        go.GetComponent<BoxCollider>().enabled = zhauntai;
        go.GetComponent<HighlightEffect>().highlighted = zhauntai;
        }
        else
        {
            Debug.Log(go + "物体为空");
        }
    }
    /// <summary>
    /// 设置温度
    /// </summary>
     public void OnQuerenWendu(float value)
    {
        CailiaoLonghua longhuaScript = GetManager.Instance.ShuningLuParent.Find("CailiaoLonghua").GetComponent<CailiaoLonghua>();
        longhuaScript.JiareTime = Mathf.Abs(15 - value / 100);
        longhuaScript.LonghuaTime = Mathf.Abs(20 - value / 100);
        longhuaScript.enabled = true;
        longhuaScript.isJiare = true;
        UIManage.Instance.SetHint("请前往观察熔炼状态后，再进行后续操作。");
        AudioManage.Instance.PlayMusicSource("请前往观察熔炼状态后，再进行后续操作。",0.5f);
        AudioManager.SetAudio(AudioManager.ShuningluAudio, "加热");
        StartCoroutine(UIManage.Instance.enumerator(longhuaScript.LonghuaTime + longhuaScript.JiareTime, () =>
        {
            UIManage.Instance.SetHint("点击屏幕“中间包加热”开启包加热。");
            UIManage.Instance.SetButtonIntera(GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather.Find("Baojiare").GetComponent<Button>(), true);
            AudioManager.ShuningluAudio.Pause();
            GetManager.Instance.ControlFlow.SetTestProgress(4);
        }));
    }
    /// <summary>
    /// 设置转速
    /// </summary>
   public void OnQuerenZhuanshu(float value)
    {
        UIManage.Instance.JiaobanModelRotSpeed = value;
        UIManage.Instance.SetHint("转速成功设定为：" + value + "转每分钟。点击自动浇铸按键");
        AudioManage.Instance.PlayMusicSource("转速成功，点击自动浇铸按键", 0.5f);
        UIManage.Instance.SetButtonIntera(GetManager.Instance.SNLKongzhitai.Shuningru_ButtonPather.Find("Zidongjiaozhu").GetComponent<Button>(), true);
    }
    /// <summary>
    /// 显示速凝炉薄片信息
    /// </summary>
    public void ShowBaopianxingxi()
    {
        Transform UITran = UIManage.Instance.popout_UI.Find("BG/Canpingxingxi");
        //string wendudanwei;
        //float temp = DataManager.Instance.GetCanshu(GongduanType.真空速凝炉, "温度", out wendudanwei);
        float temp = UIManage_3D._Instance.GetValue(GongduanType.真空速凝炉, "温度");
        //string shududanwei;
        //float speed = DataManager.Instance.GetCanshu(GongduanType.真空速凝炉, "转速", out shududanwei);
        float speed = UIManage_3D._Instance.GetValue(GongduanType.真空速凝炉, "转速");
        UITran.Find("Temp/Text").GetComponent<Text>().text = temp + "°C";
        UITran.Find("Speed/Text").GetComponent<Text>().text = speed + "r/Min";
        Text houdu = UITran.Find("HT/Text").GetComponent<Text>();
        Text isPassTest = UITran.Find("IsPassTest").GetComponent<Text>();
        float i = 0;
        int _deductScore = 0 ;
        string isPassStr = "产品制作合格";
        if (speed < 40)
        {
            i = Random.Range(40, 60);
            isPassStr = "产品制作不合格，转速过慢小于40。";
            _deductScore = 2;
        }
        else if (speed > 50)
        {
            i = Random.Range(10, 20);
            isPassStr = "产品制作不合格，转速过快大于50";
            _deductScore = 2;
        }
        else
        {
            i = Random.Range(20, 40);
        }
        float y = i/100;
        houdu.text = y.ToString("f2") +"mm";
        isPassTest.text = isPassStr;
        UIManage.Instance.ShowPopout("Canpingxingxi");
        ParameterData parameterData = new ParameterData(link.真空速凝炉工段, "薄片厚度", y.ToString("f2") +"mm", 3, 3-_deductScore, "开始选厚度时间", "结束厚度时间");

        Step step = ScoreManager._Instance.GetStep(StepType.真空速凝炉薄片厚度);
        step.endTime = ScoreManager.GetCurveTimeLong();
        int score = ScoreManager._Instance.GetParameterDataScore(link.真空速凝炉工段, "薄片厚度");
        step.score = 33 * score;
        step.repeatCount++;
    }
    /// <summary>
    /// 打包
    /// </summary>
    public void OnDabaoZhuangxiang()
    {
        UIManage.Instance.popout_UI.gameObject.SetActive(false);
        UIManage.Instance.SetHint("正在打包中...");
        AudioManage.Instance.PlayMusicSource("正在打包中...", 0.5f);
        Transform panzi = GetManager.Instance.ShuningLuParent.Find("ShuningLu/Panzi");
        Transform tuiche = GetManager.Instance.ShuningLuParent.Find("TuoChe");
        tuiche.gameObject.SetActive(true);
        Animator panzhiAnim = panzi.GetComponent<Animator>();
        panzhiAnim.Play("Panzi2 Animation");
        StartCoroutine(UIManage.Instance.enumerator(14f, () =>
        {
            panzhiAnim.enabled = false;
            foreach (Transform tran in transform.Find("ShuningLu/Panzi"))
            {
                if (tran.tag == "Kuang")
                {
                    tran.gameObject.SetActive(false);
                }
            }
            tuiche.gameObject.SetActive(false);

            string __energyValue;
            int _deductScore= ScoreManager._Instance.GetNowlinkDeductEnergyScore(link.真空速凝炉工段,out __energyValue);
            ParameterData parameterData = new ParameterData(link.真空速凝炉工段, "生产能耗", __energyValue, 3, 3- _deductScore, "开始速凝炉时间", "结束速凝炉时间");

            GetManager.Instance.InteractiveModel.Find("QingporuParent").GetComponent<QingporuTestManlag>().StartTest();

            Step step = ScoreManager._Instance.GetStep(StepType.真空速凝炉生产能耗);
            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.真空速凝炉工段, "生产能耗");
            step.score = 33 * score;
            step.repeatCount++;
        }));
    }
}
