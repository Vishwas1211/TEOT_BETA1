using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDetonator : MonoBehaviour {
    private bool isb;
    private void OnTriggerEnter(Collider other)
    {
        if (!isb&& other.transform.name == "ThirdPersonController" && TaskStepManagaer.Instance.IsEqualTaskId(28004) && NoLockView_Camera.can_21_3)
        {
            isb = true;
            UtilFunction.ResourceLoadOnPosition("Prefabs/Tools/Detonator", transform.position, transform.rotation);
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }
}
