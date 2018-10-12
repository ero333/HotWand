﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeAttack : MonoBehaviour {
	private Transform target;
	public float attackRange;
	private float lastAttackTime;
	public float attackDelay;

	[SerializeField]	private GameObject meleeHitbox;	
	[SerializeField]	private GameObject meleeAnchorPoint;

	private Animator animator;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Attacking AI

		//Check the distance between enemy and player to see if the player is close enough to attack
		float distanceToPlayer = Vector3.Distance(transform.position, target.position);
		if (distanceToPlayer < attackRange){
			//Check to see if enough time has passed since we last attacked
			if (Time.time > lastAttackTime + attackDelay){
				if (gameObject.GetComponent<WeaponPickup>().weaponEquipped != null)
				{
					GameObject swordAttack;
					GameObject axeAttack;

					switch (gameObject.GetComponent<WeaponPickup>().weaponEquipped.GetComponent<Weapon>().weaponName)
					{
						case "Sword":
							swordAttack = Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
							if (swordAttack != null) swordAttack.GetComponent<MeleeHitboxEnemy>().damage = 2;
							attackDelay = attackDelay * 1f;
							animator.SetTrigger("Sword");
						break;

						case "Axe":
							axeAttack = Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
							if (axeAttack != null) axeAttack.GetComponent<MeleeHitboxEnemy>().damage = 3;
							attackDelay = attackDelay * 1.25f;
							animator.SetTrigger("Axe");
						break;
					}
				}
				else
				{
					animator.SetTrigger("Punch");
					Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
				}
				
				//Record the time we attacked
				lastAttackTime = Time.time;
			}
		}
	}
}
