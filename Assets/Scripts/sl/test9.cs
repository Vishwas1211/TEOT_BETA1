using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test9 : MonoBehaviour
{
    float timer;
    public Text timerText;

    // Use this for initialization
    void Start()
    {
        timerText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O) || PlayerStatus.isTimeStart)
        {
            TimepieceStart(10);
        }
        else if (Input.GetKey(KeyCode.P) || PlayerStatus.isTimeStart)
        {
            TimepieceStart(0.5f);
        }
        else {
            timerText.gameObject.SetActive(false);
            timer = 0;
        }

    }

    public void TimepieceStart(float minutes)
    {
        timer += Time.deltaTime;
        timerText.gameObject.SetActive(true);
        //_colorCorrectionCurves.enabled = true;
        //timerText.text = (60 * 10 - Mathf.FloorToInt(timer)).ToString();
        //if (_playerDeathTimer >= 5)
        //{
        //    _playerState = STATE.REBIRTH;
        //    _playerDeathTimer = 0;
        //}
        qqwe(minutes);
    }

    void qqwe(float minutes)
    {
        int hh = 00;
        int mm = 00;
        int ss = 00;
        string m;
        string s;
        mm = System.Convert.ToInt32(60 * minutes - timer) / 60;
        ss = System.Convert.ToInt32(60 * minutes - timer) % 60;

        if (mm < 10)
        {
            m = "0" + mm.ToString();
        }
        else
        {
            m = mm.ToString();
        }
        if (ss < 10)
        {
            s = "0" + ss.ToString();
        }
        else
        {
            s = ss.ToString();
        }
        Debug.Log("00:" + m + ":" + s);
        timerText.text = "00:" + m + ":" + s;
    }
}
