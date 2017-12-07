using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject
{

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

    public eTeamType TEAMTYPE
    {
        get { return TeamType; }
    }

    bool _IsPlayer = false;
    public bool IsPlayer
    {
        get { return _IsPlayer; }
        set { _IsPlayer = value; }
    }

    [SerializeField]
    eTeamType _TeamType;
    public eTeamType TeamType
    {
        get { return _TeamType; }
    }

    [SerializeField]
    string TemplateKey = string.Empty;

    public string TEMPLATEKEY
    { get { return TemplateKey; } }

    GameCharacter _SelfCharacter = null;
    public GameCharacter SelfCharacter
    {
        set { _SelfCharacter = value; }
        get { return _SelfCharacter; }
    }

    // AI
    [SerializeField]
    eAIType _AIType;
    public eAIType AIType
    {
        get { return _AIType; }
    }

    BaseAI _AI = null;
    public BaseAI AI
    { get { return _AI; } }


    [SerializeField]
    bool bEnableBoard = true;

    public bool ENABLEBOARD
    {
        set { bEnableBoard = value; }
        get { return bEnableBoard; }
    }

    BaseObject HitTarget;


    public virtual void Init()
    {
        switch (AIType)
        {
            case eAIType.NormalAI:
                {
                    GameObject go =
                        new GameObject(
                            AIType.ToString(),
                            typeof(NormalAI)
                        );

                    go.transform.SetParent(SelfTransform);
                    _AI = go.GetComponent<NormalAI>();

                }
                break;
        }

        AI.TargetComponent = this;

        GameCharacter character =
            CharacterManager.Instance.AddCharacter(TemplateKey);
        character.TargetComponenet = this;
        _SelfCharacter = character;

        if (bEnableBoard)
        {
            BaseBoard board = BoardManager.Instance.AddBoard(
                this, eBoardType.BOARD_HP);

            board.SetData(ConstValue.SetData_HP,
                GetStatusData(eStatusData.MAX_HP),
                _SelfCharacter.CURRENT_HP);
        }

        ActorManager.Instance.AddActor(this);
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        AI.UpdateAI();
        if (AI.END)
            Destroy(SelfObject);
    }

    public void RunSkill()
    {
        SkillData selectSkill = SelfCharacter.SELECT_SKILL;

        if (selectSkill == null)
            return;

        for (int i = 0; i < selectSkill.SKILL_LIST.Count; i++)
        {
            SkillManager.Instance.RunSkill(this,
                                selectSkill.SKILL_LIST[i]);
        }

        SelfCharacter.SELECT_SKILL = null;


        //GameCharacter gc = TargetComponent.GetData(
        //		ConstValue.ActorData_Character)
        //		as GameCharacter;

        //Debug.Log(this.gameObject.name + "가 "
        //	+ HitTarget.name + "를 "
        //	+ SelfCharacter.GetCharacterStatus.GetStatusData
        //	(eStatusData.ATTACK) +
        //	" 공격력으로 때림.");

        //HitTarget.ThrowEvent(ConstValue.EventKey_Hit);
    }

    public override object GetData(string keyData, params object[] datas)
    {
        switch (keyData)
        {
            case ConstValue.ActorData_Team:
                {
                    return TeamType;
                }
            case ConstValue.ActorData_Character:
                {
                    return SelfCharacter;
                }
            case ConstValue.ActorData_GetTarget:
                {
                    return HitTarget;
                }
            case ConstValue.ActorData_SkillData:
                {
                    int index = (int)datas[0];
                    return SelfCharacter.GetSkillDataByIndex(index);
                }
            default:
                return base.GetData(keyData, datas);

        }

    }

    public override void ThrowEvent(string keyData, params object[] datas)
    {
        switch (keyData)
        {
            case ConstValue.ActorData_SetTarget:
                {
                    HitTarget = datas[0] as BaseObject;
                }
                break;
            case ConstValue.EventKey_Hit:
                {
                    if (ObjectState == eBaseObjectState.STATE_DIE)
                        return;

                    GameCharacter casterCharacter = datas[0] as GameCharacter;
                    SkillTemplate skilltemplate = datas[1] as SkillTemplate;

                    casterCharacter.GetCharacterStatus.AddStatusData(
                        "SKILL", skilltemplate.STATUS_DATA);

                    double attackDamage =
                        casterCharacter.
                        GetCharacterStatus.
                        GetStatusData(eStatusData.ATTACK);

                    casterCharacter.GetCharacterStatus.RemoveStatusData(
                        "SKILL");

                    SelfCharacter.IncreaseCurrentHP(-attackDamage);

                    Debug.Log(SelfObject.name + "가 데미지 " + 
                    	attackDamage +" 피해를 입었습니다. ");

                    // DamageBoard
                    //BaseBoard board = BoardManager.Instance.AddBoard(this, eBoardType.BOARD_DAMAGE);
                    //if (board != null)
                    //    board.SetData(ConstValue.SetData_Damage, attackDamage);

                    // HpBoard
                    BaseBoard board;
                    board = BoardManager.Instance.GetBoardData(
                        this, eBoardType.BOARD_HP);
                    if (board != null)
                    {
                        board.SetData(ConstValue.SetData_HP,
                                GetStatusData(eStatusData.MAX_HP),
                                SelfCharacter.CURRENT_HP);
                    }


                    // 죽었나 안죽었나
                    if (ObjectState == eBaseObjectState.STATE_DIE)
                    {
                        Debug.Log(gameObject.name + "죽음!");
                        GameManager.Instance.KillCheck(this);
                    }

                    //AI.ANIMATOR.SetInteger("Hit", 1);
                }
                break;

            case ConstValue.EventKey_SelectSkill:
                {
                    int index = (int)datas[0];
                    if (SelfCharacter.EquipSkillByIndex(index) == false)
                    {
                        Debug.LogError(this.gameObject + " 의" +
                            "Skill Index : " + index + "스킬 구동 실패");
                    }
                }
                break;


            default:
                base.ThrowEvent(keyData, datas);
                break;
        }
    }


    public double GetStatusData(eStatusData statusData)
    {
        return SelfCharacter.
            GetCharacterStatus.GetStatusData(statusData);
    }


    private void OnEnable()
    {
        if (BoardManager.Instance != null)
            BoardManager.Instance.ShowBoard(this, true);
    }

    private void OnDisable()
    {
        if (BoardManager.Instance != null)
            if (GameManager.Instance.GAME_OVER == false)
                BoardManager.Instance.ShowBoard(this, false);
    }

    public virtual void OnDestroy()
    {
        if (BoardManager.Instance != null)
            BoardManager.Instance.ClearBoard(this);

        // ActorManager RemoveActor
        if (ActorManager.Instance != null)
            ActorManager.Instance.RemoveActor(this);
    }
}
