using UnityEngine;
using System.Collections;

public class ExplodeCRINew : MonoBehaviour
{
	private static string CRI_SUICIDE_EFFECTS_PATH = "level2/new/CRI/XFJ_xiao_zibao_baozha";

	private float HURT;
	private float MAX_RANGE;
	private float _CRI_RotateSpeed;
	private float _CRI_moveSpeed;
	private Transform _moveToTarget;
	private Vector3[] _thisWayArray; 
	private bool _canMove = false;
	private int _wayNumber = 0;

	private Vector3 _excursion = Vector3.zero;
	public Vector3 excursion
	{
		get{ return _excursion;}
		set{ _excursion = value; }
	}

	private Vector3 _explodeCRIPos;
	private Quaternion _explodeCRIRot;

	void Start()
	{
		HURT = 2;

		_CRI_RotateSpeed = 5;
	}


	public void Load(Transform MoveToTarget,Transform[] waysArray)
	{
		MAX_RANGE =1;
		SetTarget (MoveToTarget);
		_canMove = true;
		//AudioController.Instance.PlayAudioLoop (2066);
		RangeWays(waysArray);
		_CRI_moveSpeed = Random.Range(4, 7);

		//_moveToTarget = NetWorkHelper.GetRandPlayerBody().transform;

	}

	void Update () 
	{
	
		UpdateMoveToTarget ();
	}

	void FixedUpdate()
	{

	}

	private void RpcTransform(Vector3 pos,Quaternion rot)
	{
		_explodeCRIPos = pos;
		_explodeCRIRot	= rot;
	}

	void OnTriggerEnter( Collider other )      //攻击玩家和玻璃  TODO
	{
		if (other.transform.tag =="Concrete")//撞到墙了
		{
            Die();
		}
        if (other.transform == Level_10_Manager.Instance.playerGO.transform)  //撞到玩家
        {
            Die();
        }
        if (other.transform.tag == "Glass")  //撞到玻璃
        {
            if(other.transform.GetComponent<Glass_WSM>())
            other.transform.GetComponent<Glass_WSM>().PoSui();
        }

    }

	public void Hurt()
	{

			Die ();
	}
	public void Die()
	{


		//GameObject ExplosionEffects = UtilFunction.ResourceLoad (CRI_SUICIDE_EFFECTS_PATH);

		//ExplosionEffects.transform.position = transform.position;
		//ExplosionEffects.transform.rotation = transform.rotation;


		CRIManagerNew.Instance.survivalExplodeCRI.Remove (this);
			//AudioController.Instance.StopAudioLoop (2066);

		Destroy (this.gameObject);
	}

	public void SetTarget(Transform MoveToTarget)
	{
		this._moveToTarget = MoveToTarget;

	}

	public void RangeWays( Transform[] wayArray)                              //改变路径点
	{
		float RangePos_X;
		float RangePos_Y;
		float RangePos_Z;
		 _thisWayArray=new Vector3[wayArray.Length]; 

		for (int i = 0; i < wayArray.Length; i++)
		{
			RangePos_X = Random.Range(-MAX_RANGE, MAX_RANGE) + wayArray[i].position.x;
			Mathf.Clamp(RangePos_X, -(wayArray[i].position.x + MAX_RANGE), (wayArray[i].position.x + MAX_RANGE));

			RangePos_Y = Random.Range(-MAX_RANGE, MAX_RANGE) + wayArray[i].position.y;
			Mathf.Clamp(RangePos_Y, -(wayArray[i].position.y + MAX_RANGE), wayArray[i].position.y + MAX_RANGE);

			RangePos_Z = Random.Range(-MAX_RANGE, MAX_RANGE) + wayArray[i].position.z;
			Mathf.Clamp(RangePos_Z, -(wayArray[i].position.z + MAX_RANGE), wayArray[i].position.z + MAX_RANGE);

			_thisWayArray[i] = new Vector3(RangePos_X, RangePos_Y, RangePos_Z);
		}
	}

	protected virtual void UpdateMoveToTarget ()                                  //移动
	{
		if (_moveToTarget == null)
		{
            _moveToTarget = Level_10_Manager.Instance.playerGO.transform;

        }

		if (_wayNumber >= _thisWayArray.Length)
		{
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (_moveToTarget.position - transform.position), _CRI_RotateSpeed * Time.deltaTime);
			transform.Translate (Vector3.forward * _CRI_moveSpeed * Time.deltaTime, Space.Self);
		}
		else if (Vector3.Distance (transform.position, _thisWayArray [_wayNumber] + _excursion) > 2f)
		{
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (_thisWayArray [_wayNumber] + _excursion - transform.position), _CRI_RotateSpeed * Time.deltaTime);
			transform.Translate (Vector3.forward * _CRI_moveSpeed * Time.deltaTime, Space.Self);
		}
		else
		{
			_wayNumber++;
		}
		_CRI_RotateSpeed += 0.1f * Time.deltaTime;

	}
}
