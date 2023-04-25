using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HuihuoTestManlag :MonoBehaviour
{ 
    Transform tuoChe;
    Transform men;
    Material material;
    private void Awake()
    {
        tuoChe = transform.Find("Tuoche");
        material = Resources.Load<Material>("Material/Kuai");
        men = transform.Find("Men");
    }

    private void Start()
    {
        UIManage_3D._Instance.KongzhitaiPingmuList.Add(transform.Find("ShaojieluKongzhitai").gameObject);
        foreach (Transform tran in transform.Find("ShaojieluKongzhitai/Canvas_3D/Content/Input_UI"))
        {
            if (tran.GetComponent<ParameterControl>())
            {
                UIManage_3D._Instance.ParameterControlList.Add(tran.GetComponent<ParameterControl>());
            }
        }
        SetMaterial(0.3f);
    }
    private void OnDestroy()
{
        UIManage_3D._Instance.KongzhitaiPingmuList.Remove(transform.Find("ShaojieluKongzhitai").gameObject);
        foreach (Transform tran in transform.Find("ShaojieluKongzhitai/Canvas_3D/Content/Input_UI"))
        {
            if (tran.GetComponent<ParameterControl>())
            {
                UIManage_3D._Instance.ParameterControlList.Remove(tran.GetComponent<ParameterControl>());
            }
        }
    }
    public void StartTest()
    {
        SetMaterial(0.3f);
        tuoChe.gameObject.SetActive(true);
        transform.Find("ShaojieluKongzhitai/Canvas_3D").gameObject.SetActive(true);
        GetManager.Instance.ControlFlow.Jingxingzong(8);
        UIManage.Instance.SetHint("现在进行回火操作，前往点击回火炉门进行装料。");
        AudioManage.Instance.PlayMusicSource("现在进行回火操作，前往点击回火炉门进行装料。", 0.5f);
        UIManage.Instance.SetTishiPos("HHLShangliao");
        MainSceneGuide.Instance.AutoMoveByIndex(15);
        men.GetComponent<HuihuoModelClick>().SetModelClick();
        GetManager.Instance.ControlFlow.SetTestProgress(0);

        Step step = ScoreManager._Instance.GetStep(StepType.回火工段生产能耗);
        step.startTime = ScoreManager.GetCurveTimeLong();
    }
    /// <summary>
    /// 上料
    /// </summary>
    public void Shangliao()
    {
        UIManage.Instance.SetHint("正在进行装料操作");
        AudioManage.Instance.PlayMusicSource("正在进行装料操作", 0.5f);
        SetTouming(true);
        men.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, -100, 0)), 1.5f).OnComplete(() =>
        {
            tuoChe.GetComponent<Animator>().enabled = true;
            StartCoroutine(UIManage.Instance.enumerator(8.5f, () =>
            {
                tuoChe.gameObject.SetActive(false);
                transform.Find("ShaojieMdoels").gameObject.SetActive(true);
                men.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 1.5f).OnComplete(() =>
                {
                    
                });
                StartCoroutine(UIManage.Instance.enumerator(1.5f, () => Huihuo()));
            }));
        });
    }
    /// <summary>
    /// 回火
    /// </summary>
    public void Huihuo()
    {
        GetManager.Instance.ControlFlow.SetTestProgress(1);
        UIManage.Instance.SetHint("前往回火炉控制台调节回火参数");
        AudioManage.Instance.PlayMusicSource("前往回火炉控制台调节回火参数", 0.5f);
        UIManage.Instance.SetTishiPos("HHLTiaojie");
        MainSceneGuide.Instance.AutoMoveByIndex(16, 0);
        Canvas3Dto2D.Instance.SetCameraTrans(4);
        SetData1();
        //void SetData1()
        //{
        //    InputManage._Instance.SetInputUI(GongduanType.回火, "一级回火温度", "请调节一级回火温度", 700, 1100, "°C", (temp1) =>
        //    {
        //        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        //        {
        //            UIManage.Instance.SetHint("你设置的一级回火温度为" + temp1 + "°C，请继续设置一级回火时间。");
        //            InputManage._Instance.SetInputUI(GongduanType.回火, "一级回火时间", "请调节一级回火时间", 1, 14, "h", (time1) =>
        //            {
        //                UIManage.Instance.SetHint("你设置的二级回火时间为" + time1 + "h，请继续设置二级回火温度。");
        //                StartCoroutine(UIManage.Instance.enumerator(0, () => {
        //                    SetData2();
        //                }));
        //            });
        //        }));
        //    });
        //}
        //void SetData2()
        //{
        //    InputManage._Instance.SetInputUI(GongduanType.回火, "二级回火温度", "请调节二级回火温度", 300, 800, "°C", (temp2) => {
        //        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        //        {
        //            UIManage.Instance.SetHint("你设置的二级回火温度为" + temp2 + "h，请继续设置二级回火时间。");
        //            InputManage._Instance.SetInputUI(GongduanType.回火, "二级回火时间", "请调节二级回火时间", 1, 14, "h", (time2) => {
        //                UIManage.Instance.SetHint("你设置的二级回火时间为" + time2 + "h，正在进行回火操作。");
        //                huihuo();
        //            });
        //        }));
        //    });
        //}
        void SetData1()
        {

            Step step = ScoreManager._Instance.GetStep(StepType.回火工段调节一级回火温度);
            step.startTime = ScoreManager.GetCurveTimeLong();

            UIManage_3D._Instance.StartInput(GongduanType.回火, "一级回火温度",()=> {
                StartCoroutine(UIManage.Instance.enumerator(0, () =>
                {
                    int score = ScoreManager._Instance.GetParameterDataScore(link.回火工段, "一级回火温度");
                    step.score = 25 * score;
                    step.repeatCount++;
                    step.endTime = ScoreManager.GetCurveTimeLong();
                    Step step2 = ScoreManager._Instance.GetStep(StepType.回火工段调节一级回火时间);
                    step2.startTime = ScoreManager.GetCurveTimeLong();

                    UIManage.Instance.SetHint("你设置的一级回火温度为" + UIManage_3D._Instance.GetNowValue() + "°C，请继续设置一级回火时间。");
                    
                    UIManage_3D._Instance.StartInput(GongduanType.回火, "一级回火时间", () => {
                        int score2 = ScoreManager._Instance.GetParameterDataScore(link.回火工段, "一级回火时间");
                        step2.score = 25 * score;
                        step2.repeatCount++;
                        step2.endTime = ScoreManager.GetCurveTimeLong();
                        UIManage.Instance.SetHint("你设置的二级回火时间为" + UIManage_3D._Instance.GetNowValue() + "h，请继续设置二级回火温度。");
                        StartCoroutine(UIManage.Instance.enumerator(0, () =>
                        {
                            SetData2();
                        }));
                    });
                }));
            });
        }
        void SetData2()
        {
            Step step = ScoreManager._Instance.GetStep(StepType.回火工段调节二级回火温度);
            step.startTime = ScoreManager.GetCurveTimeLong();
            UIManage_3D._Instance.StartInput(GongduanType.回火, "二级回火温度", () =>
            {
                StartCoroutine(UIManage.Instance.enumerator(0, () =>
                {

                    int score = ScoreManager._Instance.GetParameterDataScore(link.回火工段, "二级回火温度");
                    step.score = 25 * score;
                    step.repeatCount++;
                    step.endTime= ScoreManager.GetCurveTimeLong();
                    Step step2 = ScoreManager._Instance.GetStep(StepType.回火工段调节二级回火时间);
                    step2.startTime = ScoreManager.GetCurveTimeLong();

                    UIManage.Instance.SetHint("你设置的二级回火温度为" + UIManage_3D._Instance.GetNowValue() + "h，请继续设置二级回火时间。");
                    UIManage_3D._Instance.StartInput(GongduanType.回火, "二级回火时间", () =>
                    {
                        int score2 = ScoreManager._Instance.GetParameterDataScore(link.回火工段, "二级回火时间");
                        step2.score = 25 * score;
                        step2.repeatCount++;
                        step2.endTime = ScoreManager.GetCurveTimeLong(); 
                        UIManage.Instance.SetHint("你设置的二级回火时间为" + UIManage_3D._Instance.GetNowValue() + "h，正在进行回火操作。");
                        huihuo();
                    });
                }));
            });
        }
        
        void huihuo()
        {
            Canvas3Dto2D.Instance.SetCanvasActive(false);
            GetManager.Instance.ControlFlow.SetTestProgress(2);
            SetLight(3, 7);
            AudioManager.SetAudio(AudioManager.CanvasAudio, "加热");
            StartCoroutine(UIManage.Instance.enumerator(7, () =>
            {
                SetLight(0, 7);
                StartCoroutine(UIManage.Instance.enumerator(7, () =>
                {
                    SetLight(3, 7);
                    StartCoroutine(UIManage.Instance.enumerator(7, () =>
                    {
                        SetLight(0, 7);
                        AudioManager.CanvasAudio.Pause();
                        StartCoroutine(UIManage.Instance.enumerator(7, () => {
                            UIManage.Instance.SetHint("回火完成，点击回火炉门进行装运操作");
                            AudioManage.Instance.PlayMusicSource("回火完成，点击回火炉门进行装运操作", 0.5f);
                            men.GetComponent<HuihuoModelClick>().SetModelClick();
                            men.GetComponent<HuihuoModelClick>().IsZuangliao = true;
                            GetManager.Instance.ControlFlow.SetTestProgress(3);
                        }));
                    }));
                }));
            }));
        }
    }
    /// <summary>
    /// 装运
    /// </summary>
    public void Zhuangyun()
    {
        UIManage.Instance.SetHint("正在进行装料操作");
        AudioManage.Instance.PlayMusicSource("正在进行装料操作", 0.5f);
        transform.Find("ShaojieMdoels").gameObject.SetActive(false);
        men.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, -100, 0)), 1.5f).OnComplete(() =>
        {
            tuoChe.gameObject.SetActive(true);
            Animator anim = tuoChe.GetComponent<Animator>();
            anim.Play("Tuoche1Animation_Dao");
            SetMaterial(0.7f);
            anim.enabled = true;
            StartCoroutine(UIManage.Instance.enumerator(9, () => {
                anim.Play("Tuoche2Animation");
                men.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 1.5f);
                StartCoroutine(UIManage.Instance.enumerator(7f, () => {
                    UIManage.Instance.SetHint("回火操作已经全部完成，接下来进行检测操作。");
                    AudioManage.Instance.PlayMusicSource("回火完成，点击回火炉门进行装运操作", 0.5f);
                    tuoChe.gameObject.SetActive(false);
                    SetTouming(false);

                    string __energyValue;
                    int _deductScore = ScoreManager._Instance.GetNowlinkDeductEnergyScore(link.回火工段, out __energyValue);
                    ParameterData parameterData = new ParameterData(link.回火工段, "生产能耗", __energyValue, 3, 3 - _deductScore, "开始速凝炉时间", "结束速凝炉时间");

                    GetManager.Instance.InteractiveModel.Find("Jianche").GetComponent<JiancheTestManlag>().StartTest();

                    Step step = ScoreManager._Instance.GetStep(StepType.回火工段生产能耗);
                    step.endTime = ScoreManager.GetCurveTimeLong();
                    int score = ScoreManager._Instance.GetParameterDataScore(link.回火工段, "生产能耗");
                    step.score = 33 * score;
                    step.repeatCount++;
                }));
            }));
        });
    }
    void SetTouming(bool isTouming)
    {
        if (isTouming)
        {
            foreach (Transform tran in transform.Find("Waike"))
                tran.GetComponent<Touming>().SetToumingColor();
            transform.Find("Men").GetComponent<Touming>().SetToumingColor();
        }
        else
        {
            foreach (Transform tran in transform.Find("Waike"))
                tran.GetComponent<Touming>().SetZhencangColor();
            transform.Find("Men").GetComponent<Touming>().SetZhencangColor();
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
