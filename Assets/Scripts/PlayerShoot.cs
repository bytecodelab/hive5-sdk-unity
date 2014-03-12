using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public float duration = 0.1f;
	public GameObject missile;
	public Transform[] firePos;

	public int missileNum = 1;
	public float powerTime = 5f;
	
	private GameObject _gm;
	bool _isPowerUp = false;
	

	
	void Start () {
		_gm = GameObject.FindGameObjectWithTag("GameController");
		InvokeRepeating("Shoot", 0f, duration);
		missileNum = 1;
	}
	
	void Shoot() {
		for(int i =0; i < missileNum; i++)
		{
			//this.AddNGUIChild(missile, firePos[i]);
			PoolManager.Instance.GetPoolObject(firePos[i]);

		}
				_gm.SendMessage("MissileSound");
	}
	
	void PowerUp()
	{
		missileNum = 3;
		if(_isPowerUp)
		{
			CancelInvoke("PowerDown");	
		}
		Invoke ("PowerDown", powerTime);
		_isPowerUp = true;
	}
	
	void PowerDown()
	{
		_isPowerUp = false;
		missileNum = 1;
		
	}
	
}
