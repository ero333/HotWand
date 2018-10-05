using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public bool dead = false;
	public Animator animator;
	private SpriteRenderer sprite;
	
	public WeaponPickup weaponPickup;


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

			if (GetComponent<WeaponPickup>().weaponEquipped != null)
			{
				weaponPickup.weaponEquipped.transform.position = transform.position;
				weaponPickup.weaponEquipped.SetActive(true);
				weaponPickup.weaponEquipped = null;
				animator.SetBool("has weapon", false);
			}
		}
		else
		if (health >= 1)
		{
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
			Debug.Log("Got Knocked.");
			transform.Translate(new Vector3(0,0,0));
			//GetComponent<RigidBody2D>().velocity = new Vector3(0, 0, 0);

			if (GetComponent<WeaponPickup>().weaponEquipped != null)
			{
				weaponPickup.weaponEquipped.transform.position = transform.position;
				weaponPickup.weaponEquipped.SetActive(true);
				weaponPickup.weaponEquipped = null;
				animator.SetBool("has weapon", false);
			}
		}
		
		Debug.Log("Got Hit.");
	}
}
