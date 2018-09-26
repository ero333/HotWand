using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class weaponpickup : StateMachineBehaviour {

	GameObject enemy;
	GameObject target;
	Animator anim;
	float distanceToPickup;
	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		anim = enemy.GetComponent<Animator>();
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<WeaponPickup>().enabled = true;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//Looking for weapon only
		target = enemy.GetComponent<WeaponPickup>().target;
		anim.SetFloat("distance from weapon", Vector2.Distance(enemy.transform.position, target.transform.position));
		
		distanceToPickup = Vector2.Distance(enemy.transform.position, target.transform.position);
		if (distanceToPickup < 0.6f)
		{
			target.SetActive(false);
			enemy.GetComponent<WeaponPickup>().weaponEquipped = target;
			anim.SetBool("has weapon", true);
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<WeaponPickup>().enabled = false;
		if (enemy.GetComponent<WeaponPickup>().weaponEquipped.name == "Crossbow")
		{
			anim.SetBool("Crossbow Equipped", true);
			anim.SetBool("Ranged Mode", true);
		}
		else
		if (enemy.GetComponent<WeaponPickup>().weaponEquipped.name == "Wand")
		{
			anim.SetBool("Wand Equipped", true);
			anim.SetBool("Ranged Mode", true);
		}
		else
		if (enemy.GetComponent<WeaponPickup>().weaponEquipped.name == "Sword")
		{
			anim.SetBool("Sword Equipped", true);
			anim.SetBool("Melee Mode", true);
		}
		else
		if (enemy.GetComponent<WeaponPickup>().weaponEquipped.name == "Axe")
		{
			anim.SetBool("Axe Equipped", true);
			anim.SetBool("Melee Mode", true);
		}
		else
		{
			anim.SetBool("Melee Mode", true);
			anim.SetBool("Ranged Mode", false);
		}
	}
}
