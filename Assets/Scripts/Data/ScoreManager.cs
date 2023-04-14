 using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public enum link 
{
    牌号,
    稀土成分,
    成分配置,
    真空速凝炉工段,
    氢破炉工段,
    靶式气流磨工段,
    取向成型工段,
    烧结工段,
    回火工段,
    性能检测
}
public class ScoreManager:BaseMonoBehaviour<ScoreManager>
{
    /// <summary>
    /// 传给后台的报表
    /// </summary>
    public TESTData Data;
    public int _experimentScore;
    public int _OptionScore;
    public int _sumScore { get { return (int)(_experimentScore * 0.9 + _OptionScore * 0.1); } }
    public List<Score> scores = new List<Score>();
    /// <summary>
    /// 所有小步骤信息
    /// </summary>
    public List<ParameterData> AllParameterDataList = new List<ParameterData>();
    /// <summary>
    /// 传给网址的旧报表,作为中间媒介
    /// </summary>
    public ExperimentalData _experimentalData = new ExperimentalData();

    /// <summary>
    /// 传给Ialb网站的数据
    /// </summary>
    public ATESTData ATESTData;

    private new void Awake()
    {
        base.Awake();
        for (int i = 0; i <= (int)StepType.性能检测; i++)
        {
            Step step = new Step((StepType)i);
            step.seq = i + 1;
            step.maxScore = 100;
            _experimentalData.steps.Add(step);
        }
        //string json = File.ReadAllText(Application.dataPath + "/Resources/Data_Json");
        TextAsset text = Resources.Load<TextAsset>("Data_Json");
        string json = text.text;
        Data = JsonUtility.FromJson<TESTData>(json);
        //string json = File.ReadAllText(Application.dataPath + "/Resources/Josn1");
        //Data = JsonUtility.FromJson<TESTData>(json);
        for (int i = 0; i <= (int)link.性能检测; i++)
        {
            scores.Add(new Score((link)i));
        }
    }
   
    public Step GetStep(StepType stepType)
    {
        foreach(Step step in _experimentalData.steps)
        {
            if (step.title == stepType.ToString())
            {
                return step;
            }
        }
        Debug.Log("没有找到这个步骤的信息记录");
        return null;
    }
    [ContextMenu("设置ID")]
    void SetID()
    {
        Data.JsonPlayerData.SetID();
    }
    [ContextMenu("保存Data到Josn")]
    void ConserveData()
    {
        File.WriteAllText(Application.dataPath + "/Resources/Data_Json.txt", JsonUtility.ToJson(Data));
    }
    /// <summary>
    /// 映射最新的传给后台的实验数据
    /// </summary>
    public void MapNewIalbData()
    {
        ScoreManager._Instance.MapScore();
        ATESTData.status = 1;
        ATESTData.score = _experimentalData.score;
        ATESTData.startTime = _experimentalData.startTime;
        ATESTData.endTime = GetCurveTimeLong();
        ATESTData.timeUsed = (int)(ATESTData.endTime - ATESTData.startTime) /1000;
        ATESTData.steps.Clear();
        for(int i = 0; i < _experimentalData.steps.Count; i++)
        {
            Step step = _experimentalData.steps[i];
            StepData newStep = new StepData();
            newStep.seq = step.seq;
            newStep.title = step.title;
            newStep.startTime =step.startTime;
            newStep.endTime =step.endTime;
            newStep.expectTime = 60;//合理用时
            newStep.maxScore = step.maxScore;
            newStep.score = step.score;
            newStep.repeatCount = step.repeatCount;
            newStep.Init();

            //newStep.score = AllParameterDataList[i]._score / AllParameterDataList[i]._initialScore * newStep.maxScore;
            float scoreRatio = (float)newStep.score / newStep.maxScore;
            string evaluation;
            if (scoreRatio == 1)
            {
                evaluation = "优";
            }
            else if (scoreRatio > 0.5f)
            {
                evaluation = "良";
            }
            else
            {
                evaluation = "不合格";
            }
            newStep.evaluation = evaluation;
            newStep.scoringModel = "赋分模型";
            newStep.remarks = "";
            ATESTData.steps.Add(newStep);
        }
        SetRemaarkFrontList();
        SetATESTData();
    } 
    void SetRemaarkFrontList()
    {
        HTTP hTTP = GetComponent<HTTP>();
        for (int i = 0; i < scores.Count; i++)
        {
            Score treeObj = scores[i];
            foreach(var v in treeObj.ParameterDataList)
            {
                hTTP.RemaarkFrontList.Add(v._value);
            }
        }
    }

