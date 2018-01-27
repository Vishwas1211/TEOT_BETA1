using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class bagCtrlTest : MonoBehaviour
{
    //public GameObject win01;
    //public GameObject win02;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.CloseUI(AppConst.UIPATH_UIRadialMenuWindow);
            UIManager.Instance.CloseUI(AppConst.UIPATH_UIPropDataWindow);
            if (this.transform.GetComponent<FreeLookCam>() != null)
                this.transform.GetComponent<FreeLookCam>().enabled = true;
            if (PlayerManager.Instance.motionController != null)
                PlayerManager.Instance.motionController.IsEnabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.Instance.CloseUI(AppConst.UIPATH_UIRadialMenuWindow);
            UIManager.Instance.CloseUI(AppConst.UIPATH_UIPropDataWindow);
            UIManager.Instance.OpenWindow(AppConst.UIPATH_UIRadialMenuWindow);
            //if (win01)
            //{
            //    if (win01.activeSelf)
            //{
            if (this.transform.GetComponent<FreeLookCam>() != null)
                this.transform.GetComponent<FreeLookCam>().enabled = false;
            if (PlayerManager.Instance.motionController != null)
                PlayerManager.Instance.motionController.IsEnabled = false;

            //        win01.SetActive(false);
            //    }
            //    else
            //    {
            //        if (this.transform.GetComponent<FreeLookCam>() != null)
            //            this.transform.GetComponent<FreeLookCam>().enabled = false;
            //        if (PlayerManager.Instance.motionController != null)
            //            PlayerManager.Instance.motionController.IsEnabled = false;
            //        win01.SetActive(true);
            //    }

            //}
            //if (win02)
            //{
            //    win02.SetActive(false);
            //}
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.Instance.CloseUI(AppConst.UIPATH_UIRadialMenuWindow);
            UIManager.Instance.CloseUI(AppConst.UIPATH_UIPropDataWindow);
            UIManager.Instance.OpenWindow(AppConst.UIPATH_UIPropDataWindow);
            //if (win01)
            //{
            //    win01.SetActive(false);
            //}
            //if (win02)
            //{
            //    if (win02.activeSelf)
            //    {
            if (this.transform.GetComponent<FreeLookCam>() != null)
                this.transform.GetComponent<FreeLookCam>().enabled = false;
            if (PlayerManager.Instance.motionController != null)
                PlayerManager.Instance.motionController.IsEnabled = false;
            //        win02.SetActive(false);
            //    }
            //    else
            //    {
            //        if (this.transform.GetComponent<FreeLookCam>() != null)
            //            this.transform.GetComponent<FreeLookCam>().enabled = false;
            //        if (PlayerManager.Instance.motionController != null)
            //            PlayerManager.Instance.motionController.IsEnabled = false;
            //        win02.SetActive(true);
            //    }

            //}
        }
    }




}
