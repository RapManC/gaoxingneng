using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeRot : MonoBehaviour
{
    public bool isRot;
    public float rotSpeed;
    private void Update()
    {
        if (isRot)
        {
            if (this.name == "Poshui1")
                transform.Rotate(-Vector3.forward, rotSpeed * 360 * Time.deltaTime);
            else
                transform.Rotate(Vector3.down, rotSpeed * 360 * Time.deltaTime);
        }
    }
}
