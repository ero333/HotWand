using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectPlayer : MonoBehaviour {

	public GameObject player;
	public bool playerDetected;
	public Animator animator;
	public int fieldOfViewDegrees;
	public int visibilityDistance;
    protected bool CanSeePlayer()
     {
         RaycastHit hit;
         Vector3 rayDirection = player.transform.position - transform.position;
 
         if ((Vector3.Angle(rayDirection, transform.forward)) <= fieldOfViewDegrees * 0.5f)
         {
             // Detect if player is within the field of view
             if (Physics.Raycast(transform.position, rayDirection, out hit, visibilityDistance))
             {
                 return true;
             }
         }
 
         return false;
     }

	 void FixedUpdate()
	 {
		playerDetected = CanSeePlayer();

		if (playerDetected)
		{
			animator.SetBool("Player Detected", true);
		}
		else
		{
			animator.SetBool("Player Detected", false);
		}
	 }
}
