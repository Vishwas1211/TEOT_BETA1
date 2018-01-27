using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_01_Manager : SingletonMono<Level_01_Manager>
{
    private GameObject _l1Broken;
    private GameObject _l1Perfect;
    //private GameObject _stonePillars;
    //public GameObject stonePillars
    //{
    //    get { return _stonePillars; }
    //}

    //private GameObject _wood;
    //public GameObject wood
    //{
    //    get { return _wood; }
    //}

    private GameObject _model;
    public GameObject model
    {
        get { return _model; }
    }

    private GameObject _f1_collider;
    public GameObject f1_collider
    {
        get { return _f1_collider; }
    }

    private GameObject _arrowUI;
    public GameObject arrowUI
    {
        get { return _arrowUI; }
    }

    private GameObject _door;
    public GameObject door
    {
        get { return _door; }
    }

    private GameObject _redPlane;
    public GameObject redPlane
    {
        get { return _redPlane; }
    }

    private GameObject _greenPlane;
    public GameObject greenPlane
    {
        get { return _greenPlane; }
    }

    public static bool key;

    public void Init()
    {
        GameObject mag = UtilFunction.ResourceLoad("Prefabs/Common/Managers/Level01GameObjectManager");
        GameObject Sa_Int = GameObject.Find("SA_INT");
        _model = Sa_Int.transform.Find("EX_5A_F1").gameObject;
        _f1_collider = Sa_Int.transform.Find("WALL_Collider/1F").gameObject;
        _arrowUI = Sa_Int.transform.Find("ArrowUI").gameObject;
        _door = Sa_Int.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5A_Indoor_1F/EX_5A_1F_Back/EX_5A_1F_Door/EX_5A_1F_Door_20").gameObject;
        _redPlane = mag.transform.Find("RedPlane").gameObject;
        _greenPlane = mag.transform.Find("GreenPlane").gameObject;
        _l1Broken = mag.transform.Find("Collision").gameObject;
        _l1Perfect = Sa_Int.transform.Find("SA_Exterior_5/EX_5A_Indoor/EX_5A_Indoor_1F/EX_5A_1F_Front").gameObject;
        _arrowUI.SetActive(false);
        //_model.SetActive(true);
    }

    public void ShowArrowUI()
    {
        _arrowUI.SetActive(true);
    }

    public void L1Broken()
    {
        _l1Broken.SetActive(true);
        _l1Perfect.SetActive(false);
    }
}
