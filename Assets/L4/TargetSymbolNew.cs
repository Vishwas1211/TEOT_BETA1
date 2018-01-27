using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSymbolNew : MonoBehaviour 
{
	private int ParameterNumber = 0;

	private List<SymbolNew> _symbolArray = new List<SymbolNew>();
	public List<SymbolNew> symbolArray
	{
		get{ return _symbolArray;}
	}

	private int[] _targetArray ;

//	private int 

//	private List<SymbolNew> _targetSymbol = new List<SymbolNew>();
//	public List<SymbolNew> targetSymbol
//	{
//		get{ return _targetSymbol;}
//	}

	private List<int> _inputArray = new List<int>();

	private ParticleSystem _triumphPanel;
	private ParticleSystem _failurePanel;

	private bool isRepair_0 = false; 
	private bool isRepair_1 = false; 
	private bool isRepair_2 = false; 

	private int _targetGarbledNumber = 0;

	private bool _isTargetGarbled = false;

	private float _blinkTime =0;

	private int _symbolVale = -1;


	private TargetSymbolNew _targetSymbolNew;//脚本自己
	public TargetSymbolNew targetSymbolNew
	{
		get{ return _targetSymbolNew;}
	}

	private GameObject _repairPanel;//自己的obj
	public GameObject repairPanel
	{
		get{ return _repairPanel;}
	}

	private GameObject _triumphPanelGameObject;//成功
	public GameObject triumphPanelGameObject
	{
		get{ return _triumphPanelGameObject;}
	}

	private GameObject _failurePanelGameObject;//失败
	public GameObject failurePanelGameObject
	{
		get{ return _failurePanelGameObject;}
	}

	private GameObject _resetInput;   //重置按钮
	public GameObject resetInput
	{
		get{ return _resetInput;}
	}

	private GameObject[] _targetSymbol;//目标数组
	public GameObject[] targetSymbol
	{
		get{ return _targetSymbol;}
	}

	private GameObject _hintFinish0;//提示灯
	public GameObject hintFinish0
	{
		get{ return _hintFinish0;}
	}

	private GameObject _hintFinish1;
	public GameObject hintFinish1
	{
		get{ return _hintFinish1;}
	}

	private GameObject _hintFinish2;
	public GameObject hintFinish2
	{
		get{ return _hintFinish2;}
	}

	private ParticleSystem[] _targetFinishSymbol;//目标的正确闪烁
	public ParticleSystem[] targetFinishSymbol
	{
		get{ return _targetFinishSymbol;}
	}

	private GameObject [] _symbolGameObject;//谜题按钮
	public GameObject[] symbolGameObject
	{
		get{ return _symbolGameObject;}
	}

	private SymbolNew [] _symbol;//按钮脚本数组
	public SymbolNew[] symbol
	{
		get{ return _symbol;}
	}

	private GameObject[] _effectsSymbol;//按钮特效
	public GameObject[] effectsSymbol
	{
		get{ return _effectsSymbol;}
	}

	//[SyncVar(hook = "ShowRepair")]
	public bool _canShow = false;

	public int TargetNumber = -1;

	public int TargetNumber0 = -1;

	public int TargetNumber1 = -1;

	public int TargetNumber2 = -1;

	public void Start()
	{
//		_triumphPanel = Level4ManagerNew.Instance.L4RepairManager.triumphPanel.GetComponent<ParticleSystem> ();
//		_failurePanel = Level4ManagerNew.Instance.L4RepairManager.failurePanel.GetComponent<ParticleSystem> ();
//		_targetArray = new int[Level4ManagerNew.Instance.L4RepairManager.symbol.Length];

		_repairPanel = this.gameObject;

		_triumphPanel = transform.Find ("riddle_center_kuang_tigger_success").GetComponent<ParticleSystem> ();
		_failurePanel = transform.Find ("riddle_center_kuang_tigger_fail")   .GetComponent<ParticleSystem> ();

		_targetArray = new int[ transform.Find("operationSymbol").childCount];
	
		for (int i = 0; i < _targetArray.Length; i++) 
		{
			_targetArray [i] = i;
		}



		_triumphPanelGameObject = _repairPanel.transform.Find ("riddle_center_kuang_tigger_success").gameObject;
		_failurePanelGameObject = _repairPanel.transform.Find ("riddle_center_kuang_tigger_fail").gameObject;

		_resetInput = _repairPanel.transform.Find ("riddle_reset_trigger").gameObject;
		_resetInput.AddComponent<ResetSymbolInputNew>().SetManager(this);

		Transform _targetSymbolTransform = _repairPanel.transform.Find ("targetSymbol");
		Transform _targetFinishSymbolTransform = _repairPanel.transform.Find ("riddleCenterSelected");
		Transform _symbolTransform = _repairPanel.transform.Find ("operationSymbol");
		Transform _effectsSymbolTransform = _repairPanel.transform.Find ("effectsSymbol");


		_targetSymbol = new GameObject[_targetSymbolTransform.childCount];
		for (int i = 0; i < _targetSymbolTransform.childCount; i++) 
		{
			_targetSymbol [i] = _targetSymbolTransform.GetChild (i).gameObject;
		}

		_targetFinishSymbol = new ParticleSystem[_targetFinishSymbolTransform.childCount];
		for (int i = 0; i < _targetFinishSymbolTransform.childCount; i++) 
		{
			_targetFinishSymbol [i] = _targetFinishSymbolTransform.GetChild (i).gameObject.GetComponent<ParticleSystem>();
		}

		_effectsSymbol = new GameObject[_effectsSymbolTransform.childCount];
		for (int i = 0; i < _effectsSymbolTransform.childCount; i++) 
		{
			_effectsSymbol [i] = _effectsSymbolTransform.GetChild (i).gameObject;
		}

		_symbolGameObject = new GameObject[_symbolTransform.childCount];
		_symbol = new SymbolNew[_symbolTransform.childCount];
		for (int i = 0; i < _symbolTransform.childCount; i++) 
		{
			_symbolGameObject [i] = _symbolTransform.GetChild (i).gameObject;
			_symbol[i] = _symbolGameObject [i].AddComponent<SymbolNew> ();
			_symbol [i].Init ();
			_symbol [i].thisEffects = _effectsSymbol[i].GetComponent<ParticleSystem>();
			_symbol [i].SetManager(this);
		}

		_hintFinish0 = _repairPanel.transform.Find ("riddle_rate_of_progress_hint/riddle_rate_of_progress_trigger_0").gameObject;
		_hintFinish0.SetActive (false);
		_hintFinish1 = _repairPanel.transform.Find ("riddle_rate_of_progress_hint/riddle_rate_of_progress_trigger_1").gameObject;
		_hintFinish1.SetActive (false);
		_hintFinish2 = _repairPanel.transform.Find ("riddle_rate_of_progress_hint/riddle_rate_of_progress_trigger_2").gameObject;
		_hintFinish2.SetActive (false);

		Init ();
	}

	private void Init()
	{
//		gameObject.SetActive (false);
		//transform.position = new Vector3 (-1.565f,61.529f,25.7f);
		//transform.rotation = Quaternion.Euler( new Vector3 (0,180f,90f));
		//transform.SetParent (GameObject.FindGameObjectWithTag(ConstantTag.TAG_L4ELECATOR).transform);
	}

	void Update()
	{
		UpdeteTargetGarbled ();
		UpdateJudgeSymblo ();
		if (_blinkTime >= 0)
		{
			_blinkTime -= Time.deltaTime;
			if (_blinkTime <= 0)
			{
				_isTargetGarbled = false;
			}
		}
        //this.transform.localPosition = new Vector3(0.08f, 0.014f, 0.5627174f);
    }

	private void ShowRepair(bool canShow)
	{
		gameObject.SetActive (canShow);
	}

	public void InputSymbol(SymbolNew _symbol)
	{
		foreach (var item in _inputArray)
		{
			if (item == _symbol.value) 
			{
				return;
			}
		}
		RpcSetSymbol (_symbol.value);
		StartCoroutine (Renew_defer(1));
	}

	private void RpcSetSymbol(int symbolValue)
	{
		_symbolVale = symbolValue;
	}

	private IEnumerator Renew_defer(float deferTime)
	{
		yield return new WaitForSeconds(deferTime);
		RpcSetSymbol (-1);
	}

	private void UpdateJudgeSymblo ()
	{
		if (false || _symbolVale < 0 || _symbolVale >=_symbol.Length)//电梯完成了，电梯还没开始
			return;
		foreach (var item in _inputArray)
		{
			if (item == _symbolVale) 
			{
				return;
			}
		}
		{
			ParameterNumber++;
		}
		HintPanel (_symbol[_symbolVale]);
		_inputArray.Add (_symbolVale);
		if (ParameterNumber >= 3)
		{
			isRepair_0 = false;
			isRepair_1 = false;
			isRepair_2 = false;
//			for (int i = 0; i < 3; i++)
//			{
				bool isRepair2 = false;
			for (int j = 0; j < _inputArray.Count; j++)
			{
				if (TargetNumber0 == _inputArray [j])
				{
					isRepair_0 = true;
					break;
				}
			}
			for (int j = 0; j < _inputArray.Count; j++)
			{
				if (TargetNumber1 == _inputArray [j])
				{
					isRepair_1 = true;
					break;
				}
			}
			for (int j = 0; j < _inputArray.Count; j++)
			{
				if (TargetNumber2 == _inputArray [j])
				{
					isRepair_2 = true;
					break;
				}
			}
//				if (!isRepair2) {
//					isRepair = false;
//				}
//			}

			UnableSelect ();
            {
                if (isRepair_0 && isRepair_1 && isRepair_2)
                {
                    StartCoroutine(TriumphDecode());
                    //NetServerUtil.Instance.RpcL4RiddleTriumphDecode();
                }
                else
                {
                    StartCoroutine(ResetSymbol());
                    //NetServerUtil.Instance.RpcL4LoadResetSymbol();
                }
            }
		}
	}

	private void HintPanel(SymbolNew _symbol)
	{

		_symbol.Highlight ();


		if (_symbol.value == TargetNumber0)
		{
			_targetFinishSymbol [_symbol.value].gameObject.SetActive (true);
			_targetFinishSymbol [_symbol.value].Play ();
		}
		if (_symbol.value == TargetNumber1)
		{
			_targetFinishSymbol [_symbol.value].gameObject.SetActive (true);
			_targetFinishSymbol [_symbol.value].Play ();
		}
		if (_symbol.value == TargetNumber2)
		{
			_targetFinishSymbol [_symbol.value].gameObject.SetActive (true);
			_targetFinishSymbol [_symbol.value].Play ();
		}
	}

	//制作谜
	public IEnumerator MakePuzzle(float deferTime)
	{
		RpcMakePuzzleBlink(deferTime);
		yield return new WaitForSeconds(deferTime);

		AllReset ();

		random (_targetArray, 3);
		TargetNumber0 = _targetArray [0];
		TargetNumber1 = _targetArray [1];
		TargetNumber2 = _targetArray [2];

		//显示中心区域
		ShowMakePuzzle();
	}

//	public IEnumerator MakePuzzle_Deferred(float deferTime)
//	{
////		AudioController.Instance.PlayAudio (2113,0f);
//		_isTargetGarbled = true;
//		yield return new WaitForSeconds(deferTime);
//		_isTargetGarbled = false;
//
//		AllReset ();
//
//		random (_targetArray, 3);
//		//显示中心区域
//		ShowMakePuzzle();
//	}

	//闪烁
	public void RpcMakePuzzleBlink(float deferTime)
	{
		_blinkTime = deferTime;
		_isTargetGarbled = true;

	}

	//显示谜题目标
	private void ShowMakePuzzle()
	{
		RpcQ (-1);

		RpcQ (TargetNumber0);
		RpcQ (TargetNumber1);
		RpcQ (TargetNumber2);
	
	}

	private void UpdeteTargetGarbled()
	{
		if (_isTargetGarbled)
		{
			_targetSymbol [_targetArray [_targetGarbledNumber]].SetActive (false);
			_targetGarbledNumber = Random.Range (0,_symbol.Length);
			_targetSymbol [_targetArray [_targetGarbledNumber]].SetActive (true);
		}
	}

	//完成
	public IEnumerator TriumphDecode()
	{
		yield return new WaitForSeconds(2f);

		AllReset ();

		_triumphPanel.gameObject.SetActive (true);
		_triumphPanel.Play ();
		StartCoroutine(CanSelect_defer(2));
        //AudioControllerNew.Instance.PlayAudio(2128, 0f);
        {
			//StartCoroutine(Level4ManagerNew.Instance.L4RepairManager.FinishSymbol (2f));
		}

		TargetNumber =-1;

		{
			if (!_hintFinish0.activeSelf)
			{
                _hintFinish0.SetActive(true);
				//NetServerUtil.Instance.RpcRipairHintFinish0 ();
			}
			else if (!_hintFinish1.activeSelf)
			{
                _hintFinish1.SetActive(true);
                //NetServerUtil.Instance.RpcRipairHintFinish1 ();
            }
            else if (!_hintFinish2.activeSelf)
			{
                _hintFinish2.SetActive(true);
                //NetServerUtil.Instance.RpcRipairHintFinish2 ();
            }
        }

		yield return new WaitForSeconds(2f);
		{
            ResetInput();
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
            //NetServerUtil.Instance.RpcRipairReset ();
        }

	}

	//失败
	public IEnumerator  ResetSymbol()//关闭几秒之后再开启
	{
		yield return new WaitForSeconds(1.5f);

		foreach (var item in _symbol) 
		{
			item.ResetSymbol ();
		}

		foreach (var item in _targetFinishSymbol) 
		{
			item.gameObject.SetActive (false);
			item.Stop ();
		}

        //AudioControllerNew.Instance.PlayAudio(2092, 0f);

        _inputArray = null;
		_inputArray = new List<int> ();

		_failurePanel.gameObject.SetActive (true);
		_failurePanel.Play ();
		StartCoroutine(CanSelect_defer(2));

		ParameterNumber = 0;

		yield return new WaitForSeconds(2f);

		//NetServerUtil.Instance.RpcRipairReset ();
	}

	private IEnumerator CanSelect_defer(float deferTime)
	{
		yield return new WaitForSeconds(deferTime);

		CanSelect ();

		CloseHintPanel();
	}

	private void CloseHintPanel()
	{
		_triumphPanel.Stop ();
		_failurePanel.Stop ();

		_triumphPanel.gameObject.SetActive (false);
		_failurePanel.gameObject.SetActive (false);


	}

	private void CanSelect()
	{
		foreach (var item in _symbol) 
		{
			item.SetState (SymbolNew.SymbolState.Idel);
		}


	}

	private void UnableSelect()
	{
		foreach (var item in _symbol) 
		{
			item.SetState (SymbolNew.SymbolState.Close);
		}
	}

	public  void AllReset ()
	{
        if (_symbol == null) return;
		foreach (var item in _symbol) 
		{
			item.ResetSymbol ();
		}

		foreach (var item in _targetFinishSymbol) {
			item.gameObject.SetActive (false);
			item.Stop ();
		}

		foreach (var item in _targetSymbol) {
			item.gameObject.SetActive (false);
		}

		_inputArray = null;
		_inputArray = new List<int> ();

		StartCoroutine(CanSelect_defer(2));

		ParameterNumber = 0;
	}

	public  void ResetInput ()
	{
		foreach (var item in _symbol) 
		{
			item.ResetSymbol ();
		}

		foreach (var item in _targetFinishSymbol) {
			item.gameObject.SetActive (false);
			item.Stop ();
		}

		_inputArray = null;
		_inputArray = new List<int> ();

		StartCoroutine(CanSelect_defer(0));

		ParameterNumber = 0;
	}

	private void random(int[] array,int length)
	{
		int index;
		int value;
		for (int i = 0;i < length;i++) 
		{
			index = new System.Random ().Next (length, array.Length);

			value = array [i];
			array [i] = array [index];
			array [index] = value;
		}
	}

	private void ShowTargetNumber(int i)
	{
		if (i >= 0 && i < _targetSymbol.Length)
		{
			_targetSymbol [i].SetActive (true);
		}
		else if(i < 0)
		{
			for (int j = 0; j < _targetSymbol.Length; j++) 
			{
				_targetSymbol [j].SetActive(false);
			}
		}
	}
	void RpcQ(int i){
		if (i >= 0 && i < _targetSymbol.Length)
		{
			_targetSymbol [i].SetActive (true);
		}
		else if(i < 0)
		{
			for (int j = 0; j < _targetSymbol.Length; j++) 
			{
				_targetSymbol [j].SetActive(false);
			}
		}
	}
}
