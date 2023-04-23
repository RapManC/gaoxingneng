using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewRawImage : RawImage, IPointerClickHandler
{
    // 点击RawImage时，相对RawImage自身的坐标
    private Vector2 ClickPosInRawImg;
    // 预览映射相机
    private Camera PreviewCamera;

    void Start()
    {
        // 初始获取预览映射相机
        if (PreviewCamera == null)
        {
            PreviewCamera = GameObject.Find("CanvasCamera").GetComponent<Camera>();
        }
    }

    void Update()
    {
//#if UNITY_EDITOR
//        CheckDrawRayLine(false);
//#endif
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // 获取相对RawImage的点击坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out ClickPosInRawImg))
        {
            // Lylibs.MessageUtil.Show("坐标：x=" + ClickPosInRawImg.x + ",y=" + ClickPosInRawImg.y);
            CheckDrawRayLine(true);
        }
    }

    // 检查是否需要绘制射线
    void CheckDrawRayLine(bool isCallBack)
    {
        if (PreviewCamera != null && ClickPosInRawImg != null)
        {
            //获取预览图的长宽
            float p_imageWidth = rectTransform.rect.width;
            float p_imageHeight = rectTransform.rect.height;

            //获取在预览映射相机viewport内的坐标（坐标比例）
            float p_x = ClickPosInRawImg.x / p_imageWidth;
            float p_y = ClickPosInRawImg.y / p_imageHeight;
            Debug.Log(new Vector2(p_x, p_y));

            //从预览映射相机视口坐标发射线
            Ray ray = PreviewCamera.ViewportPointToRay(new Vector2(p_x, p_y));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Button button = hitInfo.collider.GetComponent<Button>();
                if (button != null)
                    button.onClick.Invoke();
            }
        }
    }
}
