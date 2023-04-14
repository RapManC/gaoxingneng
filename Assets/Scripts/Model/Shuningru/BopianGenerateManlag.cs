using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BopianGenerateManlag : MonoBehaviour
{
    public bool IsStartGenerate;
    public float GenerateSpeed;
    public List<GameObject> BiaopianList = new List<GameObject>();

    float timeWait;
    private void Awake()
    {
        foreach(Transform tran in transform)
        {
            BiaopianList.Add(tran.gameObject);
        }
    }

    private void Update()
    {
        if (IsStartGenerate)
        {
            timeWait += Time.deltaTime;
            if (timeWait >= 1 / GenerateSpeed)
            {
                int index= Random.Range(0, BiaopianList.Count);
                Instantiate(BiaopianList[index], this.transform).SetActive(true);
                timeWait = 0;
                //if(transform.parent.name== "ShuningLuParent")
            }
        }
    }
    public void StartGenerate()
    {
        IsStartGenerate = true;
    }
    public void SpotGenerate()
    {
        IsStartGenerate = false;
    }
}
