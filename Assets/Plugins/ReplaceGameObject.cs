using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010

//Modified by Kristian Helle Jespersen
//June 2011

public class ReplaceGameObjects : ScriptableWizard
{
	public bool copyValues = true;
	public GameObject NewType;
    public GameObject OldObject;

    private List<GameObject> OldObjects = new List<GameObject>();
    private List<GameObject> NewTypes = new List<GameObject>();

	[MenuItem("Custom/Replace GameObjects")]
	static void CreateWizard()
	{
		var replaceGameObjects = ScriptableWizard.DisplayWizard <ReplaceGameObjects>("Replace GameObjects", "Replace");
        //replaceGameObjects.OldObjects = Selection.gameObjects;
    }

	void OnWizardCreate()
	{
        //Transform[] Replaces;
        //Replaces = Replace.GetComponentsInChildren<Transform>();


        aaa(NewType);
        bbb(OldObject);
        NewType.transform.position = OldObject.transform.position;
        NewType.transform.rotation = OldObject.transform.rotation;
        NewType.transform.localScale = OldObject.transform.localScale;
        for (int i = 0; i < NewTypes.Count; i++)
        {
            NewTypes[i].transform.localPosition = OldObjects[i].transform.localPosition;
            NewTypes[i].transform.localRotation = OldObjects[i].transform.localRotation;
            NewTypes[i].transform.localScale = OldObjects[i].transform.localScale;
            DestroyImmediate(OldObjects[i]);
        }
        DestroyImmediate(OldObject);



  //      foreach (GameObject go in OldObjects)
  //{
  //	GameObject newObject;
  //	newObject = (GameObject)EditorUtility.InstantiatePrefab(NewType);
  //	newObject.transform.parent = go.transform.parent;
  //	newObject.transform.localPosition = go.transform.localPosition;
  //	newObject.transform.localRotation = go.transform.localRotation;
  //	newObject.transform.localScale = go.transform.localScale;

        //	DestroyImmediate(go);
        //}
    }

    public void aaa(GameObject a)
    {
        while (a.transform.childCount > 0)
        {
            for (int i = 0; i < a.transform.childCount; i++)
            {
                NewTypes.Add( a.transform.GetChild(i).gameObject);
                aaa(a.transform.GetChild(i).gameObject);
            }
        }
    }
    public void bbb(GameObject b)
    {
        while (b.transform.childCount > 0)
        {
            for (int i = 0; i < b.transform.childCount; i++)
            {
                OldObjects.Add(b.transform.GetChild(i).gameObject);
                bbb(b.transform.GetChild(i).gameObject);
            }
        }
    }

}