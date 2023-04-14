using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDataManager : MonoBehaviour
{
    float Chushizi;
    string danwei;
    string qianyan;
    float mubiaoValue;

    bool isStart;
    float shulv;
    float value;
    float ShowValue;

    event Action jishiEnd;
    private void FixedUpdate()
    {
        Daojishi();
    }
    /// <summary>
    /// 开始变化
    /// </summary>
    /// <param name="qishiValue">起始值</param>
    /// <param name="mubiaoValue">目标值</param>
    /// <param name="time">变化时间</param>
    /// <param name="qianyan">前言</param>
    /// <param name="danwei">单位</param>
    /// <param name="action">变化结束后调用的事件</param>
    public void StartChange(float qishiValue, float mubiaoValue, float time, string qianyan, string danwei,Action action)
    {
        jishiEnd = null;
        value = 0;
        ShowValue = qishiValue;
        Chushizi = qishiValue;
        shulv = (qishiValue - mubiaoValue) / time;
        this.mubiaoValue = mubiaoValue;
        this.qianyan = qianyan;
        this.danwei = danwei;
        isStart = true;
        transform.Find("Image").gameObject.SetActive(true);
        jishiEnd += action;
    }
    void Daojishi()
    {
        if (isStart)
        {
            value += Time.deltaTime * shulv;
            ShowValue = Chushizi - value;
            if (Chushizi > mubiaoValue)
            {
                ShowValue = Mathf.Clamp(ShowValue, mubiaoValue, Chushizi);
            }
            else
            {
                ShowValue = Mathf.Clamp(ShowValue,Chushizi, mubiaoValue);
            }
            if (ShowValue == mubiaoValue)
            {
                isStart = false;
                StartCoroutine(UIManage.Instance.enumerator(1, () =>
                {
                    transform.Find("Image").gameObject.SetActive(false);
                    jishiEnd?.Invoke();
                }));
            }
            transform.Find("Image/Text").GetComponent<Text>().text =qianyan+"："+ ShowValue.ToString("F2") + danwei;
        }
    }
}
