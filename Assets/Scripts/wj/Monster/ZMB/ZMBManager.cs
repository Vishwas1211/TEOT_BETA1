//ZMBManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 1/10/2018 10:38 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMBManager : SingletonMono<ZMBManager> 
{
    private const string ZMB_PREFAB_PATH = "Prefabs/Character/Enemy/Zombies/ZmbAIRoot";
    private const string ZMB_HAMMER_PATH = "Prefabs/Character/Enemy/Zombies/ZmbAI_HammerRoot";
    private const string ZMB_KNIFE_PATH = "ZmbAI_KnifeRoot";

    private GameObject _zombie;
    public GameObject zombie
    {
        get { return _zombie; }
    }

    private List<GameObject> _zmbList = new List<GameObject>();
    public List<GameObject> zmbList
    {
        get { return _zmbList; }
    }

    private Vector3[] _firstFloorPosAry = {
        new Vector3(-11.716f, 0.687f, 19.136f),
        new Vector3(-22.406f, 0.687f, 21.716f),
        new Vector3(-29.396f, 0.687f, 31.786f),
        new Vector3(-36.286f, 0.687f, 33.266f),
        new Vector3(15.454f, 0.687f, 39.596f),
        //new Vector3(16.924f, 0.687f, 23.156f),
        //new Vector3(4.434f, 0.687f, 19.626f),
        //new Vector3(4.434f, 0.687f, 29.966f),
        //new Vector3(-0.075f, 0.687f, 32.866f),
        //new Vector3(-12.976f, 0.687f, 33.146f),
        //new Vector3(-20.696f, 0.687f, 31.516f),
        //new Vector3(-26.796f, 0.687f, 34.386f),
        //new Vector3(-26.796f, 0.687f, 28.536f),
        //new Vector3(-35.376f, 0.687f, 26.136f),
        //new Vector3(-9.075999f, 0.687f, 40.126f),
        //new Vector3(5.924f, 0.687f, 44.246f),
        //new Vector3(8.954f, 0.687f, 15.396f),
        //new Vector3(-1.995f, 0.687f, 37.336f)
    };
    private Vector3[] _FourthFloorPosAry = {
        new Vector3(-14.86f, 20.5f, 26.27f),
        new Vector3(-18.82f, 20.5f, 31.07f),
        new Vector3(-16.991f, 20.5f, 35.82f),
        new Vector3(-27.01f, 20.5f, 24.8f),
        new Vector3(-25.223f, 20.5f, 41.778f),
        new Vector3(-26.423f, 20.5f, 41.778f),
        new Vector3(-19.571f, 20.5f, 42.174f),
        new Vector3(-17.33f, 20.5f, 40.43f),
        new Vector3(-4.351f, 20.5f, 40.43f),
        new Vector3(-1.83f, 20.5f, 38.32f),
        new Vector3(-2.08f, 20.5f, 26.46f),
        new Vector3(-0.75f, 20.5f, 32.3f),
        new Vector3(1.134f, 20.5f, 23.23f),
        new Vector3(-9.301f, 20.5f, 21.75f),
        new Vector3(4.296f, 20.5f, 19.339f)
    };
    private Vector3[] _FifthFloorPosAry = {
        new Vector3(-8.17f, 26.42f, 27.04f),
        new Vector3(-5.31f, 26.42f, 34.53f),
        new Vector3(-15.4f, 26.42f, 40.65f),
        new Vector3(-25.36f, 26.42f, 40.04f),
        new Vector3(-23.15f, 26.42f, 33.89f),
        new Vector3(-18.98f, 26.42f, 33.89f),
        new Vector3(-12.45f, 26.42f, 32.57f),
        new Vector3(-2.48f, 26.42f, 24.47f),
        new Vector3(-25.08f, 26.42f, 29.48f)
    };
    private Vector3[] _tenthFloorPosAry = {
        new Vector3(-11.17f, 46.25f, 24.47f),
        new Vector3(-8.31f, 46.25f, 31.96f),
        new Vector3(-23.26f, 46.25f, 39.65f),
        new Vector3(-26.51f, 46.25f, 33.77f),
        new Vector3(-25.09f, 46.25f, 33.1f),
        new Vector3(-24.27f, 46.25f, 25.82f),
        new Vector3(-15.8f, 46.25f, 25.65f),
        new Vector3(-1.11f, 46.25f, 31.17f),
        new Vector3(-12.61f, 46.25f, 39.65f),
        new Vector3(5.829f, 46.25f, 27.528f),
        new Vector3(-25.09f, 46.25f, 35.26f),
        new Vector3(-23.3f, 46.25f, 35.26f),
        new Vector3(-23.3f, 46.25f, 33.66f),
        new Vector3(-20.57f, 46.25f, 22.14f)
    };
    private Vector3[] _thirteenthFloorPosAry = {
        new Vector3(1.054f, 60.175f, 32.366f),
        new Vector3(-9.806f, 60.175f, 38.826f),
        new Vector3(0.354f, 60.175f, 38.826f),
        new Vector3(-24.696f, 60.175f, 31.056f)
    };
    private Vector3[] _eighteenthFloorPosAry = {
        new Vector3(-11.716f, 80.56f, 36.71f),
        new Vector3(-15.186f, 80.56f, 36.71f),
        new Vector3(-1.356f, 80.56f, 36.67f),
        new Vector3(0.904f, 80.56f, 31.28f),
        new Vector3(-5.016f, 80.56f, 31.28f),
        new Vector3(-18.896f, 80.56f, 39.19f)
    };
    private Vector3[] _twentiethFloorPosAry = {
        new Vector3(-1.165f, 88.483f, 36.71f),
        new Vector3(-1.806f, 88.483f, 36.71f),
        new Vector3(-0.4459991f, 88.483f, 36.71f),
        new Vector3(-0.4459991f, 88.483f, 35.9f),
        new Vector3(-1.075999f, 88.483f, 35.9f),
        new Vector3(-1.816f, 88.483f, 35.9f),
        new Vector3(-1.816f, 88.483f, 34.74f),
        new Vector3(-1.075999f, 88.483f, 34.74f),
        new Vector3(-0.4459991f, 88.483f, 34.74f),
        new Vector3(-15.746f, 88.483f, 35.9f),
        new Vector3(-20.366f, 88.483f, 32.28f),
        new Vector3(-26.436f, 88.483f, 39.46f),
        new Vector3(-7.066f, 88.483f, 28.94f),
        new Vector3(-7.066f, 88.483f, 31.95f),
        new Vector3(4.814001f, 88.483f, 37.71f)
    };
    private Vector3[] _twentyFirstFloorPosAry = {
        new Vector3(4.834f, 92.61f, 36.71f),
        new Vector3(4.224f, 92.61f, 29.14f),
        new Vector3(2.474f, 92.61f, 44.68f),
        new Vector3(2.474f, 92.61f, 53.84f),
        new Vector3(-7.316f, 92.61f, 53.84f),
        new Vector3(-18.696f, 92.61f, 52.89f),
        new Vector3(-24.036f, 92.61f, 46.92f),
        new Vector3(-24.036f, 92.61f, 42.62f),
        new Vector3(-12.936f, 92.61f, 32.42f),
        new Vector3(-9.436f, 92.61f, 32.42f),
        new Vector3(-7.406f, 92.61f, 38.28f),
        new Vector3(-1.585999f, 92.61f, 25.12f),
        new Vector3(-13.516f, 92.61f, 44.04f),
        new Vector3(-7.556f, 92.61f, 44.82f),
        new Vector3(-12.596f, 92.61f, 54.646f),
    };

    public enum LEVEL_FLOOR
    {
        FIRST_FLOOR,
        FOURTH_FLOOR,
        FIFTH_FLOOR,
        TENTH_FLOOR,
        THIRTEENTH_FLOOR,
        EIGHTEENTH_FLOOR,
        TWENTIETH_FLOOR,
        TWENTY_FIRST_FLOOR,
    }

    public void Init()
    {

    }

    public void CreateZmbOnFloor(Vector3[] createPos)
    {
        if (createPos.Length <= 0)
        {
            Debug.Log("创建僵尸的位置数组为空");
            return;
        }

        for (int i = 0; i < createPos.Length; i++)
        {
            GameObject go = UtilFunction.ResourceLoadOnPosition(ZMB_PREFAB_PATH, createPos[i], Quaternion.identity);
            _zmbList.Add(go);
        }
    }

    public void CreateZmbOnFloor(LEVEL_FLOOR floor)
    {
        switch (floor)
        {
            case LEVEL_FLOOR.FIRST_FLOOR:
                CreateZmbOnFloor(_firstFloorPosAry);
                break;
            case LEVEL_FLOOR.FOURTH_FLOOR:
                CreateZmbOnFloor(_FourthFloorPosAry);
                break;
            case LEVEL_FLOOR.FIFTH_FLOOR:
                CreateZmbOnFloor(_FifthFloorPosAry);
                break;
            case LEVEL_FLOOR.TENTH_FLOOR:
                CreateZmbOnFloor(_tenthFloorPosAry);
                break;
            case LEVEL_FLOOR.THIRTEENTH_FLOOR:
                CreateZmbOnFloor(_thirteenthFloorPosAry);
                break;
            case LEVEL_FLOOR.EIGHTEENTH_FLOOR:
                CreateZmbOnFloor(_eighteenthFloorPosAry);
                break;
            case LEVEL_FLOOR.TWENTIETH_FLOOR:
                CreateZmbOnFloor(_twentiethFloorPosAry);
                break;
            case LEVEL_FLOOR.TWENTY_FIRST_FLOOR:
                CreateZmbOnFloor(_twentyFirstFloorPosAry);
                break;
        }
    }

    private void Start()
    {
        //CreateZmbOnFloor(LEVEL_FLOOR.FIFTH_FLOOR);
    }
}
