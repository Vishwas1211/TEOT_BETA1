//
//  TaskProgressLevel1_8.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES_P2 on 8/2/2017 5:51 PM.
//  Description: 第一阶段
//

using UnityEngine;
using System.Collections;

public class TaskProgressLevel1_8 : TaskProgressBase
{
    public GameObject sod;
    GameObject map;
    GameObject npc;
    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 1001: //玩家在分离舱
                {
                    //TaskStepManagaer.Instance.FinishTaskTo(6003);
                    //PlayerManager.Instance.motionController.IsEnabled = false;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 1002: //玩家从分离舱出来，听絮叨
                {
                    npc = Instantiate((Resources.Load("Canvas") as GameObject));
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 1003://玩家向大楼走
                {
                    map = Instantiate((Resources.Load("Cube") as GameObject));
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 1004://玩家向大楼走
                {
                    ZMBManager.Instance.CreateZmbOnFloor(ZMBManager.LEVEL_FLOOR.FIRST_FLOOR);
                    GameObject.Destroy(map);
                    npc.SetActive(false);
                    //PlayerManager.Instance.motionController.IsEnabled = true;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
                }
                break;
            case 2001://玩家走到了门外，NPC提示，地上出现指示
                {
                    npc.SetActive(true);
                    //PlayerManager.Instance.motionController.IsEnabled = false;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;

                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 2002: // 门外
                {
                    //PlayerManager.Instance.motionController.IsEnabled = true;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 2003:// 门外
                {
                    npc.SetActive(false);

                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 2004://门外
                {
                    TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 2005://门外
                {
                    TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 2006://门外
                {
                    //触发进门事件
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 3001://进门了
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 3002://走到控制室前
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 3003://找到钥匙，用来开门  
                {
                    Debug.Log(3003);
                    PlaySoundController.Instance.PlaySoundEffect(PlayerManager.Instance.playerCollider, 10001);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 3004://找到钥匙，用来开门  
                {
                    Debug.Log(3004);
                    PlaySoundController.Instance.PlaySoundEffect(PlayerManager.Instance.playerCollider, 10001);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 3005://找到钥匙，用来开门  
                {
                    PlaySoundController.Instance.PlaySoundEffect(PlayerManager.Instance.playerCollider, 10001);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 3006://找到钥匙，用来开门  
                {
                    PlaySoundController.Instance.PlaySoundEffect(PlayerManager.Instance.playerCollider, 10001);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 4001: //门打开之后，玩家进入
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 4002: //警报响起，玩家需要打开监控器
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 4003://玩家打开了监控器，等待记录播放完毕
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 4004://记录视频播放完毕，NPC叨叨，玩家拿到钥匙，开门
                {
                    npc.SetActive(true);
                    //PlayerManager.Instance.motionController.IsEnabled = false;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 4005://记录视频播放完毕，NPC叨叨，玩家拿到钥匙，开门
                {
                    npc.SetActive(false);
                    //PlayerManager.Instance.motionController.IsEnabled = true;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 5001: // 玩家打开了112房间门，进去
                {
                    npc.SetActive(true);
                    //PlayerManager.Instance.motionController.IsEnabled = false;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;
                    Debug.Log(5001);
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 5002:// 玩家进入了房间，找到抽屉
                {
                    npc.SetActive(false);
                    //PlayerManager.Instance.motionController.IsEnabled = true;
                    GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
                    Debug.Log(5002);

                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 5003://玩家打开了抽屉，查找线索之后，要回到监控室
                {
                    test18.Fly();
                    SogBossManager.Instance.Init();
                }
                break;
            case 5004://玩家回到了监控室，破解，但是失败了
                {
                    //TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 5005://NPC提示
                {
                    Level_01_Manager.Instance.L1Broken();
                    TaskStepManagaer.Instance.FinishCurTask();
                    Level_01_Manager.Instance.ShowArrowUI();
                }
                break;
            case 5006://玩家走向电梯口
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6001://玩家到了电梯口，开始战斗
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6002://战斗结束，NPC叨叨
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6003://NPC叨叨结束，屏幕出现倒计时
                {
                    //ZMBManager.Instance.CreateZmbOnFloor(ZMBManager.LEVEL_FLOOR.FOURTH_FLOOR);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6004://入口处出现boss机器人，和大量僵尸，开始战斗
                {
                    //生成JAYCEE
                    JayceeManager.Instance.Init();
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6005://消灭了敌人之后，入口打开，开启30秒自毁，玩家逃跑
                {
                    //存纪录

                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6006://玩家逃了出去，整个街道的敌人都被吸引，进行下一步 
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 6007://判断有没有钥匙
                {
                    if (NoLockView_Camera.can_4_7 && NoLockView_Camera.can_4_8)
                    {
                        JayceeManager.Instance.isCanTalk = true;
                    }
                    else
                    {
                        PlaySoundController.Instance.PlaySoundEffect(PlayerManager.Instance.playerCollider, 10001);
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 6008://无钥匙
                {
                    //TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 6009://开门后
                {
                    GameObject go = UtilFunction.ResourceLoadOnPosition("Prefabs/Character/Enemy/ZmbBoss_4F/ZMB_1", new Vector3(-9.377f, 20.50231f, 36.269f), Quaternion.Euler(0, 90, 0));

                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 7001: //NPC提示，需要安装干扰器          //第五层  .梯子开始坍塌 1
                {
                    //ZMBManager.Instance.CreateZmbOnFloor(ZMBManager.LEVEL_FLOOR.FIFTH_FLOOR);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 7002: //安装好之后，开始计时，进行下一步 //第五层 爬到楼上
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 8001://NPC提示，需要拿出工具            //第五层  调回四楼
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 8002://拿出了抓钩工具之后，把他挂在三层   //第五层  又见到Jaycee
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
        }
    }

    public override void UpdateTask(int taskId)
    {
        base.UpdateTask(taskId);
        switch (taskId)
        {
            case 1004:
                {
                    UtilFunction.qwe(new Vector3(-9.006f, 0, 16.867f));
                }
                break;
            case 2001://玩家走到了门外，NPC提示，地上出现指示
                {
                }
                break;
            case 3002:
                {
                    UtilFunction.qwe(new Vector3(-26.125f, 0, 19.696f));
                }
                break;
            case 5002:
                {
                    UtilFunction.qwe(new Vector3(-8.147f, 0, 24.083f));
                }
                break;
            case 5004:
                {
                    UtilFunction.qwe(new Vector3(-2.96f, 0, 11f));
                }
                break;
            case 6003:
                {
                    UtilFunction.qwe(new Vector3(-17.22f, 0, 25.947f));
                }
                break;
            case 6006:
                {
                    UtilFunction.qwe(new Vector3(-23.508f, 0, 38.634f));
                }
                break;
            case 6008:
                {
                    if (NoLockView_Camera.can_4_7)
                    {
                        JayceeManager.Instance.isCanTalk = true;
                    }
                }
                break;
            case 6009:
                {
                    if (Vector3.Distance(PlayerManager.Instance.playerCollider.transform.position, Level_04_Manager.Instance.PlayerPositions[0].position) < 1f)
                    {
                        TaskStepManagaer.Instance.FinishTaskTo(11001);
                    }
                }
                break;


            case 7001: //第五层 到达楼梯口
                {
                    Level_05_Manager.Instance.UpdatePoSui();
                    if (UtilFunction.IsReachDistanceXYZ(Level_05_Manager.Instance.playerGO.transform.position, Level_05_Manager.Instance.TaskPosition[1].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;

            case 7002: //第五层 爬到楼上
                {
                    Level_05_Manager.Instance.UpdateRadio();
                    Level_05_Manager.Instance.UpdatePoSui();
                    if (UtilFunction.IsReachDistanceXYZ(Level_05_Manager.Instance.playerGO.transform.position, Level_05_Manager.Instance.TaskPosition[2].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;

            case 8001://第五层  调回四楼
                {
                    if (UtilFunction.IsReachDistanceXYZ(Level_05_Manager.Instance.playerGO.transform.position, Level_05_Manager.Instance.TaskPosition[3].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;

            case 8002://第五层  又见到Jaycee
                {
                    Level_05_Manager.Instance.UpdateJayceeOpenDoor();
                    Level_05_Manager.Instance.UpdateEnemy();

                    if (UtilFunction.IsReachDistanceXYZ(Level_05_Manager.Instance.playerGO.transform.position, Level_05_Manager.Instance.TaskPosition[3].position, 2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;

            default:
                break;
        }
    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);

        switch (taskId)
        {
            case 1001:
                break;
            case 1002:
                break;
            default:
                break;
        }
    }

}