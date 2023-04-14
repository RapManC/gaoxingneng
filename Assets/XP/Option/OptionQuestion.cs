using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionQuestion : MonoBehaviour
{
    public SingleOption singleOption;

    /// <summary>
    /// 显示答案信息
    /// </summary>
    public void ShowResultInformation()
    {
        Text _headText = transform.Find("HeadText").GetComponent<Text>();
        string str = "";
        List<int> _selectList = new List<int>();
        foreach (char v in singleOption.trueStr)
        {
            if (v != ',')
            {
                int temp = int.Parse(v.ToString());
                str += singleOption.optionArray[temp]+',';
                _selectList.Add(temp);
            }
        }
        str = str.TrimEnd(',');
        string _resulText = "（正确答案：" + str+'）';
        string _origText = _headText.text; 
        _headText.text = _origText + "<color=#FFE300>" + _resulText + "</color>";

        if (singleOption.TrueFalse)
        {
           foreach(Transform  tran in transform.Find("toggles"))
            {
                foreach(int i in _selectList)
                {
                    if(i== tran.GetSiblingIndex()-1)
                    {
                        string s = tran.Find("TextMeshPro Text").GetComponent<Text>().text;
                        tran.Find("TextMeshPro Text").GetComponent<Text>().text = "<color=#68FF00>" + s + "</color>";
                    }
                }
            }
        }
        else
        {
            foreach (Transform tran in transform.Find("toggles"))
            {
                foreach (int i in _selectList)
                {
                    if (i == tran.GetSiblingIndex()-1)
                    {
                        string s = tran.Find("TextMeshPro Text").GetComponent<Text>().text;
                        tran.Find("TextMeshPro Text").GetComponent<Text>().text = "<color=#FF0000>" + s + "</color>";
                    }
                }
            }
        }
        
    }
}
