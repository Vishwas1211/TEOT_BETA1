using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level_10_Manager : MonoBehaviour
{


    #region---------外部变量----------

    public GameObject playerGO;
    public ThirdPersonCharacter_WSM player_Script;

    public Transform FreeLookCameraRig;
    public AClose_Up texie;
    public FreeLookCam m_FreeLookCam;
    public ProtectCameraFromWallClip m_ProtectCameraFromWallClip;
    public GuanCha_WSM m_GanCha;

    public GameObject MiTiXiangJI;

    public GameObject ZhiYaoTai;  //制药台
    public L5_Intrument_F_Manager ZhiYaoTaiScript;
    public GameObject ZhiYaoTaiDaWuTi; //制药台的大盒子
    public Camera ZhiYaoTaiCamera;   //制药台Camera
    public bool CompletePharmaceutical = false;  //药品制作成功


    //路点
    public Transform[] playerPositions;
    public Transform[] TaskPosition;
    //门
    //关键门
    public GameObject Door_1;
    public GameObject Door_2;
    public Gate_WSM Door_2_Script;

    //5号门被小女孩打开
    //3号门是回来的门


    public GameObject TV;
    public Animator TV_Animator;
    public GameObject InputField_2;
    public Level_10_MakeRiddle JieMi;//谜题的管理类

    public GameObject JianShiQi;

    public Transform lookTransform;

    public bool isWanCheng = false;  //是否完成输入s

    public bool isHaveYao = false;

    public bool firstKO_SF = false;  //是否第一次打死怪物

    public bool HaveChainsaw = false;  //有电锯
    public bool CanChainsaw = false;   //可以开门

    public GameObject GuiZi;   //  砸到玩家的柜子

    public Transform[] MelissaPosition;

    public CRI_Manaager m_cri;
    public Transform[] waypoints_0;//飞虫路点
    public Transform[] waypoints_1;
    public Transform[] waypoints_2;



    public bool haveZhen = false;
    public bool haveDaYao = false;
    public bool haveXiaoYao = false;


    #endregion

    #region---------内部变量----------

    private bool TiShiZhiYao = false;
    private bool GauWu = false; //怪物出现
    private bool CRI_0 = false; //飞虫出现
    private bool CRI_1 = false; //飞虫消失点
    private bool MelissaKaiMen = false;//Melissa开门
    private static bool isInitNPC = false;

   public bool MiTi_0 = false;
   public bool MiTi_1 = false;

    private bool isBeiZa = false; //玩家是否被砸了

    public bool KaiQiZhong = false; //开启中

    public bool CompletePuzzle = false; //是否操作完控制台

    private bool IsTeXie = false;  //是否在特写

    private bool isTongGuo = false;

    #endregion

    #region---------调用方法----------

    public void Init()
    {
        GameObject SA = GameObject.Find("SA_INT");
        GameObject go = UtilFunction.ResourceLoad("Prefabs/WSM/Level10GameObjectManager");

        playerGO = GameObject.Find("ThirdPersonController");
        player_Script = playerGO.GetComponent<ThirdPersonCharacter_WSM>();
        FreeLookCameraRig = GameObject.Find("FreeLookCameraRig").transform;
        m_FreeLookCam = FreeLookCameraRig.GetComponent<FreeLookCam>();
        m_ProtectCameraFromWallClip = FreeLookCameraRig.GetComponent<ProtectCameraFromWallClip>();
        texie = FreeLookCameraRig.Find("Pivot/Main Camera").GetComponent<AClose_Up>();// 相机上的特写脚本
        m_GanCha = FreeLookCameraRig.Find("Pivot/Main Camera").GetComponent<GuanCha_WSM>();

        TV = SA.transform.Find("Ex_5C_F10/TV").gameObject;
        TV_Animator = TV.GetComponent<Animator>();

        ZhiYaoTaiDaWuTi = go.transform.Find("L5_Intrument_F_Boy").gameObject;

        InputField_2 = SA.transform.Find("Ex_5C_F10/TV/dianshiji_Da/dianshiji_3/Vault_Screen_Canvas/InputField").gameObject;
        JieMi = InputField_2.GetComponent<Level_10_MakeRiddle>();

        ZhiYaoTai = go.transform.Find("L5_Intrument_F").gameObject;
        ZhiYaoTaiScript = ZhiYaoTai.GetComponent<L5_Intrument_F_Manager>();

        ZhiYaoTaiCamera = go.transform.Find("ZhiYaoTaiCamera").GetComponent<Camera>();
        ZhiYaoTaiCamera.gameObject.SetActive(false);

        GuiZi = SA.transform.Find("Ex_5C_F10/fj_all/L_Desk_T").gameObject;

        Door_1 = SA.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_10F/EX_5B_10F_Door/EX_5B_10F_Door_1").gameObject;
        Door_2 = SA.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_10F/EX_5B_10F_Door/EX_5B_10F_Door_2").gameObject;
        Door_2_Script = Door_2.GetComponent<Gate_WSM>();

        lookTransform = go.transform.Find("LookTransform");

        playerPositions = new Transform[go.transform.Find("playerPositions").childCount];
        for (int i = 0; i < playerPositions.Length; i++)
        {
            playerPositions[i] = go.transform.Find("playerPositions").GetChild(i);
        }
        TaskPosition = new Transform[go.transform.Find("Task").childCount];
        for (int i = 0; i < TaskPosition.Length; i++)
        {
            TaskPosition[i] = go.transform.Find("Task").GetChild(i);
        }

        MiTiXiangJI = go.transform.Find("LookTransform/Camera").gameObject;
        MiTiXiangJI.SetActive(false);

        JianShiQi = SA.transform.Find("Ex_5C_F10/TV/dianshiji_Da/F10_dianshiji_videofeed").gameObject;
        Show_dianshiji_videofeed(false);

          //飞虫路点
          waypoints_0 = new Transform[go.transform.Find("CRIPositions").childCount];
        for (int i = 0; i < waypoints_0.Length; i++)
        {
            waypoints_0[i] = go.transform.Find("CRIPositions").GetChild(i);
        }
        waypoints_1 = new Transform[go.transform.Find("CRIPositions (1)").childCount];
        for (int i = 0; i < waypoints_1.Length; i++)
        {
            waypoints_1[i] = go.transform.Find("CRIPositions (1)").GetChild(i);
        }
        waypoints_2 = new Transform[go.transform.Find("CRIPositions (2)").childCount];
        for (int i = 0; i < waypoints_2.Length; i++)
        {
            waypoints_2[i] = go.transform.Find("CRIPositions (2)").GetChild(i);
        }
        m_cri = gameObject.AddComponent<CRI_Manaager>();
        m_cri.Init();


        MelissaPosition = new Transform[go.transform.Find("MelissaPosition").childCount];
        for (int i = 0; i < MelissaPosition.Length; i++)
        {
            MelissaPosition[i] = go.transform.Find("MelissaPosition").GetChild(i);
        }
    }

    public void FS_1()
    {
        firstKO_SF = true;
    }



    public void ShowTV()
    {
        texie.gameObject.SetActive(false);
        MiTiXiangJI.SetActive(true);
        GuanBiXiangJi(false);

        IsTeXie = true;
        DiYic(CompletePuzzle);

        TV_Animator.Play("Power0");
        KaiQiZhong = true;

    }

    public void WanCheng()
    {
        m_FreeLookCam.enabled = true;
        m_ProtectCameraFromWallClip.enabled = true;
    }

    public void GuanBiXiangJi(bool b)     //TODO 应该写在玩家身上     //关闭相机
    {
        m_FreeLookCam.enabled = b;
        m_ProtectCameraFromWallClip.enabled = b;
    }

    public void Landing()
    {
        TV_Animator.SetBool("1_2", true);

    }

    public void DiYic(bool b)
    {
        TV_Animator.SetBool("1_2", false);
        TV_Animator.SetBool("3_4", false);
        TV_Animator.SetBool("5_6", false);

        MiTi_0 = false;
        MiTi_1 = false;

        if (b)
        {
            TV_Animator.SetBool("2_3_a", false);
            TV_Animator.SetBool("2_3_b", true);
        }
        else
        {
            TV_Animator.SetBool("2_3_a", true);
            TV_Animator.SetBool("2_3_b", false);
        }
    }

    public void KanShiMiTi()   //开始谜题
    {
        if (!CompletePuzzle)
        {
            TV_Animator.SetBool("3_4", true);
            StartCoroutine(CloseDoor_defer(1f));
        }
        else
        {
            TV_Animator.SetBool("3_4", true);
            if (TaskStepManagaer.Instance.IsEqualTaskId(19003))
            {
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                StartCoroutine(YanShiTiaoChu());
            }
        }
    }

    public void Open_NPC_Door()  //关NPC测门关了
    {
        GeorgeManager.Instance.SetDoorOpen();
    }

    //是否显示监视器里的内容
    public void Show_dianshiji_videofeed(bool isShow)
    {
        if (isShow)
        {
            JianShiQi.transform.localPosition = new Vector3(JianShiQi.transform.localPosition.x, JianShiQi.transform.localPosition.y, 0.05f);
        }
        else
        {
            JianShiQi.transform.localPosition = new Vector3(JianShiQi.transform.localPosition.x, JianShiQi.transform.localPosition.y, 0.04f);
        }
    }

    private IEnumerator YanShiTiaoChu()
    {
        yield return new WaitForSeconds(2);
        TuiChuTeXie(true);
    }

    private IEnumerator CloseDoor_defer(float deferTime)
    {
        yield return new WaitForSeconds(deferTime);
        StartCoroutine(JieMi.CloseDoor_defer());
    }

    public void ShowZhiYaoTai()  //Dian kan Kong zhi tai
    {
        IsTeXie = true;
        GuanBiXiangJi(false);

        ZhiYaoTaiCamera.gameObject.SetActive(true);
        m_GanCha.gameObject.SetActive(false);

        player_Script.canMove = false;

    }

    #endregion

    #region---------功能方法----------

    public void InitNPCs()
    {
        AmyManager.Instance.Init();
        EmilyManager.Instance.Init();
        GeorgeManager.Instance.Init();
        LizzyManager.Instance.Init();
        MelissaManager.Instance.Init();
    }


    public void TuiChuTeXie(bool b = false)
    {
        if ((IsTeXie && Input.GetKeyDown(KeyCode.Escape))||b)
        {
            ZhiYaoTaiDaWuTi.SetActive(true);

            MiTiXiangJI.SetActive(false);
            ZhiYaoTaiCamera.gameObject.SetActive(false);
            texie.gameObject.SetActive(true);
            m_GanCha.gameObject.SetActive(true);

            GuanBiXiangJi(true);

            texie.gameObject.SetActive(true);
            texie.gameObject.transform.root.GetComponent<FreeLookCam>().enabled = true;
            player_Script.canMove = true;
            IsTeXie = false;
        }
    }



    private static Level_10_Manager _Instance;
    public static Level_10_Manager Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject go = new GameObject("Level_10_Manager");
                DontDestroyOnLoad(go);
                _Instance = go.AddComponent<Level_10_Manager>();
            }
            return _Instance;
        }
    }
    #endregion

    #region---------工具方法----------

    public void NPC_Distance()
    {
        //玩家到达指定位置加载NPC
        if (!isInitNPC && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[0].transform.position, 1))
        {
            isInitNPC = true;
            InitNPCs();
            //DoorManager.Instance.level_10_Door_Script[5].CanOpen = true;
        }

        //小女孩开门
        if (!MelissaKaiMen && MelissaManager.Instance.melissa && UtilFunction.IsReachDistanceXYZ(MelissaManager.Instance.melissa.transform.position, MelissaPosition[1].transform.position, 1f))
        {
            DoorManager.Instance.level_10_Door_Script[5].CanOpen = true;
            DoorManager.Instance.level_10_Door_Script[5].Operation();
            MelissaKaiMen = true;
        }

    }

    public void UpdatFS()
    {
        // 第一次出现FS
        if (!GauWu && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[3].transform.position, 2f))
        {
            FSManager.Instance.Init();
            GauWu = true;
        }
    }

    public void UpdateTiShiZhiYao()
    {
        //制药提示
        if (!TiShiZhiYao && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[6].transform.position, 0.5f))
        {
            PlaySoundController.Instance.PlaySoundEffect(gameObject, 1111);
            TiShiZhiYao = true;
        }
    }

    //TODO 如果进入就被机器人打死
    public void UpdateJiQiRen()
    {
        if (!isInitNPC && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[1].transform.position, 1))
        {

        }
        if (!GauWu && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[3].transform.position, 0.5f))
        {
            FSManager.Instance.Init();
            GauWu = true;

        }
    }

    public void GenerateCRI()
    {
        if (!CRI_0 && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[4].transform.position, 2f))
        {
            m_cri.CanShegnCheng(true);
            CRI_0 = true;
        }

        if (!CRI_1 && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[5].transform.position, 2f))
        {
            m_cri.CanShegnCheng(false);
            CRI_1 = true;
        }
    }

    public void UpdateBeiZa()
    {
        //被砸一下
        if (!isBeiZa && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, playerPositions[2].transform.position, 0.5f))
        {
            GuiZi.GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * 500);
            isBeiZa = true;

        }

        if (!MelissaKaiMen && MelissaManager.Instance.melissa && UtilFunction.IsReachDistanceXYZ(MelissaManager.Instance.melissa.transform.position, MelissaPosition[1].transform.position, 1f))
        {
            DoorManager.Instance.level_10_Door_Script[5].CanOpen = true;
            DoorManager.Instance.level_10_Door_Script[5].Operation();
            MelissaKaiMen = true;
        }


    }


    #endregion




    #region---------生命周期函数----------

    private void Start()
    {



    }




    void Update()
    {
        TuiChuTeXie();// 玩家是否推出特写
    }



    #endregion

}
