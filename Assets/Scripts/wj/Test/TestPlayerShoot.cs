//TestPlayerShoot.cs
//TEOT_ONLINE
//
//Create by WangJie on 11/28/2017 2:33 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerShoot : MonoBehaviour 
{
    private const string BULLET_PATH = "Prefabs/Character/Enemy/Crab/Bullet/PlayerBulletTest";
    private GameObject _bullet;
    public GameObject point;
	void Start ()
	{
		
	}
	
	void Update () 
	{
        if (Input.GetMouseButtonDown(0))
        {
            _bullet = UtilFunction.ResourceLoad(BULLET_PATH);
            _bullet.transform.position = point.transform.position;
            _bullet.transform.rotation = point.transform.rotation;
        }
	}
}
