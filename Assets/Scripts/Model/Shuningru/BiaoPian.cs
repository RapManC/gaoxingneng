using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiaoPian : MonoBehaviour
{
    public bool IsRenque = true;
    public float RenqueSpeed;
    public float color_r=255;
    public bool IsLKikePoshui;
    bool isPoshui;
    float timeLog;

    private void Update()
    {
        if (IsRenque)
        {
            color_r -= RenqueSpeed * Time.deltaTime;
            color_r = Mathf.Clamp(color_r, 0, 255);
            this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(color_r, 0, 0) / 255);
            if (color_r == 0)
            {
                IsRenque = false;
            }
        }
        if (!isPoshui)
        {
            timeLog += Time.deltaTime;
            if (timeLog >= 3f&&IsLKikePoshui)
            {
                foreach (Transform tran in this.transform)
                {
                    tran.gameObject.SetActive(true);
                    if (this.transform.parent.name == "Bopianfasheqi (1)")
                        tran.parent = this.transform.parent.parent;
                    else
                    {
                        tran.parent = GameObject.Find("Panzi").transform;
                        tran.position = new Vector3(tran.position.x, tran.position.y + 0.02f, tran.position.z);
                    }
                }
                Destroy(gameObject);
                isPoshui = true;
            }
        }
    }
}
