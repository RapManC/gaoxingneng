using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JianjieManager : JieshaoManager
{
    public Image TitleImage;
    public override void ChangeItemByIndex(int index) { 
        base.ChangeItemByIndex(index);

        TitleImage.sprite = (Items[index] as JianjieItem).TitleSprite;
    }

    public override void InitItems()
    {
        List<JianjieItem> itemDatas = (ItemsData as JianjieItemData).Items;
        for (int i = 0; i < itemDatas.Count; i++)
        {
            Items.Add(itemDatas[i]);
        }
    }

}


[System.Serializable]
public class JianjieItem : JieshaoItem
{
    public Sprite TitleSprite;
}