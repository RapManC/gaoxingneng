using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotControl : MonoBehaviour
{
    float rotSpeed { get { return UIManage.Instance.JiaobanModelRotSpeed; } }
    public bool isRot;
    private void Update()
    {
        if (isRot)
        {
            if (gameObject.tag == "Yanshi")
            {
                if (this.name == "Poshui1")
                    transform.Rotate(-Vector3.down, 3 * 360 * Time.deltaTime);
                else
                    transform.Rotate(Vector3.forward, 3 * 360 * Time.deltaTime);
            }
            else
            {
                if (this.name == "Poshui1")
                    transform.Rotate(-Vector3.down, rotSpeed * 360 * Time.deltaTime);
                else
                    transform.Rotate(Vector3.forward, rotSpeed * 360 * Time.deltaTime);
            }
        }
    }
}
