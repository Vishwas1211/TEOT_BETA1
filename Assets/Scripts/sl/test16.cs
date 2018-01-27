using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class test16 : MonoBehaviour
{
    public GameObject go;
    protected Highlighter h;
    public int id;
    private bool _b = false;
    private void Awake()
    {
        h = gameObject.AddComponent<Highlighter>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            go.SetActive(true);
        }
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
}
