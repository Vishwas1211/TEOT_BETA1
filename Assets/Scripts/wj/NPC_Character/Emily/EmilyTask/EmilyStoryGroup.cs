//EmilyStoryGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 6:15 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyStoryGroup : MonoBehaviour 
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

        foreach (NPC_StoryConfig story in _storyList)
        {
            _storyDict.Add(story.taskId, story);
        }
	}

    private void StoryListManagent()
    {
        _storyList.Add(new NPC_StoryConfig(1001, "出现", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1002, "说话：等下，好像门口有人！", Vector3.zero, _target, 2109/*1030*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1003, "说话：帮帮我们离开这里！", Vector3.zero, _target, 2110/*1038*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1004, "说话：不，你们绝不能那么做！", Vector3.zero, _target, 2111/*1047*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1005, "说话：不…我不要这么做！", Vector3.zero, _target, 2112/*1050*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1006, "说话：Rick 你倒是说些什么呀？", Vector3.zero, _target, 2113/*1052*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1007, "说话：你怎么知道，他可能是好人！", Vector3.zero, _target, 2114/*1054*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1008, "说话：不…这是错误的做法，这么做会把我们都毁了…", Vector3.zero, _target, 2115/*1057*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1009, "说话：噢，谢谢你！我就知道会发生这样的事情！", Vector3.zero, _target, 2116/*1069*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1010, "说话：你看那边！你从这门走过去，就能看到电梯。", Vector3.zero, _target, 2118/*1070*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1011, "说话：我不跟你一起去了，我还有别的事要做", Vector3.zero, _target, 2119/*1071*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1012, "说话：对了，如果你找到Rick，救他一命。", Vector3.zero, _target, 2120/*1072*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1013, "消失", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1014, "出现在平民中间", Vector3.zero, _target, 2121, "idle"));
        _storyList.Add(new NPC_StoryConfig(1015, "走出来感谢玩家", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1016, "说话：真是很谢谢你！", Vector3.zero, _target, 2122/*1083*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1017, "说话：这里的情况变得越来越糟糕，你们把电源切断后，敌人加强了这里的安全防御，很多地方被挡住了。", new Vector3(-0.464f, 88.4924f, 36.263f), _target, 2123/*1084*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1018, "说话：你可以通过这条路走到上一层，", Vector3.zero, _target, 2125/*1085*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1019, "拿起重物，扔", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1020, "如果玩家要求跳过去，然后摔死", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1021, "说话：看，你现在明白了吧", Vector3.zero, _target, 2126/*1086*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1022, "说话：我们现在就要出发了！", Vector3.zero, _target, 2127/*1087*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1023, "说话：不管你是谁", Vector3.zero, _target, -1, "idle"));
    }
}
