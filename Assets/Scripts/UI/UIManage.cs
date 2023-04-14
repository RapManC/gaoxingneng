using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum ModelObserveType 
{ 
    功能,
    原理,
    结构
}
public class UIManage : MonoBehaviour
{
    Transform shiyan_UITran;
    Transform _tishiEffect;
    GameObject mistake_UI;
    [HideInInspector] public Transform popout_UI;//弹窗
    [HideInInspector] public GameObject Player;
    [HideInInspector] public ChangeDataManager changeDataManager;
    public List<GameObject> ShiyanliuchenButtonList = new List<GameObject>();
    #region 知识学习

    [Header("学习模型")]
    private string modelName;
    public ModelObserveType ObserveType = ModelObserveType.功能;
    public float MinDistance;
    public float MaxDistance;
    public float MdoelMoveSpeed;
    public float ModelRotSpeed;
    private GameObject hintGO;
    [HideInInspector] public List<GameObject> ObserveModelList;//所有要观察模型的数组
    [HideInInspector] public int CurrentShowModelIndex;//当前显示的模型索引
    #endregion
    #region 稀土配置成分
    //稀土配置成分选择列表
    List<Toggle> chenfenList = new List<Toggle>();//所有可选择的稀土元素列表
    List<Toggle> zhutichenfenList = new List<Toggle>();
    List<Toggle> xituchenfenList = new List<Toggle>();
    List<Toggle> qitachenfenList = new List<Toggle>();
    List<Toggle> xuanzhechenfenToggleList = new List<Toggle>();//选择成分的toggle列表
    List<string> xuanzhechenfenList = new List<string>();//选择成分的元素列表
    public float Sum;//除铁以外其他元素的中和比例
    YuanshuPeizhiSlider FepeizhiSlider;
    public Dictionary<string, float> YuanshuZanbi = new Dictionary<string, float>();
   public List<YuanshuPeizhiSlider> AllPeizhiSlider = new List<YuanshuPeizhiSlider>();
    int PeizhiErrorCoun=0;
    #endregion
    #region 速凝炉
    [HideInInspector]public float JiaobanModelRotSpeed;
    #endregion

    //考核
    public GameObject TipsObj;
    public GameObject TipsObj2;
    //输入框
    public InputField setpathInput;


    Transform TestBiaobiao;

    List<Transform> scoreDataList = new List<Transform>();
    public List<string> timeString = new List<string>();
    public List<int> scorce = new List<int>();
    private static UIManage _instance;
    public static UIManage Instance { get { return _instance; } }
    private void Awake()
    {
        //print(Application.persistentDataPath);
        _instance = this;
        shiyan_UITran = transform.Find("Shiyan_UI");
        mistake_UI = transform.Find("MistakePopout_UI").gameObject;
        foreach (Transform temp in shiyan_UITran .Find("CaozhuoLiuchen_UI/Scroll View/Viewport/Content"))
        {
            if (temp.gameObject.activeSelf)
                ShiyanliuchenButtonList.Add(temp.gameObject);
        }
        hintGO = shiyan_UITran.Find("Hint_UI").gameObject;
        Player = GameObject.Find("Player");
        popout_UI = transform.Find("Popout_UI");
        changeDataManager = transform.Find("Shuju").GetComponent<ChangeDataManager>();
        _tishiEffect = GetManager.Instance.Root.Find("TishiEffect");
        InitScoreDataList();
        TestBiaobiao = shiyan_UITran.Find("Head/Biaobiao_Button");
    }
    void InitScoreDataList()
    {
        Transform tran = transform.Find("BaoBiao_UI/Scroll View/Viewport/Content/BGImage/Content");
        foreach(Transform t in tran.Find("ScoreDataS"))
        {
            scoreDataList.Add(t);
        }
    }
    private void Start()
    {
        //Time.timeScale = 3;
        Debug.Log(ScoreManager.GetCurveTimeLong());
        foreach (Transform temp in GameObject.Find("GongnenModels").transform)
        {
            ObserveModelList.Add(temp.gameObject);
        }
        XuanzheYuanshuFuzhi();
    }
    /// <summary>
    /// 点击实验操作按键
    /// </summary>
    public void OnExpect()
    {
        transform.Find("Panel_UI").gameObject.SetActive(false);
        transform.Find("Shiyan_UI").gameObject.SetActive(true);
        transform.Find("Popout_UI/BG/Hlpe").gameObject.SetActive(true);
        transform.Find("Popout_UI").gameObject.SetActive(true);
        Step step = ScoreManager._Instance.GetStep(StepType.选择材料牌号);
        step.startTime = ScoreManager.GetCurveTimeLong();
        Text1();
    }
    /// <summary>
    /// 加载下一个场景
    /// </summary>
    public void OnNextHongGui()
    {
        StartCoroutine(LoadScene("honggui"));
    }


    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IEnumerator LoadScene(string name)
    {
        //异步加载场景
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //unity内部的协程协调器法线是异步加载类型的返回对象就会等待
        //等待异步加载结束后才会继续执行迭代器函数中后面的步骤
        yield return ao;
    }



