using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMag : MonoBehaviour
{
    //关闭
    public Button btn1;
    //提示
    public Button btn2;
    public Button btn3;
    public GameObject tips_game;
    //关闭开启列车移动
    public Button btn4;
    private Animator anim;
    public GameObject game1;
    public Text text;
    public AudioSource audiosource1;
    private int i = 0;
    private List<string> wenzxi = new List<string>();


    private void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("列车").GetComponent<Animator>();
    }
    private void Start()
    {
        //game1.SetActive(true);
        anim.SetTrigger("isTir");
        SetList();
        audiosource1.Play();
        audiosource1.pitch = 1.1f;
        StartCoroutine(SetText());
        btn1.onClick.AddListener(()=> {
            StartCoroutine(LoadScene("Mian"));
        });
        btn2.onClick.AddListener(()=> {
            tips_game.SetActive(true);
        });
        btn3.onClick.AddListener(() => {
            tips_game.SetActive(false);
        });
        btn4.onClick.AddListener(() => {
            game1.SetActive(false);
        });
       
    }


    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IEnumerator LoadScene(string name)
    {
        //异步加载场景
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //unity内部的协程协调器法线是异步加载类型的返回对象就会等待
        //等待异步加载结束后才会继续执行迭代器函数中后面的步骤
        yield return ao;
    }

    public void SetList()
    {
        wenzxi.Add("江西兴国县永磁磁浮技术工程示范线线路是由江西理工大学牵头,与兴国县人民政府联合中铁六院、中铁工业、国家稀土功能材料创新中心等单位共同完成的永磁磁浮轨道交通工程试验线");
        wenzxi.Add("正线长度约 800 米，均为钢构高架线磁浮列车采用 2 辆编组,载客能力为座席 32 个定员88人,最高设计运行速度为80km/h作为国内第一条永磁悬浮技术的应用示范");
        wenzxi.Add("该项目兼具科研创新性、挑战性和示范性，其顺利推进对于发展江西稀土永磁产业链和红色旅游经济具有重要意义。2022年8月9日，世界首条永磁磁浮轨道交通工程试验线在江西兴国举行竣工仪式。");
    }

    IEnumerator SetText()
    {
        while (i<=2)
        {
            print(i);
            text.text = wenzxi[i];
            yield return new  WaitForSeconds(14f);
            i++;
            if (i == 3)
            {
                text.gameObject.SetActive(false);
            }
        }
    }

}
