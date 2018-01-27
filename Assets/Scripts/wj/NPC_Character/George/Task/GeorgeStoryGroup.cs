//GeorgeStoryGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 12:34 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeStoryGroup : MonoBehaviour
{
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

    private Transform _target;

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
	
	private void StoryListManagent () 
	{
        _storyList.Add(new NPC_StoryConfig(1001, "出现", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1002, "说话：是不是外面有人？", Vector3.zero, _target, 2176/*1031*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1003, "说话：求求你帮我们离开这里！", Vector3.zero, _target, 2177/*1033*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1004, "说话：帮我们想办法把门打开。", Vector3.zero, _target, 2178/*1039*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1005, "说话：门打开后，我们要一起干倒这个士兵", Vector3.zero, _target, 2179/*1046*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1006, "把Emily推到墙角", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1007, "说话：你现在要听我的，要不你站在我们这边", Vector3.zero, _target, 2180/*1048*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1008, "说话：哈哈哈哈…", Vector3.zero, _target, 2181/*1055*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1009, "把Emily整到屋里", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1010, "说话：你们终于来了！但是这门好像被什么卡住了！", Vector3.zero, _target, 2182/*1060*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1011, "说话：他走不了很远。他需要我们！", Vector3.zero, _target, 2183/*1065*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1012, "说话：太好了，你把门锯开了！", Vector3.zero, _target, 2184/*1068*/, "idle"));
        _storyList.Add(new NPC_StoryConfig(1013, "打玩家", Vector3.zero, _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1014, "死", Vector3.zero, _target, -1, "idle"));
    }
}
