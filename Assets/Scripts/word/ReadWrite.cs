using System.Collections.Generic;
using UnityEngine;
using NPOI.XWPF.UserModel;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

public class zong
{
    public string table1;
    public string table2;
    public string table3;
    public string table4;
    public string table5;
    public string table6;
    public string table7;
}

public class ReadWrite:BaseMonoBehaviour<ReadWrite>
{
    //文件名称
    //private  string wordName = "";

    /// <summary>
    /// 存放word中需要替换的关键字以及对应要更改的内容
    /// </summary>
    public Dictionary<string, string> DicWord = new Dictionary<string, string>();

    /// <summary>
    /// Word模板路径
    /// </summary>
    //private string path = Application.streamingAssetsPath + "/11.docx";
    private string path = Application.streamingAssetsPath + "/11.docx";

    /// <summary>
    /// 更改后的文件路径
    /// P.S. 当然可以直接修改模板文件，这里另外生成文件是个人为了方便测试。
    /// </summary>
    //private string targetPath = Application.persistentDataPath + "/实验结束数据报告.docx";
    private string targetPath ;

    List<zong> zongs = new List<zong>();

    private string[] data1 = new  string[7];
    private string[] data2 = new  string[7];
    private string[] data3 = new  string[7];
    private string[] data4 = new  string[7];
    private string[] data5 = new  string[7];
    private string[] data6 = new  string[7];
    private string[] data7 = new  string[7];
    private string[] data8 = new  string[7];
    private string[] data9 = new  string[7];
    private string[] data10 = new  string[7];
    private string[] data11 = new  string[7];


    public ScriptObjData objData;
    private void Start()
    {
        //targetPath = Application.persistentDataPath + "/实验结束数据报告.docx";
       
    }
    /// <summary>
    /// 设置word文档
    /// </summary>
    /// <param name="data"></param>
    public void SetWordData()
    {
        DicWord.Clear();
        targetPath = UIManage.Instance.setpathInput.text;
        //时间

        DicWord.Add("$1$", UIManage.Instance.timeString[0]);
        DicWord.Add("$2$", UIManage.Instance.timeString[1]);
        DicWord.Add("$3$", UIManage.Instance.timeString[2]);
        DicWord.Add("$4$", UIManage.Instance.timeString[3]);
        DicWord.Add("$5$", UIManage.Instance.timeString[4]);
        DicWord.Add("$6$", UIManage.Instance.timeString[5]);
        DicWord.Add("$7$", UIManage.Instance.timeString[6]);
        DicWord.Add("$8$", UIManage.Instance.timeString[7]);
        DicWord.Add("$9$", UIManage.Instance.timeString[8]);
        DicWord.Add("$10$", UIManage.Instance.timeString[9]);
        DicWord.Add("$11$", UIManage.Instance.timeString[10]);
        DicWord.Add("$12$", UIManage.Instance.timeString[11]);
        
        //分数
        DicWord.Add("&1&", UIManage.Instance.scorce[0].ToString());
        DicWord.Add("&2&", UIManage.Instance.scorce[1].ToString());
        DicWord.Add("&3&", UIManage.Instance.scorce[2].ToString());
        DicWord.Add("&4&", UIManage.Instance.scorce[3].ToString());
        DicWord.Add("&5&", UIManage.Instance.scorce[4].ToString());
        DicWord.Add("&6&", UIManage.Instance.scorce[5].ToString());
        DicWord.Add("&7&", UIManage.Instance.scorce[6].ToString());
        DicWord.Add("&8&", UIManage.Instance.scorce[7].ToString());
        DicWord.Add("&9&", UIManage.Instance.scorce[8].ToString());
        DicWord.Add("&10&", UIManage.Instance.scorce[9].ToString());
        DicWord.Add("&11&", UIManage.Instance.scorce[10].ToString());
        DicWord.Add("&12&", UIManage.Instance.scorce[11].ToString());
        //DicWord.Add("&12&", data.steps[11].score.ToString());


        ReplaceKeyword();



    }

    public void TestWordData(Score data)
    {
        //时间
        //DicWord.Add("$1$", data.steps[0].timeUsed.ToString());
        //DicWord.Add("$2$", data.steps[1].timeUsed.ToString());
        //DicWord.Add("$3$", data.steps[2].timeUsed.ToString());
        //DicWord.Add("$4$", data.steps[3].timeUsed.ToString());
        //DicWord.Add("$5$", data.steps[4].timeUsed.ToString());
        //DicWord.Add("$6$", data.steps[5].timeUsed.ToString());
        //DicWord.Add("$7$", data.steps[6].timeUsed.ToString());
        //DicWord.Add("$8$", data.steps[7].timeUsed.ToString());
        //DicWord.Add("$9$", data.steps[8].timeUsed.ToString());
        //DicWord.Add("$10$", data.steps[9].timeUsed.ToString());
        //DicWord.Add("$11$", data.steps[10].timeUsed.ToString());
        //DicWord.Add("$12$", data.steps[11].timeUsed.ToString());
        //分数
        //DicWord.Add("&1&", "2");
        //DicWord.Add("&2&", "2");
        //DicWord.Add("&3&", "2");
        //DicWord.Add("&4&", );
        //DicWord.Add("&5&", );
        //DicWord.Add("&6&", );
        //DicWord.Add("&7&", );
        //DicWord.Add("&8&", );
        //DicWord.Add("&9&", data.steps[8].score.ToString());
        //DicWord.Add("&10&", data.steps[9].score.ToString());
        //DicWord.Add("&11&", data.steps[10].score.ToString());
        //DicWord.Add("&12&", data.steps[11].score.ToString());
        //DicWord.Add("&12&", data.steps[11].score.ToString());
        //
        ReplaceKeyword();
    }

