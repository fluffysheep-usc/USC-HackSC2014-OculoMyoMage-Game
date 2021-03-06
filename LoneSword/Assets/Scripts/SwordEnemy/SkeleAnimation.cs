﻿using UnityEngine;
using System.Collections;

public class SkeleAnimation : MonoBehaviour {

	bool nearby;
	Transform player;

	NavMeshAgent agent;

	public enum SwordState{
		Move,
		Attack,
		Die
	}

	public SwordState state = SwordState.Move;

	void Start()
	{
		player = GameObject.Find ("OVRPlayerController").transform;
		agent = transform.GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void Update () {
		if(state == SwordState.Die) {
			return;
		}
		Vector3 direction = player.position - transform.position;

		if (Vector3.SqrMagnitude(direction) < 9)
		{
			agent.Stop();
			transform.rotation = Quaternion.LookRotation(Vector3.Normalize(new Vector3(player.position.x - transform.position.x,
			                                                                           transform.position.y, player.position.z - transform.position.z)));
			Animation anim = transform.GetComponent<Animation>();
			if (!anim.IsPlaying("attack") && !anim.IsPlaying("die")){
				player.GetComponent<Health>().TakeDamage();
				anim.Play("attack");
				SwordsmanSounds sounds = GetComponent<SwordsmanSounds>();
				sounds.PlaySound(sounds.swordClink);
			}

		} else {
			transform.rotation = Quaternion.LookRotation(Vector3.Normalize(new Vector3(player.position.x - transform.position.x,
			                                                                           transform.position.y, player.position.z - transform.position.z)));
			agent.SetDestination(new Vector3(player.position.x, transform.position.y, player.position.z));
			transform.GetComponent<Animation>().Play ("run");
			SwordsmanSounds sounds = GetComponent<SwordsmanSounds>();
			sounds.PlaySound(sounds.cackle);
		}
	}

}
