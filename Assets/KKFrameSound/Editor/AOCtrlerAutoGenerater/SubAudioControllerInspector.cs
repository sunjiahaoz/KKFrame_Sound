using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using KK.Frame.Audio;

namespace KK.KKEditor.AO
{
    [CustomEditor(typeof(KKSingleGameAudioPart))]
    public class SubAudioControllerInspector : Editor
    {
        bool _bReGenerateAO = false;
        string _strFolderPath = string.Empty;
        Rect _rectDraged;

        string _strPathRoot = "";
        KKSingleGameAudioPart _subAo = null;
        Dictionary<string, XCategory> _dictCate = new Dictionary<string, XCategory>();
        void OnEnable()
        {
            _strPathRoot = Application.dataPath.ToLower();            
            _strPathRoot = _strPathRoot.Remove(_strPathRoot.LastIndexOf("assets"));            
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _subAo = (KKSingleGameAudioPart)target;
            if (_subAo == null)
            {
                EditorGUILayout.LabelField("属性错误！！！");
                return;
            }

            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField("正在运行中，不可以修改...");
                return;
            }

            if (_bReGenerateAO = EditorGUILayout.Foldout(_bReGenerateAO, "重新生成AO配置"))
            {
                EditorGUILayout.LabelField("路径");
                _rectDraged = EditorGUILayout.GetControlRect(GUILayout.Width(400));

                _strFolderPath = EditorGUI.TextField(_rectDraged, _strFolderPath);

                if ((Event.current.type == EventType.dragUpdated ||
                    Event.current.type == EventType.DragExited) &&
                _rectDraged.Contains(Event.current.mousePosition))
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                    if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
                    {
                        _strFolderPath = _strPathRoot + DragAndDrop.paths[0];
                    }
                }

                if (_strFolderPath.Length > 0
                    && KK.Frame.Util.SimpleFileProcess.FolderExists(_strFolderPath))
                {
                    if (GUILayout.Button("重新生成"))
                    {                        
                        AOCtrlerAutoGenerate.GenerateData(_strFolderPath, _dictCate);
                        AOCtrlerAutoGenerate.GenerateAOCtrl(_subAo.subAO, _dictCate);

                        Debug.LogWarning("<color=red>" + "[哈哈—哈哈哈哈哈哈哈哈哈哈哈—哈哈]</color>");
                        Debug.Log("<color=green>" + "[____—炫炫炫—(:嘚瑟:)—耀耀耀—____]</color>");
                        Debug.Log("<color=yellow>" + "|_##—炫炫炫—:)成功(:—耀耀耀—##_|</color>");
                        Debug.Log("<color=orange>" + "|_OO—炫炫炫—):功成:(—耀耀耀—OO_|</color>---");
                        Debug.Log("<color=blue>" + "[____—炫炫炫—:(嘚瑟):—耀耀耀—____]</color>---");
                        Debug.LogWarning("<color=red>" + "[哈哈—哈哈哈哈哈哈哈哈哈哈哈—哈哈]</color>");
                    }
                }
            }
        }
    }
}
