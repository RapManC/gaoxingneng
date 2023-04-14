using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TreeObj
{
    /// <summary>
    /// 自身ID
    /// </summary>
    public string ID;
    /// <summary>
    /// 当前项的名称
    /// </summary>
    public string Name;
    /// <summary>
    /// 操作值
    /// </summary>
    public string Value;
    /// <summary>
    /// 总分
    /// </summary>
    public int GrossScore;
    /// <summary>
    /// 得分
    /// </summary>
    public int TestScore;
    /// <summary>
    /// 操作状态,0为已操作，1为未操作，2为null
    /// </summary>
    public int OperationState = 2;
    /// <summary>
    /// 开始时间
    /// </summary>
    public string BinginTime;
    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime;

    /// <summary>
    /// 所有的子节点
    /// </summary>
    public List<TreeObj> ChildrenList = new List<TreeObj>();
    public void AddChildren(TreeObj treeObj)
    {
        ChildrenList.Add(treeObj);
    }
    public TreeObj Find(string Name)
    {
        return Find(this, Name);
    }
    public TreeObj Find(TreeObj tree,string Name)
    {
        if (tree == null) 
            return null;
        if (tree.Name.Equals(Name))
            return tree;
        foreach (var t in tree.ChildrenList)
        {
            TreeObj tree1 = Find(t,Name);
            if (tree1 != null)
                return tree1;
        }
        return null;
    }
    public void SetAllTreeObj(int value)
    {
        this.OperationState = value;
        foreach (var tree in ChildrenList)
        {
            tree.OperationState = value;
            tree.SetAllTreeObj(value);
        }
    }
    public TreeObj FindID(string id)
    {
        return FindID(this, id);
    }
    /// <summary>
    /// 跟过id向自身的
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TreeObj FindID(TreeObj tree,string id)
    {
        if (tree == null)
            return null;
        if (tree.ID.Equals(id))
            return tree;
        foreach (var t in tree.ChildrenList)
        {
            TreeObj tree1 = Find(t, Name);
            if (tree1 != null)
                return tree1;
        }
        return null;
    }
    /// <summary>
    /// 通过根节点的ID，设置子节点的ID
    /// </summary>
    /// <param name="root"></param>
    public void SetID(TreeObj root)
    {
        if (root == null)
            return;
        for (int i = 0; i < root.ChildrenList.Count; i++)
        {
            var tree = root.ChildrenList[i];
            tree.ID = root.ID + "_" + i;
            SetID(tree);
        }
    }
    public void SetID()
    {
        SetID(this);
    }
    /// <summary>
    /// 设置这颗树的分数，连带子树
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public int SetTreeScore(TreeObj t)
    {
        if (t == null)
            return 0;
        if (t.ChildrenList == null || t.ChildrenList.Count == 0)
            return t.TestScore;
        t.TestScore = 0;
        foreach (var tree in t.ChildrenList)
        {
            t.TestScore += SetTreeScore(tree);
        }
        return t.TestScore;
    }

    /// <summary>
    /// 设置这颗树的总分数，连带子树
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public int SetGrossScore(TreeObj t)
    {
        if (t == null)
            return 0;
        if (t.ChildrenList == null || t.ChildrenList.Count == 0)
            return t.GrossScore;
        t.GrossScore = 0;
        foreach (var tree in t.ChildrenList)
        {
            t.GrossScore += SetGrossScore(tree);
        }
        return t.GrossScore;
    }
    /// <summary>
    /// 设置这棵树的操作状态，连带子树
    /// </summary>
    /// <param name="t"></param>
    /// <param name="isEnd"></param>
    /// <returns></returns>
    public bool SetTreeOperation(TreeObj t,bool isEnd = true)
    {
        if (t == null)
            return true;
        if (t.ChildrenList == null || t.ChildrenList.Count == 0)
            return t.OperationState == 0 ? true : false;
        bool isAllChildFinish = true;
        foreach (var tree in t.ChildrenList)
        {
            isAllChildFinish = isAllChildFinish && SetTreeOperation(tree);
        }
        if (isAllChildFinish)
            t.OperationState = 0;
        else
            t.OperationState = isEnd ? 1 : 2;
        return isAllChildFinish;
    }
    public bool SetTreeOperation(bool isEnd = true)
    {
        return SetTreeOperation(this, isEnd);
    }
    /// <summary>
    /// 计算实验得分，根据子节点的分数进行相加
    /// </summary>
    public int SetTESTScore()
    {
        return SetTreeScore(this);
    }
    public int SetGrossScore()
    {
       return SetGrossScore(this);
    }
    public void SetTESTData(bool isEnd = true)
    {
        SetTESTScore();
        SetTreeOperation(isEnd);
    }
}
[System.Serializable]
public class TESTData
{
    public string user_id;
    public string user_name;
    public string experiment_start;
    public string experiment_end;
    public int score;
    public TreeObj JsonPlayerData;
    public OptionListClass OptionListData;
    public int OptionScore;
}
[Serializable]
public class OptionListClass
{
   public List<SingleOption> OptionList; 

}