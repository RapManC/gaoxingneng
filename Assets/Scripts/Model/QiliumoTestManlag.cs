using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class QiliumoTestManlag : MonoBehaviour
{
    Transform tuoChe;
    ParticleSystem _texiaoShangliao;
    ParticleSystem _texiaoPoshui;
    ParticleSystem _texiaoZuanliao;
    ParticleSystem _texiaoDouLi;
    PipeFlow jiadanqi;
    private void Awake()
    {
        tuoChe = transform.Find("TuoChe");
        _texiaoShangliao = transform.Find("Shangliao_Texiao").GetComponent<ParticleSystem>();
        _texiaoPoshui = transform.Find("Poshui_Texiao").GetComponent<ParticleSystem>();
        _texiaoZuanliao = transform.Find("Zuangliao_Texiao").GetComponent<ParticleSystem>();
        _texiaoDouLi= transform.Find("doulideliao").GetComponent<ParticleSystem>();
        jiadanqi = transform.Find("Jidanqi").GetComponent<PipeFlow>();
    }
    /// <summary>
    /// 开始实验
    /// </summary>
    public void StartTest()
    {
        GetManager.Instance.ControlFlow.Jingxingzong(5);
        tuoChe.gameObject.SetActive(true);
        UIManage.Instance.SetHint("现在进行气流磨工段，提示地点点击真空瓶进行上料操作。");
        AudioManage.Instance.PlayMusicSource("现在进行气流磨工段，提示地点点击真空瓶进行上料操作。", 0.5f);
        UIManage.Instance.SetTishiPos("QLMShangliao");
        MainSceneGuide.Instance.AutoMoveByIndex(5);
        transform.Find("TuoChe/QLMZhenkongqing1").GetComponent<ModelClick>().SetMayClick();
        GetManager.Instance.QLMKongzhiitai.ShowCanvas();
        GetManager.Instance.ControlFlow.SetTestProgress(0);

        Step step = ScoreManager._Instance.GetStep(StepType.靶式气流磨生产能耗);
        step.startTime = ScoreManager.GetCurveTimeLong(); ;    }
    /// <summary>
    /// 倒料
    /// </summary>
    public void Daoliao()
    {
        UIManage.Instance.SetHint("正在倒料，请前往查看。");
        AudioManage.Instance.PlayMusicSource("正在倒料，请进行查看", 0.5f);
        tuoChe.GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(6, () =>
        {
            transform.Find("Zhengkongping").gameObject.SetActive(true);
            SetTouming(true);
            tuoChe.GetComponent<Animator>().enabled = false;
            tuoChe.gameObject.SetActive(false);
            _texiaoShangliao.gameObject.SetActive(true);
            
            transform.Find("Shangliao_Texiao").gameObject.SetActive(true);
            StartCoroutine(UIManage.Instance.enumerator(15, () =>
            {
                UIManage.Instance.SetHint("倒料结束，请前往控制台点击加氮气按键。");
                AudioManage.Instance.PlayMusicSource("倒料结束，请前往控制台点击加氮气按键。", 0.5f);
                UIManage.Instance.SetTishiPos("QLMKongzhitai");
                MainSceneGuide.Instance.AutoMoveByIndex(6, 0);
                Canvas3Dto2D.Instance.SetCameraTrans(2);
                _texiaoShangliao.gameObject.SetActive(false);
                UIManage.Instance.SetButtonIntera(GetManager.Instance.QLMKongzhiitai.Qingporu_ButtonPather.Find("Jiadanqi").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(1);
                _texiaoDouLi.gameObject.SetActive(true);
                StartCoroutine(UIManage.Instance.enumerator(0, () => _texiaoDouLi.Pause()));
            }));
        }));
    }
    public void Jiadanqi()
    {
        //InputManage._Instance.SetInputUI(GongduanType.气流磨, "压力", "请设置压力值", 0.2f, 1f, "Pa", (p) =>
        //{
        //    UIManage.Instance.SetHint("你设置的压力为：" + p + "Pa，正在加氮气,请前往观察。");
        //    jiadanqi.Flow(4, null);
        //    StartCoroutine(UIManage.Instance.enumerator(10, () =>
        //    {
        //        jiadanqi.Stop();
        //        UIManage.Instance.SetHint("加氮气完成，请前往控制台点击碰撞破碎按键");
        //        UIManage.Instance.SetButtonIntera(GetManager.Instance.QLMKongzhiitai.Qingporu_ButtonPather.Find("Pengzhuangposhui").GetComponent<Button>(), true);
        //        GetManager.Instance.ControlFlow.SetTestProgress(2);
        //    }));
        //});

        Step step = ScoreManager._Instance.GetStep(StepType.靶式气流磨调节破碎压力);
        step.startTime = ScoreManager.GetCurveTimeLong();

        UIManage_3D._Instance.StartInput(GongduanType.气流磨, "压力", () => {
            UIManage.Instance.SetHint("你设置的压力为：" + UIManage_3D._Instance.GetNowValue() + "Pa，正在加氮气,请前往观察。");
            AudioManage.Instance.PlayMusicSource("正在加氮气,请前往观察。", 0.5f);
            jiadanqi.Flow(4, null);

            step.endTime = ScoreManager.GetCurveTimeLong(); ;
            int score = ScoreManager._Instance.GetParameterDataScore(link.靶式气流磨工段, "压力");
            step.score = 25 * score;
            step.repeatCount++;

            StartCoroutine(UIManage.Instance.enumerator(8, () =>
            {
                jiadanqi.Stop();
                UIManage.Instance.SetHint("加氮气完成，请前往控制台点击碰撞破碎按键");
                UIManage.Instance.SetButtonIntera(GetManager.Instance.QLMKongzhiitai.Qingporu_ButtonPather.Find("Pengzhuangposhui").GetComponent<Button>(), true);
                GetManager.Instance.ControlFlow.SetTestProgress(2);
            }));
        });
    }
    public void Pengzhuangposhui()
    {
        //InputManage._Instance.SetInputUI(GongduanType.气流磨, "分选轮转速", "请设置分选轮转速", 800, 6000, "r/min",
        //    (a) =>
        //    {
        //        UIManage.Instance.SetHint("你设置的分选轮转速为：" + a + "r/min.正在进行破碎。");
        //        texiao.Play();
        //        StartCoroutine(UIManage.Instance.enumerator(17, () =>
        //        {
        //            UIManage.Instance.SetHint("破碎结束，请前往控制台点击装瓶按键");
        //            UIManage.Instance.SetButtonIntera(GetManager.Instance.QLMKongzhiitai.Qingporu_ButtonPather.Find("Zuangping").GetComponent<Button>(), true);
        //            GetManager.Instance.ControlFlow.SetTestProgress(3);
        //        }));
        //    });

        Step step = ScoreManager._Instance.GetStep(StepType.靶式气流磨调节分选轮转速);
        step.startTime = ScoreManager.GetCurveTimeLong();

        UIManage_3D._Instance.StartInput(GongduanType.气流磨, "分选轮转速", () => {
            Canvas3Dto2D.Instance.SetCanvasActive(false);
            UIManage.Instance.SetHint("你设置的分选轮转速为：" + UIManage_3D._Instance.GetNowValue() + "r/min.正在进行破碎。");
            AudioManage.Instance.PlayMusicSource("正在进行破碎。", 0.5f);
            _texiaoPoshui.gameObject.SetActive(true);
            _texiaoDouLi.Play();
            AudioManager.SetAudio(AudioManager.BGAudio, "破碎");
            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.靶式气流磨工段, "分选轮转速");
            step.score = 25 * score;
            step.repeatCount++;

            StartCoroutine(UIManage.Instance.enumerator(8, () =>
            {
                _texiaoZuanliao.gameObject.SetActive(true);
                transform.Find("QLMZhenkongqing2").gameObject.SetActive(true);
            }));
            StartCoroutine(UIManage.Instance.enumerator(30, () =>
            {
                UIManage.Instance.SetHint("破碎结束，点击装料瓶进行运输");
                AudioManage.Instance.PlayMusicSource("破碎结束，点击装料瓶进行运输", 0.5f);
                transform.Find("QLMZhenkongqing2").GetComponent<ModelClick>().SetMayClick();
                GetManager.Instance.ControlFlow.SetTestProgress(3);
                AudioManager.BGAudio.Pause();
                _texiaoPoshui.gameObject.SetActive(false);
            }));
        });
    }
    public void Yunshu()
    {
        UIManage.Instance.SetHint("正在进行运输操作");
        AudioManage.Instance.PlayMusicSource("正在进行运输操作", 0.5f);
        transform.Find("QLMZhenkongqing2").gameObject.SetActive(false);
        _texiaoZuanliao.gameObject.SetActive(false);
        tuoChe.gameObject.SetActive(true);
        Animator tuocheAnim = tuoChe.GetComponent<Animator>();
        tuocheAnim.Play("TuocheAnimation2");
        tuocheAnim.enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(19, () => {

            string __energyValue;
            int _deductScore = ScoreManager._Instance.GetNowlinkDeductEnergyScore(link.靶式气流磨工段, out __energyValue);
            ParameterData parameterData = new ParameterData(link.靶式气流磨工段, "生产能耗", __energyValue, 3,3- _deductScore, "开始速凝炉时间", "结束速凝炉时间");

            GetManager.Instance.InteractiveModel.Find("QuxiangchenxingParent").GetComponent<QuxiangchenxingTestManlag>().StartTest();
            tuoChe.gameObject.SetActive(false);

            Step step = ScoreManager._Instance.GetStep(StepType.靶式气流磨生产能耗);
            step.endTime = ScoreManager.GetCurveTimeLong();
            int score = ScoreManager._Instance.GetParameterDataScore(link.靶式气流磨工段, "生产能耗");
            step.score = 33 * score;
            step.repeatCount++;
        }));
    }
    void SetTouming(bool isTouming)
    {
        if (isTouming)
            transform.Find("Touming").GetComponent<Touming>().SetToumingColor();
        else
            transform.Find("Touming").GetComponent<Touming>().SetZhencangColor();
    }
}
