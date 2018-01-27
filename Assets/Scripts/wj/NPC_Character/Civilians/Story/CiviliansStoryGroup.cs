//CiviliansStoryGroup.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/15/2017 10:27 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiviliansStoryGroup : MonoBehaviour 
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
        _storyList.Add(new NPC_StoryConfig(1001, "出现", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1002, "被僵尸追杀", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1003, "说话： 啊啊…快来帮我！|| 说话：去死，你们这群怪物！", new Vector3(0, 0, 0), _target, 1082, "idle"));
        _storyList.Add(new NPC_StoryConfig(1004, "获救", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1005, "感谢玩家！", new Vector3(0, 0, 0), _target, 1083, "idle"));
        _storyList.Add(new NPC_StoryConfig(1006, "走到一边", new Vector3(1.073f, 88.492f, 34.784f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1007, "走到楼梯上", new Vector3(1.583f, 89.904f, 27.439f), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1008, "说话：快，来这，到这来！", new Vector3(0, 0, 0), _target, 1089, "idle"));
        _storyList.Add(new NPC_StoryConfig(1009, "说话：不要出声，等他们都走了！", new Vector3(0, 0, 0), _target, 1090, "idle"));
        _storyList.Add(new NPC_StoryConfig(1010, "如果玩家救了他的家庭，跳到下一步，如果没有，跳两步", new Vector3(0, 0, 0), _target, -1, "idle"));
        _storyList.Add(new NPC_StoryConfig(1011, "救了:Ok!他们都走了！我想现在我们都相互救了各自一命，这是电梯的钥匙，希望能帮上你。", new Vector3(0, 0, 0), _target, 1091, "idle"));
        _storyList.Add(new NPC_StoryConfig(1012, "没救：OK！他们都走了。我想现在我们都平了。", new Vector3(0, 0, 0), _target, 1092, "idle"));
        _storyList.Add(new NPC_StoryConfig(1013, "等待玩家离开，重新躲起来", new Vector3(0, 0, 0), _target, 1001, "idle"));
        //_storyList.Add(new NPC_StoryConfig(1014, "1014", new Vector3(0, 0, 0), transform, 1001, "idle"));
        //_storyList.Add(new NPC_StoryConfig(1015, "1015", new Vector3(0, 0, 0), transform, 1001, "idle"));
        //_storyList.Add(new NPC_StoryConfig(1016, "1016", new Vector3(0, 0, 0), transform, 1001, "idle"));
        //_storyList.Add(new NPC_StoryConfig(1017, "1017", new Vector3(0, 0, 0), transform, 1001, "idle"));
        //_storyList.Add(new NPC_StoryConfig(1018, "1018", new Vector3(0, 0, 0), transform, 1001, "idle"));
        //_storyList.Add(new NPC_StoryConfig(1019, "1019", new Vector3(0, 0, 0), transform, 1001, "idle"));
    }
}
