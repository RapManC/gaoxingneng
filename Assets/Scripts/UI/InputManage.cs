using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManage : BaseMonoBehaviour<InputManage>
{
    Text hintText;
    Text valueText;
    Slider slider;
    float miniValue;
    float maxValue;
    float changeValue;
    GameObject input;

    GongduanType gongduan;
    string canshuName;
    string danwei;
    float NowValue;
    public  event Action<float> ConfirmEvent;
    private void Start()
    {
        hintText = transform.Find("Input/Hint").GetComponent<Text>();
        valueText = transform.Find("Input/Value").GetComponent<Text>();
        slider = transform.Find("Input/Slider").GetComponent<Slider>();
        input = transform.Find("Input").gameObject;
        slider.onValueChanged.AddListener((a) => { SetValue(a); });
    }
    /// <summary>
    /// 设置输入信息页面
    /// </summary>
    /// <param name="gongduan"></param>
    /// <param name="canshuName"></param>
    /// <param name="hint"></param>
    /// <param name="miniValue"></param>
    /// <param name="maxValue"></param>
    /// <param name="danwei"></param>
    /// <param name="action"></param>

    public void SetInputUI(GongduanType gongduan, string canshuName,string hint, float miniValue, float maxValue, string danwei,Action<float> action)
    {
        ConfirmEvent = null;
        this.gongduan = gongduan;
        this.canshuName = canshuName;
        this.hintText.text=hint;
        this.miniValue = miniValue;
        this.maxValue = maxValue;
        this.danwei = danwei;
        ConfirmEvent += action;
        changeValue = 0;
        slider.value = 0;
        valueText.text = "";
        input.SetActive(true);
    }
     void SetValue(float value)
    {
        changeValue = miniValue + (maxValue - miniValue) * value;
        changeValue = Mathf.Clamp(changeValue, miniValue, maxValue);
        string str = changeValue.ToString("F2");
        changeValue = Convert.ToSingle(str);
        valueText.text = changeValue.ToString() + danwei;
    }
    /// <summary>
    /// 点击提交按键
    /// </summary>
    public void OnConfirm()
    {
        if (valueText.text=="")
        {
            UIManage.Instance.ShowMistakeHint("参数不能为空",null);
            return;
        }
        NowValue = changeValue;
        UIManage.Instance.SetCanshu(gongduan, canshuName, NowValue, danwei,ConfirmEvent);
        input.SetActive(false);
    }
    /// <summary>
    /// 重置
    /// </summary>
    public void ResetInput()
    {
        ConfirmEvent = null;
        input.SetActive(false);
    }
}
