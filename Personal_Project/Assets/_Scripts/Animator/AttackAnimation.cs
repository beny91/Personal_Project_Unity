using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour {

	Actor targetActor = null;
    bool bIsAttack = false;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		targetActor = animator.GetComponent<Actor>();

        if(targetActor != null && targetActor.AI.CurrentAIState == eAIStateType.AI_STATE_ATTACK)
        {
            targetActor.AI.IsAttack = true;
            bIsAttack = false;
        }

	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		if (targetActor.AI.IsAttack && animatorStateInfo.normalizedTime >= 1f)
		{
			if( targetActor.AI.CurrentAIState == eAIStateType.AI_STATE_ATTACK)
            {
                targetActor.AI.IsAttack = false;
            }
		}

        if(bIsAttack == false && animatorStateInfo.normalizedTime >= 0.5f)
        {
            bIsAttack = true;
            targetActor.RunSkill();
        }
		


		
	}
}
