using UnityEngine;
using System.Collections;

public class EnemyTrigger : MonoBehaviour {
	
	public float maxHp = 100f;
	public GameObject effect;
	public UISlider slider;
	float hp = 0;
	GameObject _gm;
	
	void Awake()
	{
		_gm = GameObject.FindGameObjectWithTag("GameController");
		hp = maxHp;	
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.CompareTag(tag))
			return;	
		
		MissileTrigger mt = c.GetComponent<MissileTrigger>();
		if(mt == null)
			return;
		
		hp -= mt.damage;
		slider.sliderValue = Mathf.Clamp01(hp / maxHp);
		
		if(hp <= 0f)
		{
			Destroy(transform.parent.parent.gameObject);
			this.AddNGUIChild(effect, transform);
			_gm.SendMessage("EnemyExp");
			_gm.SendMessage("ItemDrop", transform);
		}
	}
	
}
