//JayceeTaskGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/21/2017 9:26 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayceeTaskGroup : MonoBehaviour 
{
    public List<NPC_StoryConfig> taskList = new List<NPC_StoryConfig>();
    public Dictionary<int, NPC_StoryConfig> taskDict = new Dictionary<int, NPC_StoryConfig>();

    private Transform _target;

    private void Start()
    {
        //test
        //Load();
    }

    public void Load()
    {
        if (AppConfig.CAMERARIG_OR_FPSCONTROLLER)
            _target = PlayerManager.Instance.playerCollider.transform;
        else
            _target = GameObject.Find("ThirdPersonController").transform;

        TaskListManagent();
        for (int i = 0; i < taskList.Count; i++)
        {
            taskDict.Add(taskList[i].taskId, taskList[i]);
        }
        //for (int i = 0; i < taskList.Count; i++)
        //{
        //    Debug.Log(taskList[i].description);
        //}

        //Debug.Log(taskDict[1002].description);
    }


    private void TaskListManagent()
    {
        taskList.Add(new NPC_StoryConfig(1001, "Jaycee出现", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1002, "走向门口", new Vector3(-22.71f, 20.48f, 31.28f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1003, "回头看", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1004, "向厕所跑", new Vector3(-21.14f, 20.48f, 40.606f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1005, "在厕所等待玩家，拿起扫把", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1006, "玩家从大厅进门后，说话", Vector3.zero, _target, 2089, "idle"));
        taskList.Add(new NPC_StoryConfig(1007, "玩家到达门口，说话", Vector3.zero, _target, 2090, "idle"));
        taskList.Add(new NPC_StoryConfig(1008, "进门，打玩家", Vector3.zero, _target, 2091, "idle"));
        taskList.Add(new NPC_StoryConfig(1009, "抢完了扫把，蹲在地上说话", Vector3.zero, _target, 2092, "idle"));
        taskList.Add(new NPC_StoryConfig(1010, "玩家出去之后，到达某一点，跑出去，等玩家", new Vector3(-11.02f, 20.48f, 38.37f), _target, 2093, "idle"));
        taskList.Add(new NPC_StoryConfig(1011, "跑向房间", new Vector3(-6.3f, 20.48f, 26.07f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1012, "玩家到了另一个房间，跑出来", new Vector3(-8.59f, 20.48f, 31.3f), _target, 2094, "idle"));
        taskList.Add(new NPC_StoryConfig(1013, "跑出来之后，感谢玩家", Vector3.zero, _target, 2095, "idle"));
        taskList.Add(new NPC_StoryConfig(1014, "玩家走近之后，带领玩家走", Vector3.zero, _target, 2096, "idle"));       //少一步语音
        taskList.Add(new NPC_StoryConfig(1015, "带玩家走", new Vector3(1.7f, 23.136f, 41.809f), _target, 2098, "idle"));
        taskList.Add(new NPC_StoryConfig(1016, "楼塌，跳过去", new Vector3(2.398f, 26.411f, 35.422f), _target, 2099/*1019*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1017, "往角落跑", new Vector3(-2.16f, 26.411f, 36.03f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1018, "跑到房间", new Vector3(-3.9f, 26.411f, 40.9f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1019, "跑向一个房间，关门", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1020, "再次见到玩家，说话", Vector3.zero, _target, 2100/*1020*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1021, "往房间走", new Vector3(-17.35f, 26.421f, 40.86f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1022, "被怪物攻击", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1023, "怪物被打死之后，继续向房间走", new Vector3(-24.71f, 26.421f, 40.27f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1024, "被怪物攻击", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1025, "怪物被打死，走到柜子旁边", new Vector3(-25.83f, 26.411f, 40.26f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1026, "打开柜子", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1027, "说话", Vector3.zero, _target, 2101/*1021*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1028, "走到另一边", new Vector3(-24.94f, 26.411f, 41.28f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1029, "说话", Vector3.zero, _target, 2102/*1022*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1030, "边走边说", new Vector3(-23.93f, 26.411f, 25.98f), _target, 2103/*1023*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1031, "跑出房间，边跑边说", new Vector3(-18.2f, 26.411f, 29.75f), _target, 2104/*1024*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1032, "抱起女孩，说话", Vector3.zero, _target, 2105, "idle"));
        taskList.Add(new NPC_StoryConfig(1033, "跑向房间", new Vector3(-18.1f, 26.411f, 26.93f), _target, 2106, "idle"));
        taskList.Add(new NPC_StoryConfig(1034, "后退，撞墙", new Vector3(-18.19f, 26.411f, 30.5f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1035, "摔倒", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1036, "说话", Vector3.zero, _target, 2107/*1026*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1037, "身体抖动，说话", Vector3.zero, _target, 2108/*1027*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1038, "剧烈抖动，脸变色，说话", Vector3.zero, _target, /*1028*/-1, "idle"));
        taskList.Add(new NPC_StoryConfig(1039, "起身", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1040, "打玩家", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1041, "变boss", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1042, "倒下", Vector3.zero, _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1043, "追玩家", Vector3.zero, _target, -1, "idle"));
    }
}
