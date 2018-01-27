//
//  test5.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/12/2017 1:07 PM.
//
//

using UnityEngine;
using System.Collections;

public class test5 : MonoBehaviour
{
    public GameObject[] target;
    public GameObject targetCamera;
    public int id;
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    //if (other.tag.Contains("player") && PlayerHandController.isTrigger)
        //    {
        //        //监视器亮了
        //        for (int i = 0; i < target.GetComponent<MeshRenderer>().materials.Length; i++)
        //        {
        //            StartCoroutine(OpenTV(i));
        //        }
        //    }
        //}
    }
    bool isB = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Contains("Player") && PlayerHandController.isTrigger && !isB && TaskStepManagaer.Instance.IsEqualTaskId(id))
        {
            isB = true;
            //监视器亮了
            for (int i = 0; i < target.Length; i++)
            {
                StartCoroutine(OpenTV(i));
                //go.GetComponent<MeshRenderer>().materials[i].SetColor("_EmissionColor", new Color(0.0f / 255.0f, 152f / 255.0f, 165f / 255.0f));
            }
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }

    IEnumerator OpenTV(int t)
    {
        yield return new WaitForSeconds(t);
        if (targetCamera)
        {
            targetCamera.SetActive(true);
        }
        for (int i = 0; i < target.Length; i++)
        {
            target[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f));
            target[i].GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f));

        }
    }

    public void qwe()
    {
        if (TaskStepManagaer.Instance.IsEqualTaskId(id))
        {

            for (int i = 0; i < target.Length; i++)
            {
                StartCoroutine(OpenTV(i));

                //go.GetComponent<MeshRenderer>().materials[i].SetColor("_EmissionColor", new Color(0.0f / 255.0f, 152f / 255.0f, 165f / 255.0f));
            }
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }
}