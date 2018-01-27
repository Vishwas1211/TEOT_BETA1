using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if NETFX_CORE
using Windows.Storage;
using Windows.Storage.Streams;
#else
#endif

public class LoadJson {

    public static TaskDataConfig LoadJsonTaskFromFile(string path)
    {
        string json = "";
#if NETFX_CORE
        
#else
        if (!File.Exists(Application.dataPath + path))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + path);

        if (sr == null)
        {
            return null;
        }

        json = sr.ReadToEnd();
#endif
        //Debug.Log(josn);

        if (json.Length > 0)
        {
            return JsonUtility.FromJson<TaskDataConfig>(json);
        }

        return null;
    }

    public static AudioDataConfig LoadJsonAudioFromFile(string path)
    {
        string json = "";
#if NETFX_CORE
        TestReadJson.instance.text.text = "UWP";
#else
        //BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.dataPath + path))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + path);

        if (sr == null)
        {
            return null;
        }

        json = sr.ReadToEnd();
        //Debug.Log(json);
#endif
        if (json.Length > 0)
        {
            return JsonUtility.FromJson<AudioDataConfig>(json);
        }

        return null;
    }

    public static ToolsDataConfig LoadJsonToolsFromFile(string path)
    {
        string json = "";
#if NETFX_CORE
        TestReadJson.instance.text.text = "UWP";
#else
        //BinaryFormatter bf = new BinaryFormatter();
        if (!File.Exists(Application.dataPath + path))
        {
            return null;
        }

        StreamReader sr = new StreamReader(Application.dataPath + path);

        if (sr == null)
        {
            return null;
        }

        json = sr.ReadToEnd();
        //Debug.Log(json);
#endif
        if (json.Length > 0)
        {
            return JsonUtility.FromJson<ToolsDataConfig>(json);
        }

        return null;
    }
}
