using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_0 : MonoBehaviour
{


    #region---------外部变量----------

    public NavMeshAgent m_Nav;

    #endregion

    #region---------内部变量----------

    private bool canPao = false;

    #endregion

    #region---------调用方法----------

    public void Init()
    {
        m_Nav = GetComponent<NavMeshAgent>();
    }

    public void KanShi()
    {
        canPao = true;
    }

    public void BeiDaiZhu()
    {
        canPao = false;
    }

    public void Die()
    {
        Level_20_Manager.Instance.SetFenZhi(Level_20_Manager.FenZhi.C);
        Destroy(this.gameObject);
    }




    #endregion

    #region---------功能方法----------

    public void Pao()
    {
        if (canPao)
        {
            m_Nav.SetDestination(Level_20_Manager.Instance.positions[1].position);
        }
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
        Pao();



    }



 #endregion

}
