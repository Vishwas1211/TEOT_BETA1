using System.Collections;using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level_21_Elevator_WSM : MonoBehaviour 
{


    #region---------外部变量----------

    private Transform door_L;
    private Transform door_R;

    public float QiShiGaoDu;
    private float ZhongZhiGaoDu  =122f;



    #endregion

    #region---------内部变量----------

    private float KaiMenWeiZhi_L;
    private float KaiMenWeiZhi_R;

    private float GuanMenWeiZhi_L;
    private float GuanMenWeiZhi_R;

    private bool canUP = false;

 #endregion

 #region---------调用方法----------

	public void Init()
	{
        door_L = transform.Find("DianTi_A_Shang_DoorL");
        door_R = transform.Find("DianTi_A_Shang_DoorR");
        QiShiGaoDu = transform.position.y;

        KaiMenWeiZhi_L = door_L.position.x + 0.6f;
        KaiMenWeiZhi_R = door_R.position.x - 0.6f;

        GuanMenWeiZhi_L = door_L.position.x;
        GuanMenWeiZhi_R = door_R.position.x;
    }


    public void OpenDoor()
    {

        canUP = false;

        Level_21_Manager.Instance.playerGO.transform.parent = null;

        door_L.DOMoveX(KaiMenWeiZhi_L, 1f);
        door_R.DOMoveX(KaiMenWeiZhi_R, 1f);
    }

    public void GuanDoor()
    {
        StartCoroutine(CanShangSheng());
        door_L.DOMoveX(GuanMenWeiZhi_L, 1f);
        door_R.DOMoveX(GuanMenWeiZhi_R, 1f);
    }

    public void ShangSheng()
    {
        if (canUP)
        {
            Level_21_Manager.Instance.playerGO.transform.parent = transform;
            transform.DOMoveY(ZhongZhiGaoDu, 10f);
            StartCoroutine(DaoDa());
        }
    }

    #endregion

    #region---------功能方法----------

    public IEnumerator CanShangSheng()
    {
        yield return new WaitForSeconds(2f);
        canUP = true;
        yield return new WaitForSeconds(2f);
        ShangSheng();
    }



    public IEnumerator DaoDa()
    {
        yield return new WaitForSeconds(14f);
        OpenDoor();
    }

    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private  void Start ()
	{
		
		
	
	}

	
	
	
	void Update () 
	{
		
		
	
	}



 #endregion

}
