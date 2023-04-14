
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YuanshuPeizhiSlider : MonoBehaviour
{
    public string YuanshuName;
    //超出比例
    public float Exceed = 0.3f;
    public Slider slider;
    public float Value;
    float MiniRatio;
    float MaxRatio;
    float ratio;
    public Text text;


   // public bool IsAdjust=false;

    private void Start()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        slider.onValueChanged.AddListener((value) => { SetValue(value); SetIsPeizhiEnd(); });
        text = GetComponent<Text>();
        text.text = YuanshuName;
        switch (YuanshuName)
        {
            case "钕(Nd)":
                SetRatio(20, 35);
                break;
            case "硼(B)":
                SetRatio(0.8f, 1.3f);
                break;
            case "铁(Fe)":
                SetRatio(0, 100);
                break;
            case "镝(Dy)":
                SetRatio(1, 5);
                break;
            case "镨(Pr)":
                SetRatio(1, 5);
                break;
            case "铽(Tb)":
                SetRatio(1, 5);
                break;
            case "钴(co)":
                SetRatio(0, 15);
                break;
            case "铝(Al)":
                SetRatio(0, 0.5f);
                break;
            case "铜(Cu)":
                SetRatio(0.05f, 0.15f);
                break;
            default:
                Debug.Log("元素名字赋值出现错误：" + YuanshuName);
                break;
        }
        ratio = (1 + Exceed) * MaxRatio;
    }
    void SetRatio(float mini, float max)
    {
        MiniRatio = mini;
        MaxRatio = max;
    }
    void SetValue(float value)
    {
        
        float V = MiniRatio + (ratio-MiniRatio)*value;
        string show = V.ToString("0.00");
        Value = Convert.ToSingle(show);
        text.text = YuanshuName +" "+ show+"%";
        UIManage.Instance.Sum = 0;
        foreach (var v in UIManage.Instance.AllPeizhiSlider)
        {
            v.text.color = this.YuanshuName == v.YuanshuName ? Color.yellow : Color.white;
            if (v.YuanshuName != "铁(Fe)")
                UIManage.Instance.Sum += v.Value;
        }
        UIManage.Instance.AuotSetFeValue(100 - UIManage.Instance.Sum);
    }
    void SetIsPeizhiEnd()
    {
        bool isend = true;
        foreach (var v in UIManage.Instance.AllPeizhiSlider)
        {
            if (v.Value < 0.001f && v.YuanshuName != "铁(Fe)")
                isend = false;
        }
        GetManager.Instance.Canvas.transform.Find("Shiyan_UI/SelectPanels_UI/SelectPanel_UI (2)/Notarize_Button").GetComponent<Button>().interactable = isend;
    }
    public void AuotSetFeValue(float value)
    {
        text.text = YuanshuName + " " + value.ToString("f2") + "%";
        Value = value;
    }
}
