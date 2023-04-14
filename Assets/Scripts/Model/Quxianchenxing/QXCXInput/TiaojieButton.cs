using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class TiaojieButton : MonoBehaviour
{
    public QXCXInputControl qXCXInputControl;
    private void OnMouseDown()
    {
        if (!qXCXInputControl.IsStartTiaojie)
            return;
        switch (name)
        {
            case "Jia_1":
                qXCXInputControl.ChiCang_Jia();
                break;
            case "Jian_1":
                qXCXInputControl.ChiCang_Jian();
                break;
            case "Jia_2":
                qXCXInputControl.Yali_Jia();
                break;
            case "Jian_2":
                qXCXInputControl.Yali_Jian();
                break;
        }
    }
}
