using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideButtons : MonoBehaviour
{
    public Sprite _sprite;
    public int GuideAmount;
    // Start is called before the first frame update
    void Start()
    {
        Transform item = transform.GetChild(0);

        for (int i = 0; i < GuideAmount; i++)
        {
            Transform button = Instantiate(item, transform);
            button.gameObject.SetActive(true);
            int index = i;
            button.Find("Text").GetComponent<Text>().text = (i+1).ToString();
            button.GetComponent<Button>().onClick.AddListener(()=> {
                HongguiGuide.Instance.OnClickGuideButton(index);
                button.GetComponent<Image>().sprite = _sprite;
                CheckModelLearnState(index);
            });
        }
    }


    List<int> learnHaibaoIndexList = new List<int>() { 0, 1, 2, 3 };
    void CheckModelLearnState(int index)
    {
        if (learnHaibaoIndexList.Contains(index))
        {
            learnHaibaoIndexList.Remove(index);
            if (learnHaibaoIndexList.Count == 0)
            {
                LoadSceneManager.Instance.SetButtonActive("进入实验介绍", "Jiesao_Button");
            }
        }
    }
}