    [ContextMenu("111")]
    public void WriteWebData()
    {
        ChuShiHua();


        for (int i = 0; i <= 11; i++)
        {
            if (i != 0)
            {
                zongs[i].table4 = UIManage.Instance.timeString[i];
                zongs[i].table7 = UIManage.Instance.scorce[i].ToString();
            }
        }
        foreach (var item in zongs)
        {
            print(item.table1);
            print(item.table2);
            print(item.table3);
            print(item.table4);
            print(item.table5);
            print(item.table6);
            print(item.table7);
        }

        //赋值
        //将数据对象 序列化为 json字符串
         string str = JsonConvert.SerializeObject(zongs);
        print(str);
        //把数据序列化后的结果 存入指定路径当中
        //File.WriteAllText(Application.persistentDataPath + "/testJson.json", str);
        //print(Application.persistentDataPath);
        Application.ExternalCall("jsonHandler", str);
      //  UnityEngine.Debug.LogError(str);

    }

    public void ChuShiHua()
    {
        zong z0 = new zong();
        zong z1 = new zong();
        zong z2 = new zong();
        zong z3 = new zong();
        zong z4 = new zong();
        zong z5 = new zong();
        zong z6 = new zong();
        zong z7 = new zong();
        zong z8 = new zong();
        zong z9 = new zong();
        zong z10 = new zong();
        zong z11 = new zong();

        z0.table1 = "步骤序号";
        z0.table2 = "步骤目标要求";
        z0.table3 = "步骤合理用时（s）";
        z0.table4 = "实验开始结束时间（s）";
        z0.table5 = "目标达成度赋分模型";
        z0.table6 = "步骤满分";
        z0.table7 = "步骤分数";

        z1.table1 = "1";
        z1.table2 = "材料牌号选择：选择需要制备的材料牌号，观察不同牌号性能间的差别";
        z1.table3 = "0.5";
        z1.table4 = "";
        z1.table5 = "完成牌号选择操作，得2分";
        z1.table6 = "2";
        z1.table7 = "";

        z2.table1 = "2";
        z2.table2 = "元素成分确定：永磁材料成分选取判断，深入理解稀土元素对的影响规律";
        z2.table3 = "1.5";
        z2.table4 = "";
        z2.table5 = "稀土元素和过渡族金属元素的选定，每错误一次扣1分，共3分";
        z2.table6 = "3";
        z2.table7 = "";

        z3.table1 = "3";
        z3.table2 = "原料配比计算：计算各类永磁材料的稀土元素质量百分数";
        z3.table3 = "3";
        z3.table4 = "";
        z3.table5 = "真空速凝炉主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；温度、转速每项满分4分；薄片厚度、生产能耗每项满分3分；";
        z3.table6 = "5";
        z3.table7 = "";

        z4.table1 = "4";
        z4.table2 = "真空速凝炉工段：操作设备进行参数设置，观察原料熔炼、中间包加热、浇铸、破碎等过程，将熔化的合金液浇铸成一定形状和尺寸";
        z4.table3 = "8";
        z4.table4 = "";
        z4.table5 = "真空速凝炉主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；温度、转速每项满分4分；薄片厚度、生产能耗每项满分3分；";
        z4.table6 = "14";
        z4.table7 = "";

        z5.table1 = "5";
        z5.table2 = "氢破炉工段：进行升温、抽真空、加压等控制操作，使氢气与稀土永磁材料合金铸锭在特定温度和压强下发生歧化反应，结合透明模式观察合金膨胀断裂，得到细颗粒粉体过程";
        z5.table3 = "6";
        z5.table4 = "";
        z5.table5 = "氢破炉主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；温度、压力、时间设置每项满分4分；生产能耗满分3分；";
        z5.table6 = "15";
        z5.table7 = "";

        z6.table1 = "6";
        z6.table2 = "靶式气流磨工段：完成气流磨设备控制，气流将粉末颗粒加速到超声速，使之相互对撞而破碎成尺寸粒径更小的合金粉体，获取成分、尺寸及颗粒外形总体上达到均匀一致合金粉末";
        z6.table3 = "5";
        z6.table4 = "";
        z6.table5 = "靶式气流磨主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；压力、转速每项满分4分；生产能耗满分3分；";
        z6.table6 = "11";
        z6.table7 = "";

        z7.table1 = "7";
        z7.table2 = "取向成型压机工段：操作设备使粉末颗粒压制成一定形状和尺寸的毛坯，并将其沿易磁化方向进行取向，得到各向异性磁体，通过冷等静压在高压腔体内获得超高压，使粉末进一步致密化";
        z7.table3 = "6";
        z7.table4 = "";
        z7.table5 = "取向成型主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；压力、磁场每项满分4分，生产能耗满分3分；";
        z7.table6 = "11";
        z7.table7 = "";

        z8.table1 = "8";
        z8.table2 = "真空烧结炉工段：设置参数进行高温烧结流程，将具有高能状态的毛坯加热到粉末基体相熔点以下进行热处理，消除内部应力，促进磁体致密化磁体致密化";
        z8.table3 = "8";
        z8.table4 = "";
        z8.table5 = "取向成型主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；压力、磁场每项满分4分，生产能耗满分3分；";
        z8.table6 = "15";
        z8.table7 = "";

        z9.table1 = "9";
        z9.table2 = "二次真空烧结炉工段：设置低温条件参数进行回火操作，促进磁体内部富稀土相均匀分布，使表面更加连续平滑";
        z9.table3 = "10";
        z9.table4 = "";
        z9.table5 = "真空烧结炉回火主要参数控制在标准范围，通过计算偏差比例逐级增扣1分；一级回火温度、一级回火时间、二级回火温度、二级回火时间每项满分4分；生产能耗满分3分；";
        z9.table6 = "16";
        z9.table7 = "";

        z10.table1 = "10";
        z10.table2 = "磁性能检测：利用高温永磁测量系统对磁体的磁性能参数进行测定";
        z10.table3 = "2";
        z10.table4 = "";
        z10.table5 = "性能需控制选择牌号范围，范围偏差越大，扣分比例越大，满分5分";
        z10.table6 = "3";
        z10.table7 = "";

        z11.table1 = "11";
        z11.table2 = "知识考核：沉淀实验结果，考察实验成果";
        z11.table3 = "5";
        z11.table4 = "";
        z11.table5 = "随机获取题库中试题，完成知识考核";
        z11.table6 = "5";
        z11.table7 = "";

        zongs.Add(z0);
        zongs.Add(z1);
        zongs.Add(z2);
        zongs.Add(z3);
        zongs.Add(z4);
        zongs.Add(z5);
        zongs.Add(z6);
        zongs.Add(z7);
        zongs.Add(z8);
        zongs.Add(z9);
        zongs.Add(z10);
        zongs.Add(z11);
    }

