//
//  PlayerHandCollider.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/15/2017 11:54 AM.
//
//

using UnityEngine;
using System.Collections;

public class PlayerHandCollider : MonoBehaviour
{
    ContactPoint contact;
    Quaternion rot;
    Vector3 pos;
    bool isb = true;
    public PlayerHandStatus playerHandStatus;


    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Pickup":
                if (Vector3.Angle(collision.transform.up, transform.up) > 140)
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = this.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                    playerHandStatus.playerHandItem = PlayerHandItem.Varia;
                }
                break;
            case "Paper":
                if (Vector3.Angle(-collision.transform.right, transform.up) > 140)
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = this.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                    playerHandStatus.SetHandState(PlayerHandItem.Paper);

                }
                break;
            case "test":
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = this.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                }
                break;
            case "Tools":
                if (Vector3.Angle(collision.transform.up, transform.up) < 40)
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = this.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                }
                //PlayerStatus.payerHandItem = PlayerHandItem.Varia;
                break;
            case "Key":
                if (Vector3.Angle(collision.transform.forward, transform.up) < 40)
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = this.GetComponent<Rigidbody>();
                    r.isKinematic = true;
                }
                //PlayerStatus.payerHandItem = PlayerHandItem.Varia;
                break;
            case "qiang":
                //if (Vector3.Angle(collision.transform.forward, transform.up) < 40)
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = this.GetComponent<Rigidbody>();
                    r.useGravity = false;
                    //r.isKinematic = true;
                }
                break;
            case "Car":
                {
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                }
                break;
            case "Door":
                if (Vector3.Angle(collision.transform.up, transform.up) < 60)
                {
                    this.GetComponent<PlayerHandFollowController>().SetPos(collision.collider.transform);
                    var collider = this.GetComponent<Collider>();
                    collider.isTrigger = true;
                    Rigidbody r = collision.transform.GetComponent<Rigidbody>();
                    r.constraints = ~RigidbodyConstraints.FreezeAll;
                    playerHandStatus.playerHandItem = PlayerHandItem.Door;
                }
                else
                {
                    this.GetComponent<PlayerHandFollowController>().isFollow = false;
                    contact = collision.contacts[0];
                    rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                    pos = contact.point;
                }

                break;
            default:
                this.GetComponent<PlayerHandFollowController>().isFollow = false;
                contact = collision.contacts[0];
                rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                pos = contact.point;
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        this.GetComponent<PlayerHandFollowController>().isFollow = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            if (Vector3.Angle(other.transform.up, transform.up) < 140)
            {
                StartCoroutine(qq());
            }
        }
        //case "qiang":
        if (other.CompareTag("qiang"))
        //if (Vector3.Angle(collision.transform.forward, transform.up) < 40)
        {
            if (this.GetComponent<PlayerHandController>().isG && isb)
            {
                PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.climb);
                PlayerManager.Instance.GetComponent<testPa>().h = this.gameObject.transform.position.y;
                PlayerManager.Instance.GetComponent<testPa>().hand = this.gameObject;
                isb = false;
            }
            if (!this.GetComponent<PlayerHandController>().isG)
            {
                PlayerManager.Instance.GetComponent<testPa>().hand = null;
                PlayerManager.Instance.GetComponent<testPa>().h = 0;

                isb = true;
            }
        }

        if (other.CompareTag("test"))
        //if (Vector3.Angle(collision.transform.forward, transform.up) < 40)
        {
            if (this.GetComponent<PlayerHandController>().isG)
            {
                //PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.climb);
                PlayerManager.Instance.GetComponent<testPa>().x = this.gameObject.transform.position.x;
                PlayerManager.Instance.GetComponent<testPa>().z = this.gameObject.transform.position.z;
                PlayerManager.Instance.GetComponent<testPa>().hand = this.gameObject;
                isb = false;
            }
        }
        if (other.CompareTag("Car"))
        {
            if (this.GetComponent<PlayerHandController>().isG && isb)
            {
                isb = false;
                PlayerManager.Instance.GetComponent<testJump>().Jump();
            }
        }
        //PlayerStatus.payerHandItem = PlayerHandItem.Varia;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.transform.name);
        isb = true;
        switch (other.tag)
        {
            case "qiang":
                PlayerManager.Instance.GetComponent<testPa>().hand = null;
                PlayerManager.Instance.GetComponent<testPa>().h = 0;
                break;
            case "test":
                PlayerManager.Instance.GetComponent<testPa>().hand = null;
                PlayerManager.Instance.GetComponent<testPa>().x = 0;
                PlayerManager.Instance.GetComponent<testPa>().z = 0;
                break;
            case "Car":
                {
                    StartCoroutine(qq());
                }
                break;
            default:
                break;
        }
    }

    IEnumerator qq()
    {
        yield return new WaitForSeconds(0.25f);
        var collider = this.GetComponent<Collider>();
        collider.isTrigger = false;

        playerHandStatus.playerHandItem = PlayerHandItem.Empty;
        this.GetComponent<PlayerHandFollowController>().isFollow = true;
        isb = true;
    }
}