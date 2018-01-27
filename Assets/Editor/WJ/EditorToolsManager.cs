//
//  EditorToolsManager.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES_P2 on 8/3/2017 9:50 AM.
//
//

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
public class EditorToolsManager : EditorWindow {

    private string _inputArrayName = "";
    private string _fileName;
    private Object _object;
    private Object[] _objectGroup;

    private const string taskArrayName = "taskDataBaseGroup";
    private const string audioArrayName = "audioDataBaseGroup";
    private const string toolsArrayName = "toolsDataBaseGroup";

    [MenuItem("CustomTools/JsonFormatting")]
    static void AddWindow()
    {
        //创建窗口
        Rect wr = new Rect(0, 0, 300, 200);
        EditorToolsManager window = (EditorToolsManager)EditorWindow.GetWindowWithRect(typeof(EditorToolsManager), wr, true, "TEOT-Json格式化工具");
        window.Show();
    }

    private void OnGUI()
    {
        GUI.color = Color.green;
        EditorGUILayout.HelpBox("注意：需要操作的文件必须放在DataConfig目录下!", MessageType.Warning,true);
        GUI.color = Color.white;
        if (GUILayout.Button("加载文件", GUILayout.Width(200)))
        {
            _objectGroup = Resources.LoadAll("DataConfig");
        }
        //GUILayout.Space(15);
        //_inputArrayName = EditorGUILayout.TextField("输入Json数组名:", _inputArrayName, GUILayout.Height(20));
        //GUILayout.Space(15);
        //_object = EditorGUILayout.ObjectField("需要修改的文件:", _object, typeof(Object), true);
        //_fileName = EditorGUILayout.TextField("需要修改的文件名:", _fileName,GUILayout.Height(20));
        GUILayout.Space(10);
        
        if (GUILayout.Button("生成", GUILayout.Width(200)))
        {
            for (int i = 0; i < _objectGroup.Length; i++)
            {
                //Debug.Log(Application.dataPath + "/Resources/DataConfig/" + _fileName + ".txt");
                if (!File.Exists(Application.dataPath + "/Resources/DataConfig/" + _objectGroup[i].name + ".txt"))
                {
                    this.ShowNotification(new GUIContent("没有这个文件!" + _objectGroup[i].name));
                }
                else
                {

                    StreamReader sr = new StreamReader(Application.dataPath + "/Resources/DataConfig/" + _objectGroup[i].name + ".txt");

                    string str = sr.ReadToEnd();

                    //FileStream fs = new FileStream(Application.dataPath + "/Resources/DataConfig/" + _fileName + ".txt", FileMode.Open);
                    //fs.SetLength(0);
                    sr.Close();

                    if (str.Length <= 0)
                    {
                        this.ShowNotification(new GUIContent("内容怎么能是空的呢!" + _objectGroup[i].name));
                    }
                    else
                    {
                        if (str[0].Equals('{'))
                        {
                            this.ShowNotification(new GUIContent("这个文件内容格式已被更改!" + _objectGroup[i].name));
                        }
                        else
                        {
                            StreamWriter sw;
                            sw = File.CreateText(Application.dataPath + "/Resources/DataConfig/" + _objectGroup[i].name + ".txt");
                            switch (_objectGroup[i].name)
                            {
                                case "audio_config":
                                    sw.WriteLine("{" + "\"" + audioArrayName + "\"" + ":" + str + "}");
                                    break;
                                case "task_config":
                                    sw.WriteLine("{" + "\"" + taskArrayName + "\"" + ":" + str + "}");
                                    break;
                                case "tools_config":
                                    sw.WriteLine("{" + "\"" + toolsArrayName + "\"" + ":" + str + "}");
                                    break;
                                default:
                                    break;
                            }
                            Debug.Log(str[0]);
                            sw.Close();
                            sw.Dispose();//文件流释放  
                        }
                    }
                }
            }
        }
        //if (GUILayout.Button("显示", GUILayout.Width(200)))
        //{
        //for (int i = 0; i < _objectGroup.Length; i++)
        //{
        //if (_objectGroup[i] != null)
        //{
        //Debug.Log(_objectGroup[i].name);
        ////EditorGUILayout.ObjectField("需要修改的文件:", _objectGroup[i], typeof(Object), true);
        //GUILayout.Space(10);
        //EditorGUILayout.HelpBox("12312", MessageType.Info);
        //EditorGUILayout.TextArea("");
        //}
        //}
        //}

        //if (GUILayout.Button("Find GameObject"))
        //{
        //    foreach (var item in GameObject.FindObjectsOfType<GameObject>())
        //    {
        //        if (item.GetComponent<NetworkIdentity>())
        //        {
        //            Debug.Log(item.name);
        //        }
        //    }
        //}
    }

}