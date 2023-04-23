using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class haibao : MonoBehaviour
{
    public GameObject pageObj;
    public Button ColseBtn;

    private Action OnClickEndAction;
    public void OnMouseDown()
    {
        pageObj.SetActive(true);
    }

    public void AddClickAction(Action onClick)
    {
        OnClickEndAction = onClick;
        ColseBtn.onClick.AddListener(OnClickEnd);
    }

    private void OnClickEnd()
    {
        OnClickEndAction?.Invoke();
        OnClickEndAction = null;
        ColseBtn.onClick.RemoveListener(OnClickEnd);
    }


    private void Start()
    {
    }
}
