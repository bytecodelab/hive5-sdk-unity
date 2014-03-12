using UnityEngine;
using System.Collections;

public class MissileTrigger : MonoBehaviour {
	
	public float damage = 2f;
	public bool isTriggered = false;
	
	void OnEnable()
	{
		isTriggered = false;	
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.CompareTag(tag))
		return;	
		
		if(isTriggered)
			return;

		//Destroy (gameObject);
		//Debug.Log ("returnPool");
		isTriggered = true;
		PoolManager.Instance.ReturnPoolObject(transform);
	}
	
}
