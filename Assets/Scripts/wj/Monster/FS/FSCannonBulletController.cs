//FSCannonBulletController.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/20/2017 12:44 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSCannonBulletController : MonoBehaviour 
{
    public float moveSpeed = 20;
    private float _timer = 0f;

	void Update () 
	{
        _timer += Time.deltaTime;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

        if (_timer >= 3)
        {
            _timer = 0f;
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _timer = 0f;
            Destroy(gameObject);
        }
    }

}
