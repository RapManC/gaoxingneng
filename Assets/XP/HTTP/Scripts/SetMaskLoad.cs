using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SetMaskLoad : MonoBehaviour
{
    public enum LoadState
    {
        Load,
        Error,
        Win
    }
    public class LoadData
    {
        public Action Action;
        public float time;
    }
    private float startRequestTime = 0;

    private float loadTime = 0;
    private GameObject BG;
    private Action finishReqestEvent;
    private Queue<LoadData> AllActionQ;
    public static SetMaskLoad Instance { get; set; }
    public Text ConentText { get; set; }
    public Button Button { get; set; }
    public GameObject StateObj { get; set; }

    void Awake()
    {
        Instance = this;
        BG = transform.Find("BG").gameObject;
        ConentText = BG.transform.Find("Plane/Text").GetComponent<Text>();
        Button = BG.transform.Find("Plane/Button").GetComponent<Button>();
        StateObj = BG.transform.Find("Plane/State").gameObject;
        AllActionQ = new Queue<LoadData>();
    }
    // Update is called once per frame
    void Update()
    {
        if ( AllActionQ.Count > 0)
        {
            if (Time.time - startRequestTime > loadTime)
            {
               var  data = AllActionQ.Dequeue();
                startRequestTime = Time.time;
                loadTime = data.time;
                data.Action?.Invoke();
            }
        }
    }

    private Tweener loopT;
    public void StartMaskText(string str, bool isButton = false, Action onClick = null, LoadState loadState = LoadState.Load, float defaultLoadTime = 3,float defaultCloseTime = 0.1f, Action<GameObject> closeEvent = null)
    {
        LoadData data = new LoadData();
        data.Action = () =>
        {
            Set(str,defaultLoadTime, isButton, onClick, loadState);
        };
        data.time = defaultLoadTime;
        AllActionQ.Enqueue(data);
        if (closeEvent != null)
        {
            LoadData data1 = new LoadData();
            data1.Action = () =>
            {
                closeEvent?.Invoke(BG.gameObject);
            };
            data1.time = defaultCloseTime;
            AllActionQ.Enqueue(data1);
        }
    }
    void Set(string str, float defaultloadtime, bool isButton = false, Action onClick = null, LoadState loadState = LoadState.Load)
    {
        if (ConentText.text != str)
        {
            if (loopT != null)
                loopT.Kill();
            if (defaultloadtime < 1)
                ConentText.text = str;
            else
            {
                ConentText.text = "";
                loopT = ConentText.DOText(str, str.Length / 5f).SetEase(Ease.Linear).SetLoops(-1);
            }
            
        }
        SetStateObj(loadState);
        BG.gameObject.SetActive(true);
        Button.gameObject.SetActive(isButton);
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => { BG.gameObject.SetActive(false); startRequestTime = -100; onClick?.Invoke(); });
        
    }

    void SetStateObj(LoadState loadState)
    {
        foreach (Transform tran in StateObj.transform)
        {
           // Debug.Log(string.Format("子节点的名字={0}，枚举的名字={1}",tran.name,loadState.ToString()));
            tran.gameObject.SetActive(loadState.ToString() == tran.name);
        }
    }
}
