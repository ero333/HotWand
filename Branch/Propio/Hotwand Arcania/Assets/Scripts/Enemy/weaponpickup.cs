using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class weaponpickup : StateMachineBehaviour {

	GameObject enemy;
	GameObject weapon;
	Animator anim;
	float distanceToPickup;
	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		GameObject[] weapons;
		weapons = GameObject.FindGameObjectsWithTag("Weapon");
		GameObject closestWeapon = null;
		float distance = Mathf.Infinity;
		Vector3 position = enemy.transform.position;
		foreach (GameObject item in weapons)
		{
			Vector3 diff = item.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				closestWeapon = item;
				distance = curDistance;
			}
		}

		weapon = closestWeapon;
		anim = enemy.GetComponent<Animator>();
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<WeaponPickup>().enabled = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		anim.SetFloat("distance from weapon", Vector2.Distance(enemy.transform.position, weapon.transform.position));
		
		distanceToPickup = Vector2.Distance(enemy.transform.position, weapon.transform.position);
		if (distanceToPickup < 0.6f)
		{
			weapon.SetActive(false);
			enemy.GetComponent<WeaponPickup>().weaponEquipped = weapon;
			anim.SetBool("has weapon", true);
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<WeaponPickup>().enabled = false;
		if (enemy.GetComponent<WeaponPickup>().weaponEquipped.name == "Crossbow")
		{
			anim.SetBool("Crossbow Equipped", true);
		}
		else
		if (enemy.GetComponent<WeaponPickup>().weaponEquipped.name == "Wand")
		{
			anim.SetBool("Wand Equipped", true);
		}
	}
}
