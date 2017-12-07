using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor {

    [SerializeField]
    float rotateSpeed = 3f;

    [SerializeField]
    float moveSpeed = 10f;


	int ComboCount = 0;

    private void Start()
    {
        GameCharacter character =
         CharacterManager.Instance.AddCharacter(TEMPLATEKEY);
        character.TargetComponenet = this;
        SelfCharacter = character;

        ANIMATOR = GetComponent<Animator>();

        for (int i = 2; i < (int)eBoardType.BOARD_MAX; i++)
        {
            eBoardType boardType = (eBoardType)i;
            string boardName = boardType.ToString();

            GameObject go = GameObject.Find(boardName);
            BaseBoard board;
            board = go.GetComponent<BaseBoard>();
            BoardManager.Instance.AddBoard(this, board, boardType);

            if (boardType == eBoardType.BOARD_HP)
            {
                board.SetData(ConstValue.SetData_HP,
                SelfCharacter.GetCharacterStatus.GetStatusData(eStatusData.MAX_HP),
                SelfCharacter.CURRENT_HP);
            }
            else
            {
                board.SetData(ConstValue.SetData_Stamina,
                SelfCharacter.GetCharacterStatus.GetStatusData(eStatusData.MAX_STAMINA),
                SelfCharacter.CURRENT_STAMINA);
            }

        }

        ActorManager.Instance.AddActor(this);
    }

    public override void Init()
    {
    }

    protected override void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			// 에니메이션
			ChangeAnimator(eAcotrState.STATE_ATTACK);
			// 콤보
			ComboNext();

			CancelInvoke("CancelCombo");
			Invoke("CancelCombo", 2f);	// 초기화.
		}

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Terrain":
                    {
                        RotationChange(hit.point);
                        break;
                    }
            }
        }

        Move();

        

        //BaseBoard board;
        //board = BoardManager.Instance.GetBoardData(this, eBoardType.BOARD_HP);
        //SelfCharacter.IncreaseCurrentHP(-0.1f);
        //board.SetData(ConstValue.SetData_HP,
        //         SelfCharacter.GetCharacterStatus.GetStatusData(eStatusData.MAX_HP),
        //         SelfCharacter.CURRENT_HP);

        //board = BoardManager.Instance.GetBoardData(this, eBoardType.BOARD_STAMINA);
        //SelfCharacter.IncreaseCurrentStamina(-0.1f);
        //board.SetData(ConstValue.SetData_Stamina,
        //      SelfCharacter.GetCharacterStatus.GetStatusData(eStatusData.MAX_STAMINA),
        //      SelfCharacter.CURRENT_STAMINA);
    }

    private void RotationChange(Vector3 _Point)
    {
        if (Vector3.Distance(transform.position, _Point) < 2f)
            return;

        Vector3 targetPos = (_Point - transform.position).normalized; // 방향벡터를 구했다.

        Quaternion quater = Quaternion.LookRotation(targetPos);
        quater.x = 0;
        quater.z = 0;
        // 현재 방향에서 목표한 방향으로 보간을 이용해 회전한다.
        transform.rotation = Quaternion.Slerp(transform.rotation, quater, rotateSpeed * Time.deltaTime);
    }

    private void Move()
    {

        if (CURRENTSTATE == eAcotrState.STATE_ATTACK)
            return;

        float horizental = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizental != 0 || vertical != 0)
        {
            ChangeAnimator(eAcotrState.STATE_RUN);
            Vector3 movePosition = transform.position;
            movePosition.Set(horizental, 0, vertical); // 수평과 수직을 input과 마춘다.
            //movePosition = movePosition.normalized * Time.deltaTime * moveSpeed;

            transform.Translate(movePosition * Time.deltaTime * moveSpeed); //위치를 변화 시킨다.
        }
        else
            ChangeAnimator(eAcotrState.STATE_IDLE);
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


	public void ChangeAnimator(eAcotrState _State)
	{
        CURRENTSTATE = _State;

        switch(_State)
        {
            case eAcotrState.STATE_ATTACK:
                {
                    ThrowEvent(ConstValue.EventKey_SelectSkill, 0);
                    ANIMATOR.Rebind();
                    ANIMATOR.SetInteger("Attack", ComboCount);
                }
                break;
            case eAcotrState.STATE_IDLE:
                {
                    CancelCombo();
                }
                break;
        }

        ANIMATOR.SetInteger("State", (int)_State);
    }

}
