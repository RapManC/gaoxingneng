using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNLYuanliControl : MonoBehaviour
{
    System.Action StartAction;
    System.Action EndAction;
    public string YuanliText;
    private void Awake()
    {
        StartAction = SNLYuanli;
        EndAction = SNLJeshu;
    }
    private void OnEnable()
    {
        StartAction?.Invoke();
    }
    private void OnDisable()
    {
        EndAction?.Invoke();
    }
    void SNLYuanli()
    {
        UIManage.Instance.SetXuexiHide("正在播放" + name + "工作状态动画");
        Transform yuanliTran = GameObject.Find("YuanliModels").transform.Find("真空速凝炉");
        yuanliTran.Find("Longqi").GetComponent<Animator>().enabled = true;
        StartCoroutine(UIManage.Instance.enumerator(6f, () => { yuanliTran.transform.Find("Hezi").GetComponent<Animator>().enabled = true; }));
        StartCoroutine(UIManage.Instance.enumerator(11.6f, () =>
        {
            SetYunxingYuanliModelRot(true);
            yuanliTran.Find("Bopianfasheqi (1)").GetComponent<BopianGenerateManlag>().IsStartGenerate = true;
        }));
        StartCoroutine(UIManage.Instance.enumerator(30,() => {
            transform.Find("Longqi").GetComponent<Animator>().Play("Qingdao Animation_Dao");
            StartCoroutine(UIManage.Instance.enumerator(5.5f, () => {
                transform.Find("Poshui1").GetComponent<ModelRotControl>().isRot = false;
                transform.Find("Poshui2").GetComponent<ModelRotControl>().isRot = false;
                transform.Find("Poshui3").GetComponent<ModelRotControl>().isRot = false;
                transform.Find("Poshui2Clone").GetComponent<ModelRotControl>().isRot = false;
                transform.Find("Poshui3Clone").GetComponent<ModelRotControl>().isRot = false;
                transform.Find("Hezi").GetComponent<Animator>().Play("Hezi Animation_Dao");
            }));
        }));
        UIManage.Instance.SetModelYuanliJiesao(YuanliText);
    }
    void SNLJeshu()
    {
        SetYunxingYuanliModelRot(false);
    }
    void SetYunxingYuanliModelRot(bool b)
    {
        transform.Find("Poshui1").GetComponent<ModelRotControl>().isRot = b;
        transform.Find("Poshui2").GetComponent<ModelRotControl>().isRot = b;
        transform.Find("Poshui3").GetComponent<ModelRotControl>().isRot = b;
        transform.Find("Poshui2Clone").GetComponent<ModelRotControl>().isRot = b;
        transform.Find("Poshui3Clone").GetComponent<ModelRotControl>().isRot = b;
        BopianGenerateManlag bopianGenerate = transform.Find("Bopianfasheqi (1)").GetComponent<BopianGenerateManlag>();
        bopianGenerate.IsStartGenerate = b;
        if (b)
        {
            StartCoroutine(UIManage.Instance.enumerator(20, () =>
            {
                transform.Find("Bopianfasheqi (1)").GetComponent<BopianGenerateManlag>().IsStartGenerate = false;
            }));
        }
        if (b == false)
        {
            foreach (Transform tran in transform.Find("Bopianfasheqi (1)"))
            {
                if (tran.gameObject.activeSelf)
                {
                    Destroy(tran.gameObject);
                }
            }
            foreach (Transform tran in transform)
            {
                if(tran.tag=="Biaopian"&& tran.gameObject.activeSelf)
                Destroy(tran.gameObject);
            }
        }
    }
}
