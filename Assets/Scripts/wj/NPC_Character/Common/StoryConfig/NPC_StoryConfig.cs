//NPC_StoryConfig.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/30/2017 7:11 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_StoryConfig 
{
    public int taskId;
    public string description;
    public Vector3 targetPos;
    public Transform target;
    public int audioId;
    public string animationName;

    public NPC_StoryConfig(int taskId, string description, Vector3 targetPos, Transform target, int audioId, string animationName)
    {
        this.taskId = taskId;
        this.description = description;
        this.targetPos = targetPos;
        this.target = target;
        this.audioId = audioId;
        this.animationName = animationName;
    }
}