    /// <summary>
    /// 替换关键字
    /// </summary>
    private void ReplaceKeyword()
    {
        using (FileStream stream = File.OpenRead(path))
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            XWPFDocument doc = new XWPFDocument(fs);

            //遍历段落                  
            foreach (var para in doc.Paragraphs)
            {
                string oldText = para.ParagraphText;
                if (oldText != "" && oldText != string.Empty && oldText != null)
                {
                    string tempText = para.ParagraphText;

                    foreach (KeyValuePair<string, string> kvp in DicWord)
                    {
                        if (tempText.Contains(kvp.Key))
                        {
                            tempText = tempText.Replace(kvp.Key, kvp.Value);

                            para.ReplaceText(oldText, tempText);
                        }
                    }

                }
            }

            //遍历表格      
            var tables = doc.Tables;
            foreach (var table in tables)
            {
                foreach (var row in table.Rows)
                {
                    foreach (var cell in row.GetTableCells())
                    {
                        foreach (var para in cell.Paragraphs)
                        {
                            string oldText = para.ParagraphText;
                            if (oldText != "" && oldText != string.Empty && oldText != null)
                            {
                                //记录段落文本
                                string tempText = para.ParagraphText;
                                foreach (KeyValuePair<string, string> kvp in DicWord)
                                {
                                    if (tempText.Contains(kvp.Key))
                                    {
                                        tempText = tempText.Replace(kvp.Key, kvp.Value);

                                        //替换内容
                                        para.ReplaceText(oldText, tempText);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!File.Exists(targetPath))
            {
                //生成指定文件
                FileStream output = new FileStream(targetPath, FileMode.Create);
                //将文档信息写入文件
                doc.Write(output);

                //一些列关闭释放操作
                fs.Close();
                fs.Dispose();
                output.Close();
                output.Dispose();
            }
            else
            {
                //删除指定文件
                File.Delete(targetPath);

                //生成指定文件
                FileStream output = new FileStream(targetPath, FileMode.Create);
                //将文档信息写入文件
                doc.Write(output);

                //一些列关闭释放操作
                fs.Close();
                fs.Dispose();
                output.Close();
                output.Dispose();
            }
           

            //Process.Start(targetPath);//打开指定文件
        }
    }
}