//CrabManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/26/2017 12:37 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabManager : SingletonMono<CrabManager> 
{
    private const string CRAB_PREFAB_PATH = "Prefabs/Character/Enemy/Crab/Crab";

    private float _timer = 0f;  //计时器
    private float _checkRate = 5f;   //检测速率
    private float _shortRange = 6f;     //近战攻击距离

    private bool _isHaveInterference;   //有干扰器
    private bool _isCanCheck;
    
    public List<GameObject> _playerList = new List<GameObject>();
    [SerializeField]
    private GameObject _controlPanel;   //控制台
    public GameObject controlPanel
    {
        get { return _controlPanel; }
    }

    private GameObject _crab;
    public GameObject crab { get { return _crab; } }

    private CrabController _crabController;
    public CrabController crabController { get { return _crabController; } }

    private CrabAudioController _crabAudioController;
    public CrabAudioController crabAudioController { get { return _crabAudioController; } }


    public void Init()
    {
        //_controlPanel = ControlPanelManager.instance.controlPanel;
        _controlPanel = GameObject.Find("ControlPanel");
        _crab = UtilFunction.ResourceLoad(CRAB_PREFAB_PATH);
        _crab.name = "Crab";
        _crabController = _crab.GetComponent<CrabController>();
        _crabController.Init();
        _crabAudioController = gameObject.AddComponent<CrabAudioController>();
        _isCanCheck = true;
    }
	

	void Start ()
	{
	}
	
	void Update () 
	{
        //CheckPlayerCloseControl();

        //test
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isHaveInterference = true;
        }
    }

    private void CheckPlayerCloseControl()
    {
        _timer += Time.deltaTime;
        if (_timer >= _checkRate && _isCanCheck)
        {
            _timer = 0f;
            for (int i = 0; i < _playerList.Count; i++)
            {
                float distControlPanel = Vector3.Distance(_controlPanel.transform.position, _playerList[i].transform.position);
                if (distControlPanel <= 1f)
                {
                    _crabController.SetTarget(_controlPanel);
                    //_crabController.SetState(CrabController.CRAB_STATE.EMISSION);
                    _crabController.SetHatredLevel(CrabController.HATRED_LEVEL.MEDIUM_RANGE);
                    _crabController.AttackBehaviour();

                }
                else if (Vector3.Distance(_crab.transform.position, _playerList[i].transform.position) <= _shortRange)
                {
                    _crabController.SetTarget(_playerList[i]);
                    _crabController.SetHatredLevel(CrabController.HATRED_LEVEL.SHORT_RANGE);
                    _crabController.AttackBehaviour();
                }
                else
                {
                    //如果出现干扰器，优先攻击干扰器
                    if (_isHaveInterference)
                    {
                        _isCanCheck = false;
                        _crabController.SetTarget(_playerList[Random.Range(0, _playerList.Count)]);
                        _crabController.SetHatredLevel(CrabController.HATRED_LEVEL.INTERFERENCE);
                        _crabController.AttackBehaviour();
                    }
                    else
                    {

                        _crabController.SetTarget(_playerList[Random.Range(0, _playerList.Count)]);
                        _crabController.SetHatredLevel(CrabController.HATRED_LEVEL.LONGE_RANGE);
                        _crabController.AttackBehaviour();
                    }
                }
            }
        }
    }
}
