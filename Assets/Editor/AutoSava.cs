using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class AutoSave : EditorWindow
{

    public bool autoSaveScene = true;
    public bool showMessage = true;
    public bool isStarted = false;
    public int intervalScene;
    public DateTime lastSaveTimeScene = DateTime.Now;
    private Scene nowScene;
    [MenuItem("Window/AutoSave")]
    static void Init()
    {
        EditorWindow saveWindow = EditorWindow.GetWindow(typeof(AutoSave));
        saveWindow.minSize = new Vector2(100,100);
        saveWindow.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("信息", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("保存场景:", "" + nowScene.path);
        GUILayout.Label("选择", EditorStyles.boldLabel);
        autoSaveScene = EditorGUILayout.BeginToggleGroup("自动保存", autoSaveScene);
        intervalScene = EditorGUILayout.IntField("时间间隔(秒)", intervalScene);
        if (isStarted)
        {
            EditorGUILayout.LabelField("最后保存:", "" + lastSaveTimeScene);
        }
        EditorGUILayout.EndToggleGroup();
        showMessage = EditorGUILayout.BeginToggleGroup("显示消息", showMessage);
        EditorGUILayout.EndToggleGroup();
    }

    void Update()
    {
        nowScene = EditorSceneManager.GetActiveScene();
        if (autoSaveScene)
        {
            double second = (DateTime.Now - lastSaveTimeScene).TotalSeconds;
            if (second>= intervalScene )
            {
                lastSaveTimeScene = DateTime.Now;
                saveScene();
            }
        }
        else
        {
            isStarted = false;
        }

    }

    void saveScene()
    {
        EditorSceneManager.SaveScene(nowScene);
       
        isStarted = true;
        if (showMessage)
        {
            Debug.Log("自动保存场景: " + nowScene.path + " on " + lastSaveTimeScene);
        }
        AutoSave repaintSaveWindow = (AutoSave)EditorWindow.GetWindow(typeof(AutoSave));
        repaintSaveWindow.Repaint();
    }
}