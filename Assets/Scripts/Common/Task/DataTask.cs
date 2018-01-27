using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTask
{

    public int id;
    public int audioId;
    public string desc;
    public string storyName;
    public string stageDesc;

    public void Load(TaskDataBase taskDataBase)
    {
        id = taskDataBase.ID;
        desc = taskDataBase.DESCRIPTION;
        storyName = taskDataBase.STORY_NAME;
        stageDesc = taskDataBase.STAGE_DESCRIPTION;
        audioId = taskDataBase.NPC_SOUND;
    }
}
