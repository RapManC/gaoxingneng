using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_KaoHe : MonoBehaviour
{
    public Button perssBtn;
    public Text deFen;
    public List<ToggleGroup> toggleGroups = new List<ToggleGroup>();
    public List<Toggle> toggles = new List<Toggle>();
    public List<string> daAns = new List<string>();
    private int fenShu;
    private int[] qi = { 0,0,0};
    private void Start()
    {
        deFen.text = "";
        perssBtn.onClick.AddListener(TiJiao);
        
    }

    /// <summary>
    /// 提交数据
    /// </summary>
    public void TiJiao()
    {
        bool iszhengque;
        for (int i = 0; i < 10; i++)
        {
            if (i != 6)
            {
                foreach (var item in toggleGroups[i].ActiveToggles())
                {

                    if (item.isOn)
                    {
                        print(3);
                        if (item.name == daAns[i])
                        {
                            fenShu += 10;
                            item.transform.GetChild(1).GetComponent<Text>().color = Color.green;
                        }
                        else
                        {
                            item.transform.GetChild(1).GetComponent<Text>().color = Color.red;
                        }
                    }


                }
            }
            else
            {
                if (toggles[0].isOn)
                {
                    qi[0] += 1;
                }
                if (toggles[1].isOn)
                {
                    qi[1] += 1;
                }
                if (toggles[2].isOn)
                {
                    qi[2] += 1;
                }
                    
                //多选题赋分
                if (qi[0]+qi[1]+qi[2] == 3)
                {
                    fenShu += 10;
                }
                else
                {
                    if (qi[0] == 0)
                    {
                        toggles[0].transform.GetChild(1).GetComponent<Text>().color = Color.red;
                    }
                    else
                    {
                        toggles[0].transform.GetChild(1).GetComponent<Text>().color = Color.green;
                    }
                    if (qi[1] == 0)
                    {
                        toggles[1].transform.GetChild(1).GetComponent<Text>().color = Color.red;
                    }
                    else
                    {
                        toggles[1].transform.GetChild(1).GetComponent<Text>().color = Color.green;
                    }
                    if (qi[2] == 0)
                    {
                        toggles[2].transform.GetChild(1).GetComponent<Text>().color = Color.red;
                    }
                    else
                    {
                        toggles[2].transform.GetChild(1).GetComponent<Text>().color = Color.green;
                    }
                }
            }
            deFen.text = fenShu.ToString();
        }
    }
    }

