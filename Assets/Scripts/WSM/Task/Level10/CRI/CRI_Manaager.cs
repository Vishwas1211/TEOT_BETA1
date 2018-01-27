using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class CRI_Manaager : MonoBehaviour 
{


    #region---------外部变量----------

    // 预制体
    private GameObject CRIYuZhiTi;
    // 路点
    public Transform[] waypoints_0;
    public Transform[] waypoints_1;
    public Transform[] waypoints_2;

    private bool canShegnCheng = false;

    //已经生成的数组
    private List< ExplodeCRINew> CRIS = new List<ExplodeCRINew>();

 #endregion

 #region---------内部变量----------



 #endregion

 #region---------调用方法----------

	public void Init()
	{
        CRIYuZhiTi = Resources.Load("Prefabs/Character/Enemy/CRI/CRI") as GameObject;

        waypoints_0 = Level_10_Manager.Instance.waypoints_0;
        waypoints_1 = Level_10_Manager.Instance.waypoints_1;
        waypoints_2 = Level_10_Manager.Instance.waypoints_2;
    }

    // 生成

    public void CanShegnCheng(bool can)
    {
        canShegnCheng = can;
            StartCoroutine(CloseDoor_defer());
    }

    private IEnumerator CloseDoor_defer()
    {
       

        while (canShegnCheng)
        {
            yield return new WaitForSeconds(5f);
            Generate(waypoints_0[0], waypoints_0, 3);
        }

    }

    public void Generate(Transform initial, Transform[] ways = null, int _quantity = 1)
    {
        for (int i = 0; i < _quantity; i++)
        {
            GameObject explodeCRIObject = Instantiate(CRIYuZhiTi);
            explodeCRIObject.transform.position = initial.position;
            explodeCRIObject.transform.rotation = initial.rotation;

            ExplodeCRINew ExplodeCRIScript = explodeCRIObject.GetComponent<ExplodeCRINew>();
            ExplodeCRIScript.Load(Level_10_Manager.Instance.playerGO.transform, ways);
            CRIS.Add(ExplodeCRIScript);
        }
    }

    //全部销毁

    public void XiaoHui()
    {
        for (int i = CRIS.Count; i < 0; i--)
        {
            CRIS[i].Die();
        }
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
		
		
	
	}



 #endregion

}
