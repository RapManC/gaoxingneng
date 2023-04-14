using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowsOperateUtil {

	// Use this for initialization
    public static  void SelectDir(Action <string > callback)
    {
        OpenDialogDir ofn2 = new OpenDialogDir();
        ofn2.pszDisplayName = new string(new char[2000]); ;     // 存放目录路径缓冲区  
        ofn2.lpszTitle = "Open Project";// 标题  
        //ofn2.ulFlags = BIF_NEWDIALOGSTYLE | BIF_EDITBOX; // 新的样式,带编辑框  
        IntPtr pidlPtr = DllOpenFileDialog.SHBrowseForFolder(ofn2);

        char[] charArray = new char[2000];
        for (int i = 0; i < 2000; i++)
            charArray[i] = '\0';

        DllOpenFileDialog.SHGetPathFromIDList(pidlPtr, charArray);
        string fullDirPath = new String(charArray);
        fullDirPath = fullDirPath.Substring(0, fullDirPath.IndexOf('\0'));
      //  Debug.Log(fullDirPath);//这个就是选择的目录路径。
        if (callback != null)
        {
            callback(fullDirPath);
        }

    }

    public static void OpenProject(Action<string > callback )
    {
        OpenDialogFile pth = new OpenDialogFile();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        pth.filter = "txt (*.txt)";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath; // default path  
        pth.title = "打开项目";
        pth.defExt = "txt";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        //0x00080000   是否使用新版文件选择窗口
        //0x00000200   是否可以多选文件
        if (DllOpenFileDialog.GetOpenFileName(pth))
        {
            string filepath = pth.file; //选择的文件路径;  
           // Debug.Log(filepath);
            if (callback != null)
            {
                callback(filepath);
            }
        }

    }

    public static void SaveProject(Action<string > callback )
    {
        OpenDialogFile pth = new OpenDialogFile();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        pth.filter = "txt (*.txt)";
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath; // default path  
        pth.title = "保存项目";
        pth.defExt = "txt";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (DllOpenFileDialog.GetSaveFileName(pth))
        {
            string filepath = pth.file; //选择的文件路径;  
            if (callback != null)
            {
                callback(filepath);
            }
           // Debug.Log(filepath);
        }
    }
}
