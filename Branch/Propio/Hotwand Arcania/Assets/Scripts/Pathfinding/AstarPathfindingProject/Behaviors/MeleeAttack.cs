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
					if (gameObject.GetComponent<WeaponPickup>().weaponEquipped.tag == "Sword")
					{
						animator.SetTrigger("Sword");
						if (target != null) target.SendMessage("TakeDamage", damage * 1.5, SendMessageOptions.DontRequireReceiver);
					}
				}
				else
				{
					animator.SetTrigger("Punch");
					if (target != null) target.SendMessage("TakeDamage", damage * 1.5, SendMessageOptions.DontRequireReceiver);
				}
				//Record the time we attacked
				lastAttackTime = Time.time;
			}
		}
	}
}
