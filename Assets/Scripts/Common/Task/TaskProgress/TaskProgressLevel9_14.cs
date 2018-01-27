//
//  TaskProgressLevel9_14.cs
//  TEOT_ONLINE
//
//  Created by 王颉 on 8/4/2017 11:32 AM.
// Description: 第二阶段
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskProgressLevel9_14 : TaskProgressBase
{

    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 9001: // 玩家到达三层，敌人回复正常，回到原来的位置     //第五层   Boss战之前
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 9002:// NPC提示，拿出解码器，解码                     //第五层   Boss战之后
                {

                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 9003://开始解码，等待解码完毕                        //第五层    到达楼梯口
                {
                    //TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 9004://解码完毕，准备进门                             //第十层   开始
                {
                    //TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 9005://进门之后，关门或者直接进行下一步                //第十层   触发NPC剧情后
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10001://时间控制步骤                                  //第十层   到达Rick房间
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10002://Jaycee出现，跑掉，进行下一步                 //第十层   制药完成
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    //JayceeManager.instance.Init();
                }
                break;
            case 10003://玩家四处寻找Jaycee，当玩家靠近一间房间时，进行下一步     //第十层   FS_1 完成
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10004://玩家靠近房间，听到一声尖叫，玩家走近门口时进行下一步     //第十层    谜题完成
                {
                    //JayceeManager.instance.FinishCurTaskImmediately();//jaycee的剧情进行下一步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10005://玩家到达房间门口，Jaycee尖叫，门上出现标识，玩家需要去管理室   //第十层   回到NPC门口
                {

                    //JayceeManager.instance.FinishCurTaskImmediately();//jaycee的剧情进行下一步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10006://玩家到达管理室，有很多僵尸潜伏                                    //第十层   打开门
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10007://杀光了僵尸之后，拿到钥匙，回到门口                                //第十层   离开第十层
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    TaskStepManagaer.Instance.FinishTaskTo(19001);
                }
                break;
            case 10008://到了门口，打开门
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10009://开门之后Jaycee拿起扫把攻击玩家，玩家去夺她的扫把
                {
                    //JayceeManager.instance.FinishCurTaskImmediately();//jaycee的剧情进行下一步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10010://被夺走扫把之后，Jaycee说话，然后带玩家去楼梯
                {
                    //JayceeManager.instance.FinishCurTaskImmediately();//jaycee的剧情进行下一步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 10011://到达楼梯之后，进行下一步
                {
                    //语音18
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 11001://楼梯突然坍塌，Jaycee说话，玩家需要上去
                {
                    //Level_05_Manager.Instance.Init();  //开启第五层

                    //语音19
                    //开启掉了


                    //TODO开启震动
                    //玩家快走过去时关闭碰撞
                    //开启延时 如过，玩家按某个按钮，就向上移动  //移动到一半的时候跳步
                    //TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 11002://玩家把自己拉上去之后，Jaycee跑进屋里，把门关上
                {
                    //一个大的震动


                    //Jaycee进屋后跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 11003://玩家进入屋子，听到了广播
                {
                    //位置跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 11004://地面持续裂，玩家需要进入其他房间
                {
                    //位置跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 11005://玩家掉掉了中间层，爬出来找斧子
                {
                    //地面塌陷
                    //Level_05_Manager.Instance.ShowDiBan_5F_4(false) ;


                    //拿到斧子
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 11006://玩家跳下，找斧子，触发僵尸
                {

                    //开启僵尸

                    //打死僵尸跳步（离开战斗也算）
                    //TODO
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 12001://玩家进入房间后，寻找Jaycee
                {

                    //位置跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 12002://Jaycee带领玩家向前走
                {

                    //到指定位置跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 12003://门里冲出僵尸
                {

                    // 僵尸死亡跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 12004://提醒玩家，柜子里有药
                {
                    //JayceeManager.instance.FinishCurTaskImmediately();

                    //玩家拿到药跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 12005://当玩家拿起注射器的时候，另一个房间尖叫，玩家去另一个房间
                {
                    //JayceeManager.instance.FinishCurTaskImmediately();

                    //玩家追上Jaycee 跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 12006://当玩家到达这个房间，Jaycee进行剧情    //提醒图纸
                {
                    //JayceeManager.instance.FinishCurTaskImmediately();

                    //玩家点击图纸跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            
            case 12007://Jaycee冲出屋子
                {
                    //玩家走出屋子跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

           
            case 12008://当玩家冲出屋子时出现僵尸,jaycee 关门
                {
                    //TODO 生成一只僵尸，抱住玩家
                    //Instantiate(Level5Manager.);
                    //打死僵尸跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

          
            case 12009:  //开门时，Jaycee被打在墙上
                {
                    //玩家靠近跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

           
            case 12010: //玩家靠近，和玩家说话
                {
                    //说完话跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

          
            case 12011://Jaycee变成怪物
                {
                    //播放完动画跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            //打死Jaycee 
            case 12012://变成怪物之后，玩家需要拿注射器注射到他身上，Jaycee倒地，进行下一步
                {
                    //Jaycee 死亡 跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            
            case 12013://到屋里拿到资料
                {
                    //拿到资料跳步

                    //TODO  如果已经有了就直接跳步
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            //得知X窗户
            case 13001://发现一张字条“找到X窗户”
                {
                    //拿到纸片跳步
                    //TODO  如果已经有了就直接跳步

                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            
            case 13002://搬开椅子，发现是锁着的
                {
                    Level_05_Manager.Instance.Show_F5_21_Hint(true);


                    //搬开椅子跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

           
            case 13003: //找保安拿钥匙
                {

                    //获取钥匙跳步，//如果已经有钥匙了就直接跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            
            case 13004://开窗户，出来
                {
                    //Level5Manager.Instance.Show_X_Window(false);

                    //到达一半跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

           
            case 13005: //走到一半，Jaycee出现
                {
                    Level_05_Manager.Instance.ShowJayceeMonster_TiZi();  //出现Jaycee

                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;

            case 13006: //Jaycee也向上爬
                {

                    //到达顶端跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;


            //爬到指定位置，楼梯掉了
            case 13007://玩家爬到顶端时，梯子坍塌，boss掉下去，进行下一步
                {

                    //Level_05_Manager.Instance.JayceeMonster_TiZi_Script.PlayerArrive();
                    //Jaycee死亡跳步
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;




        
           
          




            case 14001://玩家到了楼上，NPC一段对话
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 14002://对话完成后，玩家需要跟随小女孩去走廊
                {
                    MelissaManager.Instance.Init();
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 14003://玩家到走廊，玩家需要逐个房间找钥匙
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 14004://小女孩走到其中一个房间门口，跑进去，蹲在一张桌子下面,等待玩家进屋
                {
                    //TODO 玩家进门触发 
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 14005://当玩家靠近其中一个房间，小女孩说一句话
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
        }
    }
    
    public override void UpdateTask(int taskId)
    {
        base.UpdateTask(taskId);
        switch (taskId)
        {
            case 9001://第五层   Boss战之前
                {
                    Level_05_Manager.Instance.UpdateJayceeOpenDoor();
                    Level_05_Manager.Instance.UpdateRadio();
                    Level_05_Manager.Instance.UpdateEnemy();
                    Level_05_Manager.Instance.UpdatePoSui();
                    if (true)//TODO Jaycee死了
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 9002://第五层   Boss战之后
                {
                    Level_05_Manager.Instance.UpdateJayceeOpenDoor();
                    Level_05_Manager.Instance.UpdateRadio();
                    Level_05_Manager.Instance.UpdateEnemy();
                    Level_05_Manager.Instance.UpdatePoSui();
                    if (UtilFunction.IsReachDistanceXYZ(Level_05_Manager.Instance.playerGO.transform.position, Level_05_Manager.Instance.TaskPosition[5].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 9003: //第五层    到达楼梯口
                {
                    Level_05_Manager.Instance.UpdateJayceeOpenDoor();
                    Level_05_Manager.Instance.UpdateRadio();
                    Level_05_Manager.Instance.UpdateEnemy();
                    Level_05_Manager.Instance.UpdatePoSui();
                    if (UtilFunction.IsReachDistanceXYZ(Level_05_Manager.Instance.playerGO.transform.position, Level_05_Manager.Instance.TaskPosition[6].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }

                }
                break;
            case 9004://第十层   开始
                {
                    //TaskStepManagaer.Instance.FinishCurTask();

                    Level_10_Manager.Instance.NPC_Distance();

                    if (UtilFunction.IsReachDistanceXYZ(Level_10_Manager.Instance.playerGO.transform.position, Level_10_Manager.Instance.TaskPosition[1].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 9005://第十层   触发NPC剧情后
                {
                    Level_10_Manager.Instance.UpdateJiQiRen();
                    Level_10_Manager.Instance.NPC_Distance();
                    if (UtilFunction.IsReachDistanceXYZ(Level_10_Manager.Instance.playerGO.transform.position, Level_10_Manager.Instance.TaskPosition[3].position, 1))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10001://第十层   到达Rick房间
                {
                    Level_10_Manager.Instance.UpdateBeiZa();
                    Level_10_Manager.Instance.UpdateTiShiZhiYao();

                    if (Level_10_Manager.Instance.CompletePharmaceutical)
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10002: //第十层   制药完成
                {

                    Level_10_Manager.Instance.UpdatFS();
                    if (Level_10_Manager.Instance.firstKO_SF)
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10003://第十层   FS_1 完成
                {
                    if (Level_10_Manager.Instance.CompletePuzzle)
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10004://第十层    谜题完成
                {
                    if (UtilFunction.IsReachDistanceXYZ(Level_10_Manager.Instance.playerGO.transform.position, Level_10_Manager.Instance.TaskPosition[8].position, 2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10005:  //第十层   回到NPC门口
                {

                    if (UtilFunction.IsReachDistanceXYZ(Level_10_Manager.Instance.playerGO.transform.position, Level_10_Manager.Instance.TaskPosition[9].position, 2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10006://第十层   打开门
                {
                    Level_10_Manager.Instance.GenerateCRI();

                    if (UtilFunction.IsReachDistanceXYZ(Level_10_Manager.Instance.playerGO.transform.position, Level_10_Manager.Instance.TaskPosition[10].position, 2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 10007://第十层   离开第十层
                {
                  
                }
                break;
            default:
                break;
        }
    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);
    }
}