using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HighlightPlus;

public class ModelLookover : MonoBehaviour
{
    private bool isLimit;
    float distance;
    float MinDistance { get { return UIManage.Instance.MinDistance; } }
    float MaxDistance { get { return UIManage.Instance.MaxDistance; } }
    float RotSpeed { get { return UIManage.Instance.ModelRotSpeed; } }
    float MoveSpeed { get { return UIManage.Instance.MdoelMoveSpeed; } }

    public string Name;
    public string Data;
    private void OnMouseDown()
    {
        ShowBiankuang();
        GetManager.Instance.Canvas.Find("Xuexi_UI/Hide_Text").GetComponent<Text>().text = Name;
        GetManager.Instance.Canvas.Find("Xuexi_UI/XuexiMdoel_UI/Jiegou_UI/Text").GetComponent<Text>().text = Data;
    }
    private void ShowBiankuang()
    {
        foreach(Transform tran in transform.parent)
        {
            if (tran.GetComponent<ModelLookover>() != null)
            {
                tran.GetComponent<HighlightEffect>().highlighted = (tran.name == name);
            }
        }
    }
    private void OnMouseDrag()
    {
        float H = Input.GetAxis("Mouse X");
        float V = Input.GetAxis("Mouse Y");
        Vector3 euler = transform.parent.rotation.eulerAngles;
        if (H != 0 || V != 0)
        {
            euler.x += Time.deltaTime * RotSpeed * V;
            euler.y -= Time.deltaTime * RotSpeed * H;
            transform.parent.rotation = Quaternion.Euler(euler);
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
            distance = Vector3.Distance(transform.parent.position, Camera.main.transform.position);
            distance += distance * Time.deltaTime * MoveSpeed * x;
            distance = Mathf.Clamp(distance, MinDistance, MaxDistance);
            transform.parent.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
        }
    }
}
