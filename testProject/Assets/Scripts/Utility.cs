using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UtilityCS {
	public static bool AlmostEqual(Vector3 v1,Vector3 v2){
		float precision = 0.1f;
		if (Mathf.Abs (v1.x - v2.x) > precision)
			return false;
		if (Mathf.Abs (v1.y - v2.y) > precision)
			return false;
		if (Mathf.Abs (v1.z - v2.z) > precision)
			return false;
		return true;
	}
}
