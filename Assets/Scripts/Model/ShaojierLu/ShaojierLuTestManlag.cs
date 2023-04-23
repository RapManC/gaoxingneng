using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ShaojierLuTestManlag : MonoBehaviour
{
    Transform models;
    Transform tuoChe;
    public int CaibaozhuanNum;
    Material material;
    private void Awake()
    {
        tuoChe = transform.Find("Tuoche");
        models = transform.Find("Models");
        material = transform.Find("Kuai").GetComponent<MeshRenderer>().material;
    }
    private void Start()
    {
        SetMaterial(0);
    }
    public void StartTest()
    {
        GetManager.Instance.ControlFlow.Jingxingzong(7);
        GetManager.Instance.SJLKongzhiitai.ShowCanvas();
        tuoChe.gameObject.SetActive(true);
        UIManage.Instance.SetHint("现在进行烧结炉工段实验操作，前往速凝炉加料处点击物料框进行加料操作。");
        UIManage.Instance.SetTishiPos("SJLShangliao");
        MainSceneGuide.Instance.AutoMoveByIndex(11);
        transform.Find("Tuoche/SJLLanzhi").GetComponent<ModelClick>().SetMayClick();
        GetManager.Instance.ControlFlow.SetTestProgress(0);

        Step step = ScoreManager._Instance.GetStep(StepType.烧结炉生产能耗);
        step.startTime = ScoreManager.GetCurveTimeLong();
    }
    public void Zhuangliao()
    {
        SetTouming(true);
        UIManage.Instance.SetHint("正在装料，请稍等");
        AudioManage.Instance.PlayMusicSource("正在装料，请稍等", 0.5f);
        models.Find("Boli_Men").DOLocalRotate(new Vector3(125, 0, 0), 1.5f).OnComplete(() =>
        {
            tuoChe.GetComponent<Animator>().enabled = true;
        });
        StartCoroutine(UIManage.Instance.enumerator(14f, () =>
        {
            Transform lanzi1 = tuoChe.Find("SJLLanzhi");
            Transform caibaozhuangParent = transform.Find("Caibaozhuang");
            for (int i = 2; i < 8; i++)
            {
                lanzi1.Find("Kuai (" + i.ToString() + ')').SetParent(caibaozhuangParent);
            }
            tuoChe.GetComponent<Animator>().enabled = false;
            tuoChe.gameObject.SetActive(false);
            models.Find("Boli_Men").DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 1.5f).OnComplete(() =>
            {
                UIManage.Instance.SetHint("装料完成，控制台点击抽真空按键");
                AudioManage.Instance.PlayMusicSource("装料完成，控制台点击抽真空按键", 0.5f);
                UIManage.Instance.SetTishiPos("SJLKongzhitai");
                MainSceneGuide.Instance.AutoMoveByIndex(12);
                UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Chouzhenkong").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(1);
            });
        }));
    }
    public void Chouzhenkong()
    {
        //InputManage._Instance.SetInputUI(GongduanType.烧结, "真空度", "请设置真空度", 0, 0.03f, "Pa", (a) =>
        //{
        //    UIManage.Instance.SetHint("正在进行抽真空,你设置的真空度为" + a + "Pa");
        //    PipeFlow couzhenkong = transform.Find("Couzhenkong").GetComponent<PipeFlow>();
        //    couzhenkong.Flow(4, null);
        //    UIManage.Instance.changeDataManager.StartChange(101.33f, a, 13, "当前压强：", "Pa", () =>
        //     {
        //         couzhenkong.Stop();
        //         UIManage.Instance.SetHint("抽真空结束，点击充氮气按键");
        //         UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Chongdanqi").GetComponent<Button>(), true);
        //         GetManager.Instance.ControlFlow.SetTestProgress(2);
        //     });
        //});

        Step step = ScoreManager._Instance.GetStep(StepType.烧结炉调节烧结真空度);
        step.startTime = ScoreManager.GetCurveTimeLong();

        UIManage.Instance.SetHint("请先调节真空度。");
        AudioManage.Instance.PlayMusicSource("请先调节真空度。", 0.5f);
        UIManage_3D._Instance.StartInput(GongduanType.烧结, "真空度", () => {
            UIManage.Instance.SetHint("正在进行抽真空,你设置的真空度为" + UIManage_3D._Instance.GetNowValue() + "Pa");
            AudioManage.Instance.PlayMusicSource("请先调节真空度。", 0.5f);
            PipeFlow couzhenkong = transform.Find("Couzhenkong").GetComponent<PipeFlow>();
            couzhenkong.Flow(4, null);
            AudioManager.SetAudio(AudioManager.CanvasAudio, "抽真空");

            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.烧结工段, "真空度");
            step.score = 25 * score;
            step.repeatCount++;

            UIManage.Instance.changeDataManager.StartChange(101.33f, UIManage_3D._Instance.GetNowValue(), 13, "当前压强：", "Pa", () =>
            {
                couzhenkong.Stop();
                AudioManager.CanvasAudio.Pause();
                UIManage.Instance.SetHint("抽真空结束，点击充氮气按键");
                 AudioManage.Instance.PlayMusicSource("抽真空结束，点击充氮气按键", 0.5f);
                UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Chongdanqi").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(2);
            });
        });
    }
    public void Chongdianqi()
    {
        UIManage.Instance.SetHint("正在进行充氮气");
        AudioManage.Instance.PlayMusicSource("正在进行充氮气", 0.5f);
        AudioManager.SetAudio(AudioManager.CanvasAudio, "充气");
        PipeFlow chongdanqi = transform.Find("Chongdanqi").GetComponent<PipeFlow>();
        chongdanqi.Flow(4, null);
        StartCoroutine(UIManage.Instance.enumerator(10, () =>
        {
            chongdanqi.Stop();
            AudioManager.CanvasAudio.Pause();
            GetManager.Instance.ControlFlow.SetTestProgress(3);
            UIManage.Instance.SetHint("充氮气完成,现在进行拆包装操作");

            Chaibaozhuang();
        }));
    }
    public void Chaibaozhuang()
    {
        UIManage.Instance.SetHint("正在拆包装操作");
        AudioManage.Instance.PlayMusicSource("正在拆包装操作", 0.5f);
        Transform player = GetManager.Instance.Root.Find("Player");
        player.GetComponent<ClickGroundMove>().enabled = false;
        player.Find("Main Camera").gameObject.SetActive(false);
        Transform newCamera = GetManager.Instance.Cameras.Find("CaibaozhuanCamera");
        newCamera.gameObject.SetActive(true);
        newCamera.transform.DOLocalMove(new Vector3(10.8f, 1.81f, -14.09f), 7);
        newCamera.transform.DOLocalRotate(new Vector3(25.345f, 230.779f, -5.527f), 7).OnComplete(() =>
        {
            foreach (Transform tran in transform.Find("Caibaozhuang"))
            {
                tran.GetComponent<ClickCaibaozhuang>().SetIsMayClick();
            }
            UIManage.Instance.SetHint("点击物体进行拆包装操作");
            AudioManage.Instance.PlayMusicSource("点击物体进行拆包装操作", 0.5f);
        });
    }
    public void CaibaoEnd()
    {
        GetManager.Instance.Cameras.Find("CaibaozhuanCamera").gameObject.SetActive(false);
        Transform player = GetManager.Instance.Root.Find("Player");
        player.GetComponent<ClickGroundMove>().enabled = true;
        player.Find("Main Camera").gameObject.SetActive(true);
        UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Shebeileiyunshu").GetComponent<Button>(), true);
        GetManager.Instance.ControlFlow.SetTestProgress(4);
        UIManage.Instance.SetHint("拆包装结束，前往控制台点击设备内运输按键");
        AudioManage.Instance.PlayMusicSource("拆包装结束，前往控制台点击设备内运输按键", 0.5f);
        UIManage.Instance.SetTishiPos("SJLKongzhitai");
        MainSceneGuide.Instance.AutoMoveByIndex(13);
    }
    public void Shebeileiyunshu()
    {
        UIManage.Instance.SetHint("正在利用传送带进行传送");
        AudioManage.Instance.PlayMusicSource("正在利用传送带进行传送", 0.5f);
        transform.Find("Caibaozhuang").DOLocalMoveZ(16.5f, 10).OnComplete(() =>
        {
            transform.Find("Caibaozhuang").gameObject.SetActive(false);
            transform.Find("ShaojieMdoels").gameObject.SetActive(true);
            UIManage.Instance.SetHint("设备内运输完成，点击烧结按键");
            AudioManage.Instance.PlayMusicSource("设备内运输完成，点击烧结按键", 0.5f);
            UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Shaojie").GetComponent<Button>(), true);
            GetManager.Instance.ControlFlow.SetTestProgress(5);
        });
    }
    public void Shaojie()
    {
        UIManage.Instance.SetHint("开启烧结程序，请调节烧结温度");
        AudioManage.Instance.PlayMusicSource("开启烧结程序，请调节烧结温度", 0.5f);
        //InputManage._Instance.SetInputUI(GongduanType.烧结, "烧结温度", "请设置烧结温度", 800, 1400, "°C", (a) =>
        //{
        //    UIManage.Instance.SetHint("你设置的烧结温度为" + a + "°C，请继续设置烧结时间");
        //    StartCoroutine(UIManage.Instance.enumerator(0, () =>
        //    {
        //        SetTime();
        //    }));
        //});

        Step step = ScoreManager._Instance.GetStep(StepType.烧结炉调节烧结温度);
        step.startTime = ScoreManager.GetCurveTimeLong();
        UIManage_3D._Instance.StartInput(GongduanType.烧结, "烧结温度", () =>
        {
            UIManage.Instance.SetHint("你设置的烧结温度为" + UIManage_3D._Instance.GetNowValue() + "°C，请继续设置烧结时间");
            AudioManage.Instance.PlayMusicSource("请继续设置烧结时间", 0.5f);
            StartCoroutine(UIManage.Instance.enumerator(0, () =>
            {
                SetTime();

                step.endTime = ScoreManager.GetCurveTimeLong();
                int score = ScoreManager._Instance.GetParameterDataScore(link.烧结工段, "烧结温度");
                step.score = 25 * score;
                step.repeatCount++;
            }));
        });
        //void SetTime()
        //{

        //    InputManage._Instance.SetInputUI(GongduanType.烧结, "烧结时间", "请设置烧结时间", 1, 30, "h", (vlue) =>
        //    {
        //        UIManage.Instance.SetHint("你设置的烧结时间为" + vlue + "h,正在升温开始烧结");
        //        //烧结
        //        SetLight(5, 8);
        //        UIManage.Instance.changeDataManager.StartChange(0, vlue, 20, "当前烧结时间", "小时", () =>
        //        {
        //            UIManage.Instance.SetHint("烧结完成,点击降温按键");
        //            UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Jiangwen").GetComponent<Button>(), true);
        //            GetManager.Instance.ControlFlow.SetTestProgress(6);
        //        });
        //    });
        //}
        void SetTime()
        {
            Canvas3Dto2D.Instance.SetCanvasActive(false);
            Step step3 = ScoreManager._Instance.GetStep(StepType.烧结炉调节烧结时间);
            step3.startTime = ScoreManager.GetCurveTimeLong();
            UIManage_3D._Instance.StartInput(GongduanType.烧结, "烧结时间", () =>
            {
                UIManage.Instance.SetHint("你设置的烧结时间为" + UIManage_3D._Instance.GetNowValue() + "h,正在升温开始烧结");
                AudioManage.Instance.PlayMusicSource("正在升温开始烧结", 0.5f);
                //烧结
                step3.endTime = ScoreManager.GetCurveTimeLong();
                int score3 = ScoreManager._Instance.GetParameterDataScore(link.烧结工段, "烧结时间");
                step3.score = 25 * score3;
                step3.repeatCount++;
                SetLight(5, 8);
                AudioManager.SetAudio(AudioManager.CanvasAudio, "加热");
                UIManage.Instance.changeDataManager.StartChange(0, UIManage_3D._Instance.GetNowValue(), 20, "当前烧结时间", "小时", () =>
                {
                    UIManage.Instance.SetHint("烧结完成,控制台点击降温按键");
                    AudioManage.Instance.PlayMusicSource("烧结完成,控制台点击降温按键", 0.5f);
                    AudioManager.CanvasAudio.Pause();
                    UIManage.Instance.SetButtonIntera(GetManager.Instance.SJLKongzhiitai.Saojielu_ButtonPather.Find("Jiangwen").GetComponent<Button>(), true);
                    GetManager.Instance.ControlFlow.SetTestProgress(6);
                    Canvas3Dto2D.Instance.SetCanvasActive(true);
                });
            });
        }
    }
    public void Jiangwen()
    {
        Canvas3Dto2D.Instance.SetCanvasActive(false);
        UIManage.Instance.SetHint("正在降温...");
        AudioManage.Instance.PlayMusicSource("正在降温", 0.5f);
        SetLight(0, 7);
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(true);
        else
            UIManage.Instance.changeDataManager.StartChange(0, 120, 7, "冷却时间", "min", null);
        StartCoroutine(UIManage.Instance.enumerator(7, () =>
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                UIManage.Instance.transform.Find("Shiyan_UI/Renque_UI").gameObject.SetActive(false);
            UIManage.Instance.SetHint("降温完成，前往出料口点击出料门进行出料运输");
            AudioManage.Instance.PlayMusicSource("降温完成，前往出料口点击出料门进行出料运输", 0.5f);
            GetManager.Instance.ControlFlow.SetTestProgress(7);
            UIManage.Instance.SetTishiPos("SJLChuliao");
            MainSceneGuide.Instance.AutoMoveByIndex(14);
            transform.Find("Models/SJLMen").GetComponent<ModelClick>().SetMayClick();
        }));
    }
    public void Quchu()
    {
        Transform endTuoche = transform.Find("EndTuoche");
        UIManage.Instance.SetHint("正在进行取出操作...");
        AudioManage.Instance.PlayMusicSource("正在进行取出操作...", 0.5f);
        SetMaterial(0.3f);
        SetTouming(false);
        transform.Find("Models/SJLMen").DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, -150, 0)), 1.5f).OnComplete(() =>
        {
            transform.Find("ShaojieMdoels").gameObject.SetActive(false);
            endTuoche.gameObject.SetActive(true);
            StartCoroutine(UIManage.Instance.enumerator(10, () =>
            {
                transform.Find("Models/SJLMen").DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 1.5f);
            }));
            StartCoroutine(UIManage.Instance.enumerator(14, () =>
            {
                endTuoche.GetComponent<Animator>().enabled = false;
                endTuoche.gameObject.SetActive(false);

                string __energyValue;
                int _deductScore = ScoreManager._Instance.GetNowlinkDeductEnergyScore(link.烧结工段, out __energyValue);
                ParameterData parameterData = new ParameterData(link.烧结工段, "生产能耗", __energyValue, 3, 3 - _deductScore, "开始速凝炉时间", "结束速凝炉时间");
                GetManager.Instance.InteractiveModel.Find("HuihuoParent").GetComponent<HuihuoTestManlag>().StartTest();
                Step step = ScoreManager._Instance.GetStep(StepType.烧结炉生产能耗);
                step.endTime = ScoreManager.GetCurveTimeLong();
                int score = ScoreManager._Instance.GetParameterDataScore(link.烧结工段, "生产能耗");
                step.score = 33 * score;
                step.repeatCount++;
            }));
        });
    }
    void SetTouming(bool isTouming)
    {
        if (isTouming)
        {
            foreach (Transform tran in transform.Find("Waike"))
                tran.GetComponent<Touming>().SetToumingColor();
            transform.Find("Models/SJLMen").GetComponent<Touming>().SetToumingColor();
        }
        else
        {
            foreach (Transform tran in transform.Find("Waike"))
                tran.GetComponent<Touming>().SetZhencangColor();
            transform.Find("Models/SJLMen").GetComponent<Touming>().SetZhencangColor();
        }
    }
    void SetLight(float targetValue, float time)
    {
        transform.Find("Lights/Point Light").GetComponent<Light>().DOIntensity(targetValue, time);
        transform.Find("Lights/Point Light (1)").GetComponent<Light>().DOIntensity(targetValue, time);
    }
    void SetMaterial(float vlaue)
    {
        material.SetFloat("_Metallic", vlaue);
        material.SetFloat("_Glossiness", vlaue);
    }
}
