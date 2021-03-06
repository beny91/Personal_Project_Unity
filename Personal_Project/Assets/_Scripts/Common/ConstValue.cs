﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue
{
	// SetData Key
	public const string SetData_Damage = "BOARD_DAMAGE";
	public const string SetData_HP = "BOARD_HP";
    public const string SetData_Stamina = "BOARD_STAMINA";

	// StatusData Key
	public const string CharacterStatusDataKey =
		"CHARACTER_TEMPLATE_STATUS";

	// Path Key
	public const string CharacterTemplatePath = 
		"JSON/CHARACTER_TEMPLATE";
	public const string CharacterTemplateKey =
		"CHARACTER_TEMPLATE";

	public const string SkillTemplatePath =
		"JSON/SKILL_TEMPLATE";
	public const string SkillTemplateKey =
		"SKILL_TEMPLATE";

	public const string SkillDataPath =
		"JSON/SKILL_DATA";
	public const string SkillDataKey =
		"SKILL_DATA";

	public const string StageData_Path =
		"JSON/STAGE_INFO";
	public const string StageData_Key =
		"STAGE_INFO";


	public const string UI_PATH_HP = "Prefabs/UI/BOARD_HP";
    public const string UI_PATH_STAMINA = "Prefabs/UI/BOARD_STAMINA";
	public const string UI_PATH_DAMAGE = "Prefabs/UI/BOARD_DAMAGE";


    // ThrowEvent Key
    public const string EventKey_EnemyInit  = "E_INIT";
	public const string EventKey_Hit        = "ACTOR_HIT";
	public const string EventKey_SelectSkill = "SELECT_SKILL";
	
	public const string ActorData_SetTarget = "SET_TARGET";

	// GetData Key
	public const string ActorData_GetTarget = "GET_TARGET";
	public const string ActorData_Team      = "TEAM_TYPE";
	public const string ActorData_Character = "CHARACTER";
	public const string ActorData_SkillData = "SKILL_DATA";

	// LocalSave Key
	public const string LocalSave_ItemInstance = "ITEM_INSTANCE";

}
