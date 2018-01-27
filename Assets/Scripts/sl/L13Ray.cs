using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L13Ray : MonoBehaviour
{
    public LayerMask rayLayer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 10, rayLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.name.Contains("Standby"))
                {
                    hit.transform.GetComponent<SymbolNew>().SetIsTarget();
                }
            }
        }
    }
}
