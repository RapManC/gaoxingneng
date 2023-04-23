using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class QXCX_ButtonControl : MonoBehaviour
{
    bool isClick;
    bool IsMayClick;
    QuxiangchenxingTestManlag shaojierLuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("QuxiangchenxingParent").GetComponent<QuxiangchenxingTestManlag>(); } }
    private void Awake()
    {
        
    }
    private void OnMouseDown()
    {
        if (IsMayClick)
        {
            if (!isClick)
            {
                MainSceneGuide.Instance.StopAutoMove();
                isClick = true;
                Debug.Log(name+"被点击");
                GetComponent<HighlightEffect>().highlighted = false;
                SetClickColor();
                switch (name)
                {
                    case "Yunshong":
                        shaojierLuTestManlag.Chengzhong();
                        break;
                    case "Yazi":
                        shaojierLuTestManlag.Yuazhichengxing();
                        break;
                    case "Zhengkong":
                        shaojierLuTestManlag.Couzhenkongbaozhuang();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void SetIsMayClick()
    {
        IsMayClick = true;
        GetComponent<HighlightEffect>().highlighted = true;
    }
    void SetClickColor()
    {
        transform.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material/Kongzhitai/KaiMaterial");
    }
}
