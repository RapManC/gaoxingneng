using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParameterControl : MonoBehaviour
{
    public GongduanType Gongduan;
    public string Name;
    public float MinValue;
    public float MaxValue;
    public float NowValue;
    public string Unit;
    public bool IsValuefloat;//调整值是否是float类型

    //分数
    [HideInInspector]public int BeginScore=4;
    [HideInInspector]public int NowScore;
    [HideInInspector]public bool IsEnergy;//是否有能量损耗


    TextMeshProUGUI _nameText;
    Text _valueText;
    Button _jiaButton;
    Button _jianButton;
    Button __affirmButton;

    //每次调节多少
    float _adjust;
    bool _isFirstclick = true;
    float _valueDeviation { get { return UIManage_3D._Instance.ValueDeviation; } }
    private void Awake()
    {
        _nameText = transform.Find("NameText/TextMeshPro").GetComponent<TextMeshProUGUI>();
        _valueText = transform.Find("ValueText/TextMeshPro").GetComponent<Text>();
        _jiaButton = transform.Find("Jia").GetComponent<Button>();
        _jianButton = transform.Find("Jian").GetComponent<Button>();
        __affirmButton = transform.parent.Find("Hide/Affirm").GetComponent<Button>();
        _jiaButton.onClick.AddListener(() => { OnJiaButton(); });
        _jianButton.onClick.AddListener(() => { OnJianButton(); });
    }
    private void Start()
    {
        _adjust = (MaxValue - MinValue) / 20;
        if (IsValuefloat)
        {
            _adjust = Convert.ToSingle(_adjust.ToString("F2"));
            _adjust = Mathf.Clamp(_adjust, 0.01f, 1);
        }
        else
        {
            _adjust = Convert.ToInt32(_adjust.ToString("F0"));
            _adjust = Mathf.Clamp(_adjust, 1, _adjust);
        }
        _nameText.text = Name;
    }
    public void StartSetValue()
    {
        _valueText.text = "--" + Unit;
        _jiaButton.gameObject.SetActive(true);
        _jianButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// 按下加按键
    /// </summary>
    public void OnJiaButton()
    {
        if (_isFirstclick)
        {
            _isFirstclick = false;
            if (IsValuefloat)
                NowValue = Convert.ToSingle((MinValue * (1 - _valueDeviation)).ToString("F2"));
            else
                NowValue = Convert.ToInt32((MinValue * (1 - _valueDeviation)).ToString("F0"));
            __affirmButton.gameObject.SetActive(true);
        }
        else
            NowValue += _adjust;
        UpdateValue();
    }
    /// <summary>
    /// 按下减按键
    /// </summary>
    public void OnJianButton()
    {
        if (_isFirstclick)
        {
            _isFirstclick = false;
            if (IsValuefloat)
                NowValue = float.Parse((MaxValue * (1 - _valueDeviation)).ToString("f2"));
            else
                NowValue = Convert.ToInt32((MaxValue * (1 - _valueDeviation)).ToString("f0"));

            __affirmButton.gameObject.SetActive(true);
        }
        else
            NowValue -= _adjust;
        UpdateValue();
    }
    /// <summary>
    /// 更新
    /// </summary>
    public void UpdateValue()
    {
        AudioManager.SetAudio(AudioManager.CanvasAudio, "调节按键");
        string str = "";
        if (IsValuefloat)
            str = NowValue.ToString("f2");
        else
            str = NowValue.ToString("f0");
        _valueText.text = str + Unit;
        NowValue = float.Parse(str);

        //判断是否到达最小与最大限制值
        float _value = Convert.ToSingle(str);
        if (_value < MinValue * (1 - _valueDeviation)|| _value-_adjust<=0)
            _jianButton.interactable = false;
        else if (_value > MaxValue * (1 + _valueDeviation))
            _jiaButton.interactable = false;
        else
        {
            _jianButton.interactable = true;
            _jiaButton.interactable = true;
        }
    }
    /// <summary>
    /// 确定
    /// </summary>
    void OnAffirmButton()
    {
        __affirmButton.gameObject.SetActive(false);
        _jiaButton.gameObject.SetActive(false);
        _jianButton.gameObject.SetActive(false);
        __affirmButton.onClick.RemoveAllListeners();
        _isFirstclick = true;
        SetNowScore();
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            UIManage_3D._Instance._OnAffiraction?.Invoke();
        }));
    }
    /// <summary>
    /// 设置分数信息
    /// </summary>
    void SetNowScore()
    {
        int _deductScore = 0;
        if (NowValue < MinValue)
        {
            float _difference = Mathf.Abs(NowValue - MinValue);//设定值与最小和理值的差别
            float _interval = MinValue * _valueDeviation / 3;//把最大浮动值平均分成三份，操作一份就扣一分
            float beilu = _difference / _interval;
            if (beilu <= 1)
                _deductScore = 0;
            else if (beilu > 1 && beilu <= 2)
                _deductScore = UnityEngine.Random.Range(-1,1);
            else
                _deductScore = 1;
        }
        else if (NowValue > MaxValue)
        {
            IsEnergy = true;
            float _difference = Mathf.Abs(NowValue - MaxValue);
            float _interval = MaxValue * _valueDeviation / 3;
            float beilu = _difference / _interval;
            if (beilu <= 1)
                _deductScore = 0;
            else if (beilu > 1 && beilu <= 2)
                _deductScore = 1;
            else if(beilu > 2 && beilu <= 3)
                _deductScore = 2;
            else
                _deductScore = 2;
        }
        else
            _deductScore = 1;
        Debug.Log(Name + "项扣除：" + _deductScore + "分");
        NowScore = BeginScore - _deductScore;
        AddParameterData();
    }
    /// <summary>
    /// 创建一个ParameterData类
    /// </summary>
    void AddParameterData()
    {
        link link = (link)((int)Gongduan + 2);
        ParameterData parameter = new ParameterData(link, Name, NowValue.ToString()+Unit, BeginScore, NowScore, Name + "开始时间", Name + "结束时间");
    }
    public void AddButtonEvent()
    {
        __affirmButton.onClick.AddListener(() => { OnAffirmButton(); });
    }
}
