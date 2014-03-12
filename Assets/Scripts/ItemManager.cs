using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
	
	[Range(0f, 1f)]
	public float dropRate = 0.1f;
	public GameObject item;
	
	void ItemDrop(Transform pos)
	{
		if(Random.Range(0f, 1f) <= dropRate)
		{
			this.AddNGUIChild(item, pos);
		}
	}
	
}
