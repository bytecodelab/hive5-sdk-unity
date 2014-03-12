using UnityEngine;
using System.Collections;

public class MoveScene : MonoBehaviour {
	
	public int targetScene;
	
	void OnClick()
	{
		Application.LoadLevel(targetScene);
	}
}
