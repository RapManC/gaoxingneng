using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveModelControl : MonoBehaviour
{
    private bool isLimit;
    float distance;
    float MinDistance { get { return UIManage.Instance.MinDistance; } }
    float MaxDistance { get { return UIManage.Instance.MaxDistance; } }
    float RotSpeed { get { return UIManage.Instance.ModelRotSpeed; } }
    float MoveSpeed { get { return UIManage.Instance.MdoelMoveSpeed; } }

    private void OnMouseDrag()
    {
        float H = Input.GetAxis("Mouse X");
        float V = Input.GetAxis("Mouse Y");
        if (H != 0 || V != 0)
        {
            Vector3 euler = transform.rotation.eulerAngles;
            euler.x += Time.deltaTime * RotSpeed * V;
            euler.y -= Time.deltaTime * RotSpeed * H;
            transform.rotation = Quaternion.Euler(euler);
        }
    }
    private void OnEnable()
    {
        isLimit = true;
    }

    private void OnDisable()
    {
        isLimit = false;
    }
    private void FixedUpdate()
    {
        if (isLimit)
        {
            float x = Input.GetAxis("Mouse ScrollWheel");
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            distance += distance * Time.deltaTime * MoveSpeed * x;
            distance = Mathf.Clamp(distance, MinDistance, MaxDistance);
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
    }
}
