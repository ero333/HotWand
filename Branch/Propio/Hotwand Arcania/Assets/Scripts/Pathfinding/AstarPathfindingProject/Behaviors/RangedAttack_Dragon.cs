using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class RangedAttack_Dragon : MonoBehaviour {

	public int damage;
	private float lastAttackTime;

	public GameObject rangedAnchorPoint;
	public GameObject fireBreathProjectile;
	public GameObject crossbowProjectile;
	GameObject projectile;
	private Transform player;
	public string creator_name;
	public string arma;

	private float speed = 50f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;	
		projectile = fireBreathProjectile;		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetDir = player.position - transform.position;
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

		if (Time.time > lastAttackTime + 2)
		{
				GameObject newProjectile = Instantiate(projectile, rangedAnchorPoint.transform.position, rangedAnchorPoint.transform.rotation);
				newProjectile.GetComponent<EnemyProjectile>().arma = "Dragonbreath";
				newProjectile.GetComponent<EnemyProjectile>().creator_name = gameObject.name;
				lastAttackTime = Time.time;
		}
	}
}
