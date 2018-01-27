using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Level_05_Manager : MonoBehaviour
{
    #region---------外部变量----------

    private bool CanLevel5 = false;  //开启第五层

    public Transform FreeLookCameraRig;
    public FreeLookCam m_FreeLookCam;
    public ProtectCameraFromWallClip m_ProtectCameraFromWallClip;

    public GameObject playerGO;

    private GameObject LouTi_5_1_Model;   //楼梯
    private GameObject LouTi_5_1_Collider;//楼梯的碰撞器
    private GameObject LouTi_5_1_PoSui;   //可以破损的楼梯
    private Animator LouTi_5_1_PoSuiAnimator; //破损动画

    private GameObject DiBan_F5_4_Model;   //地板
    private GameObject DiBan_F5_4_Collider;//地板碰撞器
    private GameObject DiBan_F5_4PlSun;
    public Animator DiBan_F5_4Animator;

    private GameObject FuTou;              //斧头

    private GameObject Radio_GameObject;   //收音机
    public Level5_Radio Radio_Script;

    private GameObject TiZi_F5_25_Model;             // 楼后面的梯子
    private GameObject TiZi_F5_25_Collider_Ground;
    private GameObject TiZi_F5_25_Collider_Wall;

    private GameObject JayceeMonster_TiZi;             //爬梯子的怪物Jaycee
    public Level_05_JayceeBoss_2 JayceeMonster_TiZi_Script;

    private GameObject F5_6_Zomber;         //拦路僵尸
    private Zomber_5F_6 F5_6_Zomber_Script;

    private GameObject F5_13_Zomber;         //拦路僵尸
    private Zomber_5F_6 F5_13_Zomber_Script;

    private GameObject F5_16Zomeber;
    private Zomber_F5_16 F5_16ZomeberScript;

    private GameObject F5_21_Hint;           //提示

    //public GameObject X_Window_1;           //有X的窗户
    //public GameObject X_Window_2;
    //public GameObject X_Window_3;

    public GameObject X_Window;           //有X的窗户
    public OpenDoor_WSM X_WindowScript;
    public GameObject LianTiao;          //X窗户的锁链
    public bool haveLianTiao = false;
    public GameObject JuanLianMen;       //卷帘门
    public bool canUp = false;

    public Transform[] PlayerPositions;

    public Transform[] TaskPosition;

    public GameObject F5_18;

    #endregion

    #region---------内部变量----------

    private bool PoSun_0 = false;
    private bool PoSun_1 = false;
    private bool PoSun_2 = false;

    private bool enemy_06 = false;
    private bool enemy_13 = false;
    private bool enemy_16 = false;

    private bool PoSui_DiMian = false; //地面

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        GameObject go2 = GameObject.Find("SA_INT");
        GameObject go = UtilFunction.ResourceLoad("Prefabs/WSM/Level05GameObjectManager");

        playerGO = GameObject.Find("ThirdPersonController"); //TODO
        FreeLookCameraRig = GameObject.Find("FreeLookCameraRig").transform;
        m_FreeLookCam = FreeLookCameraRig.GetComponent<FreeLookCam>();
        m_ProtectCameraFromWallClip = FreeLookCameraRig.GetComponent<ProtectCameraFromWallClip>();

        PlayerPositions = new Transform[go.transform.Find("PlayerPosition").childCount];
        for (int i=0;i< PlayerPositions.Length;i++)
        {
            PlayerPositions[i] = go.transform.Find("PlayerPosition").GetChild(i);
        }
        TaskPosition = new Transform[go.transform.Find("Task").childCount];
        for (int i = 0; i < TaskPosition.Length; i++)
        {
            TaskPosition[i] = go.transform.Find("Task").GetChild(i);
        }
       
        //楼梯
        LouTi_5_1_PoSui = go2.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_4F/EX_5B_4F_Stairs/EX_5A_4F_Stairs_B/F4_Stairs_Collision").gameObject;
        LouTi_5_1_PoSuiAnimator = LouTi_5_1_PoSui.GetComponent<Animator>();
        LouTi_5_1_Collider = go2.transform.Find("WALL_Collider/4F/Stairs/PoSuiTiZi").gameObject;
        
        LouTi_5_1_Model = GameObject.Find("LoTi_5_1");
        //LouTi_5_1_Collider = go.transform.Find("LouTi_5_1_Collider").gameObject;
        DiBan_F5_4PlSun = go2.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_5F/EX_5B_5F_Floor/F5_Floor_Collision").gameObject;
        DiBan_F5_4Animator = DiBan_F5_4PlSun.GetComponent<Animator>();

        //地板
        //DiBan_F5_4_Model = GameObject.Find("EX_5B_5F_Floor1");
        DiBan_F5_4_Collider = go.transform.Find("LouTi_5_1_Collider").gameObject;

        TiZi_F5_25_Model = GameObject.Find("LouTi_1");
        TiZi_F5_25_Collider_Ground = GameObject.Find("F5_25_Ground");
        TiZi_F5_25_Collider_Wall = GameObject.Find("F5_25_Wall");

        Radio_GameObject = go.transform.Find("Radio").gameObject;
        Radio_Script = Radio_GameObject.GetComponent<Level5_Radio>();
        Radio_Script.Init();

        F5_16Zomeber = go.transform.Find("Enemy/F5_16_Zomber").gameObject;
        F5_16ZomeberScript = F5_16Zomeber.GetComponent<Zomber_F5_16>();
        F5_16ZomeberScript.Init();

        F5_6_Zomber =go.transform.Find("Enemy/F5_06_Zomber").gameObject;
        F5_6_Zomber_Script = F5_6_Zomber.GetComponent<Zomber_5F_6>();

        F5_13_Zomber = go.transform.Find("Enemy/F5_13_Zomber").gameObject;
        F5_13_Zomber_Script = F5_13_Zomber.GetComponent<Zomber_5F_6>();

        LianTiao = go.transform.Find("WanZhengSuoLian").gameObject;
        JuanLianMen = GameObject.Find("EX_5B_5F_Curtain_2");
        X_Window = GameObject.Find("EX_5B_5F_Window_2");
        X_WindowScript = X_Window.GetComponent<OpenDoor_WSM>();

        JayceeMonster_TiZi = go.transform.Find("Enemy/JayceeBoss_2").gameObject;
        JayceeMonster_TiZi_Script = JayceeMonster_TiZi.GetComponent<Level_05_JayceeBoss_2>();
        JayceeMonster_TiZi.SetActive(false);

        F5_21_Hint = GameObject.Find("F5_21_Hint");
        Show_F5_21_Hint(false);


        CanLevel5 = true;
    }

    public void PlayLouTi_5_1_PoSuiAnimator_0()   //破损的楼梯动画到第零阶段
    {
        LouTi_5_1_PoSuiAnimator.Play("Take 001 0");
    }
    public void PlayLouTi_5_1_PoSuiAnimator_1()   //破损的楼梯动画到第一阶段
    {
        LouTi_5_1_PoSuiAnimator.SetBool("1", true);
    }
    public void PlayLouTi_5_1_PoSuiAnimator_2()   //破损的楼梯动画到第二阶段
    {
        LouTi_5_1_PoSuiAnimator.SetBool("2", true);
    }
    public void PlayLouTi_5_1_PoSuiAnimator_3()   //破损的楼梯动画到第三阶段
    {
        LouTi_5_1_Collider.SetActive(false);
    }


    public void ShowSuoLian(bool show)   //X 窗户的链子
    {
        LianTiao.SetActive(show);
        X_WindowScript.CanOpen = true;
    }

    public void ShowJayceeMonster_TiZi()  //出现爬梯子的Jaycee
    {
        Instantiate(JayceeMonster_TiZi);
    }

    public void Shock_TiZi_F5_25(float amplitude)  //梯子震动
    {
        this.amplitude = amplitude;
        canChock = true;
    }

    public void Show_F5_21_Hint(bool show)         //显示X窗户的提示
    {
        F5_21_Hint.SetActive(show);
    }

    public void GuanBiXiangJi(bool b)     //TODO 应该写在玩家身上     //关闭相机
    {
        m_FreeLookCam.enabled = b;
        m_ProtectCameraFromWallClip.enabled = b;
    }

    #endregion

    #region---------功能方法----------

    private bool canChock = false;
    private float amplitude = 0; //震动幅度
    public void UpdateShock_TiZi_F5_25()
    {
        if (canChock)
        {
            if (amplitude > 0)
            {
                TiZi_F5_25_Collider_Ground.transform.rotation = Quaternion.Euler(0, Random.Range(-amplitude, amplitude), Random.Range(-amplitude, amplitude));
                TiZi_F5_25_Model.transform.rotation = Quaternion.Euler(0, Random.Range(-amplitude, amplitude), Random.Range(-amplitude, amplitude));
                amplitude -= Time.deltaTime;
            }
            else
            {
                canChock = false;
               
            }
        }
    }

    private void UpdateShutter()  //移动卷帘门
    {
        if (canUp &&  JuanLianMen.transform.position.y < 30.22597f)
        {
        JuanLianMen.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }


    //初始化所有的门
    public void InitializationDoor() //TODO
    {

    }
    //初始化所有的敌人
    public void InitializationEnemy()
    {
        enemy_16 = false;
        enemy_06 = false;
        enemy_13 = false;
    }

    //破损
    public void UpdatePoSui()
    {
        //楼梯破损
        if (!PoSun_0 && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[0].transform.position, 1, 2))
        {
            PlayLouTi_5_1_PoSuiAnimator_0();
            PoSun_0 = true;
        }

        if (!PoSun_1 && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[1].transform.position, 1, 2))
        {
            PlayLouTi_5_1_PoSuiAnimator_1();
            PoSun_1 = true;
        }
        if (!PoSun_2 && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[2].transform.position, 1, 2))
        {
            PlayLouTi_5_1_PoSuiAnimator_2();
            PoSun_2 = true;
        }
        if (UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[9].transform.position, 1, 2))
        {
            PlayLouTi_5_1_PoSuiAnimator_3();
        }

        //地板破损
        if (UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[4].transform.position, 1, 2))
        {
            DiBan_F5_4Animator.SetBool("1", true);
            DiBan_F5_4_Collider.SetActive(false);
            PoSui_DiMian = true;
            ////隐藏掉楼梯碎片
            //LouTi_5_1_PoSui.SetActive(false);
        }

        if (PoSui_DiMian && playerGO.transform.position.y < PlayerPositions[11].position.y)
        {
            DiBan_F5_4Animator.gameObject.SetActive(false);
            PoSui_DiMian = false;
        }
    }

    //Jaycee开门
    public void  UpdateJayceeOpenDoor()
    {
        // 。。。开门

        if (TaskStepManagaer.Instance.curTaskId >= 6005
            && !DoorManager.Instance.level_05_Door_Script[5].CanOpen
            && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_05_Door_GameObjects[5].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
        {
            DoorManager.Instance.level_05_Door_Script[5].CanOpen = true;
            DoorManager.Instance.level_05_Door_Script[5].Operation();
        }

        if (TaskStepManagaer.Instance.curTaskId >= 6005 &&
            !DoorManager.Instance.level_05_Door_Script[13].CanOpen
            && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_05_Door_GameObjects[13].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
        {
            DoorManager.Instance.level_05_Door_Script[13].CanOpen = true;
            DoorManager.Instance.level_05_Door_Script[13].Operation();
        }

        if (TaskStepManagaer.Instance.curTaskId >= 6005 &&
            !DoorManager.Instance.level_05_Door_Script[1].CanOpen && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_05_Door_GameObjects[1].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
        {
            DoorManager.Instance.level_05_Door_Script[1].CanOpen = true;
            DoorManager.Instance.level_05_Door_Script[1].Operation();
        }
        if (TaskStepManagaer.Instance.curTaskId >= 6005 &&
            !DoorManager.Instance.level_05_Door_Script[20].CanOpen && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_05_Door_GameObjects[20].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
        {
            DoorManager.Instance.level_05_Door_Script[20].CanOpen = true;
            DoorManager.Instance.level_05_Door_Script[20].Operation();
        }
        if (TaskStepManagaer.Instance.curTaskId >= 6005 &&
            !DoorManager.Instance.level_05_Door_Script[0].CanOpen && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_05_Door_GameObjects[0].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
        {
            DoorManager.Instance.level_05_Door_Script[0].CanOpen = true;
            DoorManager.Instance.level_05_Door_Script[0].Operation();
        }
    }

    //收音机
    public void UpdateRadio()
    {
        if (!Radio_Script.isOpen && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[3].transform.position, 3, 2))
        {
            Radio_Script.OpenRadio(true);
        }

        if (Radio_Script.isOpen && UtilFunction.IsLeaveDistanceXYZ(playerGO.transform.position, PlayerPositions[3].transform.position, 3, 2))
        {
            Radio_Script.OpenRadio(false);
        }
    }

    //敌人
    public void UpdateEnemy()
    {

        if (!enemy_16 &&
            TaskStepManagaer.Instance.curTaskId >= 6005 &&
          (JayceeManager.Instance.jayceeStoryProcess.curTaskID == 1033
          || JayceeManager.Instance.jayceeStoryProcess.curTaskID == 1032
          || JayceeManager.Instance.jayceeStoryProcess.curTaskID == 1031)
          && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[8].transform.position, 0.5f, 2))
        {
            F5_16Zomeber.SetActive(true);
            enemy_16 = true;
        }

        if(!enemy_06
            && TaskStepManagaer.Instance.curTaskId >= 6005 
            && UtilFunction.IsReachDistanceXYZ(JayceeManager.Instance.humanState.transform.position, PlayerPositions[6].transform.position, 0.5f, 2))
        {
            enemy_06 = true;
            F5_6_Zomber_Script.KanShi();
        }

        if (!enemy_13
            && TaskStepManagaer.Instance.curTaskId >= 6005
           && UtilFunction.IsReachDistanceXYZ(JayceeManager.Instance.humanState.transform.position, PlayerPositions[7].transform.position, 0.5f, 2))
        {
            enemy_13 = true;
            F5_13_Zomber_Script.KanShi();
        }

    }

    private static Level_05_Manager _instance;
    public static Level_05_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Level_05_Manager");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<Level_05_Manager>();
            }
            return _instance;
        }
    }

    #endregion

    #region---------工具方法----------

  

    #endregion

    #region---------生命周期函数----------

    private  void Start ()
	{
    }

    void Update()
    {
        if (!CanLevel5)
        {
            return;
        }

        UpdateShock_TiZi_F5_25();
        UpdateShutter();

        //UpdateRadio();
        //UpdateEnemy();
        //UpdatePoSui();
        //UpdateJayceeOpenDoor();


        if (!JayceeMonster_TiZi.activeSelf && playerGO.transform.position.y >= PlayerPositions[10].transform.position.y)
        {
            JayceeMonster_TiZi.SetActive(true);
            JayceeMonster_TiZi_Script.GoUp();
        }

    }


   
   
   
   
   

   
   
   
   
   

   
   
   
   

 #endregion

}
