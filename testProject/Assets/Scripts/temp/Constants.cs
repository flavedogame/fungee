using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants {
	public struct AnimationTuple {
		public string parameter;
		public bool value;

		public AnimationTuple (string parameter, bool value) {
			this.parameter = parameter;
			this.value = value;
		}
	}
	internal class AnimationTuples {
		internal static AnimationTuple introAnimation = new AnimationTuple ("introAnim", true);

		internal static AnimationTuple exitAnimation = new AnimationTuple ("introAnim", false);
		internal static AnimationTuple moveAnimation = new AnimationTuple("isMoving",true);
		internal static AnimationTuple stopMoveAnimation = new AnimationTuple("isMoving",false);
		internal static AnimationTuple shootAnimation = new AnimationTuple("isShooting",true);
		internal static AnimationTuple stopShootAnimation = new AnimationTuple("isShooting",false);
		internal static AnimationTuple fallAnimation = new AnimationTuple("isFalling",true);
		internal static AnimationTuple stopFallAnimation = new AnimationTuple("isFalling",false);
		internal static AnimationTuple climbAnimation = new AnimationTuple("isClimbing",true);
		internal static AnimationTuple stopClimbAnimation = new AnimationTuple("isClimbing",false);
		internal static AnimationTuple throwOverAnimation = new AnimationTuple("isThrowingOver",true);
		internal static AnimationTuple stopThrowOverAnimation = new AnimationTuple("isThrowingOver",false);
		}
}

public class AnimatorRunner {
	static public void Run (Animator animator, Constants.AnimationTuple tuple)
	{
		animator.SetBool (tuple.parameter, tuple.value);
	}
}

public class Utility{
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
	