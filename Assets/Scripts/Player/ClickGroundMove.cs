using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum MoveType
{
    WSAD,
    Click,
    WSADAndClick,
    Null,
    HongGui

}
[RequireComponent(typeof(CharacterController))]
public class ClickGroundMove : MonoBehaviour
{
    [HideInInspector]public Vector3 OrigPos;
    [HideInInspector]public Vector3 OrigRot;
    [HideInInspector] public Vector3 ObservePos;
    public MoveType MoveType;
    public float mousespeed = 5f;
    public float minmouseY = -45f;
    public float maxmouseY = 45f;
    public float Speed = 5;
    public GameObject MoveEffcet;
    private float rotSpeed = 120;
    private float rotTime;
    float RotationY = 0f;
    float RotationX = 0f;

    GameObject texiao;
    Vector3 direction;
    private Vector3 tragetPos;
    private Quaternion origRot;
    private Quaternion laterRot;


    float distance;
    float shihsidistabce;//实时动态距离
    CharacterController CC { get { return this.GetComponent<CharacterController>(); } }

    private void Awake()
    {
        OrigPos = transform.position;
        OrigRot = transform.eulerAngles;
        ObservePos = new Vector3(-2.3f, 44.5f, -33);
    }
    private void Update()
    {
        switch (MoveType)
        {
            case MoveType.Click:
                RayClickMove(); 
                break;
            case MoveType.WSAD:
                //WASDMove();
                break;
            case MoveType.WSADAndClick:
                RayClickMove();
                //WASDMove();
                break;
            case MoveType.HongGui:
                HongGui();
                break;
        }
        ManualRot();
        //print(transform.localRotation);
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject, collision.gameObject);
    }
    /// <summary>
    /// 鼠标点击移动
    /// </summary>
    private void RayClickMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;//存放被检测到的物体
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Ground")
            {
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    tragetPos = hit.point;
                    if (GameObject.Find("Efx_Click_Green(Clone)") != null)
                    {
                        Destroy(GameObject.Find("Efx_Click_Green(Clone)"));
                    }
                    texiao = Instantiate(MoveEffcet);
                    texiao.transform.position = tragetPos;
                    origRot = transform.rotation;
                    transform.LookAt(tragetPos);
                    laterRot = transform.rotation;
                    transform.rotation = origRot;
                    float rotate_angle = Quaternion.Angle(origRot, laterRot);//计算旋转角度
                                                                             // 获得lerp速度
                    rotSpeed = rotSpeed / rotate_angle;
                    rotTime = 0;
                }

            }
        }
        distance = (tragetPos - transform.position).magnitude;
        shihsidistabce = (transform.position - tragetPos).magnitude;
        AoutRot();
    }
    private void WASDMove()
    {
        //if (shihsidistabce < 1.2f)
        //{
            float _horizontal = Input.GetAxis("Horizontal");
            float _vertical = Input.GetAxis("Vertical");
            tragetPos = transform.position;
            direction = transform.forward * _vertical + transform.right * _horizontal;
            direction.y = 0;
            Vector3 norma = direction.normalized;
            if (norma != Vector3.zero)
            {
            //CC.Move(Time.deltaTime * Speed * direction.normalized);
            CC.Move(this.transform.TransformDirection(norma)*Time.deltaTime);
            }
        //}
    }
    private void HongGui()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        if (transform.localRotation.y > 0.5)
        {
            transform.localPosition += new Vector3(-_horizontal, 0, -_vertical) * Time.deltaTime;
        }
        else
        {
            transform.localPosition += new Vector3(-_horizontal, 0, _vertical) * Time.deltaTime;
        }


    }
    /// <summary>
    /// 自动转向
    /// </summary>
    private void AoutRot()
    {
        rotTime += Time.deltaTime;
        if (rotTime <= 1)
            transform.rotation = Quaternion.Slerp(transform.rotation, laterRot, Time.deltaTime*20);
    }
    private void Move()
    {
        if (distance > 0.1f)
        {
            CC.SimpleMove((tragetPos - transform.position).normalized * Speed);
        }
    }
    /// <summary>
    /// 手动控制视角
    /// </summary>
    private void ManualRot()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotationX = transform.Find("Main Camera").localEulerAngles.x;
            RotationY = transform.localEulerAngles.y;
            RotationY +=  Input.GetAxis("Mouse X") * mousespeed;
            RotationX -= Input.GetAxis("Mouse Y") * mousespeed;
            if (RotationX > 180)
                RotationX = RotationX - 360;
            RotationX = Mathf.Clamp(RotationX, minmouseY, maxmouseY);//范围限制
            transform.Find("Main Camera").localEulerAngles = new Vector3(RotationX,0, 0);
            transform.localEulerAngles = new Vector3(0, RotationY, 0);
            origRot = transform.rotation;
            rotTime = 1;
        }
    }
}
