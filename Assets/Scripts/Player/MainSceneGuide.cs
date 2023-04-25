using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneGuide : MonoBehaviour
{
    #region 声明单例
    private static MainSceneGuide instance;
    public static MainSceneGuide Instance => instance;
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

    [SerializeField]private List<Transform> TargetTrans = new List<Transform>();

    public PlayerAutoMove PlayerAuotMove;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform trans in transform)
        {
            TargetTrans.Add(trans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AutoMoveByIndex(int index, float delay = 2f)
    {
        PlayerAuotMove.SetAutoPlayTarget(TargetTrans[index]);
        PlayerAuotMove.DelayAutoMove(delay);
    }

    public void AutoMoveDontNeedPause()
    {
        PlayerAuotMove.DontNeedPause();
    }

    public void StopAutoMove()
    {
        PlayerAuotMove.SetAutoPlayActive(false);
    }
}
