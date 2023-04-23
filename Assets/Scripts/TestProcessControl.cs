using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestProcessControl : BaseMonoBehaviour<TestProcessControl>
{
    public GongduanType NowGongduan;
    private new void Awake()
    {
        base.Awake();
        NowGongduan = GongduanType.Null;

    }
    /// <summary>
    /// 跳转实验
    /// </summary>
    /// <param name="gongduanType"></param>
    public void TiaozhuanTest(GongduanType gongduanType)
    {
        Canvas3Dto2D.Instance.SetCanvasActive(false);
        InputManage._Instance.ResetInput();
        StartCoroutine(UIManage.Instance.enumerator(0, () =>
        {
            ResetAllModel(()=> {
                StartCoroutine(UIManage.Instance.enumerator(0, () => { Congzhuo(); }));});
        }));
        //重做
        void Congzhuo()
        {
            NowGongduan = gongduanType;
            switch (gongduanType)
            {
                case GongduanType.真空速凝炉:
                    SNLCongzhuo();
                    break;
                case GongduanType.氢破炉:
                    QPLCongzhuo();
                    break;
                case GongduanType.气流磨:
                    QLMCongzhuo();
                    break;
                case GongduanType.取向成型:
                    QXCXCongzhuo();
                    break;
                case GongduanType.烧结:
                    SJLCongzhuo();
                    break;
                case GongduanType.回火:
                    HHCongzhuo();
                    break;
                case GongduanType.测试:
                    JCCongzhuo();
                    break;
            }
        }
    }

    #region 对应重做方法
    public void SNLCongzhuo()
    {
        Debug.Log("重新制作");
        UIManage.Instance.SetHint("重新制作程序启动，前往控制台开启电源");
        ShuningruTestManlag.Instance.StartTest();
    }
    public void QPLCongzhuo()
    {
        Debug.Log("氢破炉重做");
        UIManage.Instance.SetHint("重新开始氢破炉工段，前往控制台点击自动倒料");
        GetManager.Instance.InteractiveModel.Find("QingporuParent").GetComponent<QingporuTestManlag>().StartTest();
    }
    public void QLMCongzhuo()
    {
        Debug.Log("气流磨重做");
        UIManage.Instance.SetHint("重新开始气流磨工段，前往控制台点击倒料按键。");
        GetManager.Instance.InteractiveModel.Find("QilimoParent").GetComponent<QiliumoTestManlag>().StartTest();
    }
    public void QXCXCongzhuo()
    {
        Debug.Log("取向成型重做");
        UIManage.Instance.SetHint("重新开始取向成型工段，前往取向成型工段点击真空瓶进行上料。");
        GetManager.Instance.InteractiveModel.Find("QuxiangchenxingParent").GetComponent<QuxiangchenxingTestManlag>().StartTest();
    }
    public void SJLCongzhuo()
    {
        Debug.Log("烧结炉重做");
        UIManage.Instance.SetHint("重新开始烧结炉工段，前往控制台点击装料按键。");
        GetManager.Instance.InteractiveModel.Find("ShaojierLuParent").GetComponent<ShaojierLuTestManlag>().StartTest();
    }
    public void HHCongzhuo()
    {
        Debug.Log("回火重做");
        UIManage.Instance.SetHint("重新开始回火工段，点击回火炉门进行装料。");
        GetManager.Instance.InteractiveModel.Find("HuihuoParent").GetComponent<HuihuoTestManlag>().StartTest();
    }
    public void JCCongzhuo()
    {
        Debug.Log("检测重做");
        UIManage.Instance.SetHint("重新开始检测工段，点击物料放入检测机中。");
        GetManager.Instance.InteractiveModel.Find("Jianche").GetComponent<JiancheTestManlag>().StartTest();
    }
    #endregion
    /// <summary>
    /// 重置所有模型
    /// </summary>
    /// <param name="endAction"></param>
    public void ResetAllModel(Action endAction)
    {
        int i = 0;
        int target = GetManager.Instance.InteractiveModel.childCount;
        foreach (Transform tran in GetManager.Instance.InteractiveModel)
        {
            StartCoroutine(UIManage.Instance.enumerator(0, () =>
            {
                ResetModel(tran.name);
                i++;
                if (i == target)
                    endAction?.Invoke();
            }));
        }
    }
    GameObject ResetModel(string modelName)
    {
        if (Resources.Load<GameObject>("InstantiatePrefabs/" + modelName))
        {
            Destroy(GetManager.Instance.InteractiveModel.Find(modelName).gameObject);
            GameObject newModel = Instantiate(Resources.Load<GameObject>("InstantiatePrefabs/" + modelName), GetManager.Instance.InteractiveModel);
            newModel.name = modelName;
            return newModel;
        }
        else
        {
            Debug.Log("没有找到要重置的模型");
            return null;
        }
    }
}
