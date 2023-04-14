using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class OptionManager 
{
    private static OptionManager _instance;
    public static OptionManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new OptionManager();
            }
            return _instance;
        }
    }
    public bool IsFinish = false;
    private List<SingleOption> optionList;
    public List<SingleOption> OptionList
    {
        get
        {
            if (optionList == null || optionList.Count <= 0)
                Debug.LogError("选择题列表有问题");
            return optionList;
        }
        set
        {
            optionList = value;
        }
    }
    public int Score()
    {
        int score = 0;
        foreach (var option in OptionList)
        {
            if (option.TrueFalse)
            {
                score += option.Score;
            }
        }
        score = Mathf.Clamp(score, 0, 100);
        return score;
    }

    public void SetQuestionBank(HTTP.QuestionBank questionBank)
    {
        optionList = new List<SingleOption>();
        for (int i = 0; i < questionBank.data.Count; i++)
        {
            var question = questionBank.data[i];

            SingleOption singleOption = new SingleOption() { Code = i + 1, headStr = question.name };
            singleOption.optionArray = question.OptionArray().ToArray();
            singleOption.trueStr = question.TrueStr();
            singleOption.Score = question.score;
            singleOption.OptionType = question.OptionType();
            optionList.Add(singleOption);
        }
    }
    /// <summary>
    /// 获取题目
    /// </summary>
    /// <param name="questionBankJosn"></param>
    /// <returns></returns>
    public bool SetQuestionBank(string questionBankJosn)
    {

        HTTP.QuestionBank questionBank = JsonUtility.FromJson<HTTP.QuestionBank>(questionBankJosn);
        for (int i = 0; i < questionBank.data.Count; i++)
        {
            questionBank.data[i].OptionList = JsonMapper.ToObject<List<HTTP.OptionConent>>(questionBank.data[i].option);
        }
        if (questionBank.data == null || questionBank.data.Count <= 0)
            return false;
        SetQuestionBank(questionBank);
        return true;
    }
}
