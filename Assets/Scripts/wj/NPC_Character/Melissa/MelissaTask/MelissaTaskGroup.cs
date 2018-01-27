//MelissaTaskGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/23/2017 4:43 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelissaTaskGroup : MonoBehaviour 
{
    public List<NPC_StoryConfig> taskList = new List<NPC_StoryConfig>();
    public Dictionary<int, NPC_StoryConfig> taskDict = new Dictionary<int, NPC_StoryConfig>();

    private Transform _target;

    private void Start()
    {
        
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
    }

    private void TaskListManagent()
    {
        taskList.Add(new NPC_StoryConfig(1000, "出现", new Vector3(-20.61f, 46.25f, 40.64f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1001, "妈妈", new Vector3(0, 0, 0), _target, 2128/*1034*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1002, "妈妈，门口有人", new Vector3(-25.38f, 46.256f, 40.52f), _target, 2129/*1036*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1003, "跟我来", new Vector3(0, 0, 0), _target, 2130/*1041*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1004, "走向走廊", new Vector3(-18.61373f, 46.256f, 40.48238f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1005, "走向房间门口", new Vector3(-18.004f, 46.256f, 31.974f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1006, "偷瞄一眼", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1007, "开门", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1008, "跑进去", new Vector3(-14.767f, 46.256f, 31.827f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1009, "躲在桌子下面，等待玩家进入房间", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1010, "你不能使用枪械，否则整个地方都会炸开，看那里", new Vector3(0, 0, 0), _target, 2131/*1042*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1011, "跟着玩家走", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1012, "如果玩家想硬闯，跟随或者跑掉", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1013, "跟玩家建议用通风管道，等待玩家打开通风管道", new Vector3(-15.7f, 46.256f, 27.422f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1014, "张开双手，等待玩家将自己放进管道", new Vector3(0, 0, 0), _target, 2132, "idle"));
        taskList.Add(new NPC_StoryConfig(1015, "爬向出口", new Vector3(0, 0, 0), _target, 2133, "idle"));
        taskList.Add(new NPC_StoryConfig(1016, "提示玩家向通风口外看", new Vector3(0, 0, 0), _target, 2135, "idle"));
        taskList.Add(new NPC_StoryConfig(1017, "走出通风口", new Vector3(-14.34f, 46.256f, 41.84f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1018, "跟随玩家", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1019, "玩家进门时，说话", new Vector3(0, 0, 0), _target, 2136/*1043*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1020, "跟随玩家", new Vector3(- 8.01f, 46.256f, 36.98f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1021, "到达实验室，说话", new Vector3(0, 0, 0), _target, 2137/*1044*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1022, "走向桌子", new Vector3(-9.11f, 46.256f, 42.265f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1023, "玩家砸玻璃，躲到桌子下", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1024, "玩家打死 僵尸之后，跟随玩家", new Vector3(-22.67f, 46.256f, 41.05f), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1025, "妈妈，你在吗", new Vector3(0, 0, 0), _target, 2138/*1061*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1026, "好的，妈妈！我会守在这等你。", new Vector3(0, 0, 0), _target, 2139/*1063*/, "idle"));
        taskList.Add(new NPC_StoryConfig(1027, "与Amy团聚", new Vector3(0, 0, 0), _target, -1, "idle"));
        taskList.Add(new NPC_StoryConfig(1028, "Amy死了，Melissa恨死玩家，摔门而去", new Vector3(-15.822f, 46.285f, 37.241f), _target, -1, "idle"));
    }
}
