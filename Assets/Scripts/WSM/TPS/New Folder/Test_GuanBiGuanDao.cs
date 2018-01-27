using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Test_GuanBiGuanDao : MonoBehaviour 
{

	
 #region---------外部变量----------



 #endregion

 #region---------内部变量----------



 #endregion

 #region---------调用方法----------

	public void Init()
	{
	}


 #endregion

 #region---------功能方法----------



 #endregion

 #region---------工具方法----------



 #endregion

 #region---------生命周期函数----------

	private  void Start ()
	{
		
		
	
	}

    public Transform player;
    public GameObject DiBan1;
    public GameObject DiBan2;

    private bool isShow = true;

    void Update () 
	{
        if (Vector3.Distance(player.position, transform.position) < 1f)
        {
            DiBan1.SetActive(false);
            DiBan2.SetActive(false);
            isShow = false;
        }
        if (Vector3.Distance(player.position, transform.position) > 5f && !isShow )
        {
            DiBan1.SetActive(true);
            DiBan2.SetActive(true);
            isShow = true;
        }

    }

 #endregion

}
