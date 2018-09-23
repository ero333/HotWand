using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour {
	// Use this for initialization
    public bool playerInRange; // is the player within the enemy's sight range collider (this only checks if the enemy can theoretically see the player if nothing is in the way)
	//public float detectionRange;
    GameObject player; // a reference to the player for raycasting
    void Start()
    {
        playerInRange = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

  
    void FixedUpdate()
    {	
		var direction = transform.position - player.transform.position;
		var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
		RaycastHit2D enemySight = Physics2D.Linecast(transform.position, player.transform.position);
		
		if (enemySight.collider != null)
		{
			if (enemySight.collider.name == "Player")
			{
				playerInRange = true;
				Debug.DrawRay(transform.position, -direction, Color.green);
			}
			else
			{
				playerInRange = false;
				Debug.DrawRay(transform.position, -direction, Color.red);
			}
		}
		if (enemySight.collider != null)
		{
			print(enemySight.collider);
		}
	}
}
