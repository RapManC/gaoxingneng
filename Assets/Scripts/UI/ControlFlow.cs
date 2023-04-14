using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ButtonState
{
    未完成,
    进行中,
    已完成
}


public class ControlFlow : MonoBehaviour
{
    public List<FlowButtonControl> ButtonList = new List<FlowButtonControl>();
    int nowIndex;
    GameObject agoProgressImageGO;

    private void Awake()
    {
        foreach(Transform tran in this.transform.Find("Scroll View/Viewport/Content"))
        {
            if (tran.GetComponent<FlowButtonControl>() != null)
            {
                ButtonList.Add(tran.GetComponent<FlowButtonControl>());
            }
        }
    }
    private void Start()
    {
        Jingxingzong(0);
        foreach (FlowButtonControl temp in ButtonList)
        {
            if (ButtonList.IndexOf(temp) >0 )
                temp.UpdateState(ButtonState.未完成);
        }
    }
    /// <summary>
    /// 点击实验进度
    /// </summary>
    /// <param name="index"></param>
    public void Jingxingzong(int index)
    {
        UIManage.Instance.SetBiaoBiaoGlint();
        DataManager.Instance.SetFinallyTest((GongduanType)(index-2));
        foreach (FlowButtonControl temp in ButtonList)
        {
            if(ButtonList.IndexOf(temp) <= (int)DataManager.Instance.FinallyTest+2)
            {
                temp.UpdateState(ButtonState.已完成);
            }
            if (ButtonList.IndexOf(temp) > (int)DataManager.Instance.FinallyTest + 2)
            {
                temp.UpdateState(ButtonState.未完成);
            }
            if (ButtonList.IndexOf(temp) == index)
            {
                temp.UpdateState(ButtonState.进行中);
            }
            nowIndex = ButtonList[index].transform.GetSiblingIndex();
        }
        if (index == 1)
        {
            transform.Find("Scroll View/Viewport/Content/3").GetComponent<FlowButtonControl>().UpdateState(ButtonState.未完成);
        }
    }
    public void Yiwanchen(int index)
    {
        foreach (FlowButtonControl temp in ButtonList)
        {
            if (ButtonList.IndexOf(temp) == index)
            {
                temp.UpdateState(ButtonState.已完成);
            }
        }
    }
    public void SetTestProgress(int progressIndex)
    {
        Transform content = transform.Find("Scroll View/Viewport/Content");
        Image image = content.GetChild(nowIndex+1).GetChild(progressIndex).GetComponent<Image>();
        if (agoProgressImageGO)
        {
            agoProgressImageGO.GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/次级背景");
        }
        image.sprite = Resources.Load<Sprite>("ButtonImage/次级进行中背景");
        agoProgressImageGO = image.gameObject;
    }
}

        //void RestAllImage()
        //{
        //    foreach (Transform tran in content)
        //    {
        //        string name = tran.name;
        //        Debug.Log(name.Remove(0, name.Length - 3));
        //        if (name.Remove(0, name.Length - 3) == "UI")
        //        {
        //            foreach (Transform tran2 in tran)
        //            {
        //                tran2.GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/次级背景");
        //            }
        //        }
        //    }