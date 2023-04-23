using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AutoMoveManager : MonoBehaviour
{
    #region 声明单例
    private static readonly AutoMoveManager instance = new AutoMoveManager();
    /// <summary>
    /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
    /// </summary>
    static AutoMoveManager() { }
    private AutoMoveManager() { }
    public static AutoMoveManager Instance { get { return instance; } }
    #endregion

    public PlayerAutoMove PlayerAuotMove;

    public List<Transform> targetTrans = new List<Transform>();
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        PlayAutoMoveByIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAutoMoveByIndex(int index)
    {
        if (index < targetTrans.Count)
        {
            currentIndex = index;
            SetPlayerAutoMove(targetTrans[currentIndex], () => {
                PlayAutoMoveByIndex(++currentIndex);
                Debug.Log("自动寻路完成");
            });
        }
    }
    public void PlayAutoMoveByIndex(int index, Action onMoveEnd) {
        if (index < targetTrans.Count)
        {
            currentIndex = index;
            SetPlayerAutoMove(targetTrans[currentIndex], onMoveEnd);
        }
    }

    public void SetPlayerAutoMove(Transform trans, Action onMoveEnd) { 
        PlayerAuotMove.SetAutoPlayTarget(trans, onMoveEnd);
        PlayerAuotMove.SetAutoPlayActive(true);
    }

    public void OnMoveEnd() {
        Debug.Log("自动寻路完成");
    }

    public void StopAutoPlay() {
        PlayerAuotMove.SetAutoPlayActive(false); 
    }

    public void ContinueAutoPlay()
    {
        PlayerAuotMove.SetAutoPlayActive(true);
    }

}
