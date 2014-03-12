using UnityEngine;
using System.Collections;

public class BgScroller : MonoBehaviour {
	
	public Transform current;
	public Transform buffer;
	public float bgHeight = 800f;
	public float moveSpeed = 200f;
	
	// Use this for initialization
	void Start () {
		current.localPosition = new Vector3(0f, 0f, 50f);
		buffer.localPosition = new Vector3(0f, bgHeight, 50f);
	}
	
	// Update is called once per frame
	void Update () {
		current.localPosition += 
			new Vector3(0f, -moveSpeed, 0f) * Time.deltaTime;
		buffer.localPosition += 
			new Vector3(0f, -moveSpeed, 0f) * Time.deltaTime;
		
		if(current.localPosition.y <= -bgHeight)
		{
			current.localPosition = new Vector3(0f, bgHeight, 50f);
			Transform temp = current;
			current = buffer;
			buffer = temp;
		}
	}
}
