using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class haibao2 : MonoBehaviour
{
    public GameObject haibao;
    public Button closeBtn;

    private void OnMouseDown()
    {
        haibao.SetActive(true);
        print(11);
    }

    private void Start()
    {
        closeBtn.onClick.AddListener(()=> {

            haibao.SetActive(false);
        });
    }
}
