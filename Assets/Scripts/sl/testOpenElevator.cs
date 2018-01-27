using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class testOpenElevator : MonoBehaviour
{
      
    public GameObject outsideLeftDoor;
    public GameObject outsideRightDoor;
    public GameObject insideLeftDoor;
    public GameObject insideRightDoor;
    private bool _isOpen;
    public void UpUp()
    {
        transform.DOLocalMoveY(121.5f,10f);
    }

    public void OpenElevator()
    {
        if (TaskStepManagaer.Instance.IsEqualTaskId(19005) && !_isOpen)
        {
            _isOpen = true;
            outsideLeftDoor.transform.DOLocalMoveX(outsideLeftDoor.transform.localPosition.x + 0.6f, 1);
            insideLeftDoor.transform.DOLocalMoveX(insideLeftDoor.transform.localPosition.x + 0.6f, 1);
            outsideRightDoor.transform.DOLocalMoveX(outsideRightDoor.transform.localPosition.x - 0.6f, 1);
            insideRightDoor.transform.DOLocalMoveX(insideRightDoor.transform.localPosition.x - 0.6f, 1);
        }
    }

    public void CloseElevator()
    {
        if (TaskStepManagaer.Instance.IsEqualTaskId(19006))
        {
            outsideLeftDoor.transform.DOLocalMoveX(outsideLeftDoor.transform.localPosition.x - 0.6f, 1);
            insideLeftDoor.transform.DOLocalMoveX(insideLeftDoor.transform.localPosition.x - 0.6f, 1);
            outsideRightDoor.transform.DOLocalMoveX(outsideRightDoor.transform.localPosition.x + 0.6f, 1);
            insideRightDoor.transform.DOLocalMoveX(insideRightDoor.transform.localPosition.x + 0.6f, 1);
        }
    }
}
