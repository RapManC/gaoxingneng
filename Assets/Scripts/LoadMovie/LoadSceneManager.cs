using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class LoadSceneManager : MonoBehaviour
{
    #region 声明单例
    private static LoadSceneManager instance;
    public static LoadSceneManager Instance => instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(transform.gameObject);
            return;
        }
    }
    #endregion

    private bool isFirstOpen = true;
    private string TargetName;

    private AsyncOperation loadOperation;

    [HideInInspector] public GameObject NextButton;
    [HideInInspector] public Text NextButtonName;

    private void Start()
    {
        NextButton.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Mian" && isFirstOpen)
        {
            isFirstOpen = false;

            GameObject obj = GameObject.Find("Root");
            if (obj != null)
            {
                obj.GetComponent<HTTP>().StartInit();
            }
        }
    }

    public void SetButtonActive(string buttonName, string targetName)
    {
        if(NextButton.activeSelf && targetName == TargetName) return;
        NextButtonName.text = buttonName;
        TargetName = targetName;
        NextButton.SetActive(true);
    }

    public void HideNextButton() {
        NextButton.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "honggui")
        {
            // 异步加载下一个场景并存储操作
            loadOperation = SceneManager.LoadSceneAsync("Mian");

            // 注册一个回调，当场景加载完成时调用
            loadOperation.completed += OnSceneLoaded;
        }
        else
        {
            TransScene();
        }

        HideNextButton();
    }

    // 当场景加载完成时调用这个方法
    void OnSceneLoaded(AsyncOperation op)
    {
        TransScene();
    }

    void TransScene() {
        // 在加载的场景中找到目标游戏对象
        Transform obj = GameObject.Find("Root").transform.Find("Canvas/Panel_UI/" + TargetName);
        if (obj != null)
            obj.GetComponent<Button>().onClick.Invoke();
    }
}


