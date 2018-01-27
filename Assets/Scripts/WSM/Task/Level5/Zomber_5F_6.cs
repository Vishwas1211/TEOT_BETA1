using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zomber_5F_6 : MonoBehaviour 
{


    #region---------外部变量----------

    public NavMeshAgent m_Nav;

    #endregion

    #region---------内部变量----------

    private bool canMove;

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        m_Nav = GetComponent<NavMeshAgent>();
	}

    public void KanShi()
    {
        canMove = true;
    }


    #endregion

    #region---------功能方法----------

    private void MoveToJaycee()
    {
        if (canMove)
        {
            m_Nav.SetDestination(JayceeManager.Instance.humanState.transform.position);
           

        }
    }

    private void Die()
    {

        JayceeManager.Instance.FinishCurTaskDelay(0.5f);
        Destroy(this.gameObject);
    }

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
        MoveToJaycee();



    }
    private float _curHp = 5;
    public void ShoShang(float hp)
    {
        _curHp -= hp;
        if (_curHp <= 0)
        {
            //TaskStepManagaer.Instance.FinishCurTask();
            Die();
        }
    }

    #endregion

}
