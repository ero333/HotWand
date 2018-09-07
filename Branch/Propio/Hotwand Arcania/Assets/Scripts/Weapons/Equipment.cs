using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

	public GameObject equippedWeapon;
	
	public GameObject wandProjectile;
	public GameObject crossbowProjectile;

	public GameObject rangedAnchorPoint;

	public Animator animator;

	//Damage Modifiers for weapons
	public float punchDamage;
	public float swordDamage;
	public float wandProjectileDamage;

	public void EquipWeapon(GameObject item)
	{
		//Are we holding a weapon?
		//No?
		if (equippedWeapon == null) {
			//Add item
			equippedWeapon = item;
		}
		else
		//If yes...
		//Is the item we have equipped different from the one we want to pick up?
		if (equippedWeapon != item)
		{
			equippedWeapon.transform.position = item.transform.position;
			equippedWeapon.SetActive(true);
			equippedWeapon = null;
			equippedWeapon = item;
		}

		//Animation
		if (item.name == "Sword")
		{
			animator.SetBool("Sword Stance", true);	
			animator.SetBool("Wand Stance", false);	
			animator.SetBool("Crossbow Stance", false);
		}
		else
		if (item.name == "Wand")
		{
			animator.SetBool("Wand Stance", true);
			animator.SetBool("Sword Stance", false);
			animator.SetBool("Crossbow Stance", false);	
		}
		else
		if (item.name == "Crossbow")
		{
			animator.SetBool("Crossbow Stance", true);
			animator.SetBool("Wand Stance", false);
			animator.SetBool("Sword Stance", false);	
		}
		else
		{
			animator.SetBool("Wand Stance", false);
			animator.SetBool("Sword Stance", false);
			animator.SetBool("Crossbow Stance", false);
		}
	}

	public void DropWeapon()
	{
		//We check if we do have something equipped
		if (equippedWeapon != null)
		{
			equippedWeapon.transform.position = transform.position;
			equippedWeapon.SetActive(true);
			equippedWeapon = null;

			animator.SetBool("Wand Stance", false);
			animator.SetBool("Sword Stance", false);
			animator.SetBool("Crossbow Stance", false);
		}
	}

	public void Attack()
	{
		if (equippedWeapon == null){
			Collider2D[] hitObjects = Physics2D.OverlapCircleAll (transform.position, 1.0f);
			//Hit only the closest enemy
			hitObjects[1].SendMessage("TakeDamage", punchDamage, SendMessageOptions.DontRequireReceiver);
		}
		else
		if (equippedWeapon.name == "Wand"){
			Instantiate(wandProjectile, rangedAnchorPoint.transform.position, transform.rotation);
		}
		else
		if (equippedWeapon.name == "Sword"){
			Collider2D[] hitObjects = Physics2D.OverlapCircleAll (transform.position, 1.0f);
			//Hit only the closest enemy
			hitObjects[1].SendMessage("TakeDamage", swordDamage, SendMessageOptions.DontRequireReceiver);
			Debug.Log ("Hit " + hitObjects[0].name);
		}
		else
		if (equippedWeapon.name == "Crossbow"){
			Instantiate(crossbowProjectile, rangedAnchorPoint.transform.position, transform.rotation);
		}

	}
}
