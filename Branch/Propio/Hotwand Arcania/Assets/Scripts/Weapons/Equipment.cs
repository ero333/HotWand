using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
public GameObject equippedWeapon;
[SerializeField]	private GameObject meleeHitbox;	
[SerializeField]	private GameObject wandProjectile;
[SerializeField]	private GameObject crossbowProjectile;

[SerializeField]	private GameObject meleeAnchorPoint;
[SerializeField]	private GameObject rangedAnchorPoint;


	public Animator animator;
	public int crossbowAmmo = 30;
	public int wandAmmo = 50;

	//Damage Modifiers for weapons
	public int punchDamage;
	public int swordDamage;
	public int wandProjectileDamage;

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
		switch (item.GetComponent<Weapon>().weaponName)
		{
			case "Sword":
				animator.SetBool("Sword Stance", true);	
				animator.SetBool("Axe Stance", false);	
				animator.SetBool("Wand Stance", false);	
				animator.SetBool("Crossbow Stance", false);
			break;

			case "Axe":
				animator.SetBool("Sword Stance", false);	
				animator.SetBool("Axe Stance", true);	
				animator.SetBool("Wand Stance", false);	
				animator.SetBool("Crossbow Stance", false);
			break;

			case "Wand":
				animator.SetBool("Sword Stance", false);	
				animator.SetBool("Axe Stance", false);	
				animator.SetBool("Wand Stance", true);	
				animator.SetBool("Crossbow Stance", false);
			break;

			case "Crossbow":
				animator.SetBool("Sword Stance", false);
				animator.SetBool("Axe Stance", false);		
				animator.SetBool("Wand Stance", false);	
				animator.SetBool("Crossbow Stance", true);
			break;

			default:
				animator.SetBool("Sword Stance", false);
				animator.SetBool("Axe Stance", false);
				animator.SetBool("Wand Stance", false);
				animator.SetBool("Crossbow Stance", false);
			break;
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

			//We make sure to clean up animation parameters
			animator.SetBool("Sword Stance", false);
			animator.SetBool("Axe Stance", false);
			animator.SetBool("Wand Stance", false);
			animator.SetBool("Crossbow Stance", false);
		}
	}

	public void Attack()
	{
		GameObject swordAttack;
		GameObject axeAttack;

		if (equippedWeapon != null)
		{
			switch (equippedWeapon.GetComponent<Weapon>().weaponName)
			{
				case "Sword":
					swordAttack = Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
					if (swordAttack != null) swordAttack.GetComponent<MeleeHitboxPlayer>().damage = 2;
					animator.SetTrigger("Sword Attack");
				break;

				case "Axe":
					axeAttack = Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
					if (axeAttack != null) axeAttack.GetComponent<MeleeHitboxPlayer>().damage = 3;
					animator.SetTrigger("Axe Attack");
				break;

				case "Wand":
					wandAmmo = wandAmmo - 1;
					if (wandAmmo > 0) Instantiate(wandProjectile, rangedAnchorPoint.transform.position, transform.rotation);
				break;

				case "Crossbow":
					crossbowAmmo = crossbowAmmo - 1;
					if (crossbowAmmo > 0) Instantiate(crossbowProjectile, rangedAnchorPoint.transform.position, transform.rotation);
				break;
			}
		}
		else
		{
			//PUNCH MECHANIC
			animator.SetTrigger("Punch");
			Instantiate(meleeHitbox, meleeAnchorPoint.transform.position, transform.rotation);
		}
	}
}
