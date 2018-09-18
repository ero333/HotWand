using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class RangedAttack : MonoBehaviour {
	private float startShooting;
	public float shootingDelay;

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

		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
