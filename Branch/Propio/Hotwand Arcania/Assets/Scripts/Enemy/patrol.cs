using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class patrol : StateMachineBehaviour {
	GameObject player;
	Transform target;

	//For looking for weapon state only
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Patrol>().enabled = true;
		animator.GetComponent<AIPath>().maxSpeed = 0.6f;
		animator.SetBool("Walking", true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetFloat("distance", Vector2.Distance(animator.transform.position, player.transform.position));
		
		//Looking for weapon only
		float distanceToClosestWeapon = Mathf.Infinity;
		GameObject closestWeapon = null;
		GameObject[] allWeapons = GameObject.FindGameObjectsWithTag("Weapon");

		foreach (GameObject currentWeapon in allWeapons) {
			float distanceToEnemy = (currentWeapon.transform.position - animator.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestWeapon) {
				distanceToClosestWeapon = distanceToEnemy;
				closestWeapon = currentWeapon;
			}
		}

		if (closestWeapon != null)
		{
			target = closestWeapon.transform;
		}
		animator.SetFloat("distance from weapon", Vector2.Distance(animator.transform.position, target.position));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Patrol>().enabled = false;
		animator.SetBool("Walking", false);
	}
}
