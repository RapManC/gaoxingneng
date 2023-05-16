using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "JieshaoItemData", fileName = "JieshaoItemData")]
public class JieshaoItemData : ScriptableObject
{
    public List<JieshaoItem> Items = new List<JieshaoItem>();
}
