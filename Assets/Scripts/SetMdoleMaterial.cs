using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KaiguanButtonType
{
    上部红色按键,
    上部绿色按键,
    底部红色按键,
    底部绿色按键,
    旋钮
}
public class SetMdoleMaterial : Base<SetMdoleMaterial>
{
    public void SetMaterial(GameObject go, string materialPath)
    {
        Material material1 = Resources.Load<Material>(materialPath);
        if (material1 == null)
        {
            Debug.Log("未找" + go + "的材质");
        }
        else
        {
            go.GetComponent<MeshRenderer>().material = material1;
        }
    }
}
