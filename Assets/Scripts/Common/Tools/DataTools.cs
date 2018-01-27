using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTools
{
    public int id;
    public string desc;
    public string path;
    //public float volume;

    public void Load(ToolsDataBase toolsDataBase)
    {
        id = toolsDataBase.ID;
        desc = toolsDataBase.DESCRIPTION;
        path = toolsDataBase.RES_PATH;
        //volume = audioDataBase.VOLUME;
    }
}
