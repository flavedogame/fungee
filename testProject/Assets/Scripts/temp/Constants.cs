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
		}
}

public class AnimatorRunner {
	static public void Run (Animator animator, Constants.AnimationTuple tuple)
	{
		animator.SetBool (tuple.parameter, tuple.value);
	}

}