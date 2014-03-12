using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioClip missile;
	public AudioClip enemyExp;
	
	void MissileSound()
	{
		audio.volume = 0f;
		audio.PlayOneShot(missile);	
	}
	
	void EnemyExp()
	{
		audio.volume = 1f;
		audio.PlayOneShot(enemyExp);	
	}
	
}
