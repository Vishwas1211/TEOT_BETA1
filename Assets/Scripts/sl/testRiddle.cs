using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRiddle : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject otherCamera;
    public GameObject riddle;
    public FreeLookCam freeLookCam;
    public void ShowRiddle()
    {
        mainCamera.SetActive(false);
        otherCamera.SetActive(true);
        riddle.SetActive(true);
        freeLookCam.enabled = false;
        StartCoroutine(riddle.GetComponent<TargetSymbolNew>().MakePuzzle(2));
    }
}
