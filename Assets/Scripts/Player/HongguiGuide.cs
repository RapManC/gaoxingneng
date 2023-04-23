using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AutoGuideItem {
    public Transform Trans;
    public haibao Target;
}

public class HongguiGuide : MonoBehaviour
{
    #region 声明单例
    private static HongguiGuide instance;
    public static HongguiGuide Instance => instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }
    #endregion

    public List<AutoGuideItem> TargetItems = new List<AutoGuideItem>();

    public PlayerAutoMove PlayerAuotMove;

    // Start is called before the first frame update
    void Start()
    {
        //StartFirstGuide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGuideButton(int index, bool moveNext = false) {
        PlayerAuotMove.SetAutoPlayTarget(TargetItems[index].Trans, () => OnAutoMoveEnd(index, moveNext));
        PlayerAuotMove.DelayAutoMove(0);
    }

    private void OnAutoMoveEnd(int index, bool moveNext) {
        if (TargetItems[index].Target == null) return;
        TargetItems[index].Target.OnMouseDown();
        if (moveNext && index < TargetItems.Count - 1)
        {
            OnClickGuideButton(index + 1);
        }
    }

}
