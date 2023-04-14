using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QPLYuanliControl : MonoBehaviour
{
    System.Action StartAction;
    System.Action EndAction;

    float QPRotSpeed =15;
    bool IsQPRot;
    Tween QPLight1Tween;
    Tween QPLight2Tween;
    Transform Guntong;
    List<GameObject> biaopianList = new List<GameObject>();
    float time;
    int xiaoshiSpeed;
    public string YuanliText;
    private void Awake()
    {
        StartAction = QPYuanli;
        EndAction = QPJeshu;
    }
    private void OnEnable()
    {
        biaopianList.Clear();
        Guntong = transform.Find("Qingporu_touming/GunrunPather/Guntong");
        foreach (Transform baopian in Guntong.Find("Baopiao"))
        {
            biaopianList.Add(baopian.gameObject);
            baopian.gameObject.SetActive(true);
        }
        xiaoshiSpeed = (biaopianList.Count / 20);
        StartAction?.Invoke();
        UIManage.Instance.SetModelYuanliJiesao(YuanliText);
        Material material = Resources.Load<Material>("213309cwekez2ke9yx6pu6");
        material.SetColor("_Color", new Color(113, 30, 30, 255) / 255);
    }
    private void FixedUpdate()
    {
        if (IsQPRot)
        {
            Guntong.Rotate(Vector3.up, QPRotSpeed);
            time += Time.deltaTime;
            if (time >= 1)
            {
                if (biaopianList.Count > 0)
                {
                    time = 0;
                    for (int i = 0; i <= xiaoshiSpeed; i++)
                    {
                        int y = Random.Range(0, biaopianList.Count);
                        biaopianList[y].SetActive(false);
                        biaopianList.Remove(biaopianList[y]);
                    }
                }
            }
        }
    }
    private void OnDisable()
    {
        EndAction?.Invoke();
    }
    /// <summary>
    /// 氢破原理
    /// </summary>
    void QPYuanli()
    {
        UIManage.Instance.SetXuexiHide("正在播放" + name + "工作状态动画");
        transform.Find("Qilius/Jiaya").GetComponent<PipeFlow>().Flow(4, null);
        if (gameObject.activeSelf)
        {
            StartCoroutine(UIManage.Instance.enumerator(10, () =>
            {
                transform.Find("Qilius/Jiaya").GetComponent<PipeFlow>().Stop();
                Shengwen();
            }));
            //升温
            void Shengwen()
            {
                QPLight1Tween=transform.Find("Lights/Light1").GetComponent<Light>().DOIntensity(4, 7);
                QPLight2Tween=transform.Find("Lights/Light2").GetComponent<Light>().DOIntensity(4, 7).OnComplete(() =>
                {
                    IsQPRot = true;
                    time = 0;
                    Guntong.Find("PoshuiTexiao").gameObject.SetActive(true);
                    
                });
            }
            StartCoroutine(UIManage.Instance.enumerator(42, () => {
                IsQPRot = false;
                QPLight1Tween = transform.Find("Lights/Light1").GetComponent<Light>().DOIntensity(0, 7);
                QPLight2Tween = transform.Find("Lights/Light2").GetComponent<Light>().DOIntensity(0, 7).OnComplete(() =>
                {
                    Material material = Resources.Load<Material>("213309cwekez2ke9yx6pu6");
                    material.SetColor("_Color", Color.white);
                });
            }));
        }
    }
    /// <summary>
    /// 氢破结束
    /// </summary>
    void QPJeshu()
    {
        QPLight1Tween.Kill();
        QPLight2Tween.Kill();
        transform.Find("Lights/Light1").GetComponent<Light>().intensity = 0;
        transform.Find("Lights/Light2").GetComponent<Light>().intensity = 0;
        transform.Find("Qilius/Jiaya").GetComponent<PipeFlow>().Stop();
        IsQPRot = false;
        transform.Find("Qingporu_touming/GunrunPather/Guntong/PoshuiTexiao").gameObject.SetActive(false);
    }
}
