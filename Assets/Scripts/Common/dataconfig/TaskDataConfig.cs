using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TaskDataConfig
{
    public TaskDataBase[] taskDataBaseGroup;
}

[Serializable]
public class TaskDataBase
{
    public string DESCRIPTION;
    public int ID;
    public int NPC_SOUND;
    public string STAGE_DESCRIPTION;
    public string STORY_NAME;
}
