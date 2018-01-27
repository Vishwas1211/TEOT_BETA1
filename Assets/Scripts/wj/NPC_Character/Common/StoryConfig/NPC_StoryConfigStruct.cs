//NPC_StoryConfigStruct.cs
//TEOT_ONLINE
//
//Create by WangJie on 11/14/2017 3:12 PM
//Description: 
//
using UnityEngine;

public struct NPC_StoryConfigStruct
{
    public int taskId;
    public string description;
    public Vector3 targetPos;
    public Transform target;
    public int audioId;
    public string animationName;

    public NPC_StoryConfigStruct(int taskId, string description, Vector3 targetPos, Transform target, int audioId, string animationName)
    {
        this.taskId = taskId;
        this.description = description;
        this.targetPos = targetPos;
        this.target = target;
        this.audioId = audioId;
        this.animationName = animationName;
    }
}
