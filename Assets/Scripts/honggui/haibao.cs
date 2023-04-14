using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class haibao : MonoBehaviour
{
    public GameObject pageObj;
    public Button ColseBtn;
    private void OnMouseDown()
    {
        pageObj.SetActive(true);
    }
    private void Start()
    {
        ColseBtn.onClick.AddListener(()=> {

            pageObj.SetActive(false);
        });
    }
}
