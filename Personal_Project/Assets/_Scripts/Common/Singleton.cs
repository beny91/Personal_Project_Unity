using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T instance = null;

	public static T INSTANCE
	{
		get
		{
			if (instance == null)
			{
				GameObject go = new GameObject(typeof(T).ToString(),typeof(T));
				instance = go.GetComponent<T>();
				instance.Init();
			}
			else
				return instance;

			return instance;
		}
	}

	public virtual void Init()
	{ 
		DontDestroyOnLoad(instance);
	}

}
