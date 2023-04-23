using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas3Dto2D : MonoBehaviour
{
    #region 声明单例
    private static Canvas3Dto2D instance;
    public static Canvas3Dto2D Instance => instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }
    #endregion

    private Button EnlargeButton;
    private Button ShrinkButton;
    private RawImage CanvasImage;

    public Transform CanvasCamera;
    public List<Transform> CameraPoints;
    // Start is called before the first frame update
    void Start()
    {
        EnlargeButton = transform.Find("Enlarge").GetComponent<Button>();
        ShrinkButton = transform.Find("Shrink").GetComponent<Button>();
        CanvasImage = transform.Find("RawImage").GetComponent<RawImage>();
        EnlargeButton.onClick.AddListener(() => SetCanvasEnlarge(true));
        ShrinkButton.onClick.AddListener(() => SetCanvasEnlarge(false));
        SetCanvasEnlarge(false);
        SetCanvasActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCanvasEnlarge(bool active)
    {
        EnlargeButton.gameObject.SetActive(!active);
        ShrinkButton.gameObject.SetActive(active);
        CanvasImage.gameObject.SetActive(active);
    }

    public void SetCanvasActive(bool active) { 
        gameObject.SetActive(active);
    }

    public void SetCameraTrans(Transform trans) {
        CanvasCamera.parent = trans;
        CanvasCamera.localPosition = Vector3.zero;
        CanvasCamera.localEulerAngles = Vector3.zero;
    }

    public void SetCameraTrans(int index)
    {
        CanvasCamera.parent = CameraPoints[index];
        CanvasCamera.localPosition = Vector3.zero;
        CanvasCamera.localEulerAngles = Vector3.zero;
        SetCanvasActive(true);
        SetCanvasEnlarge(true);
    }
}
