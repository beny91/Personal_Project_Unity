using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor {


	int ComboCount = 0;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			// 공격
			CURRENTSTATE = eAcotrState.STATE_ATTACK;
			// 에니메이션
			ChangeAnimator(CURRENTSTATE);

			// 콤보
			ComboNext();

			CancelInvoke("CancelCombo");
			Invoke("CancelCombo", 2f);	// 초기화.
		}
	}

	private void ComboNext()
	{
		ComboCount++;
		if (ComboCount >= 3)
			ComboCount = 0;

	}

	private void CancelCombo()
	{
		ComboCount = 0;	
	}


	public override void ChangeAnimator(eAcotrState _State)
	{
		if (_State == eAcotrState.STATE_ATTACK)
			ANIMATOR.SetInteger("Attack", ComboCount);

		ANIMATOR.SetInteger("State", (int)_State);
	}
}
