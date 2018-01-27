using UnityEngine;
using System.Collections;

public class VectorHelper  {

	public static void ResizeVector(ref Vector3 v, float s)
	{
		v.Normalize();
		v *= s;
	}

	public static Vector3 ResizeVector(Vector3 v, float s)
	{
		v.Normalize();
		v *= s;
		return v;
	}

	public static void ResizeVector(ref Vector2 v, float s)
	{
		v.Normalize();
		v *= s;
	}

	public static Vector2 ResizeVector(Vector2 v, float s)
	{
		v.Normalize();
		v *= s;
		return v;
	}

	public static float Cross(Vector2 A, Vector2 B)
	{
		float c = A.x * B.y - A.y * B.x;
		return c;
	}

	public static void FloatArray2Vector(ref Vector3 v, float[] arr)
	{
		if (arr.Length > 2)
		{
			v.x = arr [0];
			v.y = arr [1];
			v.z = arr [2];
		}
	}

	public static float DistanceXZ(Vector3 v1, Vector3 v2)
	{
		v1 = new Vector3 (v1.x, 0.0f, v1.z);
		v2 = new Vector3 (v2.x, 0.0f, v2.z);
		return Vector3.Distance (v1, v2);
	}

	public static bool DistanceLessThanXZ2(Vector3 v1, Vector3 v2, float x, float z)
	{
		float tmpX = Mathf.Abs (v1.x - v2.x);
		float tmpZ = Mathf.Abs (v1.z - v2.z);

		if (tmpX <= x && tmpZ <= z) 
		{
			return true;
		}
		return false;
	}


	public static bool DistanceMoreThanXZ2(Vector3 v1, Vector3 v2, float x, float z)
	{
		float tmpX = Mathf.Abs (v1.x - v2.x);
		float tmpZ = Mathf.Abs (v1.z - v2.z);

		if (tmpX > x && tmpZ > z) 
		{
			return true;
		}
		return false;
	}
}
