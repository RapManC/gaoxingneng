using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class UIButtonHide : EventTrigger
{
    Button button;
    EventTrigger m_Trigger;
    private EventTrigger.Entry m_pointerEnter = new EventTrigger.Entry();//先定义一个触发事件


    private EventTrigger.Entry m_pointerExit = new EventTrigger.Entry();//先定义一个触发事件

    private void Awake()
    {
        m_Trigger = this.GetComponent<EventTrigger>();
        button = this.GetComponent<Button>();


        m_pointerEnter.eventID = EventTriggerType.PointerEnter;//将刚才定义的事件绑定类型，什么时候触发。
        m_pointerEnter.callback.AddListener(OnShowHint);//将委托传递到事件类型中去
        m_Trigger.triggers.Add(m_pointerEnter);//再将事件添加到EventTrigger组件中去

        m_pointerExit.eventID = EventTriggerType.PointerExit;//将刚才定义的事件绑定类型，什么时候触发。
        m_pointerExit.callback.AddListener(OnHideHint);//将委托传递到事件类型中去
        m_Trigger.triggers.Add(m_pointerExit);//再将事件添加到EventTrigger组件中去
    }

    private void OnShowHint(BaseEventData arg0)
    {
        if (!button.interactable)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void OnHideHint(BaseEventData arg0)
    {

        if (!button.interactable)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
