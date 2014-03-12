using UnityEngine;
using System.Collections;

public class ItemTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c)
	{
		if(c.CompareTag(tag) && c.name == "Player")
		{
			c.SendMessage("PowerUp");
			DestroyObject(gameObject);
		}
	}
}
