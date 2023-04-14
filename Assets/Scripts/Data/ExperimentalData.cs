using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StepType
{
    选择材料牌号,
    元素成分决断,
    配置稀土元素质量,
    真空速凝炉调节加热温度,
    真空速凝炉调节转速,
    真空速凝炉薄片厚度,
    真空速凝炉生产能耗,
    氢破炉调节反应温度,
    氢破炉调节反应压力,
    氢破炉调节破碎时间,
    氢破炉生产能耗,
    靶式气流磨调节破碎压力,
    靶式气流磨调节分选轮转速,
    靶式气流磨生产能耗,
    取向成型调节压制压力,
    取向成型调节磁场强度,
    取向成型生产能耗,
    烧结炉调节烧结真空度,
    烧结炉调节烧结温度,
    烧结炉调节烧结时间,
    烧结炉生产能耗,
    回火工段调节一级回火温度,
    回火工段调节一级回火时间,
    回火工段调节二级回火温度,
    回火工段调节二级回火时间,
    回火工段生产能耗,
    性能检测
}
[Serializable]
public class ExperimentalData
{
    public string username;//实验空间用户账号
    public string title;//用户学习的实验名称
    public int status;//1 - 完成；2 - 未完成
    public int score;//实验成绩
    public long startTime;//13 位时间戳
    public long endTime;//13 位时间戳
    public int timeUsed;//实验用时，单位：秒
    public int appid;//接入平台编号
    public string originId;//实验平台实验记录 ID
    public List<Step> steps;//实验步骤;
}
[Serializable]
public class Step
{
    public int seq;//步骤序号
    public string title;//步骤名称
    public long startTime
    {
        get
        {
            return StartTime;
        }
        set
        {
            if (startTime == 0)
            {
                StartTime = value;
            }
        }
    }//实验步骤开始时间，13位时间戳
    private long StartTime;
    public long endTime
    {
        get
        {
            return EndTime;
        }
        set
        {
            if (endTime == 0)
            {
                EndTime = value;
            }
        }
    }//实验步骤结束时间，13位时间戳
    private long EndTime;
    public int timeUsed;//实验步骤用时
    public int expectTime=30;//实验步骤合理用时
    public int maxScore;//实验步骤满分 0 ~100，百分制
    public int score;//实验步骤得分 0 ~100，百分制
    public int repeatCount;//实验步骤操作次数
    public string evaluation;//实验步骤评价
    public string scoringModel;//赋分模型
    public string remarks;//备注

    public Step(StepType stepType)
    {
        title = stepType.ToString();
    }
}

