using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PipeFlow : MonoBehaviour
{

    public FlowItem ItemTemplate;
    [Header("流动时间")]
    float FlowTime ;
    [Header("流体之间的间隔")]
    public float FlowInterval = 0.5f;
    [Header("是否仅仅一次")]
    public bool OnlyOnce = false;
    public List<Transform> FlowPath = new List<Transform>();
    [HideInInspector]
    public List<float> FlowSpeeds = new List<float>();
    public List<FlowItem> Items = new List<FlowItem>();
    private bool _isInit = false;
    private bool _isFlowing = false;
    private float _flowInterval = 0f;
    private Action _actionTrigger;
    private Coroutine _actionCoroutine;
    private void Start()
    {
        
    }
    private void Init()
    {
        float tatol = 0f;
        for (int i = 0; i < FlowPath.Count - 1; i++)
        {
            tatol += Vector3.Distance(FlowPath[i].position, FlowPath[i + 1].position);
        }
        for (int i = 0; i < FlowPath.Count - 1; i++)
        {
            float dis = Vector3.Distance(FlowPath[i].position, FlowPath[i + 1].position);
            float time = dis / tatol * (FlowTime * 50);
            FlowSpeeds.Add(1f / time);
        }
        _isInit = true;
    }
    private void FixedUpdate()
    {
        if (_isFlowing)
        {
            _flowInterval += Time.deltaTime;
            if (_flowInterval >= FlowInterval)
            {
                _flowInterval = 0f;
                ShootItem();
                if (OnlyOnce)
                {
                    _isFlowing = false;
                }
            }
        }
    }
    private void ShootItem()
    {
        if (Items.Count > 0)
        {
            Items[0].Shoot(this);
            Items.RemoveAt(0);
        }
        else
        {
            GameObject item = Instantiate(ItemTemplate.gameObject);
            item.transform.SetParent(transform);
            item.GetComponent<FlowItem>().Shoot(this);
            Items.Add(item.GetComponent<FlowItem>());
        }
    }
    public void Flow(float time, Action endAction)
    {
        FlowTime = time;
        Debug.Log("液体流动时间："+FlowTime);
        if (FlowPath.Count < 2)
        {
            Debug.LogWarning("路径点数量必须大于等于2！");
            return;
        }
        if (!ItemTemplate)
        {
            Debug.LogWarning("ItemTemplate不能为空！");
            return;
        }
        Init();
        _isFlowing = true;
        _flowInterval = FlowInterval;
        _actionTrigger = endAction;
        if (_actionCoroutine != null)
        {
            StopCoroutine(_actionCoroutine);
            _actionCoroutine = null;
        }
        if (_actionTrigger != null)
        {
            _actionCoroutine = StartCoroutine(DelayExecute(_actionTrigger, FlowTime));
        }
    }
    public void Stop()
    {
        _isFlowing = false;
        if (_actionCoroutine != null)
        {
            StopCoroutine(_actionCoroutine);
            _actionCoroutine = null;
        }
        FlowItem[] fis = transform.GetComponentsInChildren<FlowItem>();
        foreach (FlowItem fi in fis)
        {
            fi.Stop();
        }
    }
    public static IEnumerator DelayExecute(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}


