//TestPlayer.cs
//TEOT_ONLINE
//
//Create by WangJie on 10/26/2017 9:47 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour 
{
    private Animator _anim;

	
	void Start ()
	{
        _anim = GetComponent<Animator>();
	}
	
	void Update () 
	{
		
	}

    public void OnHurt()
    {
        //_anim.SetTrigger("isFlyTrigger");
        Vector3 targetPos = transform.position + (transform.forward * -5);
        transform.position = targetPos;
    }
}
