using System;

[Serializable]
public class ToolsDataConfig
{
    public ToolsDataBase[] toolsDataBaseGroup;
}
[Serializable]
public class ToolsDataBase
{
    public string DESCRIPTION;
    public int ID;
    public string RES_PATH;
    //public float VOLUME;
}