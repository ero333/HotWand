using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {

	private bool playerDetected;
	public float detectionRadius;
	public Animator animator;
	public Transform target;

	// Use this for initialization
	void Start () {
		playerDetected = false;
	}
	
	// Update is called once per frame
	void Update () {
			//Raycast to see if we have line of sight to the target
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, detectionRadius);
			//Check to see if we hit anything and what it was
			if (hit.transform == target)
			{
				playerDetected = true;
				animator.SetBool("Player Detected", true);
			}
			else
			{
				playerDetected = false;
				animator.SetBool("Player Detected", false);
			}
	}
}
