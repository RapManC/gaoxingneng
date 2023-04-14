using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
[RequireComponent(typeof(BoxCollider))]

public class JiancheModelClickControl : MonoBehaviour
{
    HighlightEffect highlightEffect;
    bool isClick;
    JiancheTestManlag jiancheTestManlag { get {return GetManager.Instance.InteractiveModel.Find("Jianche").GetComponent<JiancheTestManlag>();} }
    private void Awake()
    {
        highlightEffect = GetComponent<HighlightEffect>();
    }
    private void OnMouseDown()
    {
        if (isClick)
        {
            isClick = false;
            highlightEffect.highlighted = false;
            switch (name)
            {
                case "Lanzi":
                    jiancheTestManlag.Jialiao();
                    break;
                case "Famengan":
                    jiancheTestManlag.Xuanzhuanfamen();
                    break;
                case "Kiaguan":
                    jiancheTestManlag.Kaiqishebei();
                    break;
                case "Diannao_pingmu":
                    jiancheTestManlag.Huodejieguo();
                    break;

            }
        }
    }

    public void SetClick()
    {
        isClick = true;
        highlightEffect.highlighted = true;
    }
}
