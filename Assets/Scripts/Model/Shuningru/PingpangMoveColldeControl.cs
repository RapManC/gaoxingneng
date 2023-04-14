using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingpangMoveColldeControl : MonoBehaviour
{
    public bool isMove;
    public Vector3 Tager1;
    public Vector3 Tager2;
    public  Vector3 taget;
    private void Awake()
    {
        taget = Tager2;
    }
    private void Update()
    {
        if (isMove)
        {
            if (transform.localPosition!=taget)
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, taget, 0.01f);
            else
            {
                if (taget == Tager1)
                    taget = Tager2;
                else
                    taget = Tager1;
            }
        }
    }
}
