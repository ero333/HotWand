using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public bool dead = false;
	public Animator animator;
	private SpriteRenderer sprite;
	
	public WeaponPickup weaponPickup;
	public GameObject lastWeaponUsed;
	private GameObject player;
	private GameObject main;

	//Getting Child's Sprite
	private Transform child_transform;
	private GameObject child_object;
	private SpriteRenderer child_sprite;
	private GameObject portal;
	public void Start()
	{
		weaponPickup = gameObject.GetComponent<WeaponPickup>();
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
		child_transform = gameObject.transform.GetChild(0);
		child_object = child_transform.gameObject;
		child_sprite = child_object.GetComponent<SpriteRenderer>();

		portal = GameObject.FindGameObjectWithTag("Portal");

		main = GameObject.FindGameObjectWithTag("Main");

		player = GameObject.FindGameObjectWithTag("Player");
		lastWeaponUsed = player.GetComponent<Equipment>().equippedWeapon;
	}


	public void TakeDamage(int damage) {
        health -= damage;
		if (health <= 0)
		{
			dead = true;
			animator.SetBool("Dead", true);
            if (sprite) sprite.sortingLayerName = "Dead";
			if (child_sprite) child_sprite.sortingLayerName = "Dead";
			
			if (portal != null) portal.GetComponent<NextLevel>().enemiesAlive -= 1;

            if (main != null)
            {
                if (lastWeaponUsed != player.GetComponent<Equipment>().equippedWeapon)
                {
                    main.GetComponent<Score>().score += 500;
                }
                else
                {
                    main.GetComponent<Score>().score += 100;
                }
            }

            lastWeaponUsed = player.GetComponent<Equipment>().equippedWeapon;

            if ((GetComponent<WeaponPickup>()) != null)
			{
				if (GetComponent<WeaponPickup>().weaponEquipped != null)
				{
					weaponPickup.weaponEquipped.transform.position = transform.position;
					weaponPickup.weaponEquipped.SetActive(true);
					weaponPickup.weaponEquipped = null;
					animator.SetBool("has weapon", false);
				}
			}

            if (main != null)
            {
                if (lastWeaponUsed != player.GetComponent<Equipment>().equippedWeapon)
                {
                    main.GetComponent<Score>().score += 500;
                }
                else
                {
                    main.GetComponent<Score>().score += 100;
                }
            }
            lastWeaponUsed = player.GetComponent<Equipment>().equippedWeapon;
        }
		else
		if (health == 1)
		{
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
			//Debug.Log("Got Knocked.");
			transform.Translate(new Vector3(0,0,0));

			if (GetComponent<WeaponPickup>().weaponEquipped != null)
			{
				weaponPickup.weaponEquipped.transform.position = transform.position;
				weaponPickup.weaponEquipped.SetActive(true);
				weaponPickup.weaponEquipped = null;
				animator.SetBool("has weapon", false);
			}
		}
<<<<<<< HEAD
=======
		else
		if (health < 0)
		{
            health = 0;
		}
>>>>>>> 09cda6efb50cfe5c01a79f8f83c8a356f35a7a14

		//Debug.Log("Got Hit.");
	}
}