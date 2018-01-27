using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Climb_WSM : MonoBehaviour
{


    #region---------外部变量----------

    public BoxCollider m_Collider;
    private bool _can =false;

    #endregion

    #region---------内部变量----------



    #endregion

    #region---------调用方法----------

    public void Init()
    {
    }


   
    
    

    public void CanGo()
    {
        m_Collider.enabled = true;
    }

 #endregion

 #region---------功能方法----------



 #endregion

 #region---------工具方法----------



 #endregion

 #region---------生命周期函数----------

	private  void Start ()
	{

        m_Collider.enabled = false;

    }

	
	
	
	void Update () 
	{
		
		
	
	}



 #endregion

}
