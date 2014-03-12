using UnityEngine;
using System.Collections;

public static class NGUIHelper {
	
	public static Transform AddNGUIChild(this MonoBehaviour b, GameObject prefab, Transform target)
	{
		GameObject p = NGUITools.FindInParents<UIPanel>(b.gameObject).gameObject;
		Transform t = NGUITools.AddChild(p, prefab).transform;
		t.localScale = prefab.transform.localScale;
		t.position = target.position;
		t.rotation = target.rotation;
		
		return t;
	}

}
