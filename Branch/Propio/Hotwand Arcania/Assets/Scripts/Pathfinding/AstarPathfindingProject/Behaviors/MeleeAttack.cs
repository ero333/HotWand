using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeAttack : MonoBehaviour {
	private Transform target;
	public float attackRange;
	public int damage;
	private float lastAttackTime;
	public float attackDelay;

	public Animator animator;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Attacking AI

		//Check the distance between enemy and player to see if the player is close enough to attack
		float distanceToPlayer = Vector3.Distance(transform.position, target.position);
		if (distanceToPlayer < attackRange){
			//Check to see if enough time has passed since we last attacked
			if (Time.time > lastAttackTime + attackDelay){
				if (gameObject.GetComponent<WeaponPickup>().weaponEquipped.name == "Sword")
				{
					animator.SetTrigger("Sword");
					target.SendMessage("TakeDamage", damage*1.5);
				}
				else
				{
					animator.SetTrigger("Punch");
					target.SendMessage("TakeDamage", damage*1.5);
				}
				//Record the time we attacked
				lastAttackTime = Time.time;
			}
		}
	}
}
