﻿using UnityEngine;
using System.Collections;

public class TriggerSpawner : MonoBehaviour {


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			collider.enabled = false;
			GetComponentInParent<Spawner>().TriggerActivated();
		}
	}
}
