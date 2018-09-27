﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeleeHitboxEnemy : MonoBehaviour {
	private Vector2 target;
	public int damage;    

	// time for killing the projectile if it lasts for too long
	public float deathTimer = 1.0f;

	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;
		if(deathTimer<=0)
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Destroy(this.gameObject);
			other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}