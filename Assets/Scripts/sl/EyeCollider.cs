using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class EyeCollider : MonoBehaviour {
    VignetteAndChromaticAberration v;
    float x= 0.036f;
    float timer = 0;
    // Use this for initialization
    void Start () {
       v = GetComponent<VignetteAndChromaticAberration>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKey(KeyCode.N))
        //{
        //    timer += Time.deltaTime*2.0f;
        //    x = Mathf.Clamp(x, 0, 1);
        //    x = Mathf.Lerp(0, 1, timer);
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
        //    //x = 0.036f;
        //}
        //if (!PlayerStatus.isCanMove)
        //{
        //    PlayerStatus.isMove = false;
        //    timer += Time.deltaTime * 2.0f;
        //    x = Mathf.Lerp(0, 1, timer);
        //    x = Mathf.Clamp(x, 0, 1);
        //    v.intensity = x;
        //}
        //else {
        //    PlayerStatus.isMove = true;
        //    timer = 0;
        //    v.intensity = 0.036f;
        //}

	}

    private void OnTriggerEnter(Collider other)
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        //PlayerStatus.isCanMove = false;
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.stop);
    }

    private void OnCollisionExit(Collision collision)
    {
        //PlayerStatus.isMove = true;
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.move);
    }
}
