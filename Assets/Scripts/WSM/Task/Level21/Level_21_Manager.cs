using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Level_21_Manager : MonoBehaviour 
{

    //控制门
    //播放RAB信息
    //陷阱
    //控制电梯


    #region---------外部变量----------

    public GameObject RabTiShi;//Rad的信息；

    public GameObject Deng_GO;    //
    public Level_21_Deng Deng_Script;

    public bool HaveShouLie = false;  //有手雷

    public bool CanKaiDianTi = false;//可以开电梯

    public bool HaveKey = false;  //有开门的钥匙

    public bool RabYunLe = false; //

    public GameObject DianTi_GO;
    public Level_21_Elevator_WSM DianTi_Script;

    public Transform[] PlayerPositions;

    public GameObject playerGO;

    public GameObject Rab_GO;

    public GameObject camera_21;

    #endregion

    #region---------内部变量----------

    private bool isInDianTi = false;

    private bool GuiJi = false;

    private bool isLiang = false;

    private bool isTongGuo = false;

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        GameObject SA = GameObject.Find("SA_INT");
        GameObject go = UtilFunction.ResourceLoad("Prefabs/WSM/Level21GameObjectManager");

        playerGO = GameObject.Find("ThirdPersonController");

        Rab_GO = RabManager.Instance.rab;

        camera_21 = go.transform.Find("Camera").gameObject;
        camera_21.SetActive(false);

        DianTi_GO = SA.transform.Find("SA_Exterior_5/DianTi/DianTi_A/DianTi_A_Shang").gameObject;
        DianTi_Script = DianTi_GO.GetComponent<Level_21_Elevator_WSM>();
        DianTi_Script.Init();

        PlayerPositions = new Transform[go.transform.Find("playerPositions").childCount];
        for (int i = 0; i < PlayerPositions.Length; i++)
        {
            PlayerPositions[i] = go.transform.Find("playerPositions").GetChild(i);
        }

        RabTiShi = go.transform.Find("RABXinXi").gameObject;
        RabTiShi.SetActive(false);

        Deng_GO = go.transform.Find("Deng").gameObject;
        Deng_Script = Deng_GO.GetComponent<Level_21_Deng>();
        Deng_Script.Init();

    }

    public void XianShiRab()  //显示Rab的信息
    {
        RabTiShi.SetActive(true);
        PlaySoundController.Instance. PlaySoundEffect(RabTiShi,1111);

        if (Level_20_Manager.Instance.fenzhi == Level_20_Manager.FenZhi.B)
        {
            DoorManager.Instance.level_21_Door_Script[2].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[3].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[4].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[13].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[15].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[16].CanOpen = true;
        }
        else
        {
           
            DoorManager.Instance.level_21_Door_Script[1].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[2].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[13].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[15].CanOpen = true;
            DoorManager.Instance.level_21_Door_Script[16].CanOpen = true;
        }

      

    }

    public void Shan()
    {
        Deng_Script.Shan();
        isLiang = false;
    }

    public void Liang()
    {
        Deng_Script.Liang();
        isLiang = true;
    }

    #endregion

    #region---------功能方法----------

    public void A_Men()
    {
    }

    public void B_Men()
    {
    }

    public void B_Men_1()
    {
    }

    public void C_Men()
    {
    }

    public void C_Men_1()
    {
    }

    #endregion

    #region---------工具方法----------


    private static Level_21_Manager _Instance;
    public static Level_21_Manager Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject go = new GameObject("Level_21_Manager");
                DontDestroyOnLoad(go);
                _Instance = go.AddComponent<Level_21_Manager>();
            }
            return _Instance;
        }
    }
    #endregion

    #region---------生命周期函数----------

    private  void Start ()
	{
		
		
	
	}

	
	
	
 public	void Update_level_21 () 
	{

        if (!isInDianTi && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[0].position, 1f))
        {
                DianTi_Script.GuanDoor();
                isInDianTi = true;
        }

        if (!GuiJi && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[1].position, 1.5f))
        {
                PlaySoundController.Instance.PlaySoundEffect(PlayerPositions[1].gameObject,1111);
                GuiJi = true;
        }

        //if (!isTongGuo && UtilFunction.IsReachDistanceXYZ(playerGO.transform.position, PlayerPositions[2].transform.position, 2f))
        //{
        //    TaskStepManagaer.Instance.FinishTaskTo(28001);
        //    isTongGuo = true;
        //}

        if (isLiang && UtilFunction.IsReachDistanceXYZ(Rab_GO.transform.position, Deng_GO.transform.position, 1f))
        {
            Rab_GO.GetComponent<RabController>().OnAblepsia();
            isLiang = false;
        }

        if (camera_21.activeSelf && UtilFunction.IsLeaveDistanceXYZ(playerGO.transform.position, PlayerPositions[3].position, 3, 2))
        {
            camera_21.SetActive(false);
        }

    }



 #endregion

}
