using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class SingleOption
{
    /// <summary>
    /// 标题1,2,3,4,5
    /// </summary>
    public int Code;
    /// <summary>
    /// 选择题内容
    /// </summary>
    public string headStr;
    /// <summary>
    /// 所有选项
    /// </summary>
    public string[] optionArray;
    /// <summary>
    /// 正确的选项，对应所有选项里的索引，多个选项用逗号间隔（0,1,2)
    /// </summary>
    public string trueStr;
    /// <summary>
    /// 用户所选择的多个选择用逗号间隔为（0,1,2）
    /// </summary>
    public string selectStr;
    /// <summary>
    /// 这个题是否填对了
    /// </summary>
    public bool TrueFalse = false;
    /// <summary>
    /// 这个选择题多少分
    /// </summary>
    public int Score;
    /// <summary>
    /// 选择题类型0单选，1多选，2或
    /// </summary>
    public int OptionType;
    /// <summary>
    /// 用户选择的列表
    /// </summary>
    public List<string> SelectList;
    public SingleOption(int Code, string TrueStr, params string[] optionArray)
    {
        this.Code = Code;
        this.trueStr = TrueStr;
        this.optionArray = optionArray;
        SelectList = new List<string>();
    }
    public SingleOption() { SelectList = new List<string>(); }
    public string HeadStr
    {
        get
        {
            return string.Format("{0}、{1}", Code, headStr);
        }
    }
    public void SetSelectStr(string select, bool toggleValue)
    {
        if (OptionType == 0)
        {
            if (toggleValue)
                SelectList = new List<string>() { select };
            else
            {
                if (SelectList.Contains(select))
                    SelectList.Remove(select);
            }
        }
        else
        {
            if (toggleValue)
            {
                if (!SelectList.Contains(select))
                    SelectList.Add(select);
            }
            else
            {
                if (SelectList.Contains(select))
                    SelectList.Remove(select);
            }
        }
    }
    public bool IsTrueFalse()
    {
        string[] trueArray = trueStr.Split(',');
        selectStr = "";
        if (SelectList != null &&SelectList.Count>=1)
        {
            SelectList.Sort();
            for (int i = 0; i < SelectList.Count - 1; i++)
            {
                selectStr += SelectList[i] + ",";
            }
            selectStr += SelectList[SelectList.Count - 1];
        }
        if (OptionType == 2)
        {
            if (SelectList == null || SelectList.Count == 0)
                TrueFalse = false;
            else
            {
                foreach (var select in SelectList)
                {
                    if (!trueArray.ToList().Contains(select))
                        TrueFalse = false;
                }
                TrueFalse = true;
            }
        }
        else
        {
            TrueFalse = trueStr.Equals(selectStr);
        }
        return TrueFalse;
    }
    /// <summary>
    /// 得到这个选择题字数最多的选项
    /// </summary>
    /// <returns></returns>
    public int GetMaxSelect()
    {
        int maxLength = 0;
        for (int i = 0; i < optionArray.Length; i++)
        {
            if (optionArray[i].Length > maxLength)
            {
                maxLength = optionArray[i].Length;
            }
        }
        return maxLength;
    }
    /// <summary>
    /// 通过一行的最大字数来判断这个选择题的选项需要几行来存
    /// </summary>
    /// <param name="maxRowNum"></param>
    /// <returns></returns>
    public int GetColumn(int maxRowNum)
    {
        int maxNum = GetMaxSelect();
        if (maxNum >= maxRowNum / 2)
        {
            return 1;
        }
        else if (maxNum >= maxRowNum / 4)
        {
            return 2;
        }
        else
        {
            return 4;
        }
    }
}