    void SetATESTData()
    {
        HTTP hTTP = GetComponent<HTTP>();
        for (int i = 0; i < ATESTData.steps.Count; i++)
        {
            ATESTData.steps[i].scoringModel = hTTP.ScoringModelList[i];
            ATESTData.steps[i].remarks = hTTP.RemaarkFrontList[i];
        }
    }
    /// <summary>
    /// 映射分数表
    /// </summary>
    public void MapScore()
    {
        UIManage.Instance.UpDateScore_UI();
        _experimentalData.endTime = ScoreManager.GetCurveTimeLong();
        _experimentalData.score = ScoreManager._Instance._sumScore;
        for (int i = 0; i < scores.Count; i++)
        {
            TreeObj treeObj = Data.JsonPlayerData.ChildrenList[i];
            Score score = scores[i];
            if (score.ParameterDataList.Count == 1)
            {
                Map(treeObj, score.ParameterDataList[0]);
            }
            else
            {
                for (int y = 0; y < score.ParameterDataList.Count; y++)
                {
                    TreeObj treeObj2 = Data.JsonPlayerData.ChildrenList[i].ChildrenList[y];
                    Map(treeObj2, score.ParameterDataList[y]);
                }
            }
        }
        void Map(TreeObj treeObj, ParameterData data)
        {
            if (data._link != "牌号" || data._link != "稀土成分" || data._link != "成分配置")
                treeObj.Name = data._link + data._name;
            else
                treeObj.Name = data._name;
            treeObj.Value = data._value;
            treeObj.GrossScore = data._initialScore;
            treeObj.TestScore = data._score;
            treeObj.BinginTime = data._beginTime;
            treeObj.EndTime = data._endTime;
            treeObj.OperationState = 0;
        }
        for (int i = 0; i < Data.JsonPlayerData.ChildrenList.Count; i++)
        {
            TreeObj treeObj = Data.JsonPlayerData.ChildrenList[i];
            treeObj.OperationState = 0;

            if (treeObj.ChildrenList.Count > 1)
            {
                treeObj.SetTESTScore();
                treeObj.SetGrossScore();
                treeObj.BinginTime = treeObj.ChildrenList[treeObj.ChildrenList.Count - 1].BinginTime;
                treeObj.EndTime = treeObj.ChildrenList[treeObj.ChildrenList.Count - 1].EndTime;
            }
        }
    }

    /// <summary>
    /// 得到lIab网对应实验数据（需要在映射玩后才能查询）
    /// </summary>
    /// <param name="procedureName"></param>
    /// <returns></returns>
    public StepData GetIlabStepData(string procedureName)
    {
        foreach (StepData step in ATESTData.steps)
        {
            if (step.title == procedureName)
            {
                return step;
            }
        }
        return null;
    }
    long StringTimeToLong(string time)
    {
        long longTime;
        long.TryParse(time,out longTime);
        return longTime;
    }

    /// <summary>
    /// 传递时间戳
    /// </summary>
    /// <param name="_startTime"></param>
    /// <param name="_endTiem"></param>
    /// <returns></returns>
    public int GetTimeDifference(string _startTime, string _endTiem)
    {
        DateTime dateTime1 = ConvertLongToDateTime(_startTime);
        DateTime dateTime2 = ConvertLongToDateTime(_endTiem);
        var v = dateTime2-dateTime1;
        return v.Seconds;
    }
    /// <summary>
    /// DateTime转化为13位时间戳
    /// </summary>
    /// <param name="_utcTime"></param>
    /// <returns></returns>
    public string ConvertDateTimeToLong(DateTime time)
    {
        DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        long t = (time.Ticks- startTime.Ticks) / 10000;
        return t.ToString();
    }
    /// <summary>
    /// 得到当前时间戳
    /// </summary>
    /// <param name="_utcTime"></param>
    /// <returns></returns>
    public static long GetCurveTimeLong()
    {
        var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        var timestamp = ((long)(DateTime.Now - startTime).TotalMilliseconds);
        return timestamp;
    }

    /// <summary>
    /// 13位时间戳转化位DateTime
    /// </summary>
    /// <param name="_time"></param>
    /// <returns></returns>
    public DateTime ConvertLongToDateTime(string timeStamp)
    {
        long timestamp = long.TryParse(timeStamp, out long result) ? result : 0;
        var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return startTime.AddMilliseconds(timestamp);
    }

