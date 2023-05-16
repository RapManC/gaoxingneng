using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JieshaoManager : MonoBehaviour
{
    public Button LastButton;
    public Button NextButton;
    public Text BottomText;
    public Image ContentImage;
    public RectTransform ContentRect;
    public ScrollRect ScrollRect;
    public GameObject RootContent;
    public ScriptableObject ItemsData;
    protected List<JieshaoItem> Items = new List<JieshaoItem>();
    private int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        LastButton.onClick.AddListener(SelectLastItem);
        NextButton.onClick.AddListener(SelectNextItem);
        InitItems();
        ChangeItemByIndex(0);
    }

    protected void OnEnable()
    {
        if (name == "Zhibei")
        {
            LoadSceneManager.Instance.SetButtonActive("进入工业设备学习", "Xuexi_Button");
        }
    }

    public void SelectLastItem()
    {
        if (currentIndex <= 0) return;
        ChangeItemByIndex(currentIndex - 1);
    }

    public void SelectNextItem()
    {
        if (currentIndex >= Items.Count - 1) return;
        ChangeItemByIndex(currentIndex + 1);
    }

    public void SetButtonState() {
        if (currentIndex == 0)
        {
            LastButton.gameObject.SetActive(false);
        }
        else if (currentIndex > 0 && currentIndex < Items.Count - 1)
        {
            if(!LastButton.gameObject.activeSelf)
                LastButton.gameObject.SetActive(true);
            if (!NextButton.gameObject.activeSelf)
                NextButton.gameObject.SetActive(true);
        }
        else
        {
            NextButton.gameObject.SetActive(false);
        }
    }

    void ResetScrollRect() {
        ScrollRect.content.anchoredPosition = Vector2.zero;
        ScrollRect.velocity = Vector2.zero;
    }

    public virtual void ChangeItemByIndex(int index) {
        currentIndex = index;
        BottomText.text = Items[index].Title;
        ContentImage.sprite = Items[index].ContentImage;
        ContentImage.SetNativeSize();
        ContentRect.sizeDelta = new Vector2(ContentRect.sizeDelta.x, ContentImage.GetComponent<RectTransform>().sizeDelta.y + 20);
        ResetScrollRect();
        SetButtonState();
        if(!RootContent.activeSelf)
            RootContent.SetActive(true);
    }

    public virtual void InitItems() {
        List<JieshaoItem> itemDatas = (ItemsData as JieshaoItemData).Items;
        for (int i = 0; i < itemDatas.Count; i++)
        {
            Items.Add(itemDatas[i]);
        }
    }
}

[System.Serializable]
public class JieshaoItem
{
    public string Title;
    public Sprite ContentImage;
}
