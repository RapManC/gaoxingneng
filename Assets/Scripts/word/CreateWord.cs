using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NPOI.XWPF.UserModel;

public class CreateWord : MonoBehaviour
{
    private string filePath;//保存路径
    private string fileName = "test.docx";//文件名称
    private string path;//最终合成路径
    private XWPFDocument doc = new XWPFDocument();//新建word文档
    void Start()
    {
        filePath = Application.streamingAssetsPath + @"/Word";//设置路径
        path = Path.Combine(filePath, fileName);//组合路径
        CreateTestPara("测试文档");
    }
    //创建方法
    private void CreateTestPara(string _content)
    {
        XWPFParagraph paragraph = doc.CreateParagraph();//设置段落
        paragraph.Alignment = ParagraphAlignment.CENTER;//设置段落对齐方式
        paragraph.SetNumID("1");//设置段落编号
        XWPFRun run = paragraph.CreateRun();//设置文本对象
        run.FontSize = 20;//设置字体大小
        run.SetColor("33CC00");//设置字体颜色
        run.FontFamily = "宋体";//设置字体格式
        run.SetText(_content);//设置字体内容
        FileStream fs = new FileStream(path, FileMode.Create);//通过FileStream创建文件
        doc.Write(fs);//将文档写入文档
        fs.Close();
        fs.Dispose();
        Debug.Log("创建成功");
    }
}
