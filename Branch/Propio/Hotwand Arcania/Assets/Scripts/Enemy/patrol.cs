using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class patrol : StateMachineBehaviour {

	GameObject enemy;
	GameObject player;
	Transform target;
	Animator anim;

	//For looking for weapon state only
	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player");
		anim = enemy.GetComponent<Animator>();
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<Patrol>().enabled = true;
		enemy.GetComponent<AIPath>().maxSpeed = 0.6f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		anim.SetFloat("distance", Vector2.Distance(enemy.transform.position, player.transform.position));

		//Looking for weapon only
		float distanceToClosestWeapon = Mathf.Infinity;
		GameObject closestWeapon = null;
		GameObject[] allWeapons = GameObject.FindGameObjectsWithTag("Weapon");

		foreach (GameObject currentWeapon in allWeapons) {
			float distanceToEnemy = (currentWeapon.transform.position - enemy.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestWeapon) {
				distanceToClosestWeapon = distanceToEnemy;
				closestWeapon = currentWeapon;
			}
		}
	
		target = closestWeapon.transform;
		anim.SetFloat("distance from weapon", Vector2.Distance(enemy.transform.position, target.position));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<Patrol>().enabled = false;
	}
}
