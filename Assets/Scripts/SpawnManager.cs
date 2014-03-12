using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
	
	public GameObject enemy;
	public Transform[] pos;
	public float rate = 4f;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("Spawn", 0f, rate);
	}
	
	void Spawn()
	{
		int index = Random.Range (0, pos.Length);
		this.AddNGUIChild(enemy, pos[index]);
	}
}
