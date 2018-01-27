//TaskProgressLevel22_26.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 3:08 PM
//Description: µÚÎå½×¶Î
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressLevel22_26 : TaskProgressBase
{
    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 22001: // Èç¹ûÍæ¼Ò¾ö¶¨²»È¥¾ÈÈË
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 22002:// Íæ¼Ò×ßµ½×ßÀÈÊ±£¬ÓÐ³å×²Ç½±ÚµÄÉùÒô
                {

                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 22003://Íæ¼ÒÕÒµ½Ò»²¿±»ÒÅÆúµÄµçÌÝ£¬ÌáÊ¾ÓÃÇ¯×Ó
                {
                    TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 22004://Íæ¼Òµ½ÁË¹¤¾ßÊÒ
                {
                    TaskStepManagaer.Instance.FinishCurTask();

                }
                break;
            case 22005://´òËÀ¹ÖÎï£¬Íæ¼ÒÄÃ×ßÇ¯×Ó
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 23001://Íæ¼ÒÀ´µ½µçÌÝÇ°
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 23002://Íæ¼ÒÅÀµÄ¹ý³ÌÖÐ£¬ÓÐ¸öÊ¬Ìå´ÓÉÏÃæµôÏÂÀ´
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 23003://µ±Íæ¼ÒÅÀµ½´óÔ¼2²ãÂ¥¸ßÊ±£¬µÀÂ·±»µçÌÝµ²×¡  
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 23004://Íæ¼Ò´ò¿ªµçÌÝÃÅ×ß³öÈ¥
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 23005://Íæ¼Ò´òËÀÁË½©Ê¬
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 24001://Emily³öÀ´¸ÐÐ»Íæ¼Ò
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 24002://EmilyÈÓÖØÎï 
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 24003://Õû¸öÂ¥²ãÆÆÁÑ
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 24004://Íæ¼ÒÕÒµ½³¤ÐÎµÄÃÅ·Åµ½¶ÏÁÑ´¦
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 24005://Íæ¼ÒÍ¨¹ý¶ÏÁÑ£¬´ò¿ªÁËÍ¨ÍùµçÌÝ¼äµÄÃÅ£¬³öÏÖ»úÆ÷ÈË
                {
                    TaskStepManagaer.Instance.FinishTaskTo(25001);
                }
                break;




            case 25001://  20层  分支一   到达20层
                {

                    Level_20_Manager.Instance.SetFenZhi (Level_20_Manager.FenZhi.A );
                }
                break;
            case 25002://  20层  分支一   到达21层
                {
                }
                break;
            case 25003://  20层  分支二   到达20层
                {
                    Level_20_Manager.Instance.SetFenZhi(Level_20_Manager.FenZhi.B);
                }
                break;
            case 26001://  20层  分支二   救出所有人
                {
                   
                }
                break;
            case 26002://  20层  分支二   过了坑
                {
                  
                }
                break;
            case 26003://  20层  分支二   21层
                {

                }
                break;
            case 26004://  20层  分支三    有人死了
                {
                    Level_20_Manager.Instance.SetFenZhi(Level_20_Manager.FenZhi.C);
                }
                break;
            case 26005://  20层  分支三    boss战前
                {
                   
                }
                break;



            case 26006://  20层  分支三    21层前
                {
               
                }
                break;
            case 26007://  21层  开始
                {
                 
                }
                break;
            case 26008://  21层  结束
                {
                    TaskStepManagaer.Instance.FinishTaskTo(28001);
                }
                break;
            case 26009://·ÖÖ§¶þ¾çÇé Íæ¼Ò¸ÐÐ»²¢Àë¿ª
                {
                    //²¥·ÅÓïÒô£¬¸æËßRµÄÎ»ÖÃºÍÉÏÈ¥µÄ·½·¨
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 26010://Ã»ÓÐÕÒµ½R 
                {

                  //µ½´ïÎ»ÖÃÌø²½
                }
                break;
            case 26011://µ½´ïÃÅ¿Ú
                {
                   //Î»ÖÃÌø²½
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 26012://µ±Íæ¼Ò¿¿½üÁËµÐÈË£¬ÐðÊöÒ»·¬
                {
                    //ËµÍêÌø²½
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 26013://µ½Ö¸¶¨Î»ÖÃÏëµ½¼Æ»®
                {
                    //Ã»ÈË¹ÜÍæ¼Ò
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

            case 25001://  20层  分支一   到达20层
                {

                    if (UtilFunction.IsReachDistanceXYZ(Level_20_Manager.Instance.PlayerGO.transform.position, Level_20_Manager.Instance.TaskPosition[1].position, 1,2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 25002://  20层  分支一   到达21层
                {
                    if (DoorManager.Instance.level_21_Door_Script[0].CanOpen)
                    {
                        TaskStepManagaer.Instance.FinishTaskTo(26007);
                    }
                }
                break;
            case 25003://  20层  分支二   到达20层
                {
                    Level_20_Manager.Instance.UpdateStartLevel20();


                    if (Level_20_Manager.Instance.m_NPC_State==Level_20_Manager.NPC_State.survive)
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                    if (Level_20_Manager.Instance.m_NPC_State == Level_20_Manager.NPC_State.death)
                    {
                        TaskStepManagaer.Instance.FinishTaskTo(26004);
                    }
                }
                break;
            case 26001://  20层  分支二   救出所有人
                {
                    if (Level_20_Manager.Instance.hoaveKey_B1)
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 26002://  20层  分支二   过了坑
                {
                    if (UtilFunction.IsReachDistanceXYZ(Level_20_Manager.Instance.PlayerGO.transform.position, Level_20_Manager.Instance.TaskPosition[5].position, 1, 2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 26003://  20层  分支二   21层
                {
                    if (UtilFunction.IsReachDistanceXYZ(Level_20_Manager.Instance.PlayerGO.transform.position, Level_20_Manager.Instance.TaskPosition[5].position, 1, 2))
                    {
                        TaskStepManagaer.Instance.FinishTaskTo(26007);
                    }
                }
                break;
            case 26004://  20层  分支三    有人死了
                {
                    if (UtilFunction.IsReachDistanceXYZ(Level_20_Manager.Instance.PlayerGO.transform.position, Level_20_Manager.Instance.TaskPosition[7].position, 1, 2))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 26005://  20层  分支三    boss战前
                {
                    if (true) //boss死亡
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 26006://  20层  分支三    21层前
                {
                    if (Level_20_Manager.Instance.DianTiXiouHaoLe)
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 26007://  21层  开始
                {

                    Level_21_Manager.Instance.Update_level_21();

                    if (UtilFunction.IsReachDistanceXYZ(Level_21_Manager.Instance. playerGO.transform.position, Level_21_Manager.Instance.PlayerPositions[2].transform.position, 2f))
                    {
                        TaskStepManagaer.Instance.FinishCurTask();
                    }
                }
                break;
            case 26008://  21层  结束
                {
                   
                }
                break;




        }



    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);
    }
}
