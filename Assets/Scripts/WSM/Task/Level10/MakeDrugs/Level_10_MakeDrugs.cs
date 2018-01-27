using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_10_MakeDrugs : MonoBehaviour {

    Ray ray;
    public LayerMask rayLayer = 0 << 8;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.5f, rayLayer))
            {
                if (hitInfo.transform.name == "Door") //制药台的门
                {
                    Level_10_Manager.Instance.ZhiYaoTaiScript.CaoZuo();
                }

                if (hitInfo.transform.name == "XiaoYaoPing")//放小药
                {
                    if (Level_10_Manager.Instance.haveXiaoYao)
                    {
                        Level_10_Manager.Instance.ZhiYaoTaiScript.FangYao();
                        Level_10_Manager.Instance.haveXiaoYao = false;
                    }
                }
                if (hitInfo.transform.name == "DaYaoPing") //放大药
                {
                    if (Level_10_Manager.Instance.haveDaYao)
                    {
                        Level_10_Manager.Instance.ZhiYaoTaiScript.FangYao_2();
                        Level_10_Manager.Instance.haveDaYao = false;
                    }
                }
                if (hitInfo.transform.name == "Start") //开始
                {
                    Level_10_Manager.Instance.ZhiYaoTaiScript.KaiShi();
                }
                if (hitInfo.transform.name == "Stop") //结束
                {
                    Level_10_Manager.Instance.ZhiYaoTaiScript.JieShu();
                }
                if (hitInfo.transform.name == "Antidote_A_WanCheng") //拿药
                {
                    if (Level_10_Manager.Instance.ZhiYaoTaiScript.isOK)
                    {
                        if (Level_10_Manager.Instance.CompletePharmaceutical)
                        {
                            Level_10_Manager.Instance.isHaveYao = true;
                            hitInfo.transform.gameObject.SetActive(false);
                            Level_10_Manager.Instance.haveDaYao = true;
                        }
                    }
                }

            }
        }

    }
}
