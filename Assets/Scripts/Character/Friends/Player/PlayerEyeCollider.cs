
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerEyeCollider : MonoBehaviour
{
    ScreenOverlay v;
    float x = -5f;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        //v = GetComponent<ScreenOverlay>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.N))
        //{
        //    timer += Time.deltaTime * 2.0f;
        //    x = Mathf.Lerp(5, 0, timer);
        //    x = Mathf.Clamp(x, 0, 5);
        //    v.intensity = x;
        //}
        //if (Input.GetKey(KeyCode.J))
        //{
        //    timer += Time.deltaTime * 2.0f;
        //    x = Mathf.Clamp(x,0,1);
        //    x = Mathf.Lerp(1, 0, timer);
        //    v.intensity = x;
        //}
        //if (Input.GetKey(KeyCode.B))
        //{
        //    timer = 0;
        //    x = 0f;
        //}
        //if (!PlayerStatus.isCanMove)
        //{
        //    PlayerStatus.isMove = false;
        //    timer += Time.deltaTime * 2.0f;
        //    x = Mathf.Lerp(5, 0, timer);
        //    x = Mathf.Clamp(x, 0, 5);
        //    v.intensity = x;
        //}
        //else
        //{
        //    PlayerStatus.isMove = true;
        //    timer = 0;
        //    v.intensity = 5f;
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, other.transform.position - transform.position);
        //if (Physics.Raycast(ray, out hit, 0.5f))
        //{
        //    x = Vector3.Distance(transform.position, hit.point) * 10;
        //    //Debug.Log(x);
        //    timer += Time.deltaTime * 2.0f;
        //    x = Mathf.Lerp(0, -5, timer);
        //    x = Mathf.Clamp(x, -5, 0);
        //    v.intensity = x;
        //    if (x <= 0.0f)
        //    {
        //        //PlayerStatus.isCanMove = false;
        //        PlayerStatus.isCanMove = true;

        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //PlayerStatus.isCanMove = false;
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.stop);

    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.move);
    }
    private void OnTriggerExit(Collider other)
    {
        //timer = 0;
        //v.intensity = 0;
        //PlayerStatus.isCanMove = true;
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.move);

    }
}
