using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

public class HTTP : MonoBehaviour
{
    private static DateTime OrginTime;
    public JiangxiSciencesAbutment JiangxiSciencesAbutment;

    public string Access_Token;
    public string UserID = "";
    public string UserName = "";
    public string score;
    public bool IsFirst = true;
    public bool IsTest = false;
    public TESTData data1;
    public ATESTData ATESTData;
    public string Josn;

    [Header("赋分模型集合")]
    public List<string> ScoringModelList = new List<string>();
    [Header("操作备注集合")]
    public List<string> RemaarkFrontList = new List<string>();
    [ContextMenu("赋值赋分模型")]
    public void InitScoringModelList()
    {
        ScoringModelList.Clear();
        foreach (var step in ATESTData.steps)
        {
            ScoringModelList.Add(step.scoringModel);
        }
    }

    private string IP
    {
        get
        {
            //打包正式
            //return "https://ilab-x.jxust.edu.cn/gxnyc";
            return "https://ilab-x.jxust.edu.cn/jxust/#/details/159";
            //return "http://123.56.121.49/www/experiment?id=159";
            //测试
            //return "http://175.24.190.188:90";
        }
    }

    [Obsolete]
    void Start()
    {
        if (IsFirst && !IsTest)
        {
            Init();
            InitExperimentalData();
        }

    }
    void InitExperimentalData()
    {
        ScoreManager._Instance._experimentalData.username = UserName;
        ScoreManager._Instance._experimentalData.title = "高性能稀土永磁材料制备虚拟仿真实验";
        ScoreManager._Instance._experimentalData.startTime = ScoreManager.GetCurveTimeLong();
        ScoreManager._Instance._experimentalData.status = 2;
        ScoreManager._Instance._experimentalData.originId = "未传递";
    }

