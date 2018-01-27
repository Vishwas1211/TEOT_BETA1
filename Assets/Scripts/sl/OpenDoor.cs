using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class OpenDoor : MonoBehaviour
{
    public GameObject target;
    public enum Lock
    {
        none,
        key1,
        key2,
        key3,
        key4,
    }

    private bool _isOpen = false;
    public GameObject otherDoor;
    public bool islockDoor = false;
    public Lock lockDoor = Lock.none;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

            //OpenTheDoubleDoorLeft();
        }
        //if (!islockDoor)
        //{
        //    if (transform.GetComponent<Rigidbody>())
        //    {
        //        transform.GetComponent<Rigidbody>().isKinematic = false;
        //    }
        //}
    }

    public void OpenTheSlidingDoor()
    {
        if (_isOpen)
        {
            this.transform.DOLocalMoveX(this.transform.localPosition.x - 1.6f, 1);
            _isOpen = false;
        }
        else
        {
            this.transform.DOLocalMoveX(this.transform.localPosition.x + 1.6f, 1);
            _isOpen = true;
        }
    }

    public void OpenTheDoor()
    {
        if (_isOpen)
        {
            _isOpen = false;
            this.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
        }
        else
        {
            this.transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
            _isOpen = true;

        }
    }

    public void OpenTheDoubleDoorLeft()
    {
        if (!islockDoor)
        {
            if (!_isOpen)
            {
                _isOpen = true;
                this.transform.DOLocalRotate(new Vector3(0, -90, 0), 1);
                otherDoor.transform.GetComponent<Rigidbody>().isKinematic = false;
                otherDoor.transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
            }
            else
            {
                this.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                otherDoor.transform.GetComponent<Rigidbody>().isKinematic = false;
                otherDoor.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                _isOpen = false;
            }
            if (target)
            {
                Destroy(target);
            }
        }
    }

    public void OpenTheDoubleDoorRight()
    {
        if (!islockDoor)
        {
            if (!_isOpen)
            {
                _isOpen = true;
                this.transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
                otherDoor.transform.GetComponent<Rigidbody>().isKinematic = false;
                otherDoor.transform.DOLocalRotate(new Vector3(0, -90, 0), 1);
            }
            else
            {
                this.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                otherDoor.transform.GetComponent<Rigidbody>().isKinematic = false;
                otherDoor.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
                _isOpen = false;
            }
            if (target)
            {
                Destroy(target);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name.Contains("Jaycee"))
        {
            StartCoroutine(qwe());
        }
    }

    IEnumerator qwe()
    {
        yield return new WaitForSeconds(2);
        if (islockDoor)
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        transform.GetComponent<HingeJoint>().useSpring = false;
    }
}
