﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
	public GameObject currentWeaponOnFloor = null;
	public Weapon currentWeaponScript = null;
	public Equipment equipment;
	public Animator animator;

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

			//Attack
			if(Input.GetMouseButtonDown(0)){
				equipment.Attack();
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

