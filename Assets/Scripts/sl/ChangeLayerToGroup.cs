using UnityEngine;
using System.Collections;

public class ChangeLayerToGroup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Cube123"))
        {
            this.gameObject.layer = 9;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Cube123"))
        {
            this.gameObject.layer = 0;
        }
    }

    public void ChangePos() {
        transform.position = new Vector3(-7.06f, 3.81f, -25.22f);
    }
}
