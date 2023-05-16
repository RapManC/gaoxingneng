using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "JianjieItemData", fileName = "JianjieItemData")]
public class JianjieItemData : ScriptableObject
{
    public List<JianjieItem> Items = new List<JianjieItem>();
}
