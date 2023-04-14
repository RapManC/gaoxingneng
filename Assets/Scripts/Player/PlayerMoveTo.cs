using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveTo : MonoBehaviour
{
    //摄像机
    public GameObject playerView;

    //速度：每秒移动5个单位长度
    public float moveSpeed = 6;
    //角速度：每秒旋转135度
    public float angularSpeed = 135;
    //跳跃参数
    public float jumpForce = 200f;

    //水平视角灵敏度
    public float horizontalRotateSensitivity = 10;
    //垂直视角灵敏度
    public float verticalRotateSensitivity = 5;

    //最大俯角
    public float maxDepressionAngle = 90;

    //最大仰角
    public float maxElevationAngle = 25;

    //角色的刚体
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetMouseButton(1))
        {
            View();
        }
       
      
        
        //Jump();
    }

    void Move()
    {
        //通过键盘获取竖直、水平轴的值，范围在-1到1
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //按照矢量移动一段距离
        transform.Translate(Vector3.forward * v * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * h * Time.deltaTime * moveSpeed);
    }

    void View()
    {
        //锁定鼠标到屏幕中心
        //SetCursorToCentre();

        //当前垂直角度
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //通过鼠标获取竖直、水平轴的值，范围在-1到1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //角色水平旋转
        transform.Rotate(Vector3.up * h * Time.deltaTime * angularSpeed * horizontalRotateSensitivity);

        //计算本次旋转后，竖直方向上的欧拉角
        double targetAngle = VerticalAngle + v * Time.deltaTime * angularSpeed * verticalRotateSensitivity;

        //竖直方向视角限制
        if (targetAngle > maxDepressionAngle && targetAngle < 360 - maxElevationAngle) return;

        //摄像机竖直方向上旋转
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * angularSpeed * verticalRotateSensitivity);
    }

    void SetCursorToCentre()
    {
        //锁定鼠标后再解锁，鼠标将自动回到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        //隐藏鼠标
        Cursor.visible = false;
    }

    //void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rigidbody.AddForce(Vector3.up * jumpForce);
    //    }
    //}
}
