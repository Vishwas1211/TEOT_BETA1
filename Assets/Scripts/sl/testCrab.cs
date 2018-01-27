using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCrab : MonoBehaviour {
    public enum Shield {
        red,
        blue,
        green,
    }

    public GameObject red;
    public GameObject blue;
    public GameObject green;
    public CrabController crabController;
    public Shield shield;
    int i;
    int j;
    public static bool isb;
	// Use this for initialization
	void Start () {
        qwe();
        crabController = GetComponent<CrabController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.V))
        {
            qwe();
        }
        if (crabController.curCrabState == CrabController.CRAB_STATE.DEATH)
        {
            Debug.Log(123);
        }
        
	}

    public void qwe() {
        if (isb)
        {
            return;
        }
        ewq();
        i = Random.Range(0, 3);
        shield = (Shield)i;
        switch (shield)
        {
            case Shield.red:
                red.SetActive(true);
                break;
            case Shield.blue:
                blue.SetActive(true);
                break;
            case Shield.green:
                green.SetActive(true);
                break;
            default:
                break;
        }
        isb = true;
    }

    public void ewq() {
        Debug.Log("j="+j);
        red.SetActive(false);
        blue.SetActive(false);
        green.SetActive(false);
        if (j>4)
        {
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
            return;
        }
        j++;
        StartCoroutine(www());
    }

    IEnumerator www() {
        yield return new WaitForSeconds(2);
        qwe();
    }
}
