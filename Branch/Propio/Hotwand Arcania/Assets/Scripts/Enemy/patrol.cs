using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class patrol : StateMachineBehaviour {

	GameObject enemy;
	GameObject player;
	GameObject weapon;
	Animator anim;

	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player");
		weapon = GameObject.FindGameObjectWithTag("Weapon");
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
		anim.SetFloat("distance from weapon", Vector2.Distance(enemy.transform.position, weapon.transform.position));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<Patrol>().enabled = false;
	}
}
