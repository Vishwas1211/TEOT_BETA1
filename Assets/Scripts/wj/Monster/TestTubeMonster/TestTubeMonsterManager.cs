//TestTubeMonsterManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/12/2017 6:05 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeMonsterManager : MonoBehaviour 
{
    private const string MONSTER_PATH = "";
    private GameObject _monster;
    public GameObject monster
    {
        get { return _monster; }
    }

    private TestTubeMonsterController _monsterController;
    public TestTubeMonsterController monsterController
    {
        get { return _monsterController; }
    }
	
	public void Init ()
	{
        _monster = UtilFunction.ResourceLoad(MONSTER_PATH);
        _monsterController = _monster.GetComponent<TestTubeMonsterController>();
        _monsterController.Init();
	}
	
	void Update () 
	{
		
	}
}
