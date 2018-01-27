using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class LadderHasFallenOff : MonoBehaviour 
{
    bool can = true;
	
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



    private IEnumerator CloseDoor_defer(float deferTime)
    {
        yield return new WaitForSeconds(deferTime);
        gameObject.AddComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (can&& collision.transform.gameObject == Level_05_Manager.Instance.playerGO)
        {
            StartCoroutine(CloseDoor_defer(1f));
            can = false;
        }
    }



    #endregion

}
