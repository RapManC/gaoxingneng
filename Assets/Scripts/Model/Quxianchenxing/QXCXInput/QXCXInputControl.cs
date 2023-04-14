using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HighlightPlus;

public class QXCXInputControl : MonoBehaviour
{
    public Text ChicangText;
    public Text YaliText;

    public float MinChicangValue;
    public float MaxChicangValue;
    public int MinYaliValue;
    public int MaxYaliValue;

    public float ChicangFudu;
    public int YaliFudu;

    public float NowChicangValue;
    public int NowYaliValue;

    bool IsFirstChicang = true;
    bool IsFirstYali = true;

    public List<TiaojieButton> ButtonList = new List<TiaojieButton>();
    bool isHIde;


    [HideInInspector] public float _energy = 1;
    [HideInInspector] public bool IsStartTiaojie;

    public void StartTiaojie()
    {
        IsStartTiaojie = true;
        foreach (var v in ButtonList)
        {
            v.GetComponent<HighlightEffect>().highlighted = true;
        }
    }

    public void ChiCang_Jia()
    {
        if (IsFirstChicang)
        {
            IsFirstChicang = false;
            NowChicangValue = (MaxChicangValue - MinChicangValue) / 2 + MinChicangValue;
            foreach (var v in ButtonList)
            {
                if (v.name == "Jian_1" || v.name == "Jia_1")
                    v.GetComponent<HighlightEffect>().highlighted = false;
            }
        }
        NowChicangValue += ChicangFudu;
        NowChicangValue = Mathf.Clamp(NowChicangValue, MinChicangValue * (1 - 0.3f), MaxChicangValue * (1 + 0.3f));
        SetChicangValue(NowChicangValue);
        AllTioajieClick();
    }
    public void ChiCang_Jian()
    {
        if (IsFirstChicang)
        {
            IsFirstChicang = false;
            NowChicangValue = (MaxChicangValue - MinChicangValue) / 2 + MinChicangValue;
            foreach (var v in ButtonList)
            {
                if (v.name == "Jian_1" || v.name == "Jia_1")
                    v.GetComponent<HighlightEffect>().highlighted = false;
            }
        }
        NowChicangValue -= ChicangFudu;
        NowChicangValue = Mathf.Clamp(NowChicangValue, MinChicangValue * (1 - 0.3f), MaxChicangValue * (1 + 0.3f));
        SetChicangValue(NowChicangValue);
        AllTioajieClick();
    }
    public void Yali_Jia()
    {
        if (IsFirstYali)
        {
            IsFirstYali = false;
            NowYaliValue = (MaxYaliValue - MinYaliValue) / 2 + MinYaliValue;
            foreach (var v in ButtonList)
            {
                if (v.name == "Jian_2" || v.name == "Jia_2")
                    v.GetComponent<HighlightEffect>().highlighted = false;
            }
        }
        NowYaliValue += YaliFudu;
        NowYaliValue = (int)Mathf.Clamp(NowYaliValue, 1, MaxYaliValue * (1 + 0.3f));
        SetYaliValue(NowYaliValue);
        AllTioajieClick();
    }
    public void Yali_Jian()
    {
        if (IsFirstYali)
        {
            IsFirstYali = false;
            NowYaliValue = (MaxYaliValue - MinYaliValue) / 2 + MinYaliValue;
            foreach (var v in ButtonList)
            {
                if (v.name == "Jian_2" || v.name == "Jia_2")
                    v.GetComponent<HighlightEffect>().highlighted = false;
            }
        }
        NowYaliValue -= YaliFudu;
        NowYaliValue = (int)Mathf.Clamp(NowYaliValue, 1, MaxYaliValue * (1 + 0.3f));
        SetYaliValue(NowYaliValue);
        AllTioajieClick();
    }
    void SetChicangValue(float value)
    {
        ChicangText.text = value.ToString("F2") + "T";
        ChicangText.gameObject.SetActive(true);
    }
    void SetYaliValue(int value)
    {
        YaliText.text = value + "Pa";
        YaliText.gameObject.SetActive(true);
    }
    /// <summary>
    /// 所有的都调节了
    /// </summary>
    void AllTioajieClick()
    {
        if (!IsFirstChicang && !IsFirstYali && !isHIde)
        {
            isHIde = true;
            UIManage.Instance.SetHint("调节完成后点击提示按键进行压制成型操作");
            GetManager.Instance.InteractiveModel.Find("QuxiangchenxingParent").GetComponent<QuxiangchenxingTestManlag>().jiaohuModel.Find("Buttons/Yazi").GetComponent<QXCX_ButtonControl>().SetIsMayClick();
            GetManager.Instance.ControlFlow.SetTestProgress(2);
        }
    }

    /// <summary>
    /// 创建分数信息
    /// </summary>
    public void FoundScore(bool isChicang)
    {
        float NowValue, MinValue, MaxValue;

        if (isChicang)
        {
            NowValue = NowChicangValue;
            MinValue = MinChicangValue;
            MaxValue = MaxChicangValue;
        }
        else
        {
            NowValue = NowYaliValue;
            MinValue = MinYaliValue;
            MaxValue = MaxYaliValue;
        }
        int _deductScore = 0;
        if (NowValue < MinValue)
        {
            float _difference = Mathf.Abs(NowValue - MinValue);//设定值与最小和理值的差别
            float _interval = MinValue * 0.3f / 3;//把最大浮动值平均分成三份，操作一份就扣一分
            float beilu = _difference / _interval;
            if (beilu <= 1)
                _deductScore = 0;
            else if (beilu > 1 && beilu <= 2)
                _deductScore = 1;
            else
                _deductScore = 1;
        }
        else if (NowValue > MaxValue)
        {
            _energy += (NowValue - (MinValue + (MaxValue - MinValue) / 2)) / (MinValue + (MaxValue - MinValue) / 2);
            float _difference = Mathf.Abs(NowValue - MaxValue);
            float _interval = MaxValue * 0.3f / 3;
            float beilu = _difference / _interval;
            if (beilu <= 1)
                _deductScore = 0;
            else if (beilu > 1 && beilu <= 2)
                _deductScore = 1;
            else
                _deductScore = 1;
        }
        else
            _deductScore = 0;

        int _nowScore = 4 - _deductScore;
        if (isChicang)
        {
            Debug.Log("磁场项扣除：" + _deductScore + "分");
            ParameterData parameter = new ParameterData(link.取向成型工段, "磁场", NowValue.ToString() + "T", 4, _nowScore, "磁场调节开始时间", "磁场调节结束时间");
        }
        else
        {
            Debug.Log("压力项扣除：" + _deductScore + "分");
            ParameterData parameter = new ParameterData(link.取向成型工段, "压力", NowValue.ToString() + "Pa", 4, _nowScore, "压力调节开始时间", "压力调节结束时间");
        }
    }

    /// <summary>
    /// 创建能量损耗分数信息
    /// </summary>
    public void FoundEnergyScore()
    {
        int _deductScore = 0;
        if (_energy > 1 && _energy <= 1.5)
            _deductScore = 1;
        else if(_energy > 1.5 && _energy <= 3)
            _deductScore = 2;
        else if (_energy > 3)
            _deductScore = 3;
        Debug.Log(link.取向成型工段 + "工段扣除：" + _deductScore + "生产能耗");
        string _value = (_energy * 100).ToString("F0") + '%';
        ParameterData parameter = new ParameterData(link.取向成型工段, "生产能耗", _value + "", 3, 3-_deductScore, "取向成型开始时间", "取向成型结束时间");
    }
}
