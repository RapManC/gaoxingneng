using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI闪烁
/// </summary>

public class UIGlint : MonoBehaviour
{
    [Header("每次闪烁所用的时间")]
    public float GlintSpeed = 1;
    public bool IsGlint;
    private float timer;
    bool isBianliang = true;
    public float GlintTiem = 4;

    //public int GlintCunt;
    private Color color
    {
        get { return this.GetComponent<Image>().color; }
        set { this.GetComponent<Image>().color = value; }
    }

    private void Update()
    {
        if (IsGlint)
        {
            color = new Color(color.r, color.g, color.b, timer / GlintSpeed);
            if (isBianliang)
            {
                timer += Time.deltaTime;
                if (timer > GlintSpeed)
                    isBianliang = false;
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                    isBianliang = true; ;
            }
            timer = Mathf.Clamp(timer, 0, GlintSpeed);
        }
    }
    /// <summary>
    /// 关闭闪烁
    /// </summary>
    public void ShutGlint()
    {
        IsGlint = false;
        color = new Color(color.r, color.g, color.b,1);
    }
}

