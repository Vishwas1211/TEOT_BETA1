//LizzyStoryGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 9:57 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizzyStoryGroup : MonoBehaviour 
{
    private Transform _target;

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
	
	public void Init ()
	{
        if (AppConfig.CAMERARIG_OR_FPSCONTROLLER)
            _target = PlayerManager.Instance.playerCollider.transform;
        else
            _target = GameObject.Find("ThirdPersonController").transform;

        StoryListManagent();

        foreach (NPC_StoryConfig item in _storyList)
        {
            _storyDict.Add(item.taskId, item);
        }
	}

    private void StoryListManagent()
    {
        _storyList.Add(new NPC_StoryConfig(1001, "Lizzy出现", new Vector3(-24.294f, 80.74425f, 28.742f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1002, "快帮帮我们！我们需要帮助！", new Vector3(0, 0, 0), _target, 2153/*1032*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1003, "你到底在想些什么？", new Vector3(0, 0, 0), _target, 2154/*1051*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1004, "好我知道！", new Vector3(0, 0, 0), _target, -1/*1067*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1005, "逃跑", Vector3.zero, _target, 2155, "idle"));
        _storyList.Add(new NPC_StoryConfig(1006, "啊呃呃……", new Vector3(-24.132f, 80.564f, 29.38f), _target, -1/*1076*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1007, "啊呃呃，救我…救我出这里！", new Vector3(0, 0, 0), _target, 2156/*1077*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1008, "谁..你是谁？…噢是你！", new Vector3(0, 0, 0), _target, 2157/*1078*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1009, "（哭泣道）…我不想死…至少不要在今天…", new Vector3(0, 0, 0), _target, 2158/*1079*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1010, "不…不要…不要留我在这里等死！求求你！！", new Vector3(-13.941f, 80.62524f, 28.288f), _target, -1/*1080*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1011, "不…不要…不要留我在这里等死！求求你！！", new Vector3(0, 0, 0), _target, 2159/*1080*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1012, "谢谢你救我！", new Vector3(0, 0, 0), _target, -1/*1093*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1013, "不管怎么样…我知道有条路可以带你到顶楼。跟我来！", new Vector3(0, 0, 0), _target, 2160/*1094*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1014, "带领玩家穿过走廊,去一个房间", new Vector3(-13.522f, 80.582f, 28.686f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1015, "让玩家帮忙推橱柜", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1016, "这个通道直达那边的楼梯，", new Vector3(0, 0, 0), _target, 2161/*1095*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1017, "被怪物攻击", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1018, "啊呃呃…咳咳…(奄奄一息)", new Vector3(0, 0, 0), _target, 2162/*1096*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1019, "挣扎着爬到墙边喘气", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1020, "咳咳…我想我活不了了。救救Rick…", new Vector3(0, 0, 0), _target, 2163/*1097*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1021, "口喷鲜血", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1022, "谢谢你…还有..对不…起", new Vector3(0, 0, 0), _target, -1/*1098*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1023, "西天极乐", new Vector3(0, 0, 0), _target, -1, "idle"));
    }
}
