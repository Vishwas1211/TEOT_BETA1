using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zomber_F5_16 : Injured_WSM 
{
    private bool CanUpdate = false;

    #region---------外部变量----------

    public ThirdPersonCharacter_WSM playerScript;
    public NavMeshAgent m_Nav;
    public bool canMove = true;
    #endregion

    #region---------内部变量----------

    private bool isDie = false;

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        playerScript = GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>();
        m_Nav = GetComponent<NavMeshAgent>();
        m_Nav.SetDestination(PlayerManager.Instance.transform.position);

        gameObject.SetActive(false);

        CanUpdate = true;
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

	
	
	
	void Update () 
	{
        if (!CanUpdate)
        {
            return;
        }
        

        if (UtilFunction.IsReachDistanceXYZ(playerScript.transform.position, transform.position, 0.7f))
        {
            KunZhuWanJia();
            canMove = false;
            m_Nav.isStopped = true;
        }

        if (canMove)
        {
            m_Nav.SetDestination(playerScript.transform.position);
        }

    }

    public void KunZhuWanJia()   //困住玩家
    {
        playerScript.canMove = false;
    }


    private float _curHp = 5;
    public override void ShoShang(float hp)
    {
        _curHp -= hp;
        if (_curHp <= 0)
        {
            if (!isDie)
            {
                playerScript.canMove = true;
                //TaskStepManagaer.Instance.FinishCurTask();
                JayceeManager.Instance.FinishCurTaskImmediately();//打倒僵尸看到jaycee 撞墙
                isDie = true;
            }
            Destroy(this.gameObject);
        }
    }


    #endregion

}
