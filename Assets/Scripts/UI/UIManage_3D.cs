using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManage_3D :BaseMonoBehaviour<UIManage_3D>
{
    float _time = 0;
    [HideInInspector]public Action _OnAffiraction;

    public float ValueDeviation = 0.3f;
    public float UpdataTimeZH=1;
    ParameterControl _nowParameterControl;
    public List<GameObject> KongzhitaiPingmuList=new List<GameObject>();
    public List<ParameterControl> ParameterControlList = new List<ParameterControl>();
    private new void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
    }
    public void StartInput(GongduanType gongduanType, string _name, Action action)
    {
        _nowParameterControl  = GetParameterControl(gongduanType, _name);
        
        if (_nowParameterControl)
        {
            _OnAffiraction = null;
            _OnAffiraction = action;
            _nowParameterControl.StartSetValue();
            _nowParameterControl.AddButtonEvent();
        }
        else
            Debug.Log("没有找到：" + gongduanType + _name + "调节属性");
    }
    public float GetNowValue()
    {
        if (_nowParameterControl)
            return _nowParameterControl.NowValue;
        else
            return -1;
    }
    public float GetValue(GongduanType gongduanType, string _name)
    {
        ParameterControl parameter2 = GetParameterControl(gongduanType,_name);
        if (parameter2)
            return parameter2.NowValue;
        else
            return -1;
    }
    public ParameterControl GetParameterControl(GongduanType gongduanType, string _name)
    {
        ParameterControl _temp = null;
        foreach (ParameterControl parameter in ParameterControlList)
        {
            if (parameter.Gongduan == gongduanType)
            {
                if (parameter.Name == _name)
                {
                    _temp= parameter;
                }
            }
        }
        return _temp;
    }
    private void FixedUpdate()
    {
        UpdataTime();
    }
    void UpdataTime()
    {
        _time += Time.deltaTime;
        if (_time >= (1 / UpdataTimeZH))
        {
            _time = 0;
            foreach(var go in KongzhitaiPingmuList)
            {
                if (go.transform.Find("Canvas_3D").gameObject.activeSelf)
                {
                    go.transform.Find("Canvas_3D/Content/Hdie/Time").GetComponent<TextMeshProUGUI>().text =GetTime();
                }
            }
        }
    }
    public  string GetTime()
    {
        return "时间　"+DateTime.Now.ToString("hh:mm:ss")+"日期　" +DateTime.Now.ToString("MM:dd");
    }
}

