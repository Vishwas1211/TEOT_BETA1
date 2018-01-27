using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Passw : MonoBehaviour {

	public GameObject Pass;
	public GameObject Wrong;
    public GameObject go;
	public void OnClickStartMenu(){
		if ( Pass.GetComponent<InputField> ().text == "good") {
            go.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/PWD");
            Pass.transform.parent.gameObject.SetActive(false);

        }
        else {
			Debug.Log ("Wrong Password");
			Wrong.SetActive (true);

		}
	}
//
	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
//	void Update () {
//
//	}
}
