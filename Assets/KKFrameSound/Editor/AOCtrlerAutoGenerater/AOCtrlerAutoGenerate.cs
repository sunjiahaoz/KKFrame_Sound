using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace KK.KKEditor.AO
{
    public class AOCtrlerAutoGenerateHelp : EditorWindow
    {
        public static void AddWindow()
        {
            AOCtrlerAutoGenerateHelp window = (AOCtrlerAutoGenerateHelp)EditorWindow.GetWindow<AOCtrlerAutoGenerateHelp>("音效自动配置器帮助");
            window.Show();
        }

        TextAsset _ta = null;

        GUIStyle _style = null;
        Vector2 scrollPos = Vector2.zero;
        void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
            if (_style == null)
            {
                _style = new GUIStyle();
                _style.normal.textColor = Color.cyan;
                _style.fontSize = 16;
            }
            if (_ta == null)
            {
                _ta = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Editor/AOCtrlerAutoGenerater/readme.txt");
            }
            if (_ta != null)
            {
                EditorGUILayout.LabelField(_ta.text, _style);
            }
            else
            {
                EditorGUILayout.LabelField("找不到帮助，看代码吧！");
            }
            EditorGUILayout.EndScrollView();
        }
    }


    public class XSubItem
    {
        public string _strAoFilePath = "";
    }
    public class XItem
    {
        public string strItemName;
        public List<XSubItem> _lstSubItems = new List<XSubItem>();
    }

    public class XCategory
    {
        public string strCategoryName;
        public List<XItem> _lstItems = new List<XItem>();
    }

    public class AOCtrlerAutoGenerate : EditorWindow
    {
        [MenuItem("Tools/音效自动配置器")]
        static void AddWindow()
        {
            AOCtrlerAutoGenerate window = (AOCtrlerAutoGenerate)EditorWindow.GetWindow<AOCtrlerAutoGenerate>("音效自动配置器");
            window.Show();
        }

        AudioController _aoCtrler;
        string _strColorAOSet = "red";

        Vector2 scrollPos = Vector2.zero;
        int _nWidth0 = 150;
        GUIStyle _titleStyle = null;
        string _strFolderRoot = "";

        Dictionary<string, XCategory> _dictName = new Dictionary<string, XCategory>();

        void OnGUI()
        {
            if (_titleStyle == null)
            {
                _titleStyle = new GUIStyle();
                _titleStyle.richText = true;
            }

            AddButton("帮助", () =>
            {
                AOCtrlerAutoGenerateHelp.AddWindow();
            });

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));

            // AoCtrler
            GUILayout.Space(10);
            GUILayout.Label("<color=" + _strColorAOSet + ">将要编辑的AudioController拖进来</color>", _titleStyle);
            GUILayout.Space(10);
            _aoCtrler = EditorGUILayout.ObjectField(_aoCtrler, typeof(AudioController), true, GUILayout.Width(_nWidth0)) as AudioController;
            if (_aoCtrler != null)
            {
                _strColorAOSet = "cyan";
            }
            // 目录
            EditorGUILayout.BeginVertical();
            if (_strFolderRoot != null
               && _strFolderRoot.Length > 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("<color=cyan>" + _strFolderRoot + "</color>", _titleStyle);
                EditorGUILayout.EndHorizontal();
                AddButton("遍历生成", () =>
                {
                    GenerateData(_strFolderRoot, _dictName);
                    GenerateAOCtrl(_aoCtrler, _dictName);
                    ShowNotification(new GUIContent("生成完成！"));
                });
            }
            else
            {
                EditorGUILayout.LabelField("<color=red>" + "选择音效根目录" + "</color>", _titleStyle);
            }
            AddButton("选择音效根目录", () =>
            {
                _strFolderRoot = EditorUtility.OpenFolderPanel("音效根目录", _strFolderRoot.Length > 0 ? _strFolderRoot : Application.dataPath, "");
            });
            EditorGUILayout.EndVertical();


            EditorGUILayout.EndScrollView();
        }

        public static void GenerateAOCtrl(AudioController aoCtrler, Dictionary<string, XCategory> dictData)
        {
            // 增加
            aoCtrler.AudioCategories = new AudioCategory[dictData.Count];
            int nIndex = 0;
            foreach (var category in dictData)
            {
                AudioCategory cat = new AudioCategory(aoCtrler);
                aoCtrler.AudioCategories[nIndex] = cat;
                cat.Name = category.Key;
                nIndex++;

                cat.AudioItems = new AudioItem[category.Value._lstItems.Count];
                for (int i = 0; i < category.Value._lstItems.Count; ++i)
                {
                    AudioItem curItem = new AudioItem();
                    cat.AudioItems[i] = curItem;
                    curItem.Name = category.Value._lstItems[i].strItemName;
                    curItem.subItems = new AudioSubItem[category.Value._lstItems[i]._lstSubItems.Count];

                    for (int j = 0; j < category.Value._lstItems[i]._lstSubItems.Count; ++j)
                    {
                        curItem.subItems[j] = new AudioSubItem();
                        curItem.subItems[j].Clip = AssetDatabase.LoadAssetAtPath<AudioClip>(category.Value._lstItems[i]._lstSubItems[j]._strAoFilePath);
                    }
                }
            }
        }

        void AddButton(string strName, System.Action actionClick)
        {
            if (GUILayout.Button(strName))
            {
                if (actionClick != null)
                {
                    actionClick();
                }
            }
        }

        public static void GenerateData(string strFolderRoot, Dictionary<string, XCategory> dictData)
        {

            dictData.Clear();
            // 先遍历root，root中的所有文件夹就是一个Catergory
            KK.Frame.Util.SimpleFileProcess.TravelFoldersInFolder(strFolderRoot, (filePath) =>
            {
                //Debug.Log("<color=green>[log]</color>---" + Path.GetFileName(filePath));
                string strFolderName = Path.GetFileName(filePath);
                if (!dictData.ContainsKey(strFolderName))
                {
                    XCategory itemCategory = new XCategory();
                    itemCategory.strCategoryName = Path.GetFileName(strFolderName);

                    // 再遍历每一个文件夹，分两种情况
                    // 里面的文件夹
                    KK.Frame.Util.SimpleFileProcess.TravelFoldersInFolder(filePath, (subFolderPath) =>
                    {
                        string strSubFolderName = Path.GetFileName(subFolderPath);
                        XItem item = new XItem();
                        item.strItemName = strSubFolderName;

                        // 每个子文件夹下的所有音频文件都是subItem
                        KK.Frame.Util.SimpleFileProcess.TravelFilesInFolder(subFolderPath, (subFilePath) =>
                        {
                            if (!subFilePath.EndsWith(".wav")
                        && !subFilePath.EndsWith(".ogg")
                        && !subFilePath.EndsWith(".mp3"))
                            {
                                return;
                            }

                            XSubItem subItem = new XSubItem();
                            string strFileName = Path.GetFileNameWithoutExtension(subFilePath);
                            subItem._strAoFilePath = subFilePath.Replace(Application.dataPath, "Assets");
                            item._lstSubItems.Add(subItem);
                        }, "*", false);


                        itemCategory._lstItems.Add(item);
                    }, "*", false);

                    // 里面的文件
                    KK.Frame.Util.SimpleFileProcess.TravelFilesInFolder(filePath, (subFilePath) =>
                    {
                        if (!subFilePath.EndsWith(".wav")
                && !subFilePath.EndsWith(".ogg")
                && !subFilePath.EndsWith(".mp3"))
                        {
                            return;
                        }
                        string strFileName = Path.GetFileNameWithoutExtension(subFilePath);
                        // 每一个文件都是一个Item，并对该Item添加一个同名的subItem
                        XItem item = new XItem();
                        item.strItemName = strFileName;
                        XSubItem subItem = new XSubItem();
                        subItem._strAoFilePath = subFilePath.Replace(Application.dataPath, "Assets");
                        item._lstSubItems.Add(subItem);
                        itemCategory._lstItems.Add(item);
                    }, "*", false);

                    dictData.Add(itemCategory.strCategoryName, itemCategory);
                }
            }, "*", false);
        }
    }
}
