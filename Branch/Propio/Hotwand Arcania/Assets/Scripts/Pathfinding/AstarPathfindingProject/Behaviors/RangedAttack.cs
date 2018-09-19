using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class RangedAttack : MonoBehaviour {

	public int damage;
	private float lastAttackTime;
	public float attackDelay;

	public GameObject rangedAnchorPoint;
	public GameObject wandProjectile;
	public GameObject crossbowProjectile;
	GameObject projectile;
	public Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;	
		switch (GetComponent<WeaponPickup>().weaponEquipped.name)
		{
			case "Wand": projectile = wandProjectile;
			break;

			case "Crossbow": projectile = crossbowProjectile;
			break;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastAttackTime + attackDelay){
			//Raycast to see if we have line of sight to the target
			//RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, attackRange);
			//Check to see if we hit anything and what it was
			//if (hit.transform == player)
			//{
				GameObject newProjectile = Instantiate(projectile, rangedAnchorPoint.transform.position, rangedAnchorPoint.transform.rotation);
				lastAttackTime = Time.time;
			//}
		}
	}
}
