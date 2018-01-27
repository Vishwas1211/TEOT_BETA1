using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class PayerCameraRay : MonoBehaviour
{

    
    
    


    //Level_20
   

    void Update()
    {
        UpdateLevelFrontRay();
        UpdateLevel05FrontRay();
        UpdateLevel10FrontRay();
        UpdateLevel20FrontRay();
        UpdateLevel21FrontRay();
    }

    Ray ray;
    private bool can_1_6 = false;
    public LayerMask rayLayer = 0 << 8;

    public void UpdateLevelFrontRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {
                Debug.DrawRay(ray.origin, hitInfo.point, Color.yellow);

                if (hitInfo.transform.GetComponent<InteractiveGoods>())
                {
                    hitInfo.transform.GetComponent<InteractiveGoods>().Operation();
                }
                if ((hitInfo.transform.parent && hitInfo.transform.parent.GetComponent<InteractiveGoods>()))
                {
                    hitInfo.transform.parent.GetComponent<InteractiveGoods>().Operation();
                }

                //if (hitInfo.transform.GetComponent<OpenDoor_WSM>()) //开门
                //{
                //    hitInfo.transform.GetComponent<OpenDoor_WSM>().Operation();
                //}
                //if ((hitInfo.transform.parent && hitInfo.transform.parent.GetComponent<OpenDoor_WSM>()))
                //{
                //    hitInfo.transform.parent.GetComponent<OpenDoor_WSM>().Operation();
                //}

                if (hitInfo.transform.tag == "CanEliminated")
                {
                    Destroy(hitInfo.transform.gameObject);
                }

            }

            if (Physics.Raycast(ray, out hitInfo, 1000f, rayLayer))
            {
                Debug.DrawRay(ray.origin, hitInfo.point, Color.yellow);

                if (hitInfo.transform.name == "Boss")
                {
                    hitInfo.transform.GetComponent<Zomber_F5_18>().ShoShang(1);
                }

            }
        

        }
    }

    public void UpdateLevel05FrontRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {
         
                if (hitInfo.transform.name == "Radio")
                {
                    hitInfo.transform.GetComponent<Level5_Radio>().DianKa(true) ;
                }


                if (hitInfo.transform.name == "bijiben2") //打开监视器，看到X
                {
                    Level_05_Manager.Instance. Show_F5_21_Hint(true);
                }

                if (hitInfo.transform.name == "F5_23")                 //新链条
                {
                    Level_05_Manager.Instance.haveLianTiao = true;
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.name == "EX_5B_5F_Curtain_3_2")  //损坏的链条
                {
                    if (Level_05_Manager.Instance.haveLianTiao)
                    {
                        Level_05_Manager.Instance.ShowSuoLian(true);
                        Destroy(hitInfo.transform.gameObject);
                    }
                }

                if (hitInfo.transform.name == "WanZhengSuoLian")     //完整的链条
                {
                    Level_05_Manager.Instance.canUp = true;
                }

                if (hitInfo.transform.name == "Airduct")             //可以点开的通风口
                {
                    if (hitInfo.transform.GetComponent<Climb_WSM>())
                    hitInfo.transform.GetComponent<Climb_WSM>().CanGo();

                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.name == "EX_5A_4F_door_15_KEY")
                {
                    DoorManager.Instance.level_04_Door_Script[13].CanOpen = true;
                    DoorManager.Instance.level_04_Door_Script[13].Operation();
                }
                if (hitInfo.transform.name == "EX_5A_4F_door_16_KEY")
                {
                    DoorManager.Instance.level_04_Door_Script[14].CanOpen = true;
                    DoorManager.Instance.level_04_Door_Script[14].Operation();
                }

                //if (JayceeManager.instance.jayceeStoryProcess.curTaskID == 1030 && hitInfo.transform.name == "F5_15")
                //{
                //    JayceeManager.instance.jay
                //}


                //=============   门   ============

                if (hitInfo.transform.name == "F5_3")
                {
                    DoorManager.Instance.level_05_Door_Script[9].CanOpen = true;
                    Destroy(hitInfo.transform.gameObject);
                }
                if (hitInfo.transform.name == "FuZi2")
                {
                    DoorManager.Instance.level_05_Door_Script[7].CanOpen = true;
                    Destroy(hitInfo.transform.gameObject);
                }
                if (hitInfo.transform.name == "F5_19")
                {
                    DoorManager.Instance.level_05_Door_Script[2].CanOpen = true;
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.GetComponent<MelissaController>())
                {
                    hitInfo.transform.GetComponent<MelissaController>().SetToCreep();
                }

            }

            if (Physics.Raycast(ray, out hitInfo, 1000f, rayLayer))
            {

                if (hitInfo.transform.name == "F5_06_Zomber" || hitInfo.transform.name == "F5_13_Zomber")
                {
                    hitInfo.transform.GetComponent<Zomber_5F_6>().ShoShang(1);
                }

                if (hitInfo.transform.name == "F5_16_Zomber")
                {
                    hitInfo.transform.GetComponent<Zomber_F5_16>().ShoShang(1);
                }

                if (hitInfo.transform.name == "JayceeMonster")
                {
                    hitInfo.transform.parent.GetComponent<JayceeMonsterController>().OnHurt(1);
                }

            }

        }
    }

    public void UpdateLevel10FrontRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {
                if (hitInfo.transform.name == "Computer_B_Keyboard") //谜题开关
                {
                    Level_10_Manager.Instance.ShowTV();
                }
               
                if (hitInfo.transform.name == "Antidote_B_Zhen")//针
                {
                  Level_10_Manager.Instance.haveZhen = true;
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.name == "Antidote_A_DaYao")//大药瓶
                {
                    if (Level_10_Manager.Instance.haveZhen)
                    {
                        Level_10_Manager.Instance.haveDaYao = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                }
                if (hitInfo.transform.name == "injection")//小药瓶
                {
                    if (!Level_10_Manager.Instance.haveXiaoYao)
                    {
                        Level_10_Manager.Instance.haveXiaoYao = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                }

                if (hitInfo.transform.name == "L5_Intrument_F_Boy")
                {
                    Level_10_Manager.Instance.ShowZhiYaoTai();
                    hitInfo.transform.gameObject.SetActive(false);
                }

             

                if (hitInfo.transform.name == "DanJu")
                {
                    Level_10_Manager.Instance.HaveChainsaw = true;
                    hitInfo.transform.gameObject.SetActive(false);
                }


                if (hitInfo.transform.parent != null && hitInfo.transform.parent.GetComponent<Gate_WSM>())
                {
                    if (Level_10_Manager.Instance.CompletePuzzle)
                    {
                        hitInfo.transform.parent.GetComponent<Gate_WSM>().CaoZui();
                        FSManager.Instance.fsController.SetState(FSController.FS_STATE.IDLE);  //这里设置的FS
                    }
                }

                if (hitInfo.transform.name == "EX_5B_10F_DoorL" || hitInfo.transform.name == "EX_5B_10F_DoorR")
                {
                    if (Level_10_Manager.Instance.CanChainsaw && Level_10_Manager.Instance.HaveChainsaw)
                    {
                        hitInfo.transform.gameObject.SetActive(false);

                        Level_10_Manager.Instance.Open_NPC_Door();

                    }
                }

                if (hitInfo.transform.name == "F10_Door_2")
                {
                    DoorManager.Instance.level_10_Door_Script[3].CanOpen = true;
                    hitInfo.transform.gameObject.SetActive(false);
                }

                if (hitInfo.transform.name == "F10_1")
                {
                    Level_10_Manager.Instance.m_GanCha.KanShiGuanCha(0);
                    //Destroy(hitInfo.transform.gameObject);
                }
                if (hitInfo.transform.name == "F10_2")
                {
                    Level_10_Manager.Instance.m_GanCha.KanShiGuanCha(1);
                }

            }

            if (Physics.Raycast(ray, out hitInfo, 1000f, rayLayer))
            {

                if (hitInfo.transform.GetComponent<FSController>())
                {
                    hitInfo.transform.GetComponent<FSController>().OnHurt(10);
                }

                if (hitInfo.transform.name == "F5_16_Zomber")
                {
                    hitInfo.transform.GetComponent<Zomber_F5_16>().ShoShang(1);
                }

                if (hitInfo.transform.name == "JayceeMonster")
                {
                    hitInfo.transform.parent.GetComponent<JayceeMonsterController>().OnHurt(1);
                }

                if (hitInfo.transform.GetComponent<AmyController>())
                {
                    hitInfo.transform.GetComponent<AmyController>().OnHurt(10);
                }
                if (hitInfo.transform.GetComponent<GeorgeController>())
                {
                    hitInfo.transform.GetComponent<GeorgeController>().OnHurt(10);
                }
            }

        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
        //    {
        //        if (hitInfo.transform.name == "Door") //制药台的门
        //        {
        //            Level_10_Manager.Instance.ZhiYaoTaiScript.CaoZuo();
        //        }
        //        if (hitInfo.transform.name == "XiaoYaoPing")//放小药
        //        {
        //            if (haveXiaoYao)
        //            {
        //                Level_10_Manager.Instance.ZhiYaoTaiScript.FangYao();
        //                haveXiaoYao = false;
        //            }
        //        }
        //        if (hitInfo.transform.name == "DaYaoPing") //放大药
        //        {
        //            if (haveDaYao)
        //            {
        //                Level_10_Manager.Instance.ZhiYaoTaiScript.FangYao_2();
        //                haveDaYao = false;
        //            }
        //        }
        //        if (hitInfo.transform.name == "Start") //开始
        //        {
        //            Level_10_Manager.Instance.ZhiYaoTaiScript.KaiShi();
        //        }
        //        if (hitInfo.transform.name == "Stop") //结束
        //        {
        //            Level_10_Manager.Instance.ZhiYaoTaiScript.JieShu();
        //        }
        //        if (hitInfo.transform.name == "Antidote_A_WanCheng") //拿药
        //        {
        //            if (Level_10_Manager.Instance.ZhiYaoTaiScript.isOK)
        //            {
        //                if (Level_10_Manager.Instance.ZhiYaoTaiScript.WanChengPing)
        //                {
        //                    Level_10_Manager.Instance.isHaveYao = true;
        //                    hitInfo.transform.gameObject.SetActive(false);
        //                    haveDaYao = true;
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public void UpdateLevel20FrontRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {

                if (hitInfo.transform.GetComponent<RickController>())
                {
                    if (RickManager.Instance.rickStoryProcess.curTaskID == 1006)
                        RickManager.Instance.FinishCurTask();
                }

                if (hitInfo.transform == Level_20_Manager.Instance.Rope_1.transform)
                {
                    Level_20_Manager.Instance.PlayerGO.transform.position = new Vector3(
                        hitInfo.transform.position.x,
                        Level_20_Manager.Instance.PlayerGO.transform.position.y,
                        hitInfo.transform.position.z) + Level_20_Manager.Instance.Rope_deviation;

                    Level_20_Manager.Instance.PlayerScript.GoUpRope();
                }

                if (hitInfo.transform.name == "Key_A1")  //WuQi
                {
                    RickManager.Instance.FinishCurTask();
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.name == "Key_B1")      //获得梯子
                {
                    Level_20_Manager.Instance.hoaveKey_B1 = true;
                    hitInfo.transform.gameObject.SetActive(false);
                }

                if ((hitInfo.transform.name == "KongZhiBianBan")) //电梯开关
                {
                    if (Level_20_Manager.Instance.HaveQianZi)
                    {
                        hitInfo.transform.gameObject.SetActive(false);

                        Level_20_Manager.Instance.Rope_1.transform.position += Vector3.up * 10f;
                        Level_20_Manager.Instance.DianTi_B.SetActive(false);
                        Level_20_Manager.Instance.DianTiXiouHaoLe = true;
                    }
                }

                if (hitInfo.transform.name == "Dianju")  //电锯
                {
                    hitInfo.transform.gameObject.SetActive(false);
                    Level_20_Manager.Instance.HaveDianJu = true;
                }

                if (hitInfo.transform.name == "EX_5C_20F_Door_0")  //电锯破门
                {
                    if (Level_20_Manager.Instance.HaveDianJu)
                    {
                        hitInfo.transform.gameObject.SetActive(false);
                    }
                }


                if ((hitInfo.transform.name == "QianZi"))  //钳子
                {
                    hitInfo.transform.gameObject.SetActive(false);
                   Level_20_Manager.Instance. HaveQianZi = true;
                }

              


                if ((hitInfo.transform.name == "EX_5C_20F_Door_7"))  //破门
                {
                    hitInfo.transform.GetComponent<Level_20_Door_PuDong>().CaoZuo();
                }

            }

            if (Physics.Raycast(ray, out hitInfo, 1000f, rayLayer))
            {

                if (hitInfo.transform.GetComponent<FSController>())
                {
                    if (Level_20_Manager.Instance.Have_ShouLie)
                    {
                        hitInfo.transform.GetComponent<FSController>().FirstAppear();
                    }
                    else
                    {
                        hitInfo.transform.GetComponent<FSController>().OnHurt(10);
                    }
                }


                if (hitInfo.transform.name == "Zomber_0")
                {
                    hitInfo.transform.GetComponent<Zomber_0_WSM>().ShoShang(1);
                }

            }

        }
    }

    public void UpdateLevel21FrontRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {

                if (hitInfo.transform.GetComponent<RickController>())
                {
                    if (RickManager.Instance.rickStoryProcess.curTaskID == 1006)
                        RickManager.Instance.FinishCurTask();
                }

                if ((hitInfo.transform.name == "B2_ShouLie"))  //手雷
                {
                    hitInfo.transform.gameObject.SetActive(false);
                    Level_21_Manager.Instance.HaveShouLie = true;
                }


                if (hitInfo.transform.name == "level_21_key")     //
                {
                    Level_21_Manager.Instance.HaveKey = true;
                    DoorManager.Instance.level_21_Door_Script[2].CanOpen = true;

                    hitInfo.transform.gameObject.SetActive(false);
                }

                if (hitInfo.transform.name == "RABXinXiAnNiou_Level21")
                {
                    Level_21_Manager.Instance. XianShiRab();
                }

                if (hitInfo.transform.name == "AnNiou_Level21_Shan")
                {
                    Level_21_Manager.Instance.Shan();
                    RabManager.Instance.rabController.SetCanAttract();
                }
                if (hitInfo.transform.name == "AnNiou_Level21_Liang")
                {
                    Level_21_Manager.Instance.Liang();
                }

                if (hitInfo.transform.name == "level_21_key")   //资料室的钥匙
                {
                    Level_21_Manager.Instance.HaveKey = true;

                    DoorManager.Instance.level_21_Door_Script[2].CanOpen = true;

                }

                if (hitInfo.transform.name == "RABXinXiAnNiou_Level21")
                {
                    //Level_21_Manager.Instance.
                }


            }

            if (Physics.Raycast(ray, out hitInfo, 1000f, rayLayer))
            {

                if (hitInfo.transform.name == "Rab(Clone)")
                {
                    if (!Level_21_Manager.Instance.RabYunLe)
                    {

                        if (Level_21_Manager.Instance.HaveShouLie)
                        {
                            hitInfo.transform.GetComponent<RabController>().OnAblepsia();
                            Level_21_Manager.Instance.RabYunLe = true;
                        }
                        else
                        {
                            hitInfo.transform.GetComponent<RabController>().OnHurt(10);
                        }
                    }
                    else
                    {
                        Level_21_Manager.Instance.DianTi_Script.OpenDoor();
                    }
                }


            }

        }
    }
}
