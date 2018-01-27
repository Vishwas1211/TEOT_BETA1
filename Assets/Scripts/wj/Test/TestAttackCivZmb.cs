//TestAttackCivZmb.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/29/2017 6:21 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackCivZmb : MonoBehaviour 
{
    private float hp = 10;

    public void OnHurt(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            CiviliansManager.Instance.FinishCurTask();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("ThirdPersonController"))
        {
            OnHurt(10);
        }
    }
}