    /// <summary>
    /// 得到当前工段的能量损耗
    /// </summary>
    /// <param name="link"></param>
    /// <param name="_energyValue"></param>
    /// <returns></returns>
    public int GetNowlinkDeductEnergyScore(link link, out string _energyValue)
    {
        float _energyValue_f = 1;
        foreach (ParameterControl parameter in UIManage_3D._Instance.ParameterControlList)
        {
            if (parameter.Gongduan == (GongduanType)(link - 2))
            {
                if (parameter.IsEnergy)
                {
                    _energyValue_f += (parameter.NowValue - (parameter.MinValue + (parameter.MaxValue - parameter.MinValue) / 2)) / (parameter.MinValue + (parameter.MaxValue - parameter.MinValue) / 2);
                }
            }
        }
        int _deductScore = 0;
        if (_energyValue_f > 1 && _energyValue_f <= 1.5)
            _deductScore = 0;
        else if(_energyValue_f>1.5 && _energyValue_f <= 3)
            _deductScore = 1;
        else if(_energyValue_f > 3)
            _deductScore = 2;
        _energyValue = (_energyValue_f * 100).ToString("F0") + '%';
        return _deductScore;
    }
    public int GetParameterDataScore(link link,string naem)
    {
        int score = -1;
        foreach (ParameterData parameterData in AllParameterDataList)
        {
            if (parameterData._link == link.ToString())
            {
                if(parameterData._name == naem)
                {
                    parameterData.ToString();
                    score= parameterData._score;
                }
            }
        }
        return score;
    }
}

[Serializable]
public class Score
{
    public string _ID; 
    public string _link;
    public List<ParameterData> ParameterDataList = new List<ParameterData>();
    public Score(link link)
    {
        _link = link.ToString();
        _ID = ((int)link).ToString();
    }
}
[Serializable]
public class ParameterData
{
    public string _link;
    public string _name;
    public string _value;
    public int _initialScore;
    public int _score;
    public string _beginTime;
    public string _endTime;
    public ParameterData(link link, string name, string value, int initialScore, int score, string beginTime, string endTime)
    {
        _link = link.ToString();
        _name = name;
        _value = value;
        _initialScore = initialScore;
        _score = score;
        _beginTime = beginTime;
        _endTime = endTime;

        AddInScoreManager();
        AddInScore();
    }
    public override string ToString()
    {
        string str = string.Format("环节：{0}、内容参数：{1}、初始分：{2}、开始时间：{3}、结束时间：{4}、得分：{5}",_link,_name+_value,_initialScore,_beginTime,_endTime,_score);
        return str;
    }
    void AddInScoreManager()
    {
        ScoreManager score1 = GetManager.Instance.Root.GetComponent<ScoreManager>();
        if (score1.AllParameterDataList.Count != 0)
        {
            bool isExist = false;
            int index = 0;
            foreach (ParameterData p in score1.AllParameterDataList)
            {
                if (p._link == _link && p._name == _name)
                {
                    isExist = true;
                    break;
                }
                index++;
            }
            if (isExist)
            {
                score1.AllParameterDataList.Remove(score1.AllParameterDataList[index]);
                score1.AllParameterDataList.Insert(index, this);
            }
            else
                score1.AllParameterDataList.Add(this);
        }
        else
        {
            score1.AllParameterDataList.Add(this);
        }
    }
    void AddInScore()
    {
        //赋值环节分数
        ScoreManager score1 = GetManager.Instance.Root.GetComponent<ScoreManager>();
        foreach (Score score2 in score1.scores)
        {
            if (score2._link == _link)
            {
                bool isExist = false;
                int index = 0;
                if (score2.ParameterDataList.Count != 0)
                {
                    foreach (ParameterData data in score2.ParameterDataList)
                    {
                        if (data._name == _name)
                        {
                            isExist = true;
                            break;
                        }
                        index++;
                    }
                    if (isExist)
                    {
                        //找到环节中存在这个分数项
                        score2.ParameterDataList.Remove(score2.ParameterDataList[index]);
                        score2.ParameterDataList.Insert(index, this);
                    }
                    else
                    {
                        score2.ParameterDataList.Add(this);
                    }
                }
                else
                    score2.ParameterDataList.Add(this);
            }
        }
    }
}
