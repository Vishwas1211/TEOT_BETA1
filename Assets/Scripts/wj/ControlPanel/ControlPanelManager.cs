//ControlPanelManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 10/23/2017 3:05 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelManager : SingletonMono<ControlPanelManager> 
{
    private const string PREFAB_PATH = "Prefabs/Common/ControlPanel";

    private GameObject _controlPanel;
    public GameObject controlPanel
    {
        get { return _controlPanel; }
    }

    public void Init()
    {
        _controlPanel = UtilFunction.ResourceLoad(PREFAB_PATH);
    }
	
	void Start ()
	{
		
	}
	
	void Update () 
	{
		
	}
}
