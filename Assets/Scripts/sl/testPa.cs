using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class testPa : MonoBehaviour
{
    public float h = 0;
    public float x = 0;
    public float z = 0;


    public GameObject hand;
    public GameObject pos;
    private Vector3[] path = new Vector3[3];
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //if (hand != null && h > 0)
        {
            //if (Mathf.Abs(hand.transform.position.y - h) > 0.01f)
            //{
            //    PlayerManager.Instance.gameObject.transform.position = new Vector3(PlayerManager.Instance.gameObject.transform.position.x, PlayerManager.Instance.gameObject.transform.position.y + -(hand.transform.position.y - h), PlayerManager.Instance.gameObject.transform.position.z);
            //}
            //if (Mathf.Abs(hand.transform.position.y - h) > 0.01f)
            {
                //PlayerManager.Instance.gameObject.transform.position = new Vector3(PlayerManager.Instance.gameObject.transform.position.x + -(hand.transform.position.x - x), PlayerManager.Instance.gameObject.transform.position.y, PlayerManager.Instance.gameObject.transform.position.z + -(hand.transform.position.z - z));
            }
        }
        //if (PlayerManager.Instance.transform.position.y > 0.6f && !isb)
        //{
        //    isb = true;
        //    path[0] = PlayerManager.Instance.gameObject.transform.position;
        //    path[2] = pos.transform.position;
        //    //path[2].y = path[0].y;
        //    path[1] = (path[0] - path[2]) / 2 + path[2];
        //    path[1].y += 1f;
        //    Tweener tweener = PlayerManager.Instance.transform.DOPath(path, 1.5f, PathType.CatmullRom);
        //}
    }
}
