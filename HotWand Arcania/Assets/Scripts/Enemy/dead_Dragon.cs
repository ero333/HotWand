using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class dead_Dragon : StateMachineBehaviour {


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		animator.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
		animator.GetComponent<AIPath>().maxSpeed = 0.0f;
		animator.GetComponent<AIPath>().enabled = false;
		animator.GetComponent<BoxCollider2D>().enabled = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
