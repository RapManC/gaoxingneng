using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class HuihuoModelClick : MonoBehaviour
{
     bool IsClick;
    public bool IsZuangliao;
    private void OnMouseDown()
    {
        if (IsClick)
        {
            MainSceneGuide.Instance.StopAutoMove();
            IsClick = false;
            GetComponent<HighlightEffect>().highlighted = false;
            if (!IsZuangliao)
                GetManager.Instance.InteractiveModel.Find("HuihuoParent").GetComponent<HuihuoTestManlag>().Shangliao();
            else
                GetManager.Instance.InteractiveModel.Find("HuihuoParent").GetComponent<HuihuoTestManlag>().Zhuangyun();
        }
    }
    public void SetModelClick()
    {
        IsClick = true;
        GetComponent<HighlightEffect>().highlighted=true;
    }
}
