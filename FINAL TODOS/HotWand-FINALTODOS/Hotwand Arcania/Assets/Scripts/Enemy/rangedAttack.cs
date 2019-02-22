using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class rangedAttack : StateMachineBehaviour {
	GameObject player;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<RangedAttack>().enabled = true;
		animator.GetComponent<AIPath>().maxSpeed = 0f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetFloat("distance", Vector2.Distance(animator.transform.position, player.transform.position));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<RangedAttack>().enabled = false;
		animator.GetComponent<AIPath>().maxSpeed = 1.3f;
	}
}