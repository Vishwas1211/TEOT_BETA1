using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class test15 : MonoBehaviour
{
    public Camera _camera;
    public VRTK_ControllerEvents _events;
    private Vector3 v3;
    private void Start()
    {
        v3 = this.transform.localScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            qwe();
        }

        //this.transform.rotation = PlayerManager.Instance.rightHandController.transform.rotation;
        //this.transform.localScale = new Vector3(v3.x * Mathf.Abs(_events.GetTouchpadAxis().y+2), v3.y * Mathf.Abs(_events.GetTouchpadAxis().y+2), v3.z * Mathf.Abs(_events.GetTouchpadAxis().y+2));

    }

    public void qwe()
    {
        //this.gameObject.
        this.transform.parent = _camera.transform;
        this.transform.localPosition = new Vector3(0, 0, 1);
        this.transform.localRotation = Quaternion.LookRotation(_camera.transform.up);
    }

}
