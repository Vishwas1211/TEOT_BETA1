using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Level_20_Manager : MonoBehaviour
{


    #region---------外部变量----------
    //角色
    public GameObject PlayerGO;
    public ThirdPersonCharacter_WSM PlayerScript;

    public GameObject NPC_0_GO;
    public NPC_0 NPC_0_Script;

    public GameObject Zomber_1_GO;
    public Zomber_0_WSM Zomber_1_Script;

    public GameObject boss_1;

    public GameObject DianTi_A_Shang;

    public GameObject DianTi_B;

    //物品
    public GameObject KeyGO_1;

    public GameObject Key_B1;   //梯子
    public GameObject Key_B1_2;

    public GameObject Rope_1;
    public Vector3 Rope_deviation = new Vector3(0.5f, 0, 0.5f);

    public GameObject HaoMen_GO;
    public GameObject PoMen_GO;
    private Animator PoMen_Animatro;

    //位置

    public Transform[] positions;

    public Transform[] TaskPosition;

    public Vector3[] TuZiPositions = new Vector3[4];



    //============================================
    public bool playerHaveWuQi = false;
    public bool playerHaveKey = false;

    public FenZhi fenzhi = FenZhi.Null;

    public bool NPC_Die = false;  //是否有NPC死了

    public bool canPoMen = false;

    public bool DaoDa20 = false;//到达20层高度

    public bool hoaveKey_B1 = false;  //是否有梯子

    public bool HaveLuoSiDao = false;  //有螺丝刀

    public bool Have_ShouLie = false;  //有手雷
    public bool Have_ShouLie_b = false;

    public bool HaveQianZi = false; //有钳子
    public bool HaveDianJu = false; //电锯

    public bool HaveArms_0 = false;  //
    public bool HaveArms_1 = false;

    public bool DianTiXiouHaoLe = false;  //电梯修好了

    public NPC_State m_NPC_State = NPC_State.warring;

    #endregion

    #region---------内部变量----------

    public enum FenZhi
    {
        Null,
        A,
        B,
        C,
    }

    public enum NPC_State
    {
        warring,    // 交战状态
        survive,    // 获救
        death,      //有人死亡
    }


    #endregion

    #region---------调用方法----------

    public void Init()
    {
        PlayerGO = GameObject.Find("ThirdPersonController");
        PlayerScript = PlayerGO.GetComponent<ThirdPersonCharacter_WSM>();

        GameObject SA = GameObject.Find("SA_INT");
        GameObject go = UtilFunction.ResourceLoad("Prefabs/WSM/Level20GameObjectManager");

        NPC_0_GO = go.transform.Find("NPC_0").gameObject;  //被僵尸追的NPC
        NPC_0_Script = NPC_0_GO.GetComponent<NPC_0>();
        NPC_0_Script.Init();

        Zomber_1_GO = go.transform.Find("Zomber_0").gameObject;
        Zomber_1_Script = Zomber_1_GO.GetComponent<Zomber_0_WSM>();
        Zomber_1_Script.Init();

        boss_1 = go.transform.Find("Boss").gameObject;

        positions = new Transform[go.transform.Find("Position").childCount];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = go.transform.Find("Position").GetChild(i);
        }
        TaskPosition = new Transform[go.transform.Find("Task").childCount];
        for (int i = 0; i < TaskPosition.Length; i++)
        {
            TaskPosition[i] = go.transform.Find("Task").GetChild(i);
        }

        HaoMen_GO = SA.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_20F/EX_5C_20F_Door/EX_5C_20F_Door_BreakHide").gameObject;
        PoMen_GO = SA.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_20F/EX_5C_20F_Door/F20_Door_Collision").gameObject;
        PoMen_Animatro = PoMen_GO.GetComponent<Animator>();
        PoMen_GO.SetActive(false);

        DianTi_A_Shang = GameObject.Find("DianTi_A_Shang"); //电梯

        DianTi_B = GameObject.Find("DianTi_CG");


        Rope_1 = go.gameObject.transform.Find("Rope").gameObject;  //绳子

        Key_B1 = go.gameObject.transform.Find("Key_B1").gameObject;  //梯子


        Key_B1_2 = go.transform.Find("EX_5C_20F_Door").gameObject;
        Key_B1_2.SetActive(false);



    }

    public void SetFenZhi(FenZhi f)
    {
        fenzhi = f;

        switch (fenzhi)
        {
            case FenZhi.A:
                DoorManager.Instance.Level_21_Preprocessing_A();
                break;
            case FenZhi.B:

                //关掉Rick
                RickManager.Instance.rick.SetActive(false);
                DoorManager.Instance.Level_21_Preprocessing_B();
                break;
            case FenZhi.C:
                RickManager.Instance.rick.SetActive(false);
                DoorManager.Instance.Level_21_Preprocessing_C();
                break;
            default:
                break;
        }
    }

    public void SetNPCstate(NPC_State s)
    {
        m_NPC_State = s;
    }

    public void ShowDoor(bool isShow)
    {
        Key_B1_2.SetActive(isShow);
    }


    public void PoMen()
    {
        PoMen_GO.SetActive(true);
        HaoMen_GO.SetActive(false);
        PoMen_Animatro.SetBool("can", true);
        DaFeiWanJia();//打飞玩家


        boss_1.GetComponent<Zomber_F5_18>().ZhuiJi();
    }



    public void BranchA1_21()
    {

    }

    #endregion

    #region---------功能方法----------



    public void UpdateStartLevel20()  //玩家到达20层
    {

        if (fenzhi == FenZhi.Null
            && !DaoDa20
            && UtilFunction.IsReachDistanceXYZ(PlayerGO.transform.position, positions[0].position, 1, 1f)
            && PlayerGO.transform.position.y > positions[0].position.y)
        {
            CiviliansManager.Instance.Init();
            NPC_0_Script.KanShi();
            Zomber_1_Script.KaiShi();
            fenzhi = FenZhi.B;

            DaoDa20 = true;
        }
    }

    private void DaFeiWanJia()
    {
        PlayerGO.transform.DOMove(positions[3].position, 2f);
    }

    private static Level_20_Manager _instance;  //单例
    public static Level_20_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Level20_Manager");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<Level_20_Manager>();
            }
            return _instance;
        }
    }
    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private void Start()
    {



    }




    void Update()
    {
        if (PlayerGO == null) return;

        UpdateStartLevel20();
        if (fenzhi == FenZhi.Null && UtilFunction.IsReachDistanceXYZ(PlayerGO.transform.position, positions[5].position, 1f, 1f))
        {
            fenzhi = FenZhi.A;
        }


        if (hoaveKey_B1)
        {
            if (UtilFunction.IsReachDistanceXYZ(PlayerGO.transform.position, positions[4].position, 0.5f))
            {
                Key_B1_2.SetActive(true);
                hoaveKey_B1 = false;
            }
        }

    }



    #endregion

}
