using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class ClickNextTask : MonoBehaviour
{
    public GameObject go;
    protected Highlighter h;
    public int id;
    private bool _b = false;
    private void Awake()
    {
        //h = gameObject.AddComponent<Highlighter>();
    }

    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_b && PlayerHandController.isTrigger && TaskStepManagaer.Instance.IsEqualTaskId(id))
        {
            _b = true;
            if (go)
            {
                go.SetActive(true);
            }
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }

    public void NextTask()
    {
        if (go)
        {
            go.SetActive(true);
        }
        if (TaskStepManagaer.Instance.IsEqualTaskId(id))
        {
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }
}
