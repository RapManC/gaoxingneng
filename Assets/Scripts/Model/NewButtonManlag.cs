using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum ButtonType {
    关闭,
    停止位置,
    开始位置
}
/// <summary>
/// 新控制台物理按钮控制
/// </summary>
public class NewButtonManlag : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public bool IsClick;
    private void Awake()
    {
        meshRenderer = transform.Find("qqq:qqq:qqq:qqq:qqq:qqq:qqq:qqq:pCube18/polySurface14").GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        SetButtonType(ButtonType.关闭);
    }
    private void OnMouseDown()
    {
        if (IsClick)
        {
            IsClick = false;
            GetComponent<HighlightPlus.HighlightEffect>().highlighted = false;
            switch (name)
            {
                case "an1":
                    GetManager.Instance.SNLKongzhitai.Kaiji();
                    break;
                case "an1 (1)":
                    //NewKongzhitaiManlag._Instance.Kaiji();
                    break;
                case "an1 (2)":
                    //NewKongzhitaiManlag._Instance.Kaiji();
                    break;
                case "an1 (3)":
                    //NewKongzhitaiManlag._Instance.Kaiji();
                    break;
            }
            SetButtonType(ButtonType.开始位置);
        }
        
    }
    void SetButtonType(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.停止位置:
                meshRenderer.material = Resources.Load<Material>("NewButton_Material/Tingzhi");
                break;
            case ButtonType.关闭:
                meshRenderer.material = Resources.Load<Material>("NewButton_Material/Guanbi");
                break;
            case ButtonType.开始位置:
                meshRenderer.material = Resources.Load<Material>("NewButton_Material/Kaishi");
                break;
        }
    }
}

