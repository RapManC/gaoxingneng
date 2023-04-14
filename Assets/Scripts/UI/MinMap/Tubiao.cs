using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]
public class Tubiao : MonoBehaviour
{
    public MiniMapImageData ImageData;
    public GameObject TargetGO;
    Image image;
    GameObject textBG;
    Text text;

    string showScopeUIname= "Null";
    private void Awake()
    {
        image = GetComponent<Image>();
        textBG = transform.Find("TextBG").gameObject;
        text = transform.Find("Text").GetComponent<Text>();
        Init();
    }
    void Init()
    {
        switch (ImageData.Name)
        {
            case "真空速凝炉工段":
                showScopeUIname = "SNL";
                break;
            case "氢破炉工段":
                showScopeUIname = "QPL";
                break;
            case "气流磨工段":
                showScopeUIname = "QLM";
                break;
            case "取向成型压机工段":
                showScopeUIname = "QXCX";
                break;
            case "真空烧结工段":
                showScopeUIname = "SJL";
                break;
            case "二次烧结":
                showScopeUIname = "HH";
                break;
            case "磁性能检测":
                showScopeUIname = "JC";
                break;
            default:
                showScopeUIname = "Null";
                break;
        }
    } 
    private void Start()
    {
        if (ImageData.SpritePath!=null)
        {
            Sprite sprite = Resources.Load<Sprite>(ImageData.SpritePath);
            if (sprite)
            {
                image.sprite = sprite;
                image.color = Color.white;
            }
            else
                Debug.Log("未找到小地图图标：" + ImageData.SpritePath);
        }
        if (ImageData.IsShowName)
        {
            textBG.SetActive(true);
            text.text = ImageData.Name;
        }
        else
        {
            textBG.SetActive(false);
            text.gameObject.SetActive(false);
        }
        if (ImageData.UpdateTarget == GetManager.Instance.Root.Find("Player").gameObject)
        {
            (transform as RectTransform).sizeDelta = new Vector2(40, 40);
        }
        TargetGO = ImageData.UpdateTarget;
        UpdatePos();
    }
    /// <summary>
    /// 更新位置
    /// </summary>
    public void UpdatePos()
    {
        Vector2 pos = MinMapManager.Insteance.GetMinMapRectPos(ImageData.UpdateTarget);
        (transform as RectTransform).anchoredPosition = pos;
    }
    /// <summary>
    /// 显示工段范围
    /// </summary>
    public void ShowScope()
    {
        if(GetManager.Instance.Canvas.Find("Shiyan_UI/MinMap_UI/MaxMap").gameObject.activeSelf)
        transform.parent.parent.Find(showScopeUIname).gameObject.SetActive(true);
    }
    public void HideScope()
    {
        if (GetManager.Instance.Canvas.Find("Shiyan_UI/MinMap_UI/MaxMap").gameObject.activeSelf)
            transform.parent.parent.Find(showScopeUIname).gameObject.SetActive(false);
    }
}
