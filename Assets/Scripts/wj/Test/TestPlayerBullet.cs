//TestPlayerBullet.cs
//TEOT_ONLINE
//
//Create by WangJie on 11/28/2017 2:52 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerBullet : MonoBehaviour 
{
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 20f, Space.Self);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.transform.name == "shield")
        {
            Destroy(gameObject);
        }
        else if (other.transform.root.name == "Crab")
        {
            other.transform.root.GetComponent<CrabController>().OnHurt(1);
            Destroy(gameObject);
        }
        else if(other.transform.root.name == "SogBoss_1F Root")
        {
            other.transform.root.Find("SogBoss_1F").GetComponent<SogBossController>().OnHurt(1);
            Destroy(gameObject);
        }
        else if (other.transform.name == "ZmbAI")
        {
            other.transform.GetComponent<ZmbController>().OnHurt(4);
            Destroy(gameObject);
        }
        else if (other.transform.name.Contains("ZMB_Neck"))
        {
            if (other.transform.root.transform.Find("ZmbAI").GetComponent<ZmbController>())
                other.transform.root.transform.Find("ZmbAI").GetComponent<ZmbController>().OnHurt(30);
            Destroy(gameObject);
        }
        else if (other.transform.name.Contains("ZMB"))
        {
            if (other.transform.root.transform.Find("ZmbAI").GetComponent<ZmbController>())
                other.transform.root.transform.Find("ZmbAI").GetComponent<ZmbController>().OnHurt(4);
            Destroy(gameObject);
        }
    }
}
