using System.Collections;using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level_05_JayceeBoss_2 : MonoBehaviour 
{


    #region---------外部变量----------
    public float Juli = 2;//移动距离

    public float ZheFu = 2;//振幅

    #endregion

    #region---------内部变量----------

    private Rigidbody m_rigidbody;

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        if (m_rigidbody)
        {
            m_rigidbody.isKinematic = true;
        }
        else
        {
            gameObject.AddComponent<Rigidbody>();
        }


	}

    public void GoUp()
    {
        //TODO
        //StartCoroutine(GoUp_defer());   
    }




    private IEnumerator GoUp_defer()
    {
        while (transform.position.y<Level_05_Manager.Instance.PlayerPositions[11].position.y)
        {
            yield return new WaitForSeconds(3);
            Up();
        }
        DaoDa();
    }

    #endregion

    #region---------功能方法----------

    public void Up()
    {
            transform.DOMoveY(transform.position.y + Juli, 0.5f);
        Level_05_Manager.Instance.Shock_TiZi_F5_25(ZheFu);
    }

    public void DaoDa()  //boss到达指定高度
    {
        m_rigidbody.isKinematic = false;
        StartCoroutine(Des());
    }

    public void GongJiWanJia()  //TODO
    {
    }

    private IEnumerator Des()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
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
