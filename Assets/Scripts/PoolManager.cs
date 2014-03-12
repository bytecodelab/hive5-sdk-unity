using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PoolObject
{
	public GameObject poolObject;
	public List<Transform> activeList;
	public List<Transform> inActiveList;	
}

public class PoolManager : MonoBehaviour {
	
	public static PoolManager Instance;
	public PoolObject[] pool;
	public GameObject poolObject;
	public List<Transform> activeList;
	public List<Transform> inActiveList;
	public int count = 200;
	
	public Transform inactiveTransform;
	public Transform activeTransform;
	
	void Awake() {
		
		if(Instance == null)
		{
			Instance = this;	
		}
		
		activeList = new List<Transform>();
		inActiveList = new List<Transform>();
		
		for(int i =0; i < count; i++)
		{
			Transform t = this.AddNGUIChild(poolObject, transform);
			t.parent = inactiveTransform;
			inActiveList.Add(t);
		}
	}

	public Transform GetPoolObject( Transform target)
	{
		if(inActiveList.Count <= 0)
		{
			//Debug.LogError("InactiveList Count 0 : " + activeList.Count);
			Transform newT = this.AddNGUIChild(poolObject, transform);
			newT.parent = inactiveTransform;
			inActiveList.Add(newT);
		}
		
		Transform t = inActiveList[0];
		t.parent = activeTransform;
		t.position = target.position;
		inActiveList.RemoveAt(0);
		activeList.Add(t);
		return t;
		
	}
	
	public void ReturnPoolObject( Transform t )
	{
		if(!activeList.Contains(t) )
		{
			return;
		}
		
		t.parent = inactiveTransform;
		activeList.Remove(t);
		inActiveList.Add(t);
		
	}
	

}
