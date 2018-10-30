using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {
public GameObject equippedWeapon;
[SerializeField]	private GameObject meleeHitbox;	

//Weapon Prefabs
[SerializeField]	private GameObject swordPrefab;	
[SerializeField]	private GameObject axePrefab;	
[SerializeField]	private GameObject wandPrefab;	
[SerializeField]	private GameObject uziwandPrefab;	
[SerializeField]	private GameObject icewandPrefab;	
[SerializeField]	private GameObject crossbowPrefab;	

//All projectiles for each weapon
[SerializeField]	private GameObject wandProjectile;
[SerializeField]	private GameObject uziwandProjectile;
[SerializeField]	private GameObject crossbowProjectile;

//The points from where the hitbox objects will be created
[SerializeField]	private GameObject meleeAnchorPoint;
[SerializeField]	private GameObject rangedAnchorPoint;


	public Animator animator;
	public int crossbowAmmo = 30;
	public int wandAmmo = 50;

	//Damage Modifiers for weapons
	public int punchDamage;
	public int swordDamage;
	public int wandProjectileDamage;
	private GameObject main;
    private GameObject score;

	public void Start(){
		main = GameObject.FindGameObjectWithTag("Main");
		score = GameObject.FindGameObjectWithTag("Score");
    }
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
                animator.SetBool("Uziwand Stance", false);
                break;

			case "Axe":
				animator.SetBool("Sword Stance", false);	
				animator.SetBool("Axe Stance", true);	
				animator.SetBool("Wand Stance", false);	
				animator.SetBool("Crossbow Stance", false);
                animator.SetBool("Uziwand Stance", false);
                break;

			case "Wand":
				animator.SetBool("Sword Stance", false);	
				animator.SetBool("Axe Stance", false);	
				animator.SetBool("Wand Stance", true);	
				animator.SetBool("Crossbow Stance", false);
                animator.SetBool("Uziwand Stance", false);
                if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
			break;

			case "Uziwand":
                animator.SetBool("Uziwand Stance", true);
                animator.SetBool("Sword Stance", false);	
				animator.SetBool("Axe Stance", false);	
				animator.SetBool("Wand Stance", false);	
				animator.SetBool("Crossbow Stance", false);
				if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
			break;

			case "Ice Wand":
				animator.SetBool("Sword Stance", false);	
				animator.SetBool("Axe Stance", false);	
				animator.SetBool("Wand Stance", true);	
				animator.SetBool("Crossbow Stance", false);
                animator.SetBool("Uziwand Stance", false);
                if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
			break;

			case "Crossbow":
				animator.SetBool("Sword Stance", false);
				animator.SetBool("Axe Stance", false);		
				animator.SetBool("Wand Stance", false);	
				animator.SetBool("Crossbow Stance", true);
                animator.SetBool("Uziwand Stance", false);
                if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
			break;

			default:
				animator.SetBool("Sword Stance", false);
				animator.SetBool("Axe Stance", false);
				animator.SetBool("Wand Stance", false);
				animator.SetBool("Crossbow Stance", false);
                animator.SetBool("Uziwand Stance", false);
                break;
		}
	}

	public void ThrowWeapon()
	{	
		GameObject thrownSword;
		GameObject thrownAxe;
		GameObject thrownWand;
		GameObject thrownCrossbow;

		if (equippedWeapon != null)
		{
			switch (equippedWeapon.GetComponent<Weapon>().weaponName)
			{
				case "Sword":
					thrownSword = Instantiate(swordPrefab, meleeAnchorPoint.transform.position, transform.rotation);
					thrownSword.GetComponent<Weapon>().beingThrown = true;
					Destroy(equippedWeapon);
					ResetStance();
				break;

				case "Axe":
					thrownAxe = Instantiate(axePrefab, meleeAnchorPoint.transform.position, transform.rotation);
					thrownAxe.GetComponent<Weapon>().beingThrown = true;
					Destroy(equippedWeapon);
					ResetStance();
				break;

				case "Wand":
					thrownWand = Instantiate(wandPrefab, meleeAnchorPoint.transform.position, transform.rotation);
					thrownWand.GetComponent<Weapon>().beingThrown = true;
					Destroy(equippedWeapon);
					ResetStance();
				break;

				case "Uziwand":
					thrownWand = Instantiate(uziwandPrefab, meleeAnchorPoint.transform.position, transform.rotation);
					thrownWand.GetComponent<Weapon>().beingThrown = true;
					Destroy(equippedWeapon);
					ResetStance();
				break;

				case "Ice Wand":
					thrownWand = Instantiate(icewandPrefab, meleeAnchorPoint.transform.position, transform.rotation);
					thrownWand.GetComponent<Weapon>().beingThrown = true;
					Destroy(equippedWeapon);
					ResetStance();
				break;

				case "Crossbow":
					thrownCrossbow = Instantiate(crossbowPrefab, meleeAnchorPoint.transform.position, transform.rotation);
					thrownCrossbow.GetComponent<Weapon>().beingThrown = true;
					Destroy(equippedWeapon);
					ResetStance();
				break;
			}
		}
	}

	public void ResetStance()
	{
		equippedWeapon = null;

		//We make sure to clean up animation parameters
		animator.SetBool("Sword Stance", false);
		animator.SetBool("Axe Stance", false);
		animator.SetBool("Wand Stance", false);
		animator.SetBool("Crossbow Stance", false);
	}
	public void DropWeapon()
	{
		//We check if we do have something equipped
		if (equippedWeapon != null)
		{
			equippedWeapon.transform.position = transform.position;
			equippedWeapon.SetActive(true);
			ResetStance();
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
					if (equippedWeapon.GetComponent<Weapon>().weaponAmmo > 0) 
					{
							if (main != null) main.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;							equippedWeapon.GetComponent<Weapon>().weaponAmmo -= 1;
							Instantiate(wandProjectile, rangedAnchorPoint.transform.position, transform.rotation);
					}
				break;

				case "Uziwand":
					if (equippedWeapon.GetComponent<Weapon>().weaponAmmo > 0) 
					{
							equippedWeapon.GetComponent<Weapon>().weaponAmmo -= 1;
							Instantiate(uziwandProjectile, rangedAnchorPoint.transform.position, transform.rotation);
							if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
					}
				break;

				case "Ice Wand":
					if (equippedWeapon.GetComponent<Weapon>().weaponAmmo > 0) 
					{
							equippedWeapon.GetComponent<Weapon>().weaponAmmo -= 1;
							Instantiate(wandProjectile, rangedAnchorPoint.transform.position, transform.rotation);
							if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
					}
				break;

				case "Crossbow":
					if (equippedWeapon.GetComponent<Weapon>().weaponAmmo > 0) 
					{
							equippedWeapon.GetComponent<Weapon>().weaponAmmo -= 1;
							Instantiate(crossbowProjectile, rangedAnchorPoint.transform.position, transform.rotation);
							if (score != null) score.GetComponent<Score>().ammo = equippedWeapon.GetComponent<Weapon>().weaponAmmo;
					}
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
