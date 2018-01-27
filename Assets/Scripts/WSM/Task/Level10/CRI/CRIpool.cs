using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CRIPool : MonoBehaviour {

//	private static string EXPLODE_CRI_PATH="level2/new/L2ExplodeCRI";
//	private static string SHOOTINGMODEL_CRI_PATH = "level2/new/L2shootingModelCRI";
//	private static string CRI_BULLET_PATH = "level2/new/CRIBullet";
//	private static string CRI_BULLET_EXPLODE_PATH = "level2/new/CRI/XMF_zidan_baodian";
//	private static string CRI_BULLET_FIRE_PATH = "level2/new/CRI/XFJ_fire";
//	private static string CRI_SUICIDE_EFFECTS_PATH = "level2/new/CRI/XFJ_xiao_zibao_baozha";
//	private static string CRI_EXPLOSION_EFFECTS_PATH = "level2/new/XIAO BAOZHAO";
//
//
//	public enum PoolType
//	{
//		ExplodeCRI,
//		ShootintModelCRI,
//		CRIBullet,
//		CRIBulletFire,
//		CRIBulletExplode,
//		CRIBSuicideEffects,
//		ExplosionEffects,
//	}
//
//	private ObjectPoolManager _CRIPool;
//
//	private ObjectPoolManager _CRI2Pool;
//
//	private ObjectPoolManager _CRIBulletPool;
//
//	private ObjectPoolManager _CRIBulletExplodePool;
//
//	private ObjectPoolManager _CRIBulletFirePool;
//
//	private ObjectPoolManager _CRIBSuicideEffects;
//
//	private ObjectPoolManager _bulletEffects;
//
//	private List<ExplodeCRINew> _survivalExplodeCRI = new List<ExplodeCRINew>();
//	public List<ExplodeCRINew> survivalExplodeCRI
//	{
//		get{ return _survivalExplodeCRI;}
//	}
//
//	private List<ShootingModeCRI> _survivalShootingModeCRI = new List<ShootingModeCRI>();
//	public List<ShootingModeCRI> survivalShootingModeCRI
//	{
//		get{ return _survivalShootingModeCRI;}
//	}
//
//	private static CRIPool _Instance;
//	public static CRIPool Instance
//	{
//		get
//		{
//			if (_Instance == null)
//			{
//				GameObject go = new GameObject("CRIPool");
//				DontDestroyOnLoad(go);  
//				_Instance = go.AddComponent<CRIPool>();
//			}
//			return _Instance;
//		}
//	}
//
//	public void Init () 
//	{
//	
//		_CRIPool = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_CRIPool.Load (DataConfig.EXPLODE_CRI_POOL, EXPLODE_CRI_PATH);
//
//		foreach (GameObject go in _CRIPool.pool) 
//		{
////			ExplodeCRINew explode = go.GetComponent<ExplodeCRINew> ();
//////			explode.Init ();
//		}
//
//		_CRI2Pool = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_CRI2Pool.Load (DataConfig.SHOOTING_CRI_POOL, SHOOTINGMODEL_CRI_PATH);
//
//		foreach (GameObject go in _CRI2Pool.pool)
//		{
//			ShootingModeCRI shooting = go.GetComponent<ShootingModeCRI> ();
//			shooting.Init ();
//		}
//
//
//		_CRIBulletPool = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_CRIBulletPool.Load (DataConfig.CRI_BULLET_POOL, CRI_BULLET_PATH);
//
//		_CRIBulletFirePool = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_CRIBulletFirePool.Load (DataConfig.CRI_BULLET_FIRE_POOL, CRI_BULLET_FIRE_PATH);
//
//		_CRIBulletExplodePool = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_CRIBulletExplodePool.Load (DataConfig.CRI_BULLET_EFFECTS_POOL, CRI_BULLET_EXPLODE_PATH);
//
//		_CRIBSuicideEffects = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_CRIBSuicideEffects.Load (DataConfig.CRI_SUICIDE_EFFECTS_POOL, CRI_SUICIDE_EFFECTS_PATH);
//
//		_bulletEffects = this.gameObject.AddComponent<ObjectPoolManager> ();
//		_bulletEffects.Load (DataConfig.CRI_DIE_EFFECTS_POOL, CRI_EXPLOSION_EFFECTS_PATH);
//	}
//
//	public void AllDie()
//	{
//		for (int i = _survivalExplodeCRI.Count - 1; i >= 0; i--) 
//		{
////			_survivalExplodeCRI [i].Die ();
//		}
//
//
//		for (int i = _survivalShootingModeCRI.Count - 1; i >= 0; i--)
//		{
//			_survivalShootingModeCRI [i].Die ();
//		}		
//	}
//
//	public IEnumerator AllDie_defer(float deferTime)
//	{
//		yield return new WaitForSeconds(deferTime);
//		AllDie ();
//	}
//
//	public void OpenPool(Transform initial,PoolType type,Transform [] ways = null ,int _quantity = 1)
//	{
//		GameObject _go;
//		for (int i = 0; i < _quantity; i++) 
//		{
//			switch (type) {
//			case PoolType.ExplodeCRI:
//				{
//					_go = _CRIPool.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						ExplodeCRINew _script = _go.GetComponent <ExplodeCRINew> ();
////						_script.OpenCRI (NewPlayerCameraRigManager.Instance.player.transform, ways);
//						_survivalExplodeCRI.Add (_script); 
//					}
//				}
//				break;
//			case PoolType.ShootintModelCRI:
//				{
//					_go = _CRI2Pool.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						ShootingModeCRI _script = _go.GetComponent <ShootingModeCRI> ();
//						_script.OpenCRI (NewPlayerCameraRigManager.Instance.player.transform); 
//						_survivalShootingModeCRI.Add (_script);
//					}
//				}
//				break;
//			case PoolType.CRIBullet:
//				{
//					_go = _CRIBulletPool.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						_go.transform.rotation = initial.rotation;
////						_go.GetComponent <CRIBullet> ().OpenCRI (NewPlayerCameraRigManager.Instance.player.transform);  
//					}
//				}
//				break;
//			case PoolType.CRIBulletFire:
//				{
//					_go = _CRIBulletFirePool.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						_go.transform.rotation = initial.rotation;
//						_go.GetComponent <ExplosionEffects> ().Open ();  
//						StartCoroutine (DelayedClosePool (_go, PoolType.CRIBulletFire, 1f));
//					}
//				}
//				break;
//			case PoolType.CRIBulletExplode:
//				{
//					_go = _CRIBulletExplodePool.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						_go.transform.rotation = initial.rotation;
//						_go.GetComponent <ExplosionEffects> ().Open ();  
//						StartCoroutine (DelayedClosePool (_go, PoolType.CRIBulletFire, 1f));
//					}
//				}
//				break;
//			case PoolType.CRIBSuicideEffects:
//				{
//					_go = _CRIBSuicideEffects.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						_go.transform.rotation = initial.rotation;
//						_go.GetComponent <ExplosionEffects> ().Open ();  
//						StartCoroutine (DelayedClosePool (_go, PoolType.CRIBSuicideEffects, 1f));
//					}
//				}
//				break;
//			case PoolType.ExplosionEffects:
//				{
//					_go = _bulletEffects.GetGameObjectFromPool ();
//					if (_go != null)
//					{
//						_go.transform.position = initial.position;
//						_go.transform.rotation = initial.rotation;
//						_go.GetComponent <ExplosionEffects> ().Open ();  
//						StartCoroutine (DelayedClosePool (_go, PoolType.ExplosionEffects, 1f));
//					}
//				}
//				break;
//			default:
//				break;
//			}
//		}
//	}
//
//
//	public void ClosePool(GameObject GO,PoolType type)
//	{
//		switch (type)
//		{
//		case PoolType.ExplodeCRI:
//			{
//				_CRIPool.ReturnGameObjectToPool (GO);
//				ExplodeCRINew _script = GO.GetComponent <ExplodeCRINew> ();
////				_script.CloseCRI (); 
//				_survivalExplodeCRI.Remove (_script); 
//			}
//			break;
//		case PoolType.ShootintModelCRI:
//			{
//				_CRI2Pool.ReturnGameObjectToPool (GO);
//				ShootingModeCRI _script = GO.GetComponent <ShootingModeCRI> ();
//				_script.CloseCRI (); 
//				_survivalShootingModeCRI.Remove (_script);
//			}
//			break;
//		case PoolType.CRIBullet:
//			{
//				_CRIBulletPool.ReturnGameObjectToPool (GO);
////				GO.GetComponent <CRIBullet> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.CRIBulletFire:
//			{
//				_CRIBulletFirePool.ReturnGameObjectToPool (GO);
////				GO.GetComponent <CRIBullet> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.CRIBulletExplode:
//			{
//				_CRIBulletExplodePool.ReturnGameObjectToPool (GO);
////				GO.GetComponent <CRIBullet> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.CRIBSuicideEffects:
//			{
//				_CRIBSuicideEffects.ReturnGameObjectToPool (GO);
////				GO.GetComponent <CRIBullet> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.ExplosionEffects:
//			{
//				_bulletEffects.ReturnGameObjectToPool (GO);
//			}
//			break;
//		default:
//			break;
//		}
//	}
//
//	public IEnumerator DelayedClosePool (GameObject GO,PoolType type,float delayedTime)
//	{
//		yield return new WaitForSeconds(delayedTime);
//		switch (type)
//		{
//		case PoolType.ExplodeCRI:
//			{
//				_CRIPool.ReturnGameObjectToPool (GO);
////				GO.GetComponent <ExplodeCRINew> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.ShootintModelCRI:
//			{
//				_CRI2Pool.ReturnGameObjectToPool (GO);
//				GO.GetComponent <ShootingModeCRI> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.CRIBullet:
//			{
//				_CRIBulletPool.ReturnGameObjectToPool (GO);
////				GO.GetComponent <CRIBullet> ().CloseCRI (); 
//			}
//			break;
//		case PoolType.CRIBulletFire:
//			{
//				_CRIBulletFirePool.ReturnGameObjectToPool (GO);
//			}
//			break;
//		case PoolType.CRIBSuicideEffects:
//			{
//				_CRIBSuicideEffects.ReturnGameObjectToPool (GO);
//			}
//			break;
//		case PoolType.ExplosionEffects:
//			{
//				_bulletEffects.ReturnGameObjectToPool (GO);
//			}
//			break;
//		default:
//			break;
//		}
//	}
}
