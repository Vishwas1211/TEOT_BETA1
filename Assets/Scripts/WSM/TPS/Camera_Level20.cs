using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Camera_Level20 : MonoBehaviour 
{

    public bool KeyA1 = false;
    public bool HaveLuoSiDao = false;  //有螺丝刀

    public bool Have_ShouLie = false;  //有手雷
    public bool Have_ShouLie_b = false;

    public bool HaveQianZi = false;

    public bool HaveArms_0 = false;
    public bool HaveArms_1 = false;

    void Update()
    {
        UpdateFrontRay();
    }

    Ray ray;
    private bool can_1_6 = false;
    public LayerMask rayLayer = 0 << 8;

    public void UpdateFrontRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {
                Debug.DrawRay(ray.origin, hitInfo.point, Color.yellow);

                if (hitInfo.transform.tag == "CanEliminated")
                {
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.name == "Key_A1")
                {
                    KeyA1 = true;
                    TaskStepManagaer.Instance.FinishCurTask();
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.name == "Key_B1")
                {
                    HaveLuoSiDao = true;
                    //TaskStepManagaer.Instance.FinishCurTask();
                    Destroy(hitInfo.transform.gameObject);
                }

                //if (hitInfo.transform.name == "EX_5C_20F_Door_2R")
                //{
                //    if (HaveLuoSiDao)
                //    {
                //        hitInfo.transform.GetComponent<Level20_door>().Show();
                //    }
                //}

                if (hitInfo.transform.GetComponent<OpenDoor_WSM>()) //开门
                {
                    hitInfo.transform.GetComponent<OpenDoor_WSM>().Operation();
                }
                if ((hitInfo.transform.parent && hitInfo.transform.parent.GetComponent<OpenDoor_WSM>()))
                {
                    hitInfo.transform.parent.GetComponent<OpenDoor_WSM>().Operation();
                }

              

                if (hitInfo.transform .name == "Dianju")
                {
                    hitInfo.transform.gameObject.SetActive(false);
                    //Level20_Manager.Instance.Level20_Door_0_Script.CanOpen = true;
                }
                
                if ((hitInfo.transform.name == "QianZi"))
                {
                    hitInfo.transform.gameObject.SetActive(false);
                    HaveQianZi = true;
                }
                if ((hitInfo.transform.name == "KongZhiBianBan"))
                {
                    hitInfo.transform.gameObject.SetActive(false);

                    Level_20_Manager.Instance.Rope_1.transform.position += Vector3.up * 10f;
                    Level_20_Manager.Instance.DianTi_B.SetActive(false);
                }
                if ((hitInfo.transform.name == "B2_ShouLie"))
                {
                    hitInfo.transform.gameObject.SetActive(false);
                    Have_ShouLie = true;
                }

            }

            if (Physics.Raycast(ray, out hitInfo, 1000f, rayLayer))
            {
                Debug.DrawRay(ray.origin, hitInfo.point, Color.yellow);

                if (hitInfo.transform == Level_20_Manager.Instance.Rope_1.transform)
                {
                    Level_20_Manager.Instance.PlayerGO.transform.position = new Vector3(
                        hitInfo.transform.position.x,
                        Level_20_Manager.Instance.PlayerGO.transform.position.y,
                        hitInfo.transform.position.z) + Level_20_Manager.Instance.Rope_deviation;

                    //Level_20_Manager.Instance.PlayerScript.GoUpRope();
                }

                if (hitInfo.transform.name == "Zomber_0")
                {
                    hitInfo.transform.GetComponent<Zomber_0_WSM>().ShoShang(1);
                }

               

                if (hitInfo.transform.name == "Boss")
                {
                    hitInfo.transform.GetComponent<Zomber_F5_18>().ShoShang(1);
                }
            }

        }
    }
}
