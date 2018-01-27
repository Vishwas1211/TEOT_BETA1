using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XmlRead;

public class XMLtest : MonoBehaviour
{

	
	void Start ()
    {
        XML_ReadBase readBase = new XML_ReadItem(Application.dataPath+"/Resources/DataConfig/Item_Config.txt", "Item_Config");

        
	}
	
	
}
