using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RenderingMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent,
}

public class Touming : MonoBehaviour
{
    List <Material> marerialList=new List<Material>();
    Color YuanbenColor = Color.white;
    private void Awake()
    {
        marerialList =new List<Material> (GetComponent<MeshRenderer>().materials);
        YuanbenColor = marerialList[0].color;
        SetZhencangColor();
    }
    public void SetToumingColor()
    {
        foreach (var mareria in marerialList)
        {
            SetMaterialRenderingMode(mareria, RenderingMode.Fade);
            if(name== "Guntong")
                mareria.color = new Color(YuanbenColor.r, YuanbenColor.g, YuanbenColor.b, 0 / 255f);
            else
            mareria.color = new Color(YuanbenColor.r, YuanbenColor.g, YuanbenColor.b, 100/255f) ;
        }
    }
    public void SetZhencangColor()
    {
        foreach (var mareria in marerialList)
        {
            SetMaterialRenderingMode(mareria, RenderingMode.Opaque);
            mareria.color = YuanbenColor;
        }
    }

    public static void SetMaterialRenderingMode(Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}