    [Obsolete]
    void Init()
    {
        Debug.Log("当前平台" + Application.platform);
        SetMaskLoad.Instance.StartMaskText("正在获取用户信息，请稍后......");
        switch (Application.platform)//判断当前运行平台
        {
            case RuntimePlatform.WindowsEditor:
                //GetEditorToken();
                break;
            case RuntimePlatform.WindowsPlayer:
                GetPCToken();
                break;
            case RuntimePlatform.WebGLPlayer:
                Application.ExternalCall("SendSouces");
                // GetWebGLToken();
                break;
        }

        //OnGetAllOptionList();

    }
    [ContextMenu("写入lIabJson")]
    public void WritelIabData()
    {
        string json = JsonUtility.ToJson(ATESTData);
        File.WriteAllText(Application.dataPath + "/Resources/EndJson.txt", json);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.B))
        {
            var console = GameObject.FindObjectOfType<IngameDebugConsole.DebugLogManager>();
            if (console)
            {
                var canvas = console.GetComponent<Canvas>();
                canvas.targetDisplay = canvas.targetDisplay == 0 ? 1 : 0;
                canvas.sortingOrder = canvas.sortingOrder == 1001 ? 1003 : 1001;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = Time.timeScale == 1 ? 3 : 1;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.O))
        {
            var obj = SetMaskLoad.Instance.gameObject;
            obj.SetActive(!obj.activeSelf);
        }
    }
    void SetIlabTime()
    {
        ATESTData.steps[14].startTime = ATESTData.steps[15].startTime - 4000;
        ATESTData.steps[14].endTime = ATESTData.steps[14].startTime + 3000;
        ATESTData.steps[14].Init();

        foreach (StepData step in ATESTData.steps)
        {
            if (step.title.Length > 5)
            {
                Regex r = new Regex("生产能耗"); // 定义一个Regex对象实例
                Match m = r.Match(step.title); // 在字符串中匹配
                if (m.Success)
                {
                    step.title += "计算";
                    step.startTime = step.endTime - 2000;
                    step.Init();
                }
            }
        }
    }
    /// <summary>
    /// 得到传给后台的报表
    /// </summary>
    /// <returns></returns>
    public string GetReport()
    {
        ScoreManager._Instance.MapScore();
        ScoreManager._Instance.Data.user_id = UserID;
        ScoreManager._Instance.Data.user_name = UserName;
        ScoreManager._Instance.Data.experiment_start = DateTimeToXP(OrginTime);
        ScoreManager._Instance.Data.experiment_end = DateTimeToXP(DateTime.Now);
        ScoreManager._Instance.Data.score = ScoreManager._Instance._sumScore;
        ScoreManager._Instance.Data.OptionScore = ScoreManager._Instance._OptionScore;
        string json = JsonUtility.ToJson(ScoreManager._Instance.Data);
        return json;
    }
    /// <summary>
    /// 得到传给网站的报表
    /// </summary>
    /// <returns></returns>
    public string GetALBReport()
    {
        ExperimentalData data = ScoreManager._Instance._experimentalData;
        data.endTime = ScoreManager.GetCurveTimeLong();
        data.score = ScoreManager._Instance._sumScore;
        string json = JsonUtility.ToJson(ScoreManager._Instance._experimentalData);
        return json;
    }


    public void GetEditorToken()
    {
        OnGetAllOptionList();
        //Debug.Log("调用登录方法");
        //HTTPRequest request = new HTTPRequest(new Uri(IP + "/api/v1/login"), HTTPMethods.Post, (req, resp) =>//首先建立一个请求地址和类型根据接口文档确定
        //{
        //    try
        //    {
        //        Debug.Log("登录回包：" + resp.DataAsText);
        //        JsonClass1 jsonClass = JsonUtility.FromJson<JsonClass1>(resp.DataAsText);//从返回的json数据中提取对应的数据返回JsonClassl类型的对象
        //        UserID = jsonClass.data.user_info.user_id;
        //        UserName = jsonClass.data.user_info.user_name;
        //        Access_Token = jsonClass.data.token;
        //        OrginTime = DateTime.Now;
        //        SetMaskLoad.Instance.StartMaskText("获取成功", false, null, SetMaskLoad.LoadState.Win, 1, 0.1f, (obj => obj.SetActive(false)));
        //        OnGetAllOptionList();
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log(e);
        //        SetMaskLoad.Instance.StartMaskText("用户数据获取异常，请重试......", true, Start, SetMaskLoad.LoadState.Error);
        //    }
        //});
        //request.AddField("user_id", "admin");
        //request.AddField("user_pwd", "zghc2021");
        //request.Send();//开始处理请求
    }

    void GetPCToken()
    {
        OrginTime = DateTime.Now;
        string[] CommandLineArgs = Environment.GetCommandLineArgs();
        string str = CommandLineArgs.Aggregate<string, string>("", (a, b) => { return a + b; });
        Debug.Log("第一次" + str);
        string projectName = "nvtiepong2://";
        var index = str.IndexOf(projectName) + projectName.Length;
        str = str.Substring(index, str.Length - index - 1);
        Debug.Log("替换后出现的字符串:" + str);
        string[] strArray1 = str.Split('~');
        if (strArray1.Length >= 2)
        {
            UserID = strArray1[0];
            UserName = HttpUtility.UrlDecode(strArray1[1]);
            Access_Token = strArray1[2];
            Debug.Log(string.Format("用户ID:{0},用户姓名{1},Access_Token:{2}", UserID, UserName, Access_Token));
            SetMaskLoad.Instance.StartMaskText("获取成功", false, null, SetMaskLoad.LoadState.Win, 1, 0.1f, (obj => obj.SetActive(false)));
            OnGetAllOptionList();
        }
        else
        {
            SetMaskLoad.Instance.StartMaskText("用户数据获取异常，请重试......", true, Start, SetMaskLoad.LoadState.Error);
        }
    }
    public void GetWebGLToken(string json)
    {
        Debug.Log("得到JSON字符串" + json);
        OrginTime = DateTime.Now;
        var jsonData = JsonUtility.FromJson<WebGLTokenData>(json);
        Debug.Log(jsonData);
        if (jsonData != null)
            Debug.Log($"jsondata.user_id={jsonData.user_id},jsonData.user_name={jsonData.user_name},jsonData.access_token={jsonData.access_token}");
        UserID = jsonData.user_id;
        UserName = jsonData.user_name;
        Access_Token = jsonData.access_token;
        Debug.Log(string.Format("获取到UserID:[{0}],UserName:[{1}],AccessToken:[{2}]", UserID, UserName, Access_Token));
        SetMaskLoad.Instance.StartMaskText("获取成功", false, null, SetMaskLoad.LoadState.Win, 1, 0.1f, (obj => obj.SetActive(false)));
        OnGetAllOptionList();
    }


    public static string Unicode2String(string source)
    {
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
            source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
    }
    /// <summary>
    /// 请求获取选择题的内容
    /// </summary>
    public void OnGetAllOptionList()
    {
        #region 旧的
        // SetMaskLoad.Instance.StartMaskText("正在获取题库数据，请稍后......");
        //HTTPRequest httpRequest = new HTTPRequest(new Uri(IP + "/api/v2/subject/randomType"), HTTPMethods.Post, (req, resp) =>
        //{
        //    try
        //    {
        //        var json = resp.DataAsText;
        //        Debug.Log("获取题库接口回包："+ json);
        //        if (OptionManager.Instance.SetQuestionBank(json))
        //        {
        //            SetMaskLoad.Instance.StartMaskText("获取成功", false, null, SetMaskLoad.LoadState.Win, 1, 0.1f, (obj => obj.SetActive(false)));
        //        }
        //        else
        //        {
        //            SetMaskLoad.Instance.StartMaskText("题库数据获取异常，请重试......", true, OnGetAllOptionList, SetMaskLoad.LoadState.Error);
        //        }
        //    }
        //    catch
        //    {
        //        SetMaskLoad.Instance.StartMaskText("题库数据获取异常，请重试......", true, OnGetAllOptionList, SetMaskLoad.LoadState.Error);
        //    }
        //});
        //httpRequest.AddHeader("token", Token);
        //httpRequest.AddField("user_id", UserID);
        //httpRequest.Send();
        #endregion
        JiangxiSciencesAbutment.GetQuestionBank();

    }

    /// <summary>
    /// 最终调用的方法
    /// </summary>
    public void OnSubmitIlabScore()
    {
        ATESTData = ScoreManager._Instance.ATESTData;
        SetIlabTime();

        //推送成绩，先判断access_token是否为空，为空直接向本地网址推送，不为空先向ilab网推送
        //if (!string.IsNullOrEmpty(Access_Token))
        //    OnIlabSubmit();
        //else
        //    OnSubmit();


        ATESTData.username = UserID;
        ATESTData.originId = ATESTData.appid + "" + ScoreManager.GetCurveTimeLong().ToString();
        SetMaskLoad.Instance.StartMaskText("正在上传成绩，请稍后......");

        SubmitData();
        void SubmitData()
        {
            JiangxiSciencesAbutment.SubmitData(JsonUtility.ToJson(ATESTData), Access_Token, SubmitProblem,
               () => SetMaskLoad.Instance.StartMaskText("成绩提交失败，请重试......", true, () => SubmitData(), SetMaskLoad.LoadState.Error)
           );
        }

        void SubmitProblem()
        {
            JiangxiSciencesAbutment.SubmitProblem(JsonUtility.ToJson(ScoreManager._Instance.Data.OptionListData),
            () => SetMaskLoad.Instance.StartMaskText("答题数据提交成功", false, null, SetMaskLoad.LoadState.Win, 0.3f, 0.1f, WinEvnet),
             () => SetMaskLoad.Instance.StartMaskText("答题数据提交失败，请重试......", true, () => SubmitProblem(), SetMaskLoad.LoadState.Error)
              );
        }

    }

    #region Ilab网数据

    //public void OnIlabSubmit()
    //{
    //    //设置用户名
    //    ATESTData.username = UserID;
    //    //ATESTData.username = UserName;
    //    //  设置唯一ID
    //    ATESTData.originId = ATESTData.appid + "" + ScoreManager.GetCurveTimeLong().ToString();
    //    var json = JsonUtility.ToJson(ATESTData);
    //    Debug.Log("originid=" + ATESTData.originId + "json" + json);
    //    HTTPRequest httpRequest = new HTTPRequest(new Uri(IP + "/api/v2/upload_ilab"), HTTPMethods.Post, (req1, resp1) =>
    //    {
    //        try
    //        {
    //            Debug.Log("ilab网推送回包：" + resp1.DataAsText);
    //            Debug.Log($"UserID:{UserID},UserName:{UserName},Token:{Token},access_token:{Access_Token}");
    //            UpdateLoad load = JsonUtility.FromJson<UpdateLoad>(resp1.DataAsText);
    //            if (load == null || load.status != "200")//推送失败
    //            {
    //                SetMaskLoad.Instance.StartMaskText("成绩提交失败，请重试......", true, () => { OnIlabSubmit(); }, SetMaskLoad.LoadState.Error);
    //            }
    //            else
    //            {
    //                //推送成功，向本地网址推送实验数据
    //                OnSubmit();
    //                //SetMaskLoad.Instance.StartMaskText("成绩提交成功", true, null, SetMaskLoad.LoadState.Win, 0.3f, 0.1f, WinEvnet);
    //            }
    //        }
    //        catch
    //        {
    //            SetMaskLoad.Instance.StartMaskText("成绩提交失败，请重试......", true, () => { OnIlabSubmit(); }, SetMaskLoad.LoadState.Error);
    //        }
    //    });
    //    httpRequest.AddHeader("token", Token);
    //    httpRequest.AddField("access_token", Access_Token);
    //    httpRequest.AddField("report", json);
    //    httpRequest.Send();
    //}


    ////本地网址推送实验报表
    //public void OnSubmitScore(string score, string jsonPlayerData, string optionList, string optionScore)
    //{
    //    Debug.Log($"分数{score},实验报告:{jsonPlayerData},题目报表:{optionList},题目分数:{optionScore}");
    //    HTTP.score = score;

    //    HTTPRequest httpRequest = new HTTPRequest(new Uri(IP + "/api/v2/experiment/insert"), HTTPMethods.Post, (req, resp) =>
    //    {
    //        var josn = resp.DataAsText;
    //        Debug.Log("本地接口Ilab回包：" + josn);
    //        try
    //        {
    //            SetMaskLoad.Instance.StartMaskText("成绩提交成功", false, null, SetMaskLoad.LoadState.Win, 0.3f, 0.1f, WinEvnet);
    //        }
    //        catch
    //        {
    //            SetMaskLoad.Instance.StartMaskText("成绩提交失败，请重试......", false, () => { OnSubmitScore(score, jsonPlayerData, optionList, optionScore); }, SetMaskLoad.LoadState.Error);
    //        }
    //    });
    //    httpRequest.AddHeader("token", Token);
    //    httpRequest.AddField("user_id", UserID);
    //    httpRequest.AddField("user_name", UserName);
    //    httpRequest.AddField("experiment_start", DateTimeToXP(OrginTime));
    //    httpRequest.AddField("experiment_end", DateTimeToXP(DateTime.Now));
    //    httpRequest.AddField("score", score.ToString());
    //    httpRequest.AddField("JsonPlayerData", jsonPlayerData);
    //    httpRequest.AddField("OptionList", optionList);
    //    httpRequest.AddField("OptionScore", optionScore);
    //    httpRequest.Send();
    //}

    //void OnSubmit()
    //{
    //    data1 = ScoreManager._Instance.Data;
    //    string json = JsonUtility.ToJson(data1);
    //    OptionListClass OptionList = new OptionListClass();
    //    OptionList = data1.OptionListData;
    //    string score = data1.score.ToString();
    //    string jsonPlayerData = JsonUtility.ToJson(data1.JsonPlayerData);
    //    string optionList = JsonUtility.ToJson(OptionList);
    //    string optionScore = data1.OptionScore.ToString();
    //    OnSubmitScore(score, jsonPlayerData, optionList, optionScore);
    //}
    #endregion
