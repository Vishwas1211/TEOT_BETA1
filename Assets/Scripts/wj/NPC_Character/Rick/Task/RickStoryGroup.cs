//RickStoryGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/13/2017 10:13 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickStoryGroup : MonoBehaviour
{
    private Transform _target;

    private List<NPC_StoryConfigStruct> _storyStructList = new List<NPC_StoryConfigStruct>();

    private List<NPC_StoryConfig> _storyList = new List<NPC_StoryConfig>();
    public List<NPC_StoryConfig> storyList
    {
        get { return _storyList; }
    }

    private Dictionary<int, NPC_StoryConfig> _storyDict = new Dictionary<int, NPC_StoryConfig>();
    public Dictionary<int, NPC_StoryConfig> storyDict
    {
        get { return _storyDict; }
    }

    public void Init()
    {
        if (AppConfig.CAMERARIG_OR_FPSCONTROLLER)
            _target = PlayerManager.Instance.playerCollider.transform;
        else
            _target = GameObject.Find("ThirdPersonController").transform;

        StoryListManagent();

        foreach (NPC_StoryConfig story in _storyList)
        {
            _storyDict.Add(story.taskId, story);
        }
    }

    private void StoryListManagent()
    {
        //_storyStructList.Add(new NPC_StoryConfigStruct(1001, "出现", new Vector3(0, 0, 0), transform, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1001, "出现", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1002, "在屋里面，进行剧情", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1003, "躲到一边", new Vector3(-21.15f, 46.231f, 24.99f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1004, "逃跑", new Vector3(-4.215f, 88.4924f, 35.195f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1005, "再次出现", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1006, "打玩家", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1007, "说话：不！你没法活捉我！滚开！", new Vector3(0, 0, 0), _target, 2164/*1099*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1008, "被玩家捉住之后，说话：不要…不要杀我！我能帮到你…", new Vector3(0, 0, 0), _target, 2165/*1100*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1009, "说话：你来这里是为了救…我？但是为什么呢…?", new Vector3(0, 0, 0), _target, 2166/*1101*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1010, "说话：好吧，那让我离开这该死的洞吧！首先，我们需要去武器室！", new Vector3(0, 0, 0), _target, 2167/*-1*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1011, "带领玩家去找武器", new Vector3(4.481f, 88.4924f, 29.086f), _target, 2168/*-1*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1012, "打开一个抽屉", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1013, "向楼上走", new Vector3(1.121f, 92.607f, 31.086f), _target, 2169/*1103*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1014, "向玩家介绍rab", new Vector3(0, 0, 0), _target, -1/*1104*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1015, "开门", new Vector3(0, 0, 0), _target, 2170/*1105*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1016, "带玩家走,去武器库", new Vector3(-12.68f, 92.607f, 52.17f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1017, "跟随玩家走", new Vector3(0, 0, 0), _target, -1/*1106*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1018, "说话：用光弹吧，闪瞎它。你有1分钟的时间，1分钟后", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1019, "向后退，远远的看着", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1020, "玩家拿到钥匙之后，跑向玩家", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1021, "跑到玩家跟前，跟随", new Vector3(0, 0, 0), _target, -1, "idle"));
    }
}
