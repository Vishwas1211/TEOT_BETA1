//FSBlastWaveController.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/20/2017 12:53 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSBlastWaveController : MonoBehaviour 
{
    private float _speed = 40;
    private float _timer = 0f;
	
	void Update () 
	{
        _timer += Time.deltaTime;
        transform.localScale = new Vector3(1, 1, transform.localScale.z + _speed * Time.deltaTime);

        if (_timer >= 3)
        {
            _timer = 0f;
            Destroy(gameObject);
        }
	}
}
