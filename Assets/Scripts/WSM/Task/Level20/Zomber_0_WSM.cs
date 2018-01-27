using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zomber_0_WSM : MonoBehaviour
{


    #region---------外部变量----------

    private NavMeshAgent m_Nav;

    private Vector3 currentTarget;
    private Transform currentTargetTransform;


    public float maxDistance = 3f; //玩家跟随时的最大间隔距离

    private float _curHp = 5;



    #endregion

    #region---------内部变量----------

    private Transform m_body;

    private bool KaiQi = false;//开启跟随

    private bool GongJi = false;
    private float GongJiTime_0 = 10;
    private float GongjiTime_1 = 0;

    private bool GongjiWanCheng = false;

    #endregion

    #region---------调用方法----------

    public void Init()
    {
        m_Nav = GetComponent<NavMeshAgent>();
        m_body = transform.Find("RickBody");
    }


    public void KaiShi()
    {
        KaiQi = true;
    }

   
    public void ShoShang(float hp)           //受伤
    {
        _curHp -= hp;
        if (_curHp <= 0)
        {
            //TaskStepManagaer.Instance.FinishCurTask();
            Destroy(this.gameObject);
        }
    }



    #endregion

    #region---------功能方法----------

    private void UpdateZhuiJi()
    {
        if (KaiQi)
        {
            m_Nav.SetDestination(Level_20_Manager.Instance.NPC_0_GO.transform.position);
            if (UtilFunction.IsReachDistanceXYZ(transform.position, Level_20_Manager.Instance.NPC_0_GO.transform.position, 0.5f))
            {
                KaiQi = false;
                GongJi = true;
                Level_20_Manager.Instance.NPC_0_Script.BeiDaiZhu();

            }
        }

    }

    private void UpdateGongJi()
    {
        if (GongJi)
        {
            GongJiTime_0 -= Time.deltaTime;

            if (!GongjiWanCheng && GongJiTime_0 <= 0)
            {
                Level_20_Manager.Instance.NPC_0_Script.Die();
                GongjiWanCheng = true;
            }

        }
    }

    #endregion

    #region---------工具方法----------
    private Vector3 __target;
    private float __Speed;
    private float __Time;
    private bool isMove = false;
    private float __time = 0;
    private void UpdateMove()
    {
        if (isMove)
        {
            m_body.transform.Translate(__target * Time.deltaTime * __Speed);
            __time += Time.deltaTime;
            if (__time > __Time)
            {
                __time = 0;
                isMove = false;
            }
        }
    }

    #endregion

    #region---------生命周期函数----------

    private void Start()
    {



    }




    void Update()
    {
        UpdateZhuiJi();
        UpdateGongJi();
    }
    #endregion

}
