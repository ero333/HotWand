﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class weaponpickup : StateMachineBehaviour {
	GameObject target;
	float distanceToPickup;


	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<WeaponPickup>().enabled = true;
		animator.SetBool("Walking", true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//Looking for weapon only
		target = animator.GetComponent<WeaponPickup>().target;
		animator.SetFloat("distance from weapon", Vector2.Distance(animator.transform.position, target.transform.position));
		
		distanceToPickup = Vector2.Distance(animator.transform.position, target.transform.position);
		if (distanceToPickup < 0.6f)
		{
			target.SetActive(false);
			animator.GetComponent<WeaponPickup>().weaponEquipped = target;
			animator.SetBool("has weapon", true);
		}

		if (animator.GetComponent<Rigidbody2D>().velocity.sqrMagnitude != 0)
		{
			animator.SetBool("Walking", true);
		}
		else
		{
			animator.SetBool("Walking", false);
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<WeaponPickup>().enabled = false;
		if (animator.GetComponent<WeaponPickup>().weaponEquipped != null)
		{
			switch (animator.GetComponent<WeaponPickup>().weaponEquipped.tag)
			{
				case "Sword":
					animator.SetBool("Sword Equipped", true);
					animator.SetBool("Melee Mode", true);
				break;

				case "Axe":
					animator.SetBool("Axe Equipped", true);
					animator.SetBool("Melee Mode", true);
				break;

				case "Wand":
					animator.SetBool("Wand Equipped", true);
					animator.SetBool("Ranged Mode", true);
				break;

				case "Crossbow":
					animator.SetBool("Crossbow Equipped", true);
					animator.SetBool("Ranged Mode", true);
				break;

				default: 
					animator.SetBool("Melee Mode", true);
					animator.SetBool("Ranged Mode", false);
				break;
			}
		}
		else
		{
			//PUNCH
			animator.SetBool("Melee Mode", true);
			animator.SetBool("Ranged Mode", false);
		}

		animator.SetBool("Walking", false);
	}
	
}
