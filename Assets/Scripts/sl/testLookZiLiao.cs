using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLookZiLiao : MonoBehaviour {
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject go;
    public bool isb;
    private float x;
    private float y;
    private float s = 1;
	// Update is called once per frame
	void Update () {
        if (isb)
        {
            //transform.rotation = Quaternion.Euler(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            //transform.Rotate(Input.GetAxis("Mouse Y"), 0, -Input.GetAxis("Mouse X"));
            x += Input.GetAxis("Mouse Y");
            y += Input.GetAxis("Mouse X");
            s += Input.GetAxis("Mouse ScrollWheel");
            transform.localRotation = Quaternion.Euler(x, 0, -y);
            transform.localScale = new Vector3(s, s, s);
            if (Input.GetMouseButtonDown(0))
            {
                Camera1.transform.root.GetComponent<FreeLookCam>().enabled = true;
                Camera1.SetActive(true);
                Camera2.SetActive(false);
                isb = false;
                this.gameObject.SetActive(false);
            }
        }
	}

    public void qwe() {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
        Camera1.transform.root.GetComponent<FreeLookCam>().enabled = false;
        transform.parent = go.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0,0,0);
        transform.localScale = Vector3.one;
        isb = true;
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
    }
}
