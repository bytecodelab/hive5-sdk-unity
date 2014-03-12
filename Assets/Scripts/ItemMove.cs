using UnityEngine;
using System.Collections;

public class ItemMove : MonoBehaviour {
	
	public float jumpSpeed = 60f;
	public float fallSpeed = 80f;
	
	float _yMove;
	
	// Use this for initialization
	void Start () {
		_yMove = jumpSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
		_yMove -= fallSpeed * Time.deltaTime;
		transform.localPosition += new Vector3(0f, _yMove, 0f) * Time.deltaTime;

	}
}
