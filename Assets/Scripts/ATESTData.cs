using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(menuName = "创建ilab网实验数据", fileName = "ATESTData")]
public class ATESTData : ScriptableObject
{
    /// <summary>用户名字</summary>
    public string username;
    /// <summary> 实验标题</summary>
    public string title;
    /// <summary>1 - 完成；2 - 未 完成  </summary>
    public int status;
    /// <summary>实验成绩，0-100分</summary>
    public int score;
    /// <summary> 开始时间戳，13位 </summary>
    public long startTime;
    /// <summary>结束时间戳，13位</summary>
    public long endTime;
    /// <summary>实验用时，单位秒 </summary>
    public int timeUsed;
    /// <summary>接入平台编号，由实验空间分配</summary>
    public int appid;
    /// <summary>实验平台实验记录 ID ，平台唯一，可以用当前时间戳+username </summary>
    public string originId;
    /// <summary>实验步骤，不能少于10步 </summary>
    public List<StepData> steps;

}


[System.Serializable]
public class StepData
{
    /// <summary>实验步骤序号 </summary>
    public int seq;
    /// <summary>步骤名称 </summary>
    public string title;
    /// <summary>实验步骤开始时间戳</summary>
    public long startTime;
    /// <summary>实验步骤结束时间戳</summary>
    public long endTime;
    /// <summary>///实验步骤用时，单位秒/// </summary>
    public int timeUsed;
    /// <summary>///实验步骤合理用时，单位秒/// </summary>
    public int expectTime;
    /// <summary>///实验步骤总分，0-100/// </summary>
    public int maxScore;
    /// <summary>///实验步骤得分/// </summary>
    public int score;
    /// <summary>///实验步骤操作次数/// </summary>
    public int repeatCount;
    /// <summary>///步骤评价/// </summary>
    public string evaluation;
    /// <summary>///赋分模型/// </summary>
    public string scoringModel;
    /// <summary>///备注/// </summary>
    public string remarks;
    /// <summary>///计算时间合理用时/// </summary>
    public void Init()
    {
        timeUsed = (int)(endTime - startTime) / 1000;
        if (timeUsed == 0)
        {
            timeUsed = 1;
        }
    }

}