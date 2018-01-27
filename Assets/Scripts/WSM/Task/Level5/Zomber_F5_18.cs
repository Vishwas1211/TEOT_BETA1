using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zomber_F5_18 : MonoBehaviour 
{

    private float _curHp = 5;
    #region---------外部变量----------

    private NavMeshAgent m_nav;

    #endregion

    #region---------内部变量----------



    #endregion

    #region---------调用方法----------

    public void Init()
    {
    }

    public void ZhuiJi()
    {
        m_nav.SetDestination(Level_20_Manager.Instance.positions[3].position);
    }

    #endregion

    #region---------功能方法----------



    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private void Start()
    {
        m_nav = GetComponent<NavMeshAgent>();
       

    }




    void Update()
    {



    }

    public void ShoShang(float hp)
    {
        _curHp -= hp;
        if (_curHp <= 0)
        {
            //TaskStepManagaer.Instance.FinishCurTask();
            Destroy(this.gameObject);
        }
    }
    #endregion
}
