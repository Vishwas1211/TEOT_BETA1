//FSManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/19/2017 9:47 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSManager : SingletonMono<FSManager>
{
    private const string FS_PREFAB_PATH = "Prefabs/Character/Enemy/FS/FSRoot";

    private GameObject _Fs;
    public GameObject Fs
    {
        get { return _Fs; }
    }

    private FSController _fsController;
    public FSController fsController
    {
        get { return _fsController; }
    }

    public void Init()
    {
        _Fs = UtilFunction.ResourceLoad(FS_PREFAB_PATH);
        _fsController = _Fs.GetComponent<FSController>();
        _fsController.Init();
    }
}
