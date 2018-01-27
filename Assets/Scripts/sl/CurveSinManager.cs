using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveSinManager : MonoBehaviour {

	private const string BLACK_PATH = "level5/curve/curve_sin_black";
	private const string RED_PATH = "level5/curve/curve_sin_red";
	private const string GREEN_PATH = "level5/curve/curve_sin_green";
	private const string BLUE_PATH = "level5/curve/curve_sin_blue";

	public enum TYPE
	{
		RED,
		BLUE,
		GREEN,
		BLACK,
	}

	private List<CurveSin> _curveSins= new List<CurveSin> ();
	private CurveSin curCurveSin;
	private float curNormalScale;

	private Vector3 _pos;
	private Vector3 _angle;
	private Vector3 _scale;

	void Start()
	{
		//Init(new Vector3(-3.064f, 97.689f, 26.408f), new Vector3(162.521f,0f,0f), new Vector3(1.1f, 0.8f, 1f));
	}

	public void Init(Vector3 pos, Vector3 angle, Vector3 scale)
	{
		_pos = pos;
		_angle = angle;
		_scale = scale;

		CreateGameObject (BLACK_PATH, TYPE.BLACK);
		CreateGameObject (RED_PATH, TYPE.RED);
		CreateGameObject (GREEN_PATH, TYPE.GREEN);
		CreateGameObject (BLUE_PATH, TYPE.BLUE);
	}

	public void Show(TYPE type)
	{
		foreach(CurveSin sin in _curveSins)
		{
			sin.gameObject .SetActive(false);
			if (type == sin.sinType)
			{
				sin.gameObject .SetActive(true);
				curCurveSin = sin;
			}

			if (sin.sinType == TYPE.BLACK)
			{
				sin.gameObject.SetActive(true);
			}
		}
	}

	public void SetNormal(float scale01)
	{
		float scale = ConvertScale (scale01);

		CurveSin curveSin = GetCurveSin (TYPE.BLACK);
		curveSin.UpdateScale (scale);
		curNormalScale = scale;
	}

	public bool UpdateScale(float scale01)
	{
		float scale = ConvertScale (scale01);
		if (curCurveSin != null)
		{
			curCurveSin.UpdateScale (scale);
			return IsCurveMatch (scale);
		}

		return false;
	}

	private bool IsCurveMatch(float scale)
	{
		if (Mathf.Abs (curNormalScale - scale) <= 0.06f)
			return true;
		return false;
	}

	private float ConvertScale(float scale01)
	{
		return scale01 * 3.0f + 1.0f;
	}

	private CurveSin GetCurveSin(TYPE type)
	{
		foreach(CurveSin sin in _curveSins)
		{
			if (type == sin.sinType)
			{
				return sin;
			}
		}

		return null;
	}

	private void CreateGameObject(string path, TYPE type)
	{
		GameObject go = UtilFunction.ResourceLoad (path);
		go .SetActive(false);

		go.transform.position = _pos;
		go.transform.eulerAngles = _angle;
		go.transform.localScale = _scale;

//		NetworkServer.Spawn (go);
		CurveSin curveSin = go.GetComponent<CurveSin> ();
		curveSin.sinType = type;
		_curveSins.Add (curveSin);
	}



}
