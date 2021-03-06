﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int HP = 10;

	Animation anim;

	// this is the enemy.
	public bool damagedByBullets = true;

	void Start()
	{
		anim = transform.GetComponent<Animation> ();

	}

	void OnCollisionEnter(Collision c)
	{
		if (c.transform.CompareTag("Projectile"))
		{
			if(c.gameObject.name.Contains("FireBall") && GetComponent<ShooterPlayerController>()) {
				TakeDamage();
				Debug.Log("Player damaged");
			} else if(c.gameObject.name.Contains("Bullet") && !GetComponent<ShooterPlayerController>()) {
				TakeDamage();
				Debug.Log("Enemy damaged");
			}
		}
	}

	public void TakeDamage()
	{
		HP -= 1;
		if (HP == 0)
		{
			if (damagedByBullets)
			{
				anim.Play("die");
				Destroy (gameObject, anim.GetClip("die").length);
				if(GetComponent<SwordsmanSounds>())
				{
					SwordsmanSounds sounds = GetComponent<SwordsmanSounds>();
					sounds.PlaySound(sounds.death);
					GetComponent<SkeleAnimation>().state = SkeleAnimation.SwordState.Die;
					SingletonObject.Get.getGameState().totalEnemiesKilled++;
				} else if(GetComponent<MageSounds>())
				{
					SingletonObject.Get.getGameState().CurEnemies-=1;
					MageSounds sounds = GetComponent<MageSounds>();
					sounds.PlaySound(sounds.death);
					GetComponent<EnemyShoot>().state = EnemyShoot.State.Die;
					SingletonObject.Get.getGameState().totalEnemiesKilled++;
				}
			} else {
				Component.FindObjectOfType<Fader>().FadeToWhite();
			}
		}
	}

}
