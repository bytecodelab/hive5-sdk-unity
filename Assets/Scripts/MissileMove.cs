using UnityEngine;
using System.Collections;



public class MissileMove : MonoBehaviour {
	
	public float moveSpeed = 100f;
	public float timeToDie = 5f;
	
	// Use this for initialization
	void Start () {
		//Destroy(gameObject, timeToDie);
		Invoke("Die", timeToDie);
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.localPosition += transform.up * moveSpeed * Time.deltaTime;
	}
	
	void OnEnable()
	{
		Invoke("Die", timeToDie);	
	}
	
	void OnDisable()
	{
		CancelInvoke("Die");	
	}
	
	void Die()
	{
		PoolManager.Instance.ReturnPoolObject(transform);	
	}
}
