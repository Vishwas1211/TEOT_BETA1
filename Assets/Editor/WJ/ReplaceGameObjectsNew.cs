//ReplaceGameObjectsNew.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/23/2017 2:01 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplaceGameObjectsNew : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject NewObject;
    public GameObject OldObject;
    private List<GameObject> oldPoisiontArray = new List<GameObject>();
    private List<GameObject> newPositionArray = new List<GameObject>();

    [MenuItem("Custom/Replace GameObjectsNew")]
    static void CreateWizard()
    {
        var replaceGameObjectsNew = ScriptableWizard.DisplayWizard<ReplaceGameObjectsNew>("Replace GameObjectsNew", "Replace");
        replaceGameObjectsNew.OldObject = Selection.gameObjects[0];
        replaceGameObjectsNew.oldPoisiontArray.Clear();
        replaceGameObjectsNew.newPositionArray.Clear();
    }

    void OnWizardCreate()
    {
        GameObject newObject = (GameObject)EditorUtility.InstantiatePrefab(NewObject);

        foreach (Transform t in OldObject.GetComponentInChildren<Transform>())
        {
            oldPoisiontArray.Add(t.gameObject);
            if (t.childCount > 0)
            {
                OldObjectFuction(t);
            }
        }

        foreach (Transform t in newObject.GetComponentInChildren<Transform>())
        {
            newPositionArray.Add(t.gameObject);
            if (t.childCount > 0)
            {
                NewObjectFuction(t);
            }
        }

        ChangeObject();
        newObject.transform.position = OldObject.transform.position;
        oldPoisiontArray.Clear();
        newPositionArray.Clear();
        DestroyImmediate(OldObject);
    }

    private void OldObjectFuction(Transform t)
    {
        foreach (Transform t1 in t)
        {
            oldPoisiontArray.Add(t1.gameObject);
            if (t1.childCount > 0)
            {
                OldObjectFuction(t1);
            }
        }
    }

    private void NewObjectFuction(Transform t)
    {
        foreach (Transform t1 in t)
        {
            newPositionArray.Add(t1.gameObject);
            if (t1.childCount > 0)
            {
                NewObjectFuction(t1);
            }
        }
    }

    private void ChangeObject()
    {
        for (int i = 0; i < oldPoisiontArray.Count; i++)
        {
            newPositionArray[i].transform.localPosition = oldPoisiontArray[i].transform.localPosition;
        }

    }
}
