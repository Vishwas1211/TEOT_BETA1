using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;


public class TestReadJson : MonoBehaviour {
    public Text text;

    public static TestReadJson instance;

    string test;
    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {

        DataManager.instance.LoadData();
        //TaskStepManagaer.Instance.Load();
        //TaskStepManagaer.Instance.StartTask();
        //JayceeManager.instance.Init();
        MelissaManager.Instance.Init();
        AmyManager.Instance.Init();
        GeorgeManager.Instance.Init();
        EmilyManager.Instance.Init();
        LizzyManager.Instance.Init();
        //Level_10_Manager.Instance.Init();
        //RabManager.instance.Init();
        //CiviliansManager.instance.Init();
        //RickManager.instance.Init();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //TaskDataConfig taskData = LoadJson.LoadJsonTaskFromFile("/Resources/DataConfig/task_config.txt");
            //for (int i = 0; i < taskData.taskDataBaseGroup.Length; i++)
            //{
            //    Debug.Log(taskData.taskDataBaseGroup[i].END_POS);
            //}

            //Debug.Log(DataManager.instance.audioGroup.GetAllAudio().Count);
            //List<DataAudio> aaa = DataManager.instance.audioGroup.GetAllAudio();
            //for (int i = 0; i < aaa.Count; i++)
            //{
            //    Debug.Log(aaa[i].id + "," + aaa[i].path);
            //}

            //Debug.Log(DataManager.instance.audioGroup.dataAudio[2001].path);

            //StreamWriter sw = new StreamWriter(Application.dataPath + "/Resources/DataConfig/test.txt");
            //Debug.Log(File.Exists(Application.dataPath + "/Resources/DataConfig/test.txt"));
            //sw.WriteLine("test wen");

            //StreamWriter sw;
            //if (!File.Exists(Application.dataPath + "//" + "/Resources/DataConfig/test1.txt"))
            //{
            //    sw = File.CreateText(Application.dataPath + "//" + "/Resources/DataConfig/test1.txt");//创建一个用于写入 UTF-8 编码的文本  
            //    Debug.Log("文件创建成功！");
            //}
            //else
            //{
            //    sw = File.AppendText(Application.dataPath + "//" + "/Resources/DataConfig/test1.txt");//打开现有 UTF-8 编码文本文件以进行读取  
            //}
            //sw.WriteLine("success!");//以行为单位写入字符串  
            //sw.Close();
            //sw.Dispose();//文件流释放  

        }


	}
}