#if UNITY_EDITOR
    void WinEvnet(GameObject obj)
    {
        UnityEditor.EditorApplication.isPaused = true;
        //obj.SetActive(false);
    }
#elif UNITY_WEBGL
                void WinEvnet(GameObject obj)
            {
                Application.ExternalCall("receiveSouces",ATESTData.score );
                Application.Quit();
            }
#else
            void WinEvnet(GameObject obj)
            {
                Application.Quit();
            }
#endif




    public string DateTimeToXP(DateTime dateTime)
    {
        string newStr = string.Format("{0}-{1}-{2} {3}:{4}:{5}", dateTime.Year, dateTime.Month, dateTime.Day, GetStr(dateTime.Hour.ToString()), GetStr(dateTime.Minute.ToString()), GetStr(dateTime.Second.ToString()));
        return newStr;

        string GetStr(string str)
        {
            return str.Length == 1 ? "0" + str : str;
        }
    }

    #region JsonData
    [System.Serializable]
    public class JsonClass1
    {
        public Data data;
        [System.Serializable]
        public class Data
        {
            public string token;
            public UserInfo user_info;
        }
        [System.Serializable]
        public class UserInfo
        {
            public string user_id;
            public string user_name;
        }
    }
    [System.Serializable]
    public class WebGLTokenData
    {
        public string user_id;
        public string user_name;
        public string access_token;
    }
    [System.Serializable]
    public class QuestionBank
    {
        public List<Question> data;
    }
    [System.Serializable]
    public class Question
    {
        public int id;
        public string name;
        public string option;
        public List<OptionConent> OptionList;
        public string manyChoice;
        public int score;
        public int difficulty;
        public string TrueStr()
        {
            string trueStr = "";
            List<string> trueList = new List<string>();
            for (int i = 0; i < OptionList.Count; i++)
            {
                if (OptionList[i].correct)
                    trueList.Add(i.ToString());
            }
            for (int i = 0; i < trueList.Count; i++)
            {
                trueStr += trueList[i];
                if (i != trueList.Count - 1)
                {
                    trueStr += ',';
                }
            }
            return trueStr;
        }
        public List<string> OptionArray()
        {
            List<string> optinNameList = new List<string>();
            for (int i = 0; i < OptionList.Count; i++)
            {
                var optionContent = OptionList[i];
                optinNameList.Add(optionContent.name);
            }
            return optinNameList;
        }
        public int OptionType()
        {
            return int.Parse(manyChoice);
        }
    }
    [System.Serializable]
    public class OptionConent
    {
        public int id;
        public string name;
        public bool correct;
    }
    #endregion



    [System.Serializable]
    public class UpdateLoad
    {
        public string status;
    }
}
