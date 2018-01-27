using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_21_Deng : MonoBehaviour {

    private Material m_Materia;

    private bool CanShan = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init()
    {
        m_Materia = GetComponent<MeshRenderer>().material;
        m_Materia.color = Color.black;
    }

    public void Shan()
    {
        CanShan = true;
        Level_21_Manager.Instance.camera_21.SetActive(true);
    }

    public void UpdateShan()
    {
        if (CanShan)
        {
            m_Materia.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    public void Liang()
    {
        CanShan = false;
        m_Materia.color = Color.white;
        Level_21_Manager.Instance.camera_21.SetActive(true);
    }
}
