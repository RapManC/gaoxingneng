using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HighlightPlus;

public class KaiguanManlag : MonoBehaviour
{
    public KaiguanButtonType ButtonType;
    public bool IsStart;


    Quaternion quaternion;
    Material material;
    private void Awake()
    {
        quaternion = this.transform.rotation;
        material = this.GetComponent<MeshRenderer>().material;
        this.GetComponent<BoxCollider>().enabled = false;
    }
    private void OnMouseDown()
    {
        OnClick();
    }
    public void OnClick()
    {
        if (!IsStart)
        {
            switch (this.name)
            {
                case "开盖启动":
                    ShuningruTestManlag.Instance.KaigaiQidong();
                    this.GetComponent<BoxCollider>().enabled = false;
                    break;
                case "合盖启动":
                    ShuningruTestManlag.Instance.HegaiQidong();
                    break;
                case "加热启动":
                    ShuningruTestManlag.Instance.JaireQidong();
                    break;
                case "加热停止":
                    ShuningruTestManlag.Instance.JaireStop();
                    break;
                case "包加热开关":
                    ShuningruTestManlag.Instance.Baojiarekaiguan();
                    break;
                case "自动浇筑":
                    ShuningruTestManlag.Instance.Zhidongjiaozhu();
                    break;
                case "自动浇筑暂停":
                    ShuningruTestManlag.Instance.ZhidongjiaozhuStop();
                    break;
                case "充氢气启动":
                    ShuningruTestManlag.Instance.Congqingqiqidong();
                    break;
                case "破碎启动":
                    //ShuningruTestManlag.Instance.Poshuiqidong();
                    break;
                case "破碎停止":
                    ShuningruTestManlag.Instance.PoshuiStop();
                    break;
                case "风冷启动":
                    ShuningruTestManlag.Instance.Fenglenqidong();
                    break;
                case "风冷停止":
                    ShuningruTestManlag.Instance.FenglenStop();
                    break;
                case "气动板阀开关":
                    ShuningruTestManlag.Instance.Qidongban();
                    break;
            }

            switch (ButtonType)
            {
                case KaiguanButtonType.上部红色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/上部分/上部红色按键开启");
                    break;
                case KaiguanButtonType.上部绿色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/上部分/上部绿色按键开启");
                    break;
                case KaiguanButtonType.底部红色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/下部分/底部红色按键顶部开启");
                    break;
                case KaiguanButtonType.底部绿色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/下部分/底部绿色按键顶部开启");
                    break;
                case KaiguanButtonType.旋钮:
                    transform.DORotate(new Vector3(0, -90, 0), 0.5f);
                    break;
            }
            IsStart = true;
            ShuningruTestManlag.Instance.KaigaunKuangtai(this.gameObject, false);
        }
        else
        {
            switch (ButtonType)
            {
                case KaiguanButtonType.上部红色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/上部分/上部红色按键关闭");
                    break;
                case KaiguanButtonType.上部绿色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/上部分/上部绿色按键关闭");
                    break;
                case KaiguanButtonType.底部红色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/下部分/底部红色按键顶部关闭");
                    break;
                case KaiguanButtonType.底部绿色按键:
                    SetMdoleMaterial.Instance.SetMaterial(this.gameObject, "Button_Material/下部分/底部绿色按键顶部关闭");
                    break;
                case KaiguanButtonType.旋钮:
                    transform.DORotate(new Vector3(0,-180f,0), 0.5f);
                    break;
            }
            IsStart = false;
        }
    }
    public void KaiguanReset()
    {
        IsStart = false;
        transform.rotation = quaternion;
        this.GetComponent<MeshRenderer>().material = material;
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<HighlightEffect>().highlighted = false;
    }
}
