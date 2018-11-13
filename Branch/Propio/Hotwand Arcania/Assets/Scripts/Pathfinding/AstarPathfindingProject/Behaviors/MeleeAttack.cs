using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeAttack : MonoBehaviour {
	private Transform target;
	public float attackRange;
	private float lastAttackTime;
	public float attackDelay;


	[SerializeField]	private GameObject meleeHitbox;	
	[SerializeField]	private GameObject meleeAnchorPoint;

	private Animator animator;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //Attacking AI
        GameObject attack;
        //Check the distance between enemy and player to see if the player is close enough to attack
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
		if (distanceToPlayer < attackRange){
			//Check to see if enough time has passed since we last attacked
			if (Time.time > lastAttackTime + attackDelay){
				if (gameObject.GetComponent<WeaponPickup>().weaponEquipped != null)
				{
                    attack = Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
                    attack.GetComponent<MeleeHitboxEnemy>().arma = gameObject.GetComponent<WeaponPickup>().weaponEquipped.GetComponent<Weapon>().weaponName;
                    switch (gameObject.GetComponent<WeaponPickup>().weaponEquipped.GetComponent<Weapon>().weaponName)
					{
						case "Sword":
							if (attack != null){
                                attack.GetComponent<MeleeHitboxEnemy>().damage = 2;
                                attack.GetComponent<MeleeHitboxEnemy>().creator_name = gameObject.name;
                            }
                            attackDelay = attackDelay * 1f;
							animator.SetTrigger("Sword");
						break;

						case "Axe":
							if (attack != null) {
                                attack.GetComponent<MeleeHitboxEnemy>().damage = 3;
                                attack.GetComponent<MeleeHitboxEnemy>().creator_name = gameObject.name;
 
                            }

                            print(  "dano hacha" + attack.GetComponent<MeleeHitboxEnemy>().damage);
                            print( "nombre hacha" + attack.GetComponent<MeleeHitboxEnemy>().creator_name);
							attackDelay = attackDelay * 1.25f;
							animator.SetTrigger("Axe");
    				    break;

                        default:
                            break;


					}
				}
				else
				{
                    animator.SetTrigger("Punch");
					attack = Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
                    attack.GetComponent<MeleeHitboxEnemy>().arma = "Punch";
                    if (attack != null)
                    {
                        attack.GetComponent<MeleeHitboxEnemy>().damage = 1;
                        attack.GetComponent<MeleeHitboxEnemy>().creator_name = gameObject.name;
 
                    }
                }

                //Record the time we attacked
                lastAttackTime = Time.time;
			}
		}
	}
}
