using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class chase_Dragon : StateMachineBehaviour {

	GameObject player;
	float chance;
    void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Chase>().enabled = true;
		animator.GetComponent<AIPath>().enableRotation = false;
		animator.GetComponent<RotateToTarget>().enabled = true;
		animator.GetComponent<AIPath>().maxSpeed = 1.0f;
        animator.SetBool("Walking", true);

		chance = Random.Range(0.0f, 1.0f);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetFloat("distance", Vector2.Distance(animator.transform.position, player.transform.position));
		if (chance > 0.5f)
		{
			animator.SetBool("Ranged Mode", true);
			animator.SetBool("Melee Mode", false);
		}
		else
		if (chance < 0.5f)
		{
			animator.SetBool("Melee Mode", true);
			animator.SetBool("Ranged Mode", false);
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Chase>().enabled = false;
		animator.GetComponent<AIPath>().enableRotation = true;
		animator.GetComponent<RotateToTarget>().enabled = false;
		animator.SetBool("Walking", false);
	}
}
