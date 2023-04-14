using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    public List<SingleOption> OptionList;
    public float Score = 0;
    /// <summary>
    /// 答题考核是否完成
    /// </summary>
    public bool IsFinish { get { return OptionManager.Instance.IsFinish; } set { OptionManager.Instance.IsFinish = value; } }
    public int TrueCount
    {
        get
        {
            int trueCount = 0;
            foreach (var option in OptionList)
            {
                if (option.TrueFalse)
                    trueCount++;
            }
            return trueCount;
        }
    }
    private void Awake()
    {
        var optionList = OptionManager.Instance.OptionList;
        if (optionList != null && optionList.Count > 0)
            OptionList = optionList;
    }
    void Start()
    {
        for(int i=0;i< OptionList.Count; i++)
        {
            InstanceOption(i);
        }
    }
    /// <summary>
    /// 提交
    /// </summary>
    public void OnSubmit()
    {
        foreach (var v in OptionList)
            v.IsTrueFalse();
        foreach (var v in GameObject.FindObjectsOfType<OptionQuestion>())
        {
           // Debug.Log("---"+v);

            v.ShowResultInformation();
        }
        float score = 0;
        foreach (var option in OptionList)
        {
            if (option.TrueFalse)
            {
                score += option.Score;
            }
        }
        Score = score;
        ScoreManager._Instance._OptionScore = (int)score;
        OptionListClass optionListClass = new OptionListClass();
        optionListClass.OptionList = OptionList;
        ScoreManager._Instance.Data.OptionListData = optionListClass;

        Debug.Log(score);
        transform.Find("EndOptionPlane").gameObject.SetActive(true);
        transform.Find("BG").gameObject.SetActive(true);
        transform.Find("EndOptionPlane/Text").GetComponent<Text>().text = string.Format("考核完成，你获得的分数为{0}。", Score);
        Transform content = transform.Find("Content/Scroll View/Viewport/Content");
        LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        IsFinish = true;

        //确定后选项不可点击了
        foreach (Transform tran in content)
        {
            foreach (Transform tran2 in tran.Find("toggles"))
            {
                tran2.GetComponent<Toggle>().interactable = false;
            }
        }
    }
    /// <summary>
    /// 实例化选择题
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    GameObject InstanceOption(int index)
    {
        SingleOption _singleOption = OptionList[index];
        GameObject _target = transform.Find("Content/Scroll View/Viewport/Content/TargetSingleOption").gameObject;
        GameObject _newSingleOption = Instantiate(_target, transform.Find("Content/Scroll View/Viewport/Content"));
        _newSingleOption.GetComponent<OptionQuestion>().singleOption = _singleOption;
        Transform _parent = _newSingleOption.transform.Find("toggles");
        GameObject _togglePrefab = _parent.Find("page").gameObject;
        Text headText = _newSingleOption.transform.Find("HeadText").GetComponent<Text>();
        headText.text = _singleOption.HeadStr;
        for (int i = 0; i < _singleOption.optionArray.Length; i++)
        {
            string selectStr = _singleOption.optionArray[i];
            string selectIndexStr = i.ToString();
            GameObject page = Instantiate(_togglePrefab);
            page.SetActive(true);
            page.transform.SetParent(_parent.transform);
            page.transform.localPosition = Vector3.zero;
            page.transform.localScale = Vector3.one;
            Text pageText = page.transform.Find("TextMeshPro Text").GetComponent<Text>();
            pageText.text = selectStr;
            if (pageText.text == "")
            {
                page.gameObject.SetActive(false);
            }
            Toggle toggle = page.GetComponent<Toggle>();
            if (_singleOption.OptionType == 0)
            {
                toggle.group = _parent.GetComponent<ToggleGroup>();
            }
            else
            {
                if(_singleOption.OptionType == 1)
                    headText.text = _singleOption.HeadStr + "（多选）";
                else
                    headText.text = _singleOption.HeadStr + "（复选）";
                page.transform.Find("Background").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/icon_复选_默认");
                page.transform.Find("Background/Checkmark").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/icon_复选_选择");
            }
            if (_singleOption.SelectList == null)
                toggle.isOn = false;
            else
                toggle.isOn = _singleOption.SelectList.Contains(i.ToString());

            if (OptionManager.Instance.IsFinish)
            {
                var trueList = _singleOption.trueStr.Split(',').ToList();
                toggle.interactable = false;
                if (toggle.isOn)
                    toggle.transform.Find("TextMeshPro Text").GetComponent<Text>().color = trueList.Contains(i.ToString()) ? Color.green : Color.red;
                else
                    toggle.transform.Find("TextMeshPro Text").GetComponent<Text>().color = trueList.Contains(i.ToString()) ? Color.green : Color.black;
            }
            else
            {
                toggle.onValueChanged.AddListener((bool value) =>
                {
                    if (IsFinish)
                        return;
                    _singleOption.SetSelectStr(selectIndexStr, value);
                        SetButtonShow();
                });
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(_parent.transform as RectTransform);
            void SetButtonShow()
            {
                if (IsFinish)
                    return;
                bool isAllXauznhe = true;
                foreach(var  v in OptionList)
                {
                    if (v.SelectList.Count<1)
                    {
                        isAllXauznhe = false;
                        break;
                    }
                }
                transform.Find("Content/Submit_Button (1)").gameObject.SetActive(isAllXauznhe);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        _newSingleOption.SetActive(true);
        return _newSingleOption;
    }
}
