using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    private Transform targetTrans;
    //是否在自动漫游状态
    private bool isAutoPlay;
    //是否暂停了自动漫游状态
    private bool isPauseAutoPlay;
    public float MaxPauseTime = 2f;
    private float pauseTime;

    public float CheckDistance = 0.2f;
    public float CheckAngle = 60f;
    public float MaxMoveSpeed = 5;
    public float MaxRotateSpeed = 60;
    //第二阶段每秒钟位移数
    private Vector2 moveSpeed;
    private float rotateSpeed;

    private float moveTime;

    private Action OnMoveEndAction;

    private Coroutine transCor;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        if (targetTrans != null)
            DelayAutoMove(2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAutoPlay)
        {
            if (isPauseAutoPlay)
            {
                pauseTime -= Time.deltaTime;
                if (pauseTime <= 0)
                { 
                    isPauseAutoPlay = false;
                    StartAutoPlay();
                }
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Input.GetMouseButton(1))
            {
                PauseAutoPlay();
            }
        }
    }


    #region 自动位移
    public void SetAutoPlayTarget(Transform trans, Action onMoveEnd = null)
    {
        targetTrans = trans;
        OnMoveEndAction = onMoveEnd;
    }

    public void DelayAutoMove(float delay = 2f)
    {
        StartCoroutine(DelayStartMove(delay));
    }

    IEnumerator DelayStartMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetAutoPlayActive(true);
    }

    public void SetAutoPlayActive(bool active)
    {
        StopLastAutoPlayCor();
        if (active)
            StartAutoPlay();
        else
            StopAutoPlay();
    }

    private void StartAutoPlay() {
        if (targetTrans == null)
        {
            isAutoPlay = false; 
            return;
        } 
        isAutoPlay = true;
        StopLastAutoPlayCor();

        isPauseAutoPlay = false;
        pauseTime = 0;
        transCor = StartCoroutine(StartAutoTrans());
    }

    private void StopAutoPlay()
    {
        isAutoPlay = false;
        targetTrans = null;
    }

    IEnumerator StartAutoTrans() {
        Vector2 directionDifference = OverlookHeight(targetTrans.position - transform.position);
        Debug.Log(directionDifference.magnitude);
        //与目标距离太进则直接进入第三阶段
        if (directionDifference.magnitude > CheckDistance)
        {
            //先旋转至看到目标
            Vector3 RotateDirection = targetTrans.position - transform.position;
            RotateDirection.y = 0;
            while (!CanSeeTarget())
            {
                transform.forward = Vector3.RotateTowards(transform.forward, RotateDirection, MaxRotateSpeed * Time.deltaTime * 0.02f, 0);
                yield return null;
            }

            moveTime = directionDifference.magnitude / MaxMoveSpeed;
            moveSpeed = directionDifference / moveTime;
            //走过去
            while (moveTime > 0)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed.x * Time.deltaTime, transform.position.y, transform.position.z + moveSpeed.y * Time.deltaTime);
                moveTime -= Time.deltaTime;
                yield return null;
            }
        }

        //旋转至目标朝向
        float rotateDifference = GetStandardY((targetTrans.eulerAngles.y - transform.eulerAngles.y));
        moveTime = Mathf.Abs(rotateDifference) / MaxRotateSpeed;
        rotateSpeed = rotateDifference / moveTime;
        while (moveTime > 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotateSpeed * Time.deltaTime, 0);
            moveTime -= Time.deltaTime;
            yield return null;
        }

        //传送结束
        StopAutoPlay();
        OnMoveEndAction?.Invoke();
        OnMoveEndAction = null;
    }

    private void StopLastAutoPlayCor() {
        if (transCor != null)
            StopCoroutine(transCor);
    }

    private void PauseAutoPlay() {
        pauseTime = MaxPauseTime;
        isPauseAutoPlay = true;
        StopLastAutoPlayCor();
    }

    private bool CanSeeTarget() {
        return Vector2.Angle(OverlookHeight(transform.forward), OverlookHeight(targetTrans.position - transform.position)) <= CheckAngle;
    }
    #endregion

    private Vector2 OverlookHeight(Vector3 vector3) {
        return new Vector2(vector3.x, vector3.z);
    }

    private float GetStandardY(float y)
    {
        y = y % 360;
        if (y >= -180 && y <= 180)
            return y;
        else if (y > 180)
            return y -= 360;
        else
            return y += 360;
    }
}
