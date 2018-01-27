//
//  PlayerToolsBase.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/14/2017 9:54 AM.
//
//

using UnityEngine;
using System.Collections;
using VRTK;

public class PlayerToolsBase : MonoBehaviour
{


    public VRTK_ControllerEvents events;
    public int id;


    protected virtual void OnEnable()
    {
        //events.TriggerPressed += new ControllerInteractionEventHandler(DoTrigger);
    }

    protected virtual void OnDisable()
    {
        //events.TriggerPressed -= new ControllerInteractionEventHandler(DoTrigger);
    }

    public virtual void DoTrigger(object sender, ControllerInteractionEventArgs e)
    {
    }

    public virtual void UseTheTools()
    {

    }

    public virtual void ReTheTools()
    {
    }

    public virtual void rrrr()
    {
        //GameObject go = GameObject.Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation);
        ////StartCoroutine(aaa(go));
        //if (!go.GetComponent<Rigidbody>())
        //{
        //    go.gameObject.AddComponent<Rigidbody>();
        //}
        //go.AddComponent<AddTools>().id = 1;
        //go.GetComponent<Rigidbody>().useGravity = true;
        //this.gameObject.SetActive(false);
        //events.gameObject.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<BackpackController>().RemoveTools(id);
    }

    IEnumerator aaa(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
    }
}