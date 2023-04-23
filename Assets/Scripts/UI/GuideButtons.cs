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
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
