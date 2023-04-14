using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowButtonControl : MonoBehaviour
{
    public ButtonState buttonState = ButtonState.未完成;
    Button button;
    Image image;

    private void Awake()
    {
        button = this.GetComponent<Button>();
        image = this.GetComponent<Image>();
        button.onClick.AddListener(()=>{ OnClick(); });
    }
    public void UpdateState(ButtonState state)
    {
        switch (state)
        {
            case ButtonState.未完成:
                button.enabled = false;
                image.sprite = Resources.Load<Sprite>("ButtonImage/未完成");
                break;
            case ButtonState.进行中:
                button.enabled = true;
                image.sprite = Resources.Load<Sprite>("ButtonImage/进行中");
                break;
            case ButtonState.已完成:
                button.enabled = true;
                image.sprite = Resources.Load<Sprite>("ButtonImage/已完成");
                Transform resetImage = transform.GetChild(0);
                if (resetImage.name== "Image")
                {
                    resetImage.GetComponent<Image>().color = Color.white;
                }
                break;
        }
        buttonState = state;
    }
    void OnClick()
    {
        if(name!="1"&& name != "2" && name != "3")
        {
            int index = transform.GetSiblingIndex();
            GameObject ui = transform.parent.GetChild(index + 1).gameObject;
            foreach (Transform tran in transform.parent)
            {
                if (!tran.GetComponent<FlowButtonControl>() && tran.gameObject != ui && tran.gameObject.activeSelf)
                {
                    tran.gameObject.SetActive(false);
                }
            }
            if (buttonState == ButtonState.进行中)
            {
                ui.SetActive(!ui.activeSelf);
            }
            else if(buttonState == ButtonState.已完成)
            {
                GongduanType nowGongdaun = GongduanType.Null;
                switch (name)
                {
                    case "4":
                        nowGongdaun = GongduanType.真空速凝炉;
                        break;
                    case "5":
                        nowGongdaun = GongduanType.氢破炉;
                        break;
                    case "6":
                        nowGongdaun = GongduanType.气流磨;
                        break;
                    case "7":
                        nowGongdaun = GongduanType.取向成型;
                        break;
                    case "8":
                        nowGongdaun = GongduanType.烧结;
                        break;
                    case "9":
                        nowGongdaun = GongduanType.回火;
                        break;
                    case "10":
                        nowGongdaun = GongduanType.测试;
                        break;
                }
                TestProcessControl._Instance.TiaozhuanTest(nowGongdaun);
            }
        }
    }
}
