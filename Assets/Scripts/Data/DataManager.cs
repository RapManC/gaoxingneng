using System;
using System.Collections.Generic;
using UnityEngine;
public class Caozhocanshu
{
    public GongduanType gongduan;
    /// <summary>
    /// 第一个为工段名+"*"+参数名，第二个为参数值+"*"+单位
    /// </summary>
    public Dictionary<string, string> CanshuD = new Dictionary<string, string>();
    public Caozhocanshu(GongduanType gongduan)
    {
        this.gongduan = gongduan;
    }

    public override string ToString()
    {
        string str = "";
        List<string> nameList = new List<string>();
        foreach(string k in CanshuD.Keys)
        {
            nameList.Add(k);
        }
        List<string> valueList = new List<string>();
        foreach (string v in CanshuD.Values)
        {
            valueList.Add(v);
        }
        for(int i=0;i< CanshuD.Count; i++)
        {
            str += nameList[i] + valueList[i]+"   ";
        }
        Debug.Log(str);
        return str;
    }
}
/// <summary>
/// 工段
/// </summary>

public enum GongduanType
{
    Null,
    真空速凝炉,
    氢破炉,
    气流磨,
    取向成型,
    烧结,
    回火,
    测试
}
public class DataManager : Base<DataManager>
{
    /// <summary>
    /// 牌号选择
    /// </summary>
    public int PaihaoXaunzhe;
    /// <summary>
    /// 选择成分
    /// </summary>
    public List<string> ChenfenList = new List<string>();
    /// <summary>
    /// 配置的各个类型元素比例
    /// </summary>
    public Dictionary<string, float> YuanshuZanbi = new Dictionary<string, float>();
    /// <summary>
    /// 稀土配置
    /// </summary>
    public int Xitupeizhi;
    /// <summary>
    /// 是否点击自动配置
    /// </summary>
    public bool IsAuotPeizhi;
    public GongduanType FinallyTest;

    public List<Score> scores = new List<Score>();


    //旧版用户输入系统（无用）
    public List<Caozhocanshu> CaozhoCanshuList = new List<Caozhocanshu>();

    public string GetPaihao()
    {
        string str = null;
        switch (PaihaoXaunzhe)
        {
            case 0:
                str = "高牌号 N28SH-N35SH";
                break;
            case 1:
                str = "中牌号 N35M-N50M";
                break;
            case 2:
                str = "低牌号 N38-N45";
                break;
        }
        return str;
    }
    public string GetChengfen()
    {
        string str = "";
        foreach (string s in ChenfenList)
        {
            str += s + ",";
        }
        str = str.TrimEnd(',');
        str += "。";
        return  str;
    }
    public string GetXituPeizhi()
    {
        return Xitupeizhi.ToString() + "wt.%";
    }
    public void SetFinallyTest(GongduanType gongduanType)
    {
        if (gongduanType == 0)
        {
            FinallyTest = gongduanType;
        }
        else
        {
            if ((int)gongduanType > (int)FinallyTest)
            {
                FinallyTest = gongduanType;
            }
        }
    }


    #region 旧版用户输入信息方法
    public float GetCanshu(GongduanType gongduan, string cansuName, out string danwei)
    {
        string K = gongduan + "*" + cansuName;
        string V = "查找错误";
        danwei = "错误单位";
        string value = "";
        bool isContain = false;
        Caozhocanshu caozhocanshu1 = new Caozhocanshu(GongduanType.取向成型);
        foreach (Caozhocanshu caozhocanshu in CaozhoCanshuList)
        {
            if (caozhocanshu.gongduan == gongduan)
            {
                isContain = true;
                caozhocanshu1 = caozhocanshu;
            }
        }
        if (isContain)
        {
            bool isContainK = caozhocanshu1.CanshuD.ContainsKey(K);
            if (isContainK)
            {
                V = caozhocanshu1.CanshuD[K];
                var arr = V.Split('*');
                value = arr[0];
                danwei = arr[arr.Length - 1];
            }
        }
        Debug.Log(value);
        return Convert.ToSingle(value);
    }
    public void GetAllCaozhoCanshu()
    {
        Debug.Log("输出所有用户输入参数");
        foreach (var v in CaozhoCanshuList)
        {
            v.ToString();
        }
    }
    #endregion
}