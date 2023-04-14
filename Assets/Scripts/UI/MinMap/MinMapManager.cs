using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class MiniMapImageData
{
    /// <summary>
    /// 因为这个物体会被删除重新加载，所有要得到这物体的地址，后面通过地址加载目标物体
    /// </summary>
    GameObject target;
    public string SpritePath;
    public string Name;
    public string Tag;
    public bool IsShowName;
    public GameObject UpdateTarget { get { return GetManager.Instance.Root.Find(path).gameObject; } }
    string path;
    public MiniMapImageData( string name,string tag,GameObject target, bool isShowname, string spritePath = null)
    {
        this.Name = name;
        this.Tag = tag;
        this.target = target;
        this.IsShowName = isShowname;
        this.SpritePath = spritePath;
        SetPath();
    }
    public void SetPath()
    {
        string naem = target.name;
        path = naem;
        Transform patent = target.transform.parent;
        while (true)
        {
            if (patent.name == "Root")
                break;
            path = path.Insert(0, patent.name+"/");
            patent = patent.parent;
        }
    }
}

public class MinMapManager : MonoBehaviour
{
    //地图长宽数据
    public float SceneLength = 75;
    public float SceneWide = 45;
    //地图左下角的世界坐标，（设为原点坐标，方便计算）
    public Vector3 Origin = new Vector3(-30, 0, -27.5f);
    public RectTransform MinMapBG;
    public RectTransform MaxMapBG;
    public Vector2 MinMapCentre =new Vector2(175,115f);


    public RectTransform NowMapBG;

    public List<Tubiao> TubiaoList = new List<Tubiao>();
    public static MinMapManager Insteance;
    public GameObject Player { get { return GameObject.FindGameObjectWithTag("Player"); } }
    GameObject PlayerIcom;


    GameObject shuninglu;
    GameObject qingpolu;
    GameObject qiliumo;
    GameObject quxiangchenxing;
    GameObject shaojielu;
    GameObject huihuo;
    GameObject jianche;
    private void Awake()
    {
        NowMapBG = MinMapBG;
        Insteance = this;

        shuninglu = GetManager.Instance.InteractiveModel.Find("ShuningLuParent/PingpangMoveBiaopianCollde").gameObject;
        qingpolu= GetManager.Instance.InteractiveModel.Find("QingporuParent").gameObject;
        qiliumo = GetManager.Instance.InteractiveModel.Find("QilimoParent").gameObject;
        quxiangchenxing = GetManager.Instance.InteractiveModel.Find("QuxiangchenxingParent").gameObject;
        shaojielu = GetManager.Instance.InteractiveModel.Find("ShaojierLuParent").gameObject;
        huihuo = GetManager.Instance.InteractiveModel.Find("HuihuoParent").gameObject;
        jianche = GetManager.Instance.InteractiveModel.Find("Jianche").gameObject;
    }
    private void Start()
    {
        PlayerIcom= AddMinMapIcon(new MiniMapImageData("Player", "Player", Player, false, "MinMap/img_视角定位"));

        AddMinMapIcon(new MiniMapImageData( "真空速凝炉工段", "仪器", shuninglu, true));
        AddMinMapIcon(new MiniMapImageData( "氢破炉工段", "仪器", qingpolu, true));
        AddMinMapIcon(new MiniMapImageData( "气流磨工段", "仪器", qiliumo, true));
        AddMinMapIcon(new MiniMapImageData( "取向成型压机工段", "仪器", quxiangchenxing, true));
        AddMinMapIcon(new MiniMapImageData( "真空烧结工段", "仪器", shaojielu, true));
        AddMinMapIcon(new MiniMapImageData( "二次烧结", "仪器", huihuo, true));
        AddMinMapIcon(new MiniMapImageData( "磁性能检测", "仪器", jianche, true));
    }
    private void Update()
    {
        SetMinMapPos();
    }
    /// <summary>
    /// 设置小地图的位置、随玩家移动跟随效果
    /// </summary>
    void SetMinMapPos()
    {
        //实时更新Player图标位置和转向
        Vector2 pos = GetMinMapRectPos(Player);
        (PlayerIcom.transform as RectTransform).anchoredPosition = pos;
        (PlayerIcom.transform as RectTransform).eulerAngles = new Vector3(0,0, -Player.transform.eulerAngles.y);

        pos = new Vector2(MinMapCentre.x - pos.x, MinMapCentre.y - pos.y);
        //限制小地图背景移动范围
        pos.x = Mathf.Clamp(pos.x, -203, 0);
        pos.y = Mathf.Clamp(pos.y, -100, 0);
        MinMapBG.anchoredPosition = pos;
    }
    //向小地图添加图标
    public GameObject AddMinMapIcon(MiniMapImageData imageData)
    {
        GameObject prefab = transform.Find("Tubiao").gameObject;
        GameObject newTubiaoGO = Instantiate(prefab, MinMapBG);
        Tubiao newTubiao=newTubiaoGO.GetComponent<Tubiao>();
        newTubiao.ImageData = imageData;
        newTubiaoGO.SetActive(true);
        TubiaoList.Add(newTubiao);
        return newTubiaoGO;
    }
    /// <summary>
    /// 删除图标
    /// </summary>
    /// <param name="name"></param>
    public void DeleteMinMap(string name)
    {
        foreach(Tubiao tubiao in TubiaoList)
        {
            if (tubiao.ImageData.Name == name)
            {
                TubiaoList.Remove(tubiao);
                Destroy(tubiao.gameObject);
                return;
            }
        }
        Debug.Log("没有找到要删除的图标，图标名为：" + name);
    }
    /// <summary>
    /// 得到目标物体在小地图上的位置
    /// </summary>
    /// <param name="taretGO"></param>
    /// <returns></returns>
    public Vector2 GetMinMapRectPos(GameObject taretGO)
    {
        Vector3 OriginPlayertran = taretGO.transform.position - Origin;
        float xPianyi = OriginPlayertran.x / SceneLength;
        float yPianyi = OriginPlayertran.z / SceneWide;
        Vector2 RectPos = new Vector2(xPianyi * NowMapBG.sizeDelta.x, yPianyi * NowMapBG.sizeDelta.y);
        return RectPos;
    }
    /// <summary>
    /// 切换大地图
    /// </summary>
    public void OnMaxMapButton()
    {
        NowMapBG = MaxMapBG;
        MinMapBG.parent.gameObject.SetActive(false);
        MaxMapBG.parent.gameObject.SetActive(true);
        foreach (var tubiao in TubiaoList)
        {
            tubiao.transform.SetParent(MaxMapBG);
            tubiao.UpdatePos();
        }
    }
    /// <summary>
    /// 切换小地图
    /// </summary>
    public void OnMinMapButton()
    {
        NowMapBG = MinMapBG;
        MaxMapBG.parent.gameObject.SetActive(false);
        MinMapBG.parent.gameObject.SetActive(true);
        foreach (var tubiao in TubiaoList)
        {
            tubiao.transform.SetParent(MinMapBG);
            tubiao.UpdatePos();
        }
    }
}
