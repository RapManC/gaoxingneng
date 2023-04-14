using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class ModelClick : MonoBehaviour
{
     bool isMayClick;
    QingporuTestManlag qingporuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("QingporuParent").GetComponent<QingporuTestManlag>(); } }
    QuxiangchenxingTestManlag quxiangchenxingLuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("QuxiangchenxingParent").GetComponent<QuxiangchenxingTestManlag>(); } }
    QiliumoTestManlag qiliumoTestManlag { get { return GetManager.Instance.InteractiveModel.Find("QilimoParent").GetComponent<QiliumoTestManlag>(); } }
    ShaojierLuTestManlag  shaojierLuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("ShaojierLuParent").GetComponent<ShaojierLuTestManlag>(); } }
    private void OnMouseDown()
    {
        if (isMayClick)
        {
            isMayClick = false;
            switch (name)
            {
                case "QiGuan1_Pingshen":
                quxiangchenxingLuTestManlag.Sangliao();
                    break;
                case "Fendui":
                quxiangchenxingLuTestManlag.Shebeileiyunshu();
                    break;
                case "Men":
                quxiangchenxingLuTestManlag.Yunshu();
                    break;
                case "QPLKuan":
                    qingporuTestManlag.KaigaiAnddaoliao();
                    break;
                case "QPLMen":
                    qingporuTestManlag.Zhuangyun();
                    break;
                case "QLMZhenkongqing1":
                    qiliumoTestManlag.Daoliao();
                    break;
                case "QLMZhenkongqing2":
                    qiliumoTestManlag.Yunshu();
                    break;
                case "SJLLanzhi":
                    shaojierLuTestManlag.Zhuangliao();
                    break;
                case "SJLMen":
                    shaojierLuTestManlag.Quchu();
                    break;
            }
        }
        GetComponent<HighlightEffect>().highlighted = false;
    }
    public void SetMayClick()
    {
        GetComponent<HighlightEffect>().highlighted = true;
        isMayClick = true;
    }
}
