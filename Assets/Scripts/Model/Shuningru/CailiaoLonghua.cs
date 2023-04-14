using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CailiaoLonghua : MonoBehaviour
{
    public bool isJiare;
    public float JiareTime;
    public float LonghuaTime;

    private float ColorVarySpeed { get { return 255 / JiareTime; } }
    public List<GameObject> CailiaoList = new List<GameObject>();
    private float color_r;
    GameObject yemian;
    private void Awake()
    {
        yemian = transform.parent.Find("ShuningLu/Longqi/Yemian/zhuti").gameObject;
    }
    private void Start()
    {
        foreach (Transform temp in transform)
        {
            CailiaoList.Add(temp.gameObject);
            temp.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (isJiare)
        {
            color_r += ColorVarySpeed * Time.deltaTime;
            color_r = Mathf.Clamp(color_r, 0, 255);
            foreach (GameObject temp in CailiaoList)
            {
                temp.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(color_r, 0, 0) / 255);
            }
            if (color_r == 255)
            {
                Debug.Log("开始溶解");
                Yemiansanshen();
                isJiare = false;
                foreach (GameObject temp in CailiaoList)
                {
                    temp.transform.DOScaleY(0, LonghuaTime).OnComplete(() => { temp.SetActive(false); });
                }
            }
        }
    }
    /// <summary>
    /// 液面上升
    /// </summary>
    public void Yemiansanshen()
    {
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yemian.SetActive(true);
        yemian.transform.DOScaleY(0.13f, LonghuaTime);
        yield return new WaitForSecondsRealtime(LonghuaTime);
        Debug.Log("融化完成");
    }
}
