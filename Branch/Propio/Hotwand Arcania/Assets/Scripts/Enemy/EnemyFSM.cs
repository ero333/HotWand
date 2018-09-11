using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFSM : MonoBehaviour {

	public Idle idleState;
	public Patrol patrolState;
	public Wander wanderState;
	public AIDestinationSetter chaseState;
	public WeaponPickup weaponpickupState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