    /// <summary>
    /// 选择成分元素列表赋值
    /// </summary>
    void XuanzheYuanshuFuzhi()
    {
        foreach (Toggle toggle in shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (1)/YuanshuSelectBG").GetComponentsInChildren<Toggle>())
        {
            chenfenList.Add(toggle);
        }
        foreach (Toggle toggle in shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (1)/YuanshuSelectBG/Zhitu").GetComponentsInChildren<Toggle>())
        {
            zhutichenfenList.Add(toggle);
        }
        foreach (Toggle toggle in shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (1)/YuanshuSelectBG/Xitu").GetComponentsInChildren<Toggle>())
        {
            xituchenfenList.Add(toggle);
        }
        foreach (Toggle toggle in shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (1)/YuanshuSelectBG/Qita").GetComponentsInChildren<Toggle>())
        {
            qitachenfenList.Add(toggle);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

           //TestProcessControl._Instance.TiaozhuanTest(GongduanType.回火);
        }
    }
    public void AuotSetFeValue(float value)
    {
        FepeizhiSlider.AuotSetFeValue(value);
    }
    /// <summary>
    /// 退出模型介绍
    /// </summary>
    public void OnExit()
    {
        foreach (Transform temp in transform)
        {
            temp.gameObject.SetActive(temp.name == "Panel_UI" || temp.name == "Input_UI" || temp.name == "Shuju" || temp.name == "MaskLoad");
        }
        OnExitXuexi();
    }
    /// <summary>
    /// 退出实验
    /// </summary>
    public void OnExitTest()
    {

        foreach (Transform temp in transform)
        {
            temp.gameObject.SetActive(temp.name == "Panel_UI" || temp.name == "Input_UI" || temp.name == "Shuju" || temp.name == "MaskLoad");
        }

        Camera.main.transform.parent.GetComponent<ClickGroundMove>().enabled = false;
        Player.GetComponent<PlayerRig>().enabled = false;

    }
    
    //学习部分的按键事件
    public void OnXuexi()
    {

    }

    /// <summary>
    /// 显示各个工段的UI介绍
    /// </summary>
    /// <param name="game"></param>
    public void OnShowGongduanUI(GameObject game)
    {
        game.SetActive(!game.activeSelf);
    }
    /// <summary>
    /// 切换模型
    /// </summary>
    /// <param name="go"></param>
    public void OnXuexiMdoel(GameObject go)
    {
        foreach (Transform temp in transform.Find("Xuexi_UI/XuexiMdoel_UI").transform)
        {
            temp.gameObject.SetActive(temp.name == go.name);
        }
        if (go == gameObject)
            return;
        Transform XuexiButtons_tran = transform.Find("Xuexi_UI/XuexiButtons_UI");
        XuexiButtons_tran.Find("Gongnen_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/btn_设备功能_默认");
        XuexiButtons_tran.Find("Yuanli_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/btn_运行原理_默认");
        XuexiButtons_tran.Find("Jiegou_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/btn_查看结构_默认");
        switch (go.name)
        {
            case "Gongnen_UI":
                ObserveType = ModelObserveType.功能;
                XuexiButtons_tran.Find("Gongnen_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/btn_设备功能_按下");
                SetXuexiHide("点击3D模型可旋转查看模型");
                XuexiModelQiehuan(modelName);
                break;
            case "Yuanli_UI":
                ObserveType = ModelObserveType.原理;
                XuexiButtons_tran.Find("Yuanli_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/btn_运行原理_按下");
                XuexiModelQiehuan(modelName);
                SetXuexiHide("正在播放"+modelName+"工作状态动画");
                break;
            case "Jiegou_UI":
                ObserveType = ModelObserveType.结构;
                XuexiButtons_tran.Find("Jiegou_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("ButtonImage/btn_查看结构_按下");
                XuexiModelQiehuan(modelName);
                SetXuexiHide("你可点击查看部分结构详细信息");
                break;
        }
    }
    /// <summary>
    /// 设置学习模型的头文字
    /// </summary>
    /// <param name="text"></param>
     public  void SetXuexiHide(string text)
    {
        transform.Find("Xuexi_UI/Hide_Text").GetComponent<Text>().text = text;
    }
    /// <summary>
    /// 设置模型功能介绍
    /// </summary>
    void SetModelGongnenJiesao(string text)
    {
        transform.Find("Xuexi_UI/XuexiMdoel_UI/Gongnen_UI/Scroll View/Viewport/Content/Text").GetComponent<Text>().text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.Find("Xuexi_UI/XuexiMdoel_UI/Gongnen_UI/Scroll View") as RectTransform);
        
    }
    /// <summary>
    /// 设置模型原理介绍
    /// </summary>
    public void SetModelYuanliJiesao(string text)
    {
        string value= text;
        if (text.IndexOf("n") != -1)
        {
            value = text.Replace("n", "\n");
        }
        transform.Find("Xuexi_UI/XuexiMdoel_UI/Yuanli_UI/Scroll View/Viewport/Content/Text").GetComponent<Text>().text = value;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.Find("Xuexi_UI/XuexiMdoel_UI/Yuanli_UI") as RectTransform);
    }
    /// <summary>
    /// 显示学习模型
    /// </summary>
    void XuexiModelQiehuan(string modelname)
    {
        List<GameObject> modelList = new List<GameObject>();
        foreach(Transform tran in GameObject.Find("ObserveModels").transform)
        {
            foreach(Transform tran2 in tran)
            {
                tran2.gameObject.SetActive(false);
                modelList.Add(tran2.gameObject);
            }
        }
        if (modelname == "")
        {
            return;
        }
        switch (ObserveType)
        {
            case ModelObserveType.功能:
                GameObject.Find("GongnenModels").transform.Find(modelName).gameObject.SetActive(true);
                string gongnenText = "";
                switch (modelName)
                {
                    case "真空速凝炉":
                        gongnenText = "　　真空速凝甩带炉是生产永磁材料的专用设备，用途磁性材料的熔炼及快速冷凝，是实现合金熔炼速凝铸片的理想设备。";
                        break;
                    case "氢破炉":
                        gongnenText = "　　大块铸片破裂，利用稀土金属间化合物吸氢后产生的体积膨胀和内应力将合金破碎成几十到几百微米的颗粒。";
                        break;
                    case "气流磨":
                        gongnenText = "　　将氢破后的大颗粒细化，得到3-5μm的粉末颗粒。";
                        break;
                    case "取向成型":
                        gongnenText = "　　尽可能使每个颗粒易磁化轴沿磁场方向保持一致，提高取向度，使磁体达到最佳性能。";
                        break;
                    case "烧结炉":
                        gongnenText = "　　将压型磁体加热到粉末基体相熔点以下的某一温度，进行一段时间的热处理的工艺过程。其过程中包括了粉末颗粒表面吸附气体的排除，有机物的挥发，应力的消除粉末颗粒表面氧化物的还原，变形粉末颗粒的回复和再结品，接着是原子的扩散和物质的迁移，颗粒间由机械接触转化为物理化学接触，形成金属键或共价键，晶粒的长大，密度提高等一系列物理化学变化。";
                        break;
                    default:
                        gongnenText = "　　这是啥模型呀？";
                        break;
                }
                SetModelGongnenJiesao(gongnenText);
                break;
            case ModelObserveType.原理:
                GameObject.Find("YuanliModels").transform.Find(modelName).gameObject.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.Find("Xuexi_UI/XuexiMdoel_UI/Yuanli_UI/Scroll View") as RectTransform);
                break;
            case ModelObserveType.结构:
                GameObject.Find("JiegouModels").transform.Find(modelName).gameObject.SetActive(true);
                SetModelGongnenJiesao("点击模型对应部件，可以查看详细信息");
                break;
        }
    }
    public void OnUP()
    {
        if (CurrentShowModelIndex >= 1)
        {
            CurrentShowModelIndex -= 1;
            ShowObserveModel(CurrentShowModelIndex);
            XuexiModelQiehuan(modelName);
        }
    }
    public void OnNext()
    {
        if (CurrentShowModelIndex <= ObserveModelList.Count - 2)
        {
            CurrentShowModelIndex += 1;
            ShowObserveModel(CurrentShowModelIndex);
            XuexiModelQiehuan(modelName);
        }
    }
    /// <summary>
    /// 切换不同模型
    /// </summary>
    /// <param name="index"></param>
    public void ShowObserveModel(int index)
    {
        CurrentShowModelIndex = index;
        if (index == -1)
        {
            XuexiModelQiehuan("");
            return;
        }
        GameObject.Find("ModelName_Text").GetComponent<Text>().text = ObserveModelList[CurrentShowModelIndex].name;
        modelName= ObserveModelList[CurrentShowModelIndex].name;
    }
    /// <summary>
    /// 退出学习
    /// </summary>
    public void OnExitXuexi()
    {
        ShowObserveModel(-1);
        OnXuexiMdoel(this.gameObject);
    }
    /// <summary>
    /// Player位置赋值到厂房里
    /// </summary>
   public void PlayerTran()
    {
        //StartCoroutine(enumerator(0, () =>
        //{
            Player.transform.position = Player.GetComponent<ClickGroundMove>().OrigPos;
            Player.transform.eulerAngles = Player.GetComponent<ClickGroundMove>().OrigRot;
            StartCoroutine(enumerator(0, () => {
                StartCoroutine(enumerator(0, () => {
                    Camera.main.transform.parent.GetComponent<ClickGroundMove>().enabled = true;
                Player.GetComponent<PlayerRig>().enabled = true;
            }));
            }));
      //}));
    }
    /// <summary>
    /// Playerw位置赋值在厂房外面
    /// </summary>
    public void PlayerTranWai()
    {
        ClickGroundMove groundMove = Player.GetComponent<ClickGroundMove>();
        Player.transform.position = groundMove.ObservePos;
        Player.transform.eulerAngles = Vector3.zero;
        Player.GetComponent<PlayerRig>().enabled = false;
        Player.transform.Find("Main Camera").eulerAngles = Vector3.zero;
        groundMove.enabled = false;

        ShowObserveModel(0);
        OnXuexiMdoel(transform.Find("Xuexi_UI/XuexiMdoel_UI/Gongnen_UI").gameObject);
    }
    //实验考核部分按键事件
    public void ShowXingnen(int index)
    {
        string str = null;
        switch (index.ToString())
        {
            case "0":
                str = "　　高牌号\n剩磁感应强度Br: 1280-1320 mT内禀矫顽力Hcj: ≥1592 KA/m\n最大磁能积 318 - 342iHc（BH）max KJ/m3\n最高工作温度 150Tw ℃";
                break;
            case "1":
                str = "　　中牌号\n剩磁感应强度Br: 1210-1250 mT\n内禀矫顽力Hcj: ≥955 KA/m\n最大磁能积 287-310 iHc（BH）max KJ/m3\n最高工作温度 120Tw ℃";
                break;
            case "2":
                str = "　　低牌号\n剩磁感应强度Br: 1170-1210 mT\n内禀矫顽力Hcj: ≥955 KA/m\n最大磁能积 263-287	iHc（BH）max KJ/m3\n最高工作温度 80Tw ℃";
                break;
            default:
                str = "超出索引";
                return;
        }
        GameObject.Find("Shiyan_UI/Parameter_UI/Text").GetComponent<Text>().text = str;
    }
    /// <summary>
    /// 点击查看选择数据
    /// </summary>
    public void OnLookXuanzheData()
    {
        string message = null;
        switch (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name)
        {
            case "1":
                message = "你选择的牌号是：" + DataManager.Instance.GetPaihao();
                break;
            case "2":
                message = "你选择的实验成分有：" + DataManager.Instance.GetChengfen();
                break;
            case "3":
                message = "你选择的配置稀土质量是：" + DataManager.Instance.GetXituPeizhi();
                break;
        }
        this.transform.Find("Xuanzhexingxi/Text").GetComponent<Text>().text = message;
        this.transform.Find("Xuanzhexingxi").gameObject.SetActive(true);
    }

    /// <summary>
    /// 点击完成元素选择
    /// </summary>
    public void OnWanchenYuanshuXuanzhe()
    {
        xuanzhechenfenList.Clear();
        xuanzhechenfenToggleList.Clear();
        foreach (Toggle toggle in chenfenList)
        {
            if (toggle.isOn)
            {
                xuanzhechenfenToggleList.Add(toggle);
                xuanzhechenfenList.Add(toggle.transform.Find("Label").GetComponent<Text>().text);
            }
        }
        List<string> quesaozhutiyaunshu = new List<string>();
        foreach(Toggle t in zhutichenfenList)
        {
            string str = Getquesaoyaunshu(t.transform.Find("Label").GetComponent<Text>().text);
            if(str!="")
            quesaozhutiyaunshu.Add(str);
        }
        List<string> quesaoxiyouyaunshu = new List<string>();
        foreach(Toggle t in xituchenfenList)
        {
            string str = Getquesaoyaunshu(t.transform.Find("Label").GetComponent<Text>().text);
            if (str != "")
                quesaoxiyouyaunshu.Add(str);
        }
        List<string> quesaoqitayaunshu = new List<string>();
        foreach(Toggle t in qitachenfenList)
        {
            string str = Getquesaoyaunshu(t.transform.Find("Label").GetComponent<Text>().text);
            if (str != "")
                quesaoqitayaunshu.Add(str);
        }
        bool isXuanzhezuti= (quesaozhutiyaunshu.Count==0);
        bool isXuanzheXitu= (quesaoxiyouyaunshu.Count<2);
        string hintStr = "";
        if (!isXuanzhezuti)
        {
            hintStr += "缺少基础原材：";
            foreach (string str in quesaozhutiyaunshu)
            {
                hintStr += str + "、";
            }
        }
        if (!isXuanzheXitu)
        {
            hintStr += "\n缺少赣南特有离子型稀土元素：";
            foreach (string str in quesaoxiyouyaunshu)
            {
                hintStr += str + "、";
            }
        }
        hintStr=hintStr.TrimEnd('、');
        hintStr+="。";
        if (!isXuanzhezuti || !isXuanzheXitu)
        {
            ShowMistakeHint(hintStr,shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (1)").gameObject);
            return;
        }
        SetHint("请配置稀土元素质量Nd2Fe14B,使元素质量比达到27-32wt.%");
        AudioManage.Instance.PlayMusicSource("请配置稀土元素质量Nd2Fe14B,使元素质量比达到27-32wt", 0.5f);
        GetManager.Instance.ControlFlow.Jingxingzong(2);
        BaochunXauznheChenfen();
        shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (1)").gameObject.SetActive(false);
        SetPeizhiXitu_UI();
    }
    string Getquesaoyaunshu(string value)
    {
        bool iscunzai = false;
        foreach (string str in xuanzhechenfenList)
            if (value == str)
                iscunzai = true;
        if (iscunzai)
            return "";
        else
            return value;
    }
    //保存选择成分
     void BaochunXauznheChenfen()
    {
        DataManager.Instance.ChenfenList = xuanzhechenfenList;
        Step step = ScoreManager._Instance.GetStep(StepType.元素成分决断);
        step.endTime = ScoreManager.GetCurveTimeLong();
        step.score = 100;
        step.repeatCount++;

        Step step2 = ScoreManager._Instance.GetStep(StepType.配置稀土元素质量);
        step2.startTime = ScoreManager.GetCurveTimeLong();
        ParameterData parameterData = new ParameterData(link.稀土成分, "元素成分决断", DataManager.Instance.GetChengfen(), 3, 3, "开始选择成分时间", "结束选择成分时间");
    }
    /// <summary>
    /// 设置配置稀土UI
    /// </summary>
    void SetPeizhiXitu_UI()
    {
        GameObject peizhi_UI = shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (2)").gameObject;
        Transform peizhiBGTran = peizhi_UI.transform.Find("PeizhiBG");
        foreach (Toggle toggle in xuanzhechenfenToggleList)
        {
            GameObject game=Instantiate(peizhiBGTran.Find("Zhitu/BG/peizi").gameObject, peizhiBGTran.Find(toggle.transform.parent.name+ "/BG"));
            YuanshuPeizhiSlider peizhiSlider= game.GetComponent<YuanshuPeizhiSlider>();
            AllPeizhiSlider.Add(peizhiSlider);
            peizhiSlider.YuanshuName = toggle.transform.Find("Label").GetComponent<Text>().text;
            if (peizhiSlider.YuanshuName == "铁(Fe)")
            {
                FepeizhiSlider = peizhiSlider;
                game.transform.Find("Slider").gameObject.SetActive(false);
            }
            game.SetActive(true);
            if (toggle.transform.parent.name == "Qita")
                peizhiBGTran.Find("Qita").gameObject.SetActive(true);
        }
        peizhi_UI.gameObject.SetActive(true);
    }
    /// <summary>
    /// 点击默认配置稀土
    /// </summary>
    public void OnMorenPeizhi()
    {
        DataManager.Instance.IsAuotPeizhi = true;
        foreach (var v in AllPeizhiSlider)
        {
            if (v.YuanshuName == "钕(Nd)")
            {
                v.slider.value = 0.3f;
            }
            else if (v.YuanshuName != "铁(Fe)")
                v.slider.value = 0.5f;
        }
    }
    /// <summary>
    /// 配置稀土
    /// </summary>
    public void OnPeizhiXitu()
    {
        GameObject peizhiUI = shiyan_UITran.Find("SelectPanels_UI/SelectPanel_UI (2)").gameObject;
        YuanshuZanbi.Clear();
        foreach (var v in AllPeizhiSlider)
        {
            YuanshuZanbi.Add(v.YuanshuName, v.Value);
        }
        int zanbu = 0;
        float xiyouValue = 0;
        if (YuanshuZanbi.ContainsKey("镝(Dy)"))
        {
            xiyouValue = YuanshuZanbi["镝(Dy)"];
        }else if (YuanshuZanbi.ContainsKey("镨(Pr)"))
        {
            xiyouValue = YuanshuZanbi["镨(Pr)"];
        }else if (YuanshuZanbi.ContainsKey("铽(Tb)"))
        {
            xiyouValue = YuanshuZanbi["铽(Tb)"];
        }
        zanbu = (int)(YuanshuZanbi["钕(Nd)"] + xiyouValue);
        DataManager.Instance.Xitupeizhi = zanbu;
        StartCoroutine(enumerator(0, () =>
        {
            if (zanbu >= 27 && zanbu <= 32)
            {
                peizhiUI.SetActive(false);
                DataManager.Instance.YuanshuZanbi = YuanshuZanbi;
                ShuningruTestManlag.Instance.StartTest();
                int _deductScore = 0;
                if (DataManager.Instance.IsAuotPeizhi)
                    _deductScore = 1;

                Step step = ScoreManager._Instance.GetStep(StepType.配置稀土元素质量);
                step.endTime = ScoreManager.GetCurveTimeLong();
                step.score = (5 - _deductScore)*20;
                step.repeatCount++;

                ParameterData parameterData = new ParameterData(link.成分配置, "配置稀土元素质量", GetXituchenfenPeibi(), 5, 5- _deductScore, "开始配置成分时间", "结束配置成分时间");
            }
            else
            {
                SetHint("配置稀土元素的质量在所有元素的百分比有误。稀土元素质量标准范围27-32wt.%");
                AudioManage.Instance.PlayMusicSource("配置稀土元素的质量在所有元素的百分比有误。稀土元素质量标准范围27-32wt", 0.5f);
                if (zanbu < 27)
                    ShowMistakeHint(zanbu + "wt.% \n" + "稀土相不足，矫顽力偏差，存在Fe相。", peizhiUI);
                if (zanbu>32)
                    ShowMistakeHint(zanbu + "wt.% \n" + "稀土相过多，主相降低，磁能积下降。", peizhiUI);
                PeizhiErrorCoun++;
                if (PeizhiErrorCoun >= 3)
                {
                    peizhiUI.transform.Find("Moren_Button").gameObject.SetActive(true);
                }
            }
        }));
        /// 得到稀土成分配比
        string GetXituchenfenPeibi()
        {
            string value = "";
            foreach(var v in YuanshuZanbi)
            {
                value += v.Key +":"+ v.Value+"%、";
            }
            value=value.TrimEnd('、');
            value += '。';
            return value;
        }
    }
    /// <summary>
    /// 保存用户输入信息
    /// </summary>
    /// <param name="gongduan"></param>
    /// <param name="valueName"></param>
    /// <param name="value"></param>
    /// <param name="danwei"></param>
    public void SetCanshu(GongduanType gongduan, string valueName, float value, string danwei, Action<float> action)
    {
        string K = gongduan + "*" + valueName;
        string V = value + "*" + danwei;
        bool IsContain = false;
        foreach (Caozhocanshu caozhocanshu in DataManager.Instance.CaozhoCanshuList)
        {
            if (caozhocanshu.gongduan == gongduan)
            {
                IsContain = true;
            }
        }
        //不存在改工段的数据类
        if (!IsContain)
        {
            Caozhocanshu caozhocanshu1 = new Caozhocanshu(gongduan);
            caozhocanshu1.CanshuD.Add(K, V);
            DataManager.Instance.CaozhoCanshuList.Add(caozhocanshu1);
        }
        else
        {
            foreach (Caozhocanshu caozhocanshu in DataManager.Instance.CaozhoCanshuList)
            {
                //找到已经存在该工段的数据保存类
                if (caozhocanshu.gongduan == gongduan)
                {
                    //判断是否有对应项的记录
                    bool isCunzi = caozhocanshu.CanshuD.ContainsKey(K);
                    if (IsContain)
                    {
                        caozhocanshu.CanshuD[K] = V;
                    }
                    else
                    {
                        caozhocanshu.CanshuD.Add(K, V);
                    }
                }
            }
        }
        action(value);
    }
    /// <summary>
    /// 显示错误信息，隔三秒后自动关闭
    /// </summary>
    /// <param name="value"></param>
    /// <param name="hideGo"></param>
    public void ShowMistakeHint(string value, GameObject hideGo)
    {
        if (hideGo)
            hideGo.SetActive(false);
        mistake_UI.transform.Find("BG/ChuowuHint/Text").GetComponent<Text>().text = value;
        mistake_UI.SetActive(true);
        AudioManager.SetAudio(AudioManager.CanvasAudio, "错误提示");
        StartCoroutine(enumerator(3, () =>
        {
            mistake_UI.SetActive(false);
            if (hideGo)
                hideGo.SetActive(true);
        }));
    }
    public void OnSNLToMing()
    {
        ShuningruTestManlag.Instance.Toming();
    }
    public void OnSNLHuihu()
    {
        ShuningruTestManlag.Instance.Huhu();
    }
    /// <summary>
    /// 速凝炉打包装箱
    /// </summary>
    public void OnDabaoZhuangxiang()
    {
        ShuningruTestManlag.Instance.OnDabaoZhuangxiang();
    }
    /// <summary>
    /// 速凝炉重新制作
    /// </summary>
    public void OnCongxingZhizhuo()
    {
        Debug.Log("重新制作");
        popout_UI.gameObject.SetActive(false);
        TestProcessControl._Instance.TiaozhuanTest(GongduanType.真空速凝炉);
    }
    /// <summary>
    /// 设置检测结果UI
    /// </summary>
    public void SetJianchejieguoUI(int br,int hcl ,int hcb,int bh)
    {
        Transform _jianchejieguoUI = popout_UI.Find("BG/Jianchejieguo");
        _jianchejieguoUI.Find("Group/BrText").GetComponent<Text>().text = br.ToString();
        _jianchejieguoUI.Find("Group/HclText").GetComponent<Text>().text = hcl.ToString();
        _jianchejieguoUI.Find("Group/HcbText").GetComponent<Text>().text = hcb.ToString();
        _jianchejieguoUI.Find("Group/BhText").GetComponent<Text>().text = bh.ToString();
    }
    public void CloseJianchejieguo()
    {
        popout_UI.Find("BG/Jianchejieguo").gameObject.SetActive(false);
        popout_UI.gameObject.SetActive(false);
    }
    public void SetHint(string str)
    {
        hintGO.transform.Find("Text").GetComponent<Text>().text = str;
        //AudioManager.SetAudio(AudioManager.CanvasAudio, "提示音");
    }
    /// <summary>
    /// 设置弹窗
    /// </summary>
    /// <param name="goName">弹窗名字</param>
    public void ShowPopout(string goName)
    {
        foreach(Transform tran in popout_UI.Find("BG"))
        {
            tran.gameObject.SetActive(tran.name == goName);
        }
        popout_UI.gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新报表ui内容
    /// </summary>
    public void UpDateScore_UI()
    {
        Transform content = transform.Find("BaoBiao_UI/Scroll View/Viewport/Content/BGImage/Content");
        int sum = 0;
        int count = 0;
        for (int i = 0; i < ScoreManager._Instance.AllParameterDataList.Count; i++)
            Atatement(i);
        ScoreManager._Instance._experimentScore = sum;
        content.Find("SumScoers/SumScoer").GetComponent<Text>().text = sum.ToString();
        void Atatement(int index)
        {
            Transform _scoreUI = scoreDataList[index];
            ParameterData _parameterData = ScoreManager._Instance.AllParameterDataList[index];
            _scoreUI.Find("Value").GetComponent<Text>().text = _parameterData._name + _parameterData._value;
            _scoreUI.Find("Tmie").GetComponent<Text>().text = GetTime();
            timeString.Add(GetTime());
            _scoreUI.Find("BaginScore").GetComponent<Text>().text = _parameterData._initialScore.ToString();
            _scoreUI.Find("Score").GetComponent<Text>().text = _parameterData._score.ToString();
            scorce.Add(_parameterData._score);
            sum += _parameterData._score;
            count++;

            foreach (var item in UIManage.Instance.timeString)
            {
                print(item);
            }
            foreach (var item in UIManage.Instance.scorce)
            {
                print(item);
            }
            string GetTime()
            {
                DateTime _startDateTime = ScoreManager._Instance.ConvertLongToDateTime(ScoreManager._Instance._experimentalData.steps[index].startTime.ToString());
                DateTime _endDateTime = ScoreManager._Instance.ConvertLongToDateTime(ScoreManager._Instance._experimentalData.steps[index].endTime.ToString());

                string _stareTime = _startDateTime.ToLongTimeString();
                string _endTime = _endDateTime.ToLongTimeString();
                _parameterData._beginTime = GetManager.Instance.Root.GetComponent<HTTP>().DateTimeToXP(_startDateTime);
                _parameterData._endTime = GetManager.Instance.Root.GetComponent<HTTP>().DateTimeToXP(_endDateTime);
                return _stareTime + "--" + _endTime;
            }
        }
    }
    /// <summary>
    /// 提交数据所用的方法
    /// </summary>
    public void PutExperimentalData()
    {
        //调用此方法
        Debug.Log("调用传输数据方法");
        //设置word文档
        //ReadWrite._Instance.SetWordData(ScoreManager._Instance.ATESTData);
        UpDateScore_UI();
            #if UNITY_WEBGL
                 ReadWrite._Instance.WriteWebData();
            #elif UNITY_STANDALONE_WIN
                TipsObj2.SetActive(false);
                TipsObj.SetActive(true);
               
            #endif
        //打开提示面板
        HTTP hTTP = GetManager.Instance.Root.GetComponent<HTTP>();
        string _data = hTTP.GetReport();
        Debug.Log("------------传给后台数据-------");
        Debug.Log(_data);
        int _score = ScoreManager._Instance._sumScore;
        string scoreData="";
        foreach(var v in ScoreManager._Instance.AllParameterDataList)
        {
            scoreData += v._name+"实验总分：";
            scoreData += v._initialScore+"  实验得分：";
            scoreData += v._score+"   ";
        }
        Debug.Log("---------------实验分值---------------");
        Debug.Log(scoreData);
        //File.WriteAllText(Application.dataPath + "/Resources/Josn1", _data);
        //string data2 = hTTP.GetALBReport();
        //File.WriteAllText(Application.dataPath + "/Resources/Josn2", data2);
        ScoreManager._Instance.Data = JsonUtility.FromJson<TESTData>(_data);
        // hTTP.OnSubmitScore(_score.ToString(), ScoreManager._Instance.Data);
        ScoreManager._Instance.MapNewIalbData();
        hTTP.OnSubmitIlabScore();
        
    }
    /// <summary>
    /// 关闭文件提示
    /// </summary>
    public void CloseFileTips()
    {
        TipsObj.SetActive(false);
    }
    /// <summary>
    /// 设置按键是否能点击
    /// </summary>
    /// <param name="button"></param>
    /// <param name="isIntera"></param>
    public void SetButtonIntera(Button button, bool isIntera)
    {
        button.interactable = isIntera;
    }
    public IEnumerator enumerator(float temp, System.Action action)
    {
        if (temp == 0)
            yield return null;
        else
            yield return new WaitForSecondsRealtime(temp);
        action?.Invoke();
    }

    public void SetTishiPos(string targetPosname)
    {
        Transform target = GetManager.Instance.Root.Find("TishiTagetPos/" + targetPosname);
        if (target)
            _tishiEffect.position = target.position;
        _tishiEffect.gameObject.SetActive(true);
    }
    /// <summary>
    /// 设置报表闪烁
    /// </summary>
    public void SetBiaoBiaoGlint()
    {
        UIGlint uIGlint = TestBiaobiao.GetComponent<UIGlint>();
        uIGlint.IsGlint = true;
        StartCoroutine(enumerator(4, () => { uIGlint.ShutGlint(); }));
    }

    #region 文本提示处 
    public void Text1()
    {  
            AudioManage.Instance.PlayMusicSource("请根据右侧性能参数数据选择实验材料牌号", 0.5f);
    }
    public void Text2()
    {
            AudioManage.Instance.PlayMusicSource("请依据牌号选择实验主要成分", 0.5f);
    }
    #endregion

    #region
    /// <summary>
    /// 获取路径
    /// </summary>
    public void getFIle()
    {

        WindowsOperateUtil.SelectDir((dir) =>
        {
            setpathInput.text = dir;
        });
    }
    #endregion

    public void SetFile()
    {
        ReadWrite._Instance.SetWordData();
    }

}
