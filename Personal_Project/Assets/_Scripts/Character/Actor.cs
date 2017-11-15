using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject {

	Animator animator;
	eAcotrState currentState = eAcotrState.STATE_IDLE;

	public eAcotrState CURRENTSTATE
	{
		set { currentState = value; }
		get { return currentState; }
	}

	public Animator ANIMATOR
	{
		set { animator = value; }
		get { return animator; }
	}


	private void Start()
	{
		animator = GetComponent<Animator>();
	}


	public virtual void ChangeAnimator(eAcotrState _State)
	{
		ANIMATOR.SetInteger("State", (int)_State);
	}
}
