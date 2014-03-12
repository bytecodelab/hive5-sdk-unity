using UnityEngine;
using System.Collections;

public class NGUISpriteAnim : MonoBehaviour {
	
	public float fps = 24;
	public string prefix;
	public int start = 1;
	public int end = 7;
	public bool isLoop = false;
	
	float _t = 0f;
	int _c;
	UISprite _sp;
	
	void Start()
	{
		_c = start;
		_t = 0f;
		_sp = GetComponent<UISprite>();
		
		InvokeRepeating("SpriteAnimation", 0f, 1f/fps);
	}
	
	void SpriteAnimation()
	{
		_sp.spriteName = string.Format ("{0}{1}", prefix, _c);
		//_sp.MakePixelPerfect();	
		_c++;
		if(_c > end)
		{
			if(isLoop)
			{
				_c = start;
			} else {
				CancelInvoke("SpriteAnimation");
				gameObject.SetActive(false);
			}
		}
	}
}
