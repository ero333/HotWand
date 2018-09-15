using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedAttack : MonoBehaviour {

	private float shootingDelay;
	public float startShooting;

	public GameObject rangedAnchorPoint;
	public GameObject wandProjectile;
	public GameObject crossbowProjectile;
	private GameObject projectile;

	//private Transform player;

	// Use this for initialization
	void Start () {
		if (GetComponent<WeaponPickup>().weaponEquipped.name == "Wand")
		{
			projectile = wandProjectile;
		}
		else if (GetComponent<WeaponPickup>().weaponEquipped.name == "Crossbow")
		{
			projectile = crossbowProjectile;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (shootingDelay <= 0)
		{
			Instantiate(projectile, rangedAnchorPoint.transform.position, transform.rotation);
			shootingDelay = startShooting;
		}	
		else
		{
			shootingDelay -= Time.deltaTime;
		}
	}
}
