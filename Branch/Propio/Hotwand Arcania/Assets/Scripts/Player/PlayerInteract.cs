using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currentWeaponOnFloor = null;
	public Weapon currentWeaponScript = null;
	public Equipment equipment;
	public Animator animator;
	private float lastAttackTime;

	[SerializeField] private float swordDelay;
	[SerializeField] private float axeDelay;
	[SerializeField] private float wandDelay;
	[SerializeField] private float uziwandDelay;
	[SerializeField] private float icewandDelay;
	[SerializeField] private float crossbowDelay;

	private float attackDelay;
	
	void Update(){
		if (!GetComponent<Health>().dead)
		{
			//Pick up item from the floor
			if(Input.GetMouseButtonDown(1)){
				if (currentWeaponOnFloor){
					//Check to see if this object is to be stored in inventory
					if (currentWeaponScript.equippable)
					{
						equipment.EquipWeapon(currentWeaponOnFloor);
						currentWeaponOnFloor.SendMessage("PickedUp");
						currentWeaponOnFloor = null;
					}
				}
				else 
				if (!currentWeaponOnFloor){
					equipment.ThrowWeapon();
				}
			}

			if (equipment.equippedWeapon != null)
			{
				switch (equipment.equippedWeapon.GetComponent<Weapon>().weaponName)
				{
					case "Sword":
						attackDelay = swordDelay;
					break;

					case "Axe":
						attackDelay = axeDelay;
					break;

					case "Wand":
						attackDelay = wandDelay;
					break;

					case "Uziwand":
						attackDelay = uziwandDelay;
					break;

					case "Ice Wand":
						attackDelay = icewandDelay;
					break;

					case "Crossbow":
						attackDelay = crossbowDelay;
					break;
				}
			}

			//Attack
			if (Input.GetMouseButtonDown(0))
			{
				if (Time.time > lastAttackTime + attackDelay) 
				{	
					equipment.Attack();
					lastAttackTime = Time.time;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Weapon"))
		{
			//Debug.Log(other.name);
			currentWeaponOnFloor = other.gameObject;
			currentWeaponScript = currentWeaponOnFloor.GetComponent<Weapon>();
		}
		else
		if(other.CompareTag("Enemy"))
		{
			GetComponent<Health>().TakeDamage(1);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Weapon"))
		{
			if(other.gameObject == currentWeaponOnFloor)
			{
				currentWeaponOnFloor = null;
				currentWeaponScript = null;
			}
		}
	}

	

}

