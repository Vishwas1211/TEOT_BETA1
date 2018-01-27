using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemTest : MonoBehaviour
{
    RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.gameObject.name);
                PickUp(hit.transform);
            }
        }
       
	}



    void PickUp(Transform transform)
    {
        ItemEntity entity=transform.GetComponent<ItemEntity>();
        if (entity == null)
        {
            Debug.Log(transform.gameObject.name+ "不包含道具实体  ItemEntity");
            return;
        }
        BagModule.BagManager.Instance.PutInBag(entity.item);
    }


}
