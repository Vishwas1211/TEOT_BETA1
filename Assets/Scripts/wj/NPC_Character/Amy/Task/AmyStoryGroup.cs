//AmyStoryGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 9:55 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyStoryGroup : MonoBehaviour 
{
    public List<NPC_StoryConfig> storyList = new List<NPC_StoryConfig>();
    public Dictionary<int, NPC_StoryConfig> storyDict = new Dictionary<int, NPC_StoryConfig>();
	
	public void Init ()
	{
        StoryListManagent();

        foreach (NPC_StoryConfig task in storyList)
        {
            storyDict.Add(task.taskId, task);
        }
	}
	
    private void StoryListManagent()
    {
        storyList.Add(new NPC_StoryConfig(1000, "出现", new Vector3(0, 0, 0), transform, -1, "idle"));
        storyList.Add(new NPC_StoryConfig(1001, "RICK 你快滚开", new Vector3(0, 0, 0), transform, 2140/*1029*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1002, "是你吗，宝贝你是怎么逃出来的", new Vector3(0, 0, 0), transform, 2141/*1035*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1003, "外面的先生，不管你是谁，请不要伤害我的女儿", new Vector3(0, 0, 0), transform, 2142/*1037*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1004, "MElissa可以帮你，麻烦你照顾好她", new Vector3(0, 0, 0), transform, 2143/*1040*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1005, "Amy走过去，平息George", new Vector3(0, 0, 0), transform, -1, "idle"));
        storyList.Add(new NPC_StoryConfig(1006, "Emily，他说得也有道理…难道你不想离开这里？", new Vector3(0, 0, 0), transform, 2145/*1049*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1007, "你愿意牺牲你自己，我也可以", new Vector3(0, 0, 0), transform, 2146/*1053*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1008, "你曾经有个儿子，你忘了？", new Vector3(0, 0, 0), transform, 2147/*1056*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1009, "好 现在让我们等！", new Vector3(0, 0, 0), transform, 2148/*1058*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1010, "Melissa?宝贝，我在！", new Vector3(0, 0, 0), transform, 2149/*1062*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1011, "咦，Rick呢！George，Rick不见了！", new Vector3(0, 0, 0), transform, 2150/*1064*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1012, "Liz你觉得我们要去哪里？", new Vector3(0, 0, 0), transform, 2151/*1066*/, "idle"));
        storyList.Add(new NPC_StoryConfig(1013, "与Melissa团聚", new Vector3(0, 0, 0), transform, -1, "idle"));
        storyList.Add(new NPC_StoryConfig(1014, "拿起激光锯打玩家", new Vector3(0, 0, 0), AppConfig.CAMERARIG_OR_FPSCONTROLLER ? PlayerManager.Instance.playerCollider.transform : GameObject.Find("ThirdPersonController").transform, -1, "idle"));
        storyList.Add(new NPC_StoryConfig(1015, "身死道消", new Vector3(0, 0, 0), transform, 2152, "idle"));
    }
}
