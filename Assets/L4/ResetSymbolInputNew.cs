using UnityEngine;
using System.Collections;

public class ResetSymbolInputNew : MonoBehaviour {

	private TargetSymbolNew _myManager;
	private bool _canReset = true;

	public void SetManager(TargetSymbolNew Manager)
	{
		_myManager = Manager;
	}

	public void SetIsTarget()
	{
		if (_canReset)
		{
//			_myManager.ResetInput ();
			//NetServerUtil.Instance.RpcRipairReset ();
			_canReset = false;
		}
		StartCoroutine(CanReset_defer(1f));
	}

	private IEnumerator CanReset_defer(float deferTime)
	{
		yield return new WaitForSeconds(deferTime);
		_canReset = true;
	}

}
