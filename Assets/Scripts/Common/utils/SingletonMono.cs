using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour where T:SingletonMono<T> {

    protected static T _instance = null;

    public static T Instance {
        get {
            if (_instance == null)
            {
                GameObject go = GameObject.Find(typeof(T).Name);
                if (go == null)
                {
                    go = new GameObject();
                    go.name = typeof(T).Name;
                    _instance = go.AddComponent<T>();
                }
                else
                {
                    if (go.GetComponent<T>()==null)
                    {
                        go.AddComponent<T>();
                    }
                    _instance = go.GetComponent<T>();
                }
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
}
