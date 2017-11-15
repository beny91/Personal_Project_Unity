using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour {

	Actor targetActor = null;
	bool bAttack;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		targetActor = animator.GetComponent<Actor>();
		bAttack = true;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		if (targetActor != null && animatorStateInfo.normalizedTime >= 1f)
		{
			targetActor.CURRENTSTATE = eAcotrState.STATE_IDLE;
			targetActor.ChangeAnimator(targetActor.CURRENTSTATE);
			bAttack = false;
		}
		else
		{
			
		}

		
	}
}
