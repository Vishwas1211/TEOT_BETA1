using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testButtle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(-transform.forward * Time.deltaTime * 2);
        transform.position += transform.forward * Time.deltaTime * 5;
    }

    public void CreatButtle(Vector3 pos,Quaternion quaternion)
    {
        Instantiate(this.gameObject, pos, quaternion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.name == "SogBoss_1F Root(Clone)")
        {
            other.transform.root.Find("SogBoss_1F").GetComponent<SogBossController>().OnHurt(10);
        }
        if (other.transform.name == "18SOG (1)")
        {
            other.transform.GetComponent<testL18Boss>().ShouShang(1);
        }
        if (other.transform.name == "SOG")
        {
            other.transform.GetComponent<testZSQ>().ShoShang(1);
        }
        if (other.transform.name.Contains("Jaycee"))
        {
            JayceeManager.Instance.jayceeHumanController.RobBesom();
        }
        if (other.transform.name == "meidikongtiao")
        {
            other.transform.GetComponent<tset>().flash();
        }
        if (other.transform.name == "ZB_1")
        {
            other.transform.GetComponent<testZMB>().ShoShang(1);
        }
        if (other.transform.root.name == "Crab")
        {
            if (testCrab.isb || !TaskStepManagaer.Instance.IsEqualTaskId(28005))
            {
                return;
            }
            other.transform.root.GetComponent<CrabController>().OnHurt(1);
        }
        if (other.transform.name.Contains("ZMB"))
        {
            if (other.transform.root.transform.Find("ZmbAI").GetComponent<ZmbController>())
                other.transform.root.transform.Find("ZmbAI").GetComponent<ZmbController>().OnHurt(4);
            Destroy(gameObject);
        }
    }

}
