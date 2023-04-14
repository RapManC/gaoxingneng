using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PipeFlow)), CanEditMultipleObjects]
public class PipeFlowEditor : Editor
{
    //private PipeFlow _pipeFlow;
    //private int _currentIndex = -1;
    //private bool _showInEditor = true;
    //private void OnEnable()
    //{
    //    _pipeFlow = target as PipeFlow;
    //}
    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    EditorGUILayout.BeginVertical("HelpBox");
    //    EditorGUILayout.BeginHorizontal();
    //    GUILayout.Label("FlowPath");
    //    _showInEditor = GUILayout.Toggle(_showInEditor, "Show In Editor");
    //    EditorGUILayout.EndHorizontal();
    //    if (_showInEditor)
    //    {
    //        EditorGUILayout.BeginHorizontal();
    //        if (GUILayout.Button("路径倒置", "ButtonLeft"))
    //        {
    //            if (EditorUtility.DisplayDialog("提示", "是否将整条路径倒置？", "是的", "我再想想"))
    //            {
    //                if (_pipeFlow.FlowPath.Count > 1)
    //                {
    //                    _pipeFlow.FlowPath.Reverse();
    //                }
    //            }
    //        }
    //        if (GUILayout.Button("清空路径点", "ButtonRight"))
    //        {
    //            if (EditorUtility.DisplayDialog("提示", "是否清空路径点？", "是的", "我再想想"))
    //            {
    //                _pipeFlow.FlowPath.Clear();
    //                _currentIndex = -1;
    //            }
    //        }
    //        EditorGUILayout.EndHorizontal();
    //        for (int i = 0; i < _pipeFlow.FlowPath.Count; i++)
    //        {
    //            EditorGUILayout.BeginHorizontal();
    //            GUI.backgroundColor = _currentIndex == i ? Color.cyan : Color.white;
    //            if (GUILayout.Button("path point" + (i + 1), "prebutton"))
    //            {
    //                _currentIndex = i;
    //                Tools.current = Tool.None;
    //            }
    //            GUI.backgroundColor = Color.white;
    //            if (GUILayout.Button("", "OL Minus", GUILayout.Width(16)))
    //            {
    //                _pipeFlow.FlowPath.RemoveAt(i);
    //                _currentIndex = -1;
    //            }
    //            EditorGUILayout.EndHorizontal();
    //        }
    //        EditorGUILayout.BeginHorizontal();
    //        GUILayout.FlexibleSpace();
    //        if (GUILayout.Button("", "OL Plus", GUILayout.Width(16)))
    //        {
    //            if (_currentIndex != -1)
    //            {
    //                _pipeFlow.FlowPath.Add(_pipeFlow.FlowPath[_currentIndex]);
    //            }
    //            else
    //            {
    //                _pipeFlow.FlowPath.Add(new Vector3(0, 0, 0));
    //            }
    //        }
    //        EditorGUILayout.EndHorizontal();
    //    }
    //    EditorGUILayout.EndVertical();
    //}
    //private void OnSceneGUI()
    //{
    //    if (_showInEditor)
    //    {
    //        Handles.color = Color.cyan;
    //        if (_pipeFlow.FlowPath.Count > 0)
    //        {
    //            Handles.Label(_pipeFlow.FlowPath[0], "[" + _pipeFlow.transform.name + "]起点", "ErrorLabel");
    //        }
    //        if (_pipeFlow.FlowPath.Count > 1)
    //        {
    //            Handles.Label(_pipeFlow.FlowPath[_pipeFlow.FlowPath.Count - 1], "[" + _pipeFlow.transform.name + "]终点", "ErrorLabel");
    //        }
    //        for (int i = 0; i < _pipeFlow.FlowPath.Count; i++)
    //        {
    //            if (i < _pipeFlow.FlowPath.Count - 1)
    //                Handles.DrawLine(_pipeFlow.FlowPath[i], _pipeFlow.FlowPath[i + 1]);
    //        }
    //        if (_currentIndex != -1)
    //        {
    //            _pipeFlow.FlowPath[_currentIndex] = Handles.PositionHandle(_pipeFlow.FlowPath[_currentIndex], Quaternion.identity);
    //        }
    //    }
    //}
}


