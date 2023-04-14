using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRig : MonoBehaviour
{
    [Header("大于这个高度就要进行重力模拟")]
    public float Dis = 1;
    private float V0 = 0;
    public Ray Ray { get { return new Ray(transform.position, -Vector3.up); } }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics.Raycast(Ray, out RaycastHit hit))
        {
            float dis = Vector3.Distance(hit.point, transform.position);
            if (dis > Dis)
            {
                V0 += 9.8f * Time.deltaTime;
                float height = V0 * Time.deltaTime+ 0.5f * 9.8f * Mathf.Pow(Time.deltaTime, 2);
                var pos = transform.position;
                pos.y -= height;
                //pos.y = Mathf.Min(hit.point.y,pos.y);
                transform.position = pos;
            }
            else
            {
                V0 = 0;
            }
        }
        else
        {
            V0 = 0;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Ray);
    }
}
