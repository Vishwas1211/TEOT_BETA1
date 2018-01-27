using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSkipStep : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(8))//Íæ¼Ò²ã
        {
            if (TaskStepManagaer.Instance.IsEqualTaskId(6005))
            {
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
            }
        }
    }


}
