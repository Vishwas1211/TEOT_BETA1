using UnityEngine;
using System.Collections;
using HighlightingSystem;

public class GamerRangeDetection : MonoBehaviour
{
    public float range;
    public GameObject go;
    Collider[] _collider;
    HighlighterController hc;
    private void Update()
    {
        switch (PlayerMode.Instance.playerMode)
        {
            case PlayerMode.ePlayerMode.vive:
                _collider = Physics.OverlapSphere(transform.position, range);
                break;
            case PlayerMode.ePlayerMode.pc:
                _collider = Physics.OverlapSphere(go.transform.position, range);
                break;
            default:
                break;
        }
        for (int i = 0; i < _collider.Length; i++)
        {
            if (hc = _collider[i].GetComponent<HighlighterController>())
            {
                hc.MouseOver();
            }
        }
    }

}
