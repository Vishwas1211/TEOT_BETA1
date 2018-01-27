using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorManager : MonoBehaviour 
{
    private const string LEVEL_01_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5A_Indoor_1F/EX_5A_1F_Back/EX_5A_1F_Door/";
    private const string LEVEL_04_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_4F/EX_4F_Door/";
    private const string LEVEL_05_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_5F/EX_5B_5F_Door/";
    private const string LEVEL_10_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_10F/EX_5B_10F_Door/";
    private const string LEVEL_11_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5B_Indoor_11F/EX_5B_11F_Door/";
    private const string LEVEL_13_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_13F/EX_5C_13F_Door/";
    private const string LEVEL_18_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_18F/EX_5C_18F_Door/";
    private const string LEVEL_20_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_20F/EX_5C_20F_Door/";
    private const string LEVEL_21A_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_21F/EX_5C_Indoor_21F_A/EX_5C_21F_A_Door/";
    private const string LEVEL_21B_DOOR_ROUTE = "SA_Exterior_5/EX_5A_Indoor/EX_5C_Indoor_21F/EX_5C_Indoor_21F_B/EX_5C_21F_B_Door/";
    private const string LEVEL_28_DOOR_ROUTE_1 = "SA_Exterior_5/Exterior_5C/Exterior_5C2/Exterior_5C2_Door";
    private const string LEVEL_28_DOOR_ROUTE_2 = "SA_Exterior_5/Exterior_5C/Exterior_5C3/Exterior_5C3_Door/Exterior_5C3_Door_1";


    #region---------外部变量----------

    public List<GameObject> level_01_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_04_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_05_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_10_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_11_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_13_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_18_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_20_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_21_Door_GameObjects = new List<GameObject>();
    public List<GameObject> level_28_Door_GameObjects = new List<GameObject>();


    public List<OpenDoor_WSM> level_01_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_04_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_05_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_10_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_11_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_13_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_18_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_20_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_21_Door_Script = new List<OpenDoor_WSM>();
    public List<OpenDoor_WSM> level_28_Door_Script = new List<OpenDoor_WSM>();

    public GameObject Player_GameObject
    {
        get
        {
            if (_player_GameObject == null)
            {
                _player_GameObject = GameObject.Find("ThirdPersonController");
            }
            return _player_GameObject;
        }
    }
    private GameObject _player_GameObject;

    #endregion

    #region---------内部变量----------



    #endregion

    #region---------调用方法----------

    public void Init()
    {
        GameObject go = GameObject.Find("SA_INT");
        Init_level_01(go);
        Init_level_04(go);
        Init_level_05(go);
        Init_level_10(go);
        Init_level_11(go);
        Init_level_13(go);
        Init_level_18(go);
        Init_level_20(go);
        Init_level_21(go);
        Init_level_28(go);

        Level_01_Preprocessing();
        Level_04_Preprocessing();
        Level_05_Preprocessing();
        Level_10_Preprocessing();
        Level_11_Preprocessing();
        Level_13_Preprocessing();
        Level_18_Preprocessing();
        Level_20_Preprocessing();
    }



    #endregion

    #region---------功能方法----------

    public void Init_level_01(GameObject go)
    {
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_1").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_2").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_3").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_4").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_5").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_6").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_7").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_8").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_9").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_10").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_11").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_12").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_13").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_14").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_15").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_16").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_17").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_18").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_19").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_20").gameObject);
        level_01_Door_GameObjects.Add(go.transform.Find(LEVEL_01_DOOR_ROUTE + "EX_5A_1F_Door_21").gameObject);

        for (int i = 0; i < level_01_Door_GameObjects.Count; i++)
        {
            if (!level_01_Door_GameObjects[i].GetComponent<OpenDoor_WSM>())
            {
             level_01_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            }
            level_01_Door_Script.Add(level_01_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_01_Door_Script [i].Init();
        }


    }
    public void Init_level_04(GameObject go)
    {
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_1").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_2").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_3").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_4").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_5").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_6").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_7").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_8").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_9").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_10").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_11").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_13").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_14").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_15").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_16").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_17").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_18").gameObject);
        level_04_Door_GameObjects.Add(go.transform.Find(LEVEL_04_DOOR_ROUTE + "EX_5A_4F_door_19").gameObject);


        for (int i = 0; i < level_04_Door_GameObjects.Count; i++)
        {
            if (!level_04_Door_GameObjects[i].GetComponent<OpenDoor_WSM>())
            {
                level_04_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
                if (i==17)
                {
                    level_04_Door_GameObjects[i].AddComponent<L4CloseDoor>();
                }
            }
            level_04_Door_Script.Add(level_04_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_04_Door_Script[i].Init();
        }
    }
    public void Init_level_05(GameObject go)
    {
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_1").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_2").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_3").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_4").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_5").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_6").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_7").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_8").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_10").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_11").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_12").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_13").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_15").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_16").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_17").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_18").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_19").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_20").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_21").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_22").gameObject);
        level_05_Door_GameObjects.Add(go.transform.Find(LEVEL_05_DOOR_ROUTE + "EX_5B_5F_Doo_24").gameObject);

        for (int i = 0; i < level_05_Door_GameObjects.Count; i++)
        {
            level_05_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_05_Door_Script.Add(level_05_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_05_Door_Script[i].Init();
        }
    }
    public void Init_level_10(GameObject go)
    {
        //level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_1").gameObject);
        //level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_2").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_3").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_4").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_5").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_6").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_7").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_8").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_9").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_10").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_11").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_12").gameObject);
        level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_13").gameObject);
        //level_10_Door_GameObjects.Add(go.transform.Find(LEVEL_10_DOOR_ROUTE + "EX_5B_10F_Door_14").gameObject);


        for (int i = 0; i < level_10_Door_GameObjects.Count; i++)
        {
            level_10_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_10_Door_Script.Add(level_10_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_10_Door_Script[i].Init();
        }
    }
    public void Init_level_11(GameObject go)
    {
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_1").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_2").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_3").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_4").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_5").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_6").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_7").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_8").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_9").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_10").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_11").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_12").gameObject);
        level_11_Door_GameObjects.Add(go.transform.Find(LEVEL_11_DOOR_ROUTE + "EX_5B_11F_Door_13").gameObject);


        for (int i = 0; i < level_11_Door_GameObjects.Count; i++)
        {
            level_11_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_11_Door_Script.Add(level_11_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_11_Door_Script[i].Init();
        }
    }
    public void Init_level_13(GameObject go)
    {
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_1").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_2").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_4").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_5").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_6").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_7").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_8").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_9").gameObject);
        level_13_Door_GameObjects.Add(go.transform.Find(LEVEL_13_DOOR_ROUTE + "EX_5C_13F_Door_10").gameObject);

        for (int i = 0; i < level_13_Door_GameObjects.Count; i++)
        {
            level_13_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_13_Door_Script.Add(level_13_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_13_Door_Script[i].Init();
        }
    }
    public void Init_level_18(GameObject go)
    {
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_2").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_3").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_4").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_5").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_6").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_7").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_8").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_9").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_10").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_11").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_12").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_13").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_14").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_15").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_16").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_17").gameObject);
        level_18_Door_GameObjects.Add(go.transform.Find(LEVEL_18_DOOR_ROUTE + "EX_5C_18F_Door_18").gameObject);


        for (int i = 0; i < level_18_Door_GameObjects.Count; i++)
        {
            level_18_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_18_Door_Script.Add(level_18_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_18_Door_Script[i].Init();
        }
    }
    public void Init_level_20(GameObject go)
    {
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_0").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_1").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_2").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_3").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_4").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_5").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_6").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_8").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_9").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_10").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_11").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_13").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_14").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_15").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_16").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_17").gameObject);
        level_20_Door_GameObjects.Add(go.transform.Find(LEVEL_20_DOOR_ROUTE + "EX_5C_20F_Door_18").gameObject);


        for (int i = 0; i < level_20_Door_GameObjects.Count; i++)
        {
            level_20_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_20_Door_Script.Add(level_20_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_20_Door_Script[i].Init();
        }
    }
    public void Init_level_21(GameObject go)
    {
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_1").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_2").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_3").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_4").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_5").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_6").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_9").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_10").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_15").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_16").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_17").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21A_DOOR_ROUTE + "EX_5C_21F_A_Door_18").gameObject);

        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_1").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_2").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_3").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_4").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_5").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_6").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_7").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_8").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_9").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_10").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_13").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_14").gameObject);
        level_21_Door_GameObjects.Add(go.transform.Find(LEVEL_21B_DOOR_ROUTE + "EX_5C_21F_B_Door_15").gameObject);



        for (int i = 0; i < level_21_Door_GameObjects.Count; i++)
        {
            level_21_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_21_Door_Script.Add(level_21_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_21_Door_Script[i].Init();
        }
    }
    public void Init_level_28(GameObject go)
    {
        level_28_Door_GameObjects.Add(go.transform.Find(LEVEL_28_DOOR_ROUTE_1).gameObject);
        level_28_Door_GameObjects.Add(go.transform.Find(LEVEL_28_DOOR_ROUTE_2).gameObject);


        for (int i = 0; i < level_28_Door_GameObjects.Count; i++)
        {
            level_28_Door_GameObjects[i].AddComponent<OpenDoor_WSM>();
            level_28_Door_Script.Add(level_28_Door_GameObjects[i].GetComponent<OpenDoor_WSM>());
            level_28_Door_Script[i].Init();
        }
    }

    public void Level_01_Preprocessing()
    {
        level_01_Door_Script[19].CanOpen = false;
        //level_04_Door_Script[7].CanOpen = false;


        //要反转轴的门
        level_01_Door_Script[3].isFanXiang = true;
        level_01_Door_Script[4].isFanXiang = true;
        level_01_Door_Script[7].isFanXiang = true;
        level_01_Door_Script[8].isFanXiang = true;
        level_01_Door_Script[12].isFanXiang = true;
        level_01_Door_Script[15].isFanXiang = true;
        level_01_Door_Script[19].isFanXiang = true;
        level_01_Door_Script[20].isFanXiang = true;
    }
    public void Level_04_Preprocessing()
    {
        level_04_Door_Script[12].CanOpen = false;
        level_04_Door_Script[16].CanOpen = false;
        level_04_Door_Script[15].CanOpen = false;
        level_04_Door_Script[17].CanOpen = false;
        level_04_Door_Script[7].CanOpen = false;
        level_04_Door_Script[1].CanOpen = false;
        level_04_Door_Script[13].CanOpen = false;
        level_04_Door_Script[14].CanOpen = false;


        //要反转轴的门
        level_04_Door_Script[0].isFanXiang = true;
        level_04_Door_Script[1].isFanXiang = true;
        level_04_Door_Script[3].isFanXiang = true;
        level_04_Door_Script[8].isFanXiang = true;
    }
    public void Level_05_Preprocessing()
    {
        //要锁的门
        level_05_Door_Script[6].CanOpen = false;
        level_05_Door_Script[9].CanOpen = false;
        level_05_Door_Script[7].CanOpen = false;
        level_05_Door_Script[2].CanOpen = false;
        level_05_Door_Script[5].CanOpen = false;
        level_05_Door_Script[13].CanOpen = false;
        level_05_Door_Script[1].CanOpen = false;
        level_05_Door_Script[20].CanOpen = false;
        level_05_Door_Script[0].CanOpen = false;
        //要反转轴的门
        level_05_Door_Script[17].isFanXiang = true;
        level_05_Door_Script[14].isFanXiang = true;
        level_05_Door_Script[19].isFanXiang = true;
    }
    public void Level_10_Preprocessing()
    {
        level_10_Door_Script[3].CanOpen = false;
        level_10_Door_Script[5].CanOpen = false;

        //要反转轴的门
        level_10_Door_Script[5].isFanXiang = true;
        level_10_Door_Script[7].isFanXiang = true;
        level_10_Door_Script[8].isFanXiang = true;
        level_10_Door_Script[9].isFanXiang = true;
    }
    public void Level_11_Preprocessing()
    {
        level_11_Door_Script[3].CanOpen = false;
        level_11_Door_Script[4].CanOpen = false;
        level_11_Door_Script[10].CanOpen = false;
        level_11_Door_Script[9].CanOpen = false;
        level_11_Door_Script[1].CanOpen = false;
        level_11_Door_Script[6].CanOpen = false;
        level_11_Door_Script[7].CanOpen = false;

        level_11_Door_Script[11].CanOpen = false;
        level_11_Door_Script[12].CanOpen = false;

        //要反转轴的门
        level_11_Door_Script[0].isFanXiang = true;
        level_11_Door_Script[1].isFanXiang = true;
        level_11_Door_Script[2].isFanXiang = true;
    }
    public void Level_13_Preprocessing()
    {
        //要反转轴的门
        level_13_Door_Script[0].isFanXiang = true;
        level_13_Door_Script[1].isFanXiang = true;
        level_13_Door_Script[5].isFanXiang = true;
        level_13_Door_Script[8].isFanXiang = true;
    }
    public void Level_18_Preprocessing()
    {
        //要反转轴的门
        level_18_Door_Script[3].isFanXiang = true;
        level_18_Door_Script[4].isFanXiang = true;
        level_18_Door_Script[5].isFanXiang = true;
        level_18_Door_Script[6].isFanXiang = true;
        level_18_Door_Script[7].isFanXiang = true;
        level_18_Door_Script[9].isFanXiang = true;
        level_18_Door_Script[12].isFanXiang = true;
        level_18_Door_Script[13].isFanXiang = true;
        level_18_Door_Script[14].isFanXiang = true;
        level_18_Door_Script[15].isFanXiang = true;
    }
    public void Level_20_Preprocessing()
    {
        level_20_Door_Script[6].CanOpen = false;


        //要反转轴的门
        level_20_Door_Script[7].isFanXiang = true;
        level_20_Door_Script[9].isFanXiang = true;
        level_20_Door_Script[10].isFanXiang = true;
        level_20_Door_Script[11].isFanXiang = true;
        level_20_Door_Script[12].isFanXiang = true;
        level_20_Door_Script[15].isFanXiang = true;

        level_21_Door_Script[7].isFanXiang = true;
        level_21_Door_Script[9].isFanXiang = true;
        level_21_Door_Script[24].isFanXiang = true;
    }
    public void Level_21_Preprocessing_A()
    {
        //关
        level_20_Door_Script[0].CanOpen = false;
        level_20_Door_Script[1].CanOpen = false;
        level_20_Door_Script[2].CanOpen = false;
        level_20_Door_Script[3].CanOpen = false;
        level_20_Door_Script[4].CanOpen = false;
        level_20_Door_Script[5].CanOpen = false;
        level_20_Door_Script[6].CanOpen = false;
        level_20_Door_Script[7].CanOpen = false;
        level_20_Door_Script[8].CanOpen = false;
        level_20_Door_Script[9].CanOpen = false;
        level_20_Door_Script[10].CanOpen = false;
        level_20_Door_Script[11].CanOpen = false;
        level_20_Door_Script[12].CanOpen = false;
        level_20_Door_Script[18].CanOpen = false;
        level_20_Door_Script[15].CanOpen = false;

        level_20_Door_Script[1].CanOpen = true;
        level_20_Door_Script[13].CanOpen = true;
        level_20_Door_Script[14].CanOpen = true;

        level_21_Door_Script[0].CanOpen = false;
        level_21_Door_Script[1].CanOpen = false;
        level_21_Door_Script[2].CanOpen = false;
        level_21_Door_Script[3].CanOpen = false;
        level_21_Door_Script[5].CanOpen = false;
        level_21_Door_Script[6].CanOpen = false;
        level_21_Door_Script[8].CanOpen = false;
        level_21_Door_Script[9].CanOpen = false;
        level_21_Door_Script[13].CanOpen = false;
        level_21_Door_Script[14].CanOpen = false;
        level_21_Door_Script[15].CanOpen = false;



        //开
        level_21_Door_Script[4].CanOpen = true;
        level_21_Door_Script[7].CanOpen = true;
        level_21_Door_Script[10].CanOpen = true;
        level_21_Door_Script[11].CanOpen = true;
        level_21_Door_Script[12].CanOpen = true;
        level_21_Door_Script[16].CanOpen = true;
        level_21_Door_Script[17].CanOpen = true;
        level_21_Door_Script[18].CanOpen = true;
        level_21_Door_Script[19].CanOpen = true;
        level_21_Door_Script[20].CanOpen = true;
        level_21_Door_Script[20].CanOpen = true;
        level_21_Door_Script[22].CanOpen = true;
        level_21_Door_Script[23].CanOpen = true;
        level_21_Door_Script[24].CanOpen = true;

    }
    public void Level_21_Preprocessing_B()
    {
        level_20_Door_Script[0].CanOpen = false;
        level_20_Door_Script[4].CanOpen = false;
        level_20_Door_Script[5].CanOpen = false;
        level_20_Door_Script[6].CanOpen = false;
        level_20_Door_Script[7].CanOpen = false;
        level_20_Door_Script[8].CanOpen = false;
        level_20_Door_Script[18].CanOpen = false;
        level_20_Door_Script[11].CanOpen = false;

        level_20_Door_Script[1].CanOpen = true;
        level_20_Door_Script[2].CanOpen = true;
        level_20_Door_Script[3].CanOpen = true;
        level_20_Door_Script[9].CanOpen = true;
        level_20_Door_Script[10].CanOpen = true;
        level_20_Door_Script[12].CanOpen = true;
        level_20_Door_Script[13].CanOpen = true;
        level_20_Door_Script[14].CanOpen = true;


        level_21_Door_Script[0].CanOpen = false;
        level_21_Door_Script[7].CanOpen = false;
        level_21_Door_Script[14].CanOpen = false;
        level_21_Door_Script[24].CanOpen = false;

        level_21_Door_Script[2].CanOpen = false;
        level_21_Door_Script[3].CanOpen = false;
        level_21_Door_Script[4].CanOpen = false;
        level_21_Door_Script[13].CanOpen = false;
        level_21_Door_Script[15].CanOpen = false;
        level_21_Door_Script[16].CanOpen = false;

        level_21_Door_Script[1].CanOpen = true;
        level_21_Door_Script[5].CanOpen = true;
        level_21_Door_Script[6].CanOpen = true;
        level_21_Door_Script[8].CanOpen = true;
        level_21_Door_Script[9].CanOpen = true;
        level_21_Door_Script[10].CanOpen = true;
        level_21_Door_Script[11].CanOpen = true;
        level_21_Door_Script[12].CanOpen = true;
        level_21_Door_Script[17].CanOpen = true;
        level_21_Door_Script[18].CanOpen = true;
        level_21_Door_Script[19].CanOpen = true;
        level_21_Door_Script[20].CanOpen = true;
        level_21_Door_Script[21].CanOpen = true;
        level_21_Door_Script[22].CanOpen = true;
        level_21_Door_Script[23].CanOpen = true;
    }
    public void Level_21_Preprocessing_C()
    {
        level_20_Door_Script[0].CanOpen = false;
        level_20_Door_Script[1].CanOpen = false;
        level_20_Door_Script[3].CanOpen = false;
        level_20_Door_Script[4].CanOpen = false;
        level_20_Door_Script[5].CanOpen = false;
        level_20_Door_Script[6].CanOpen = false;
        level_20_Door_Script[9].CanOpen = false;
        level_20_Door_Script[15].CanOpen = false;
        level_20_Door_Script[16].CanOpen = false;
       

        level_20_Door_Script[2].CanOpen = true;
        level_20_Door_Script[7].CanOpen = true;
        level_20_Door_Script[8].CanOpen = true;
        level_20_Door_Script[10].CanOpen = true;
        level_20_Door_Script[11].CanOpen = true;
        level_20_Door_Script[12].CanOpen = true;
        level_20_Door_Script[13].CanOpen = true;
        level_20_Door_Script[14].CanOpen = true;

        level_21_Door_Script[0].CanOpen = false;
        level_21_Door_Script[1].CanOpen = false;
        level_21_Door_Script[4].CanOpen = false;
        level_21_Door_Script[5].CanOpen = false;
        level_21_Door_Script[7].CanOpen = false;
        level_21_Door_Script[8].CanOpen = false;
        level_21_Door_Script[9].CanOpen = false;
        level_21_Door_Script[14].CanOpen = false;

        level_21_Door_Script[1].CanOpen = false;
        level_21_Door_Script[2].CanOpen = false;
        level_21_Door_Script[13].CanOpen = false;
        level_21_Door_Script[15].CanOpen = false;
        level_21_Door_Script[16].CanOpen = false;

        level_21_Door_Script[6].CanOpen = true;
        level_21_Door_Script[10].CanOpen = true;
        level_21_Door_Script[11].CanOpen = true;
        level_21_Door_Script[12].CanOpen = true;
        level_21_Door_Script[17].CanOpen = true;
        level_21_Door_Script[18].CanOpen = true;
        level_21_Door_Script[19].CanOpen = true;
        level_21_Door_Script[20].CanOpen = true;
        level_21_Door_Script[21].CanOpen = true;
        level_21_Door_Script[22].CanOpen = true;
        level_21_Door_Script[23].CanOpen = true;
        level_21_Door_Script[24].CanOpen = true;
    }
    public void Level_28_Preprocessing()
    {
        level_28_Door_Script[0].CanOpen = false;
    }

    private static DoorManager _Instance;
    public static DoorManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject go = new GameObject("DoorManager");
                DontDestroyOnLoad(go);
                _Instance = go.AddComponent<DoorManager>();
            }
            return _Instance;
        }
    }
    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private void Start ()
	{

    }

	
	
	
	void Update () 
	{
		
		
	
	}



 #endregion

}
