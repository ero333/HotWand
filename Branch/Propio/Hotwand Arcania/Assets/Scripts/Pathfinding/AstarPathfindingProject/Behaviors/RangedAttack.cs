using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class RangedAttack : MonoBehaviour {
	private float startShooting;
	public float shootingDelay;

	public GameObject rangedAnchorPoint;
	public GameObject wandProjectile;
	public GameObject crossbowProjectile;
	GameObject projectile;
	public Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;	

		if (gameObject.GetComponent<WeaponPickup>().weaponEquipped.name == "Wand")
		{
			projectile = wandProjectile;
		}
		else
		if (gameObject.GetComponent<WeaponPickup>().weaponEquipped.name == "Crossbow")
		{
			projectile = crossbowProjectile;
		}

		shootingDelay = startShooting;
	}
	
	// Update is called once per frame
	void Update () {
		if (shootingDelay <= 0)
		{
			Instantiate(projectile, rangedAnchorPoint.transform.position, Quaternion.identity);
			shootingDelay = startShooting;
		}
		else
		{
			shootingDelay -= Time.deltaTime;
		}
	}
}
