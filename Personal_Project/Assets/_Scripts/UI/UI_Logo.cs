﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Logo : BaseObject 
{
	UIButton StartBtn = null;

	void Start () 
	{
		Transform temp = this.GetChild("StartButton");
		if(temp == null)
		{
			Debug.Log("Logo 에 StartBtn이 없습니다.");
			return;
		}

		StartBtn = temp.GetComponent<UIButton>();

		//StartBtn.onClick.Add(new EventDelegate(this, "GoLobby"));
		//EventDelegate.Add(StartBtn.onClick, new EventDelegate(this, "GoLobby"));

		EventDelegate.Add(StartBtn.onClick,
			// Lamda
			() =>
			{
				Scene_Manager.Instance.LoadScene(
					eSceneType.Scene_Game);
			}
			
			);

	}
}
