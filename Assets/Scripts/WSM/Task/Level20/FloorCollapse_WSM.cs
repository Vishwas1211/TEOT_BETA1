using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class DiBan_B1 : MonoBehaviour 
{


    #region---------外部变量----------

    private Animator m_animator;

 #endregion

 #region---------内部变量----------



 #endregion

 #region---------调用方法----------

	public void Init()
	{
        m_animator = GetComponent<Animator>();

    }


 #endregion

 #region---------功能方法----------



 #endregion

 #region---------工具方法----------



 #endregion

 #region---------生命周期函数----------

	private  void Start ()
	{

        Init();


    }

	
	
	
	void Update () 
	{
		
		
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject == Level_20_Manager.Instance.PlayerGO)
        {
            m_animator.SetBool("can",true);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.gameObject == Level_20_Manager.Instance.PlayerGO)
    //    {
    //        m_animator.SetBool("can", true);
    //    }
    //}


    #endregion

}
