using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTips : MonoBehaviour
{
    public Button closeBtn;
    public GameObject tipsObject;

    private void Start()
    {
        closeBtn.onClick.AddListener(()=>{
            tipsObject.SetActive(false);
        
        });
    }
}
