using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickCaibaozhuang : MonoBehaviour
{
    bool isClick;
    bool IsMayClick;
    ShaojierLuTestManlag shaojierLuTestManlag { get { return GetManager.Instance.InteractiveModel.Find("ShaojierLuParent").GetComponent<ShaojierLuTestManlag>(); } }
    private void OnMouseDown()
    {
        if (IsMayClick)
        {
            if (!isClick)
            {
                isClick = true;
                GetComponent<HighlightEffect>().highlighted = false;
                Caibaozhuang();
            }
        }
    }
    public void SetIsMayClick()
    {
        IsMayClick = true;
        GetComponent<HighlightEffect>().highlighted = true;
    }
    void Caibaozhuang()
    {
        Transform daizhi = transform.Find("DaiZi");
        Transform zhuoDaizi = daizhi.Find("polySurface220");
        Transform youDaizi = daizhi.Find("polySurface221");
        zhuoDaizi.DOLocalMoveX(0.0045f, 1.5f);
        youDaizi.DOLocalMoveX(-0.0045f, 1.5f);
        StartCoroutine(UIManage.Instance.enumerator(3, () =>
        {
            zhuoDaizi.gameObject.SetActive(false);
            youDaizi.gameObject.SetActive(false);
        }));


        shaojierLuTestManlag.CaibaozhuanNum++;
        if (shaojierLuTestManlag.CaibaozhuanNum == 6)
        {
            StartCoroutine(UIManage.Instance.enumerator(4, () =>
            {
                shaojierLuTestManlag.CaibaoEnd();
            }));
        }
    }
}
