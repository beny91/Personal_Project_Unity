using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
	BaseObject TargetObject = null;

	public BaseObject TargetComponent
	{
		get { return TargetObject; }
		set { TargetObject = value; }
	}


	public Transform SelfTransform
	{
		get
		{
			if (TargetComponent == null)
				return this.transform;
			else
				return TargetComponent.transform;
		}
	}

	virtual public object GetData(string keyData, params object[] datas)
	{
		return null;
	}

	virtual public void ThrowEvent(string keyData, params object[] datas)
	{
	
	}

	public Transform GetChild(string strName)
	{
		// this.GetChild(string); -> BaseObject
		// transform.GetChild(int);

		return _GetChild(strName, SelfTransform);
	}

	private Transform _GetChild(string strName, Transform trans)
	{
		if (trans.name == strName)
			return trans;

		for(int i = 0; i < trans.childCount; i++)
		{
			Transform returnTrans = _GetChild(strName, trans.GetChild(i));
			if (returnTrans != null)
				return returnTrans;
		}

		return null;
	}

	
}
