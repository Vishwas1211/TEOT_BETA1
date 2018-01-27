using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class testJump : MonoBehaviour
{
    private Vector3[] path = new Vector3[3];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.jump);
            //PlayerStatus.isJump = true;
            path[0] = PlayerManager.Instance.gameObject.transform.position;
            path[2] = PlayerManager.Instance.eye.transform.forward * 3 + PlayerManager.Instance.gameObject.transform.position;
            path[2].y = path[0].y;
            path[1] = (path[0] - path[2]) / 2 + path[2];
            path[1].y += 1f;
            Tweener tweener = PlayerManager.Instance.transform.DOPath(path, 1.5f, PathType.CatmullRom).OnComplete(CompleteJump);
        }
    }

    void CompleteJump()
    {
        PlayerManager.Instance.playerStatus.SetPlayerStateStop();

        //PlayerStatus.isJump = false;
    }

    public void Jump() {
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.jump);
        //PlayerStatus.isJump = true;
        path[0] = PlayerManager.Instance.gameObject.transform.position;
        path[2] = PlayerManager.Instance.eye.transform.forward * 3 + PlayerManager.Instance.gameObject.transform.position;
        path[2].y = path[0].y;
        path[1] = (path[0] - path[2]) / 2 + path[2];
        path[1].y += 6f;
        Tweener tweener = PlayerManager.Instance.transform.DOPath(path, 1.5f, PathType.CatmullRom).OnComplete(CompleteJump);
    }
}
