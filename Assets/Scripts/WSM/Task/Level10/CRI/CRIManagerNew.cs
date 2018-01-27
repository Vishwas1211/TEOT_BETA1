using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CRIManagerNew : MonoBehaviour {

	private static string EXPLODE_CRI_PATH="level2/new/CRI/L2ExplodeCRI";
	private static string SHOOTINGMODEL_CRI_PATH = "level2/new/CRI/L2shootingModelCRI";

	public enum CRIType
	{
		ExplodeCRI,
		ShootintModelCRI,
	}

		private List<ExplodeCRINew> _survivalExplodeCRI = new List<ExplodeCRINew>();
		public List<ExplodeCRINew> survivalExplodeCRI
		{
			get{ return _survivalExplodeCRI;}
		}
	
		//private List<ShootingModeCRI> _survivalShootingModeCRI = new List<ShootingModeCRI>();
		//public List<ShootingModeCRI> survivalShootingModeCRI
		//{
		//	get{ return _survivalShootingModeCRI;}
		//}

	private static CRIManagerNew _Instance;
	public static CRIManagerNew Instance
	{
		get
		{
			if (_Instance == null)
			{
				GameObject go = new GameObject("CRIManagerNew");
				DontDestroyOnLoad(go);  
				_Instance = go.AddComponent<CRIManagerNew>();
			}
			return _Instance;
		}
	}

	public void Init()
	{
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AllDie()
		{
			for (int i = _survivalExplodeCRI.Count - 1; i >= 0; i--) 
			{
			    _survivalExplodeCRI [i].Hurt ();
			}
	
	
			//for (int i = _survivalShootingModeCRI.Count - 1; i >= 0; i--)
			//{
			//	_survivalShootingModeCRI [i].Die ();
			//}		
		}

		public IEnumerator AllDie_defer(float deferTime)
		{
			yield return new WaitForSeconds(deferTime);
			AllDie ();
		}

	public void Generate(Transform initial,CRIType type,Transform [] ways = null ,Vector3 [] waysVector3 = null,int _quantity = 1)
	{
		for (int i = 0; i < _quantity; i++)
		{
			
			switch (type)
			{
			case CRIType.ExplodeCRI:
				{
					GameObject explodeCRIObject = UtilFunction.ResourceLoad (EXPLODE_CRI_PATH) as GameObject;
					explodeCRIObject.transform.position = initial.position;
					explodeCRIObject.transform.rotation = initial.rotation;

					ExplodeCRINew ExplodeCRIScript = explodeCRIObject.GetComponent<ExplodeCRINew> ();
					ExplodeCRIScript.Load (initial, ways);
					_survivalExplodeCRI.Add (ExplodeCRIScript); 

				}
				break;
			case CRIType.ShootintModelCRI:
				{
					//GameObject shootintModelCRIObject = UtilFunction.ResourceLoad (SHOOTINGMODEL_CRI_PATH) as GameObject;
					//ShootingModeCRI shootintModelCRIScript = shootintModelCRIObject.GetComponent<ShootingModeCRI> ();
					//shootintModelCRIScript.Init (waysVector3);
					//shootintModelCRIObject.transform.position = initial.position;
					//shootintModelCRIObject.transform.rotation = initial.rotation;

					//_survivalShootingModeCRI.Add (shootintModelCRIScript); 
				}
				break;
			default:
				break;
			}
		}

	}
}
