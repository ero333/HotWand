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
        if (animator.name.Substring(0, 2) == "Elf")
        {
            animator.GetComponent<AIPath>().maxSpeed = 1.0f;
        }
        if (animator.name.Substring(0, 2) == "Orc")
        {
            animator.GetComponent<AIPath>().maxSpeed = 0.6f;
        }
        if (animator.name.Substring(0, 4) == "Human")
        {
            animator.GetComponent<AIPath>().maxSpeed = 0.8f;
        }
        animator.SetBool("Walking", true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetFloat("distance", Vector2.Distance(animator.transform.position, player.transform.position));

        if (animator.name.Substring(0, 2) == "Elf")
        {
            animator.GetComponent<AIPath>().maxSpeed = 1.0f;
        }
        if (animator.name.Substring(0, 2) == "Orc")
        {
            animator.GetComponent<AIPath>().maxSpeed = 0.6f;
        }
        if (animator.name.Substring(0, 4) == "Human")
        {
            animator.GetComponent<AIPath>().maxSpeed = 0.8f;
        }
        //Looking for weapon only
        float distanceToClosestWeapon = Mathf.Infinity;
		GameObject closestWeapon = null;
		GameObject[] allWeapons = GameObject.FindGameObjectsWithTag("Weapon");

		if (allWeapons != null)
		{
			animator.SetBool("weapon exists", true);
			foreach (GameObject currentWeapon in allWeapons) {
				float distanceToEnemy = (currentWeapon.transform.position - animator.transform.position).sqrMagnitude;
				if (distanceToEnemy < distanceToClosestWeapon) {
					distanceToClosestWeapon = distanceToEnemy;
					closestWeapon = currentWeapon;
					break;
				}
			}

			if (closestWeapon != null)
			{
				target = closestWeapon.transform;
			}
			if (target != null ) animator.SetFloat("distance from weapon", Vector2.Distance(animator.transform.position, target.position));
		}
		else
		{
			animator.SetBool("weapon exists", false);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<Patrol>().enabled = false;
		animator.SetBool("Walking", false);
	}
}
