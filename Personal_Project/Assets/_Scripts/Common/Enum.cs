

public enum eAcotrState
{
	STATE_IDLE,
	STATE_MOVE,
	STATE_RUN,
	STATE_ATTACK,
	STATE_GUARD,
	STATE_DODGE,
	STATE_HIT,
	STATE_JUMP,
	STATE_DIE,
}

public enum eAttackCombo
{
	COMBO_1,
	COMBO_2,
	COMBO_3,
	COMBO_MAX
}

public enum eBaseObjectState
{
    STATE_NORMAL,
    STATE_DIE
}

// AI , Animation
public enum eAIStateType
{
    AI_STATE_NONE = 0,
    AI_STATE_IDLE,
    AI_STATE_ATTACK,
    AI_STATE_MOVE,
    AI_STATE_DIE,
}

// Enemy 관련
public enum eRegeneratorType
{
    NONE,
    REGENTIME_EVENT,
    TRIGGER_EVENT,
}

public enum eEnemyType
{
    Monster_1,
    MAX
}

public enum eTeamType
{
    TEAM_1,
    TEAM_2,

}

public enum eAIType
{
    NormalAI,
}

public enum eStatusData
{
    MAX_HP,
    ATTACK,
    DEFFENCE,
    MAX_STAMINA,
    MAX
}

public enum eSkillTemplateType
{
    TARGET_ATTACK,
    RANGE_ATTACK
}

public enum eSkillAttackRangeType
{
    RANGE_BOX,
    RANGE_SPHERE,
}

public enum eSceneType
{
    Scene_None,
    Scene_Logo,
    Scene_Lobby,
    Scene_Game,
}

public enum eUIType
{
    Pf_UI_Logo,
    Pf_UI_Loading,
    Pf_UI_Lobby,
    Pf_UI_Inventory,
    Pf_UI_Popup,
    Pf_UI_Stage,
    Pf_UI_Gacha,
}

public enum eBoardType
{
    BOARD_NONE,
    BOARD_DAMAGE,
    BOARD_HP,
    BOARD_STAMINA,
    BOARD_MAX
}



public enum eClearType
{
    CLEAR_KILLCOUNT,
    CLEAR_TIME,
}

public enum eSlotType
{
    Slot_None = -1,
    Slot_Weapon = 0,
    Slot_Armor,
    Slot_Shield,
    Slot_Acc,
    Slot_Max,
}