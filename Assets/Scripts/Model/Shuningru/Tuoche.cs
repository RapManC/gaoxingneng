using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuoche : MonoBehaviour
{
    /// <summary>
    /// （动画事件）推车动画结束后调用
    /// </summary>
    public void StartQingporu()
    {
        Anim1End();
    }
    /// <summary>
    /// 推车结束后的状态
    /// </summary>
    public void Anim1End()
    {
        GetComponent<Animator>().enabled = false;
        transform.localPosition = new Vector3(7.95f, 1, 3.32f);
        transform.localEulerAngles = new Vector3(0, 90, 0);
        ShuningruTestManlag.Instance.Huhu();
        GetManager.Instance.Canvas.Find("Shiyan_UI/Huihu_Button").gameObject.SetActive(false);
        GetManager.Instance.Canvas.Find("Shiyan_UI/Toumingmoshi_Button").gameObject.SetActive(false);
        transform.Find("Kuangkuang").gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    /// <summary>
    /// (动画事件)氢破炉倒料
    /// </summary>
    public void DaoliaoAnimEnd()
    {
        GetComponent<Animator>().enabled = false;
        Animator menAnim= GetManager.Instance.QingporuParent.Find("Qingporu/Qingporu_Men").GetComponent<Animator>();
        menAnim.Play("KaimenAnimation_Dao");
        StartCoroutine(UIManage.Instance.enumerator(5.5f, () =>
        {
            gameObject.SetActive(false);
            UIManage.Instance.SetHint("倒料结束，控制台点击升温按键");
            AudioManage.Instance.PlayMusicSource("倒料结束，控制台点击升温按键", 0.5f);
            UIManage.Instance.SetTishiPos("QPLKongzhitai");
            UIManage.Instance.SetButtonIntera(GetManager.Instance.QPLKongzhitai.Qingporu_ButtonPather.Find("Shengwen").GetComponent<Button>(), true);
        }));
    }
    /// <summary>
    /// （动画事件）开始气流磨
    /// </summary>
    public void StartQiliumo()
    {
        GetComponent<Animator>().enabled = false;
        gameObject.SetActive(false);

        string __energyValue;
        int _deductScore = ScoreManager._Instance.GetNowlinkDeductEnergyScore(link.氢破炉工段, out __energyValue);
        ParameterData parameterData = new ParameterData(link.氢破炉工段, "生产能耗", __energyValue, 3, 3 - _deductScore, "开始速凝炉时间", "结束速凝炉时间");

        GetManager.Instance.InteractiveModel.Find("QilimoParent").GetComponent<QiliumoTestManlag>().StartTest();

        Step step = ScoreManager._Instance.GetStep(StepType.氢破炉生产能耗);
        step.endTime = ScoreManager.GetCurveTimeLong();
        int score = ScoreManager._Instance.GetParameterDataScore(link.氢破炉工段, "生产能耗");
        step.score = 33 * score;
        step.repeatCount++;
    }
}
