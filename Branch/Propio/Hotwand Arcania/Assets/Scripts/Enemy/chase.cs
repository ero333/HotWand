using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class chase : StateMachineBehaviour {

	GameObject player;

    void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Chase>().enabled = true;
		animator.GetComponent<AIPath>().enableRotation = false;
		animator.GetComponent<RotateToTarget>().enabled = true;

        
        if ( animator.name.Substring(0,2) == "Elf" )
        {
            animator.GetComponent<AIPath>().maxSpeed = 1.8f;
        };
        if (animator.name.Substring(0, 2) == "Orc")
        {
            animator.GetComponent<AIPath>().maxSpeed = 1.2f;
        };
        if (animator.name.Substring(0, 4) == "Human")
        {
            animator.GetComponent<AIPath>().maxSpeed = 1.5f;
        };
        if (animator.name.Substring(0, 3) == "Mage")
        {
            animator.GetComponent<AIPath>().maxSpeed = 1.2f;
        };




        animator.SetBool("Walking", true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetFloat("distance", Vector2.Distance(animator.transform.position, player.transform.position));
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Chase>().enabled = false;
		animator.GetComponent<AIPath>().enableRotation = true;
		animator.GetComponent<RotateToTarget>().enabled = false;
		animator.SetBool("Walking", false);
	}
}
