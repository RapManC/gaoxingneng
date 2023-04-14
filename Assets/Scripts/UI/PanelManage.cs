using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManage : MonoBehaviour
{
    public List<Toggle> toggleList = new List<Toggle>();
    public int CurveCorrectOption;
    public int OrrectOption;
    public GameObject ConfirmButton;

    public List<GameObject> PanelList=new List<GameObject>();
    private void Awake()
    {
        foreach (Toggle time in toggleList)//toggle按键添加事件
        {
            time.onValueChanged.AddListener((bool value) =>
            {
                CurveCorrectOption = toggleList.IndexOf(time);
                if (this.name == "SelectPanel_UI")
                    UIManage.Instance.ShowXingnen(CurveCorrectOption);
            });
        }
        //提交按键
        ConfirmButton.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                if(this.name== "SelectPanel_UI")
                {
                    DataManager.Instance.PaihaoXaunzhe = CurveCorrectOption;
                    Step step = ScoreManager._Instance.GetStep(StepType.选择材料牌号);
                    step.endTime = ScoreManager.GetCurveTimeLong();
                    step.score = 100;
                    step.repeatCount++;

                    Step step2 = ScoreManager._Instance.GetStep(StepType.元素成分决断);
                    step2.startTime = ScoreManager.GetCurveTimeLong();
                    ParameterData parameterData = new ParameterData(link.牌号, "选择材料牌号", DataManager.Instance.GetPaihao(), 2, 2, "开始选择牌号时间", "结束选择牌号时间");
                }
            });

        
    }
    private void Start()
    {
        if (this.name == "SelectPanel_UI")
            UIManage.Instance.ShowXingnen(CurveCorrectOption);
    }
}
