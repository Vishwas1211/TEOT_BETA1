using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(HingeJoint))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(VRTK_InteractableObject))]
[ExecuteInEditMode]
public class test8 : MonoBehaviour
{
    Rigidbody r;
    VRTK_InteractableObject vrtk;
    BoxCollider box;
    // Use this for initialization
    void Start()
    {
        r = GetComponent<Rigidbody>();
        //r.constraints = RigidbodyConstraints.FreezeAll;
        r.useGravity = false;
        r.mass = 1;
        r.drag = 2;
        r.angularDrag = 1;
        vrtk = GetComponent<VRTK_InteractableObject>();
        vrtk.isGrabbable = true;
        vrtk.holdButtonToUse = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
