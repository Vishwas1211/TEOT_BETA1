using UnityEngine;
using System.Collections;
//using UnityEngine.Networking;

public class L28SliderController : MonoBehaviour
{
    //private const string BTN_COMMON_MAT_PATH = "level5/Material/ControlPanelMaterial/Ext_KongZhiTai";
    //private const string BTN_BLACK_MAT_PATH = "level5/Material/ControlPanelMaterial/Ext_KongZhiTai_black";

    private float _posX;
    private float _min = -0.4f;
    private float _max = 0.4f;
    private float _curSliderValue = 1f;
    public float curSliderValue
    {
        get { return _curSliderValue; }
    }

    private bool _isMatch = false;
    public bool isMatch
    {
        get { return _isMatch; }
    }

    private Material _btnBlackMat;
    Ray r;
    private bool _startCurveSin = false;
    private Material _btnCommonMat;

    private GameObject _bar;
    public LayerMask rayLayer = 0 << 8;

    //public NewPlayerSaoMaQi saomiaoqiang;

    void Awake()
    {
    }

    void Start()
    {
        //_btnCommonMat = Resources.Load(BTN_COMMON_MAT_PATH) as Material;
        //_btnBlackMat = Resources.Load(BTN_BLACK_MAT_PATH) as Material;
        _bar = transform.Find("KongZhiTai_Button/KongZhiTai_Button_4").gameObject;
    }

    public void StartCurveSin()
    {
        _startCurveSin = true;
    }


    public void StopCurveSin()
    {
        _startCurveSin = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StopCurveSin();
        }
        if (TaskStepManagaer.Instance.IsEqualTaskId(28002))
        {
         r= Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 10, rayLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.name == "KongZhiTai_Button_4")
                {
                    StartCurveSin();
                }
                if (CurveSin123.isT)
                {
                    if (hit.transform.name == "KongZhiTai_Button_1")
                    {
                        hit.transform.GetComponent<testBlue>().Finish();
                    }
                    if (hit.transform.name == "KongZhiTai_Button_2")
                    {
                        hit.transform.GetComponent<testBlue>().Finish();
                    }
                    if (hit.transform.name == "KongZhiTai_Button_3")
                    {
                        hit.transform.GetComponent<testBlue>().Finish();
                    }

                }
            }
        }

        {
            //if (!_startCurveSin)
            //{
            //    return;
            //}
            //GameObject serverPlayer = GameObject.FindGameObjectWithTag("MainCamera");
            //saomiaoqiang = GameObject.Find("PlayerRoot").transform.Find("Controllers/leftController/SaoMiaoQiang").GetComponent<NewPlayerSaoMaQi>();
            //saomiaoqiang = serverPlayer.transform.Find("SOD_PalmL/SaoMiaoQiang").GetComponent<NewPlayerSaoMaQi>();
            if (true)
            {
                //_bar.GetComponent<Renderer>().material = _btnBlackMat;
                if (_startCurveSin)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(_bar.transform.position); // 目的获取z，在Start方法  
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = screenPos.z; // 这个很关键  
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                    _bar.transform.position = worldPos;

                    //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //Debug.Log(pos);
                    //_bar.transform.position = Vector3.Lerp(_bar.transform.position, pos, 0.2f);//TODO
                }
            }
            else
            {
                _bar.GetComponent<Renderer>().material = _btnCommonMat;
            }
        }


        _posX = _bar.transform.localPosition.x;
        _posX = Mathf.Clamp(_posX, _min, _max);
        _bar.transform.localPosition = new Vector3(_posX, -0.01f, 0.107f);
        _curSliderValue = Mathf.Abs((_posX - 0.4f) / (_max - _min));

        //		Trace.trace ("+++++++curSliderValue : " + _curSliderValue, Trace.CHANNEL.LEVEL5);
    }

}
