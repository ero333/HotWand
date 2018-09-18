using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class chase : StateMachineBehaviour {

	GameObject enemy;
	GameObject player;
	Animator anim;

	void Awake()
	{
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		player = GameObject.FindGameObjectWithTag("Player");
		anim = enemy.GetComponent<Animator>();
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<Chase>().enabled = true;
		enemy.GetComponent<AIPath>().enableRotation = false;
		enemy.GetComponent<RotateToTarget>().enabled = true;
		enemy.GetComponent<AIPath>().maxSpeed = 1.3f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		anim.SetFloat("distance", Vector2.Distance(enemy.transform.position, player.transform.position));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemy.GetComponent<Chase>().enabled = false;
		enemy.GetComponent<AIPath>().enableRotation = true;
		enemy.GetComponent<RotateToTarget>().enabled = false;
	}
}
