using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackAnimation : StateMachineBehaviour{

    Player player;
    bool bIsAttack = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();
        bIsAttack = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if( animator != null && animatorStateInfo.normalizedTime >= 1f)
        {
            player.CURRENTSTATE = eAcotrState.STATE_IDLE;
            player.ChangeAnimator(player.CURRENTSTATE);
        }

        if(bIsAttack == false && animatorStateInfo.normalizedTime >= 0.5f)
        {
            bIsAttack = true;
            player.RunSkill();
        }
    }



}
