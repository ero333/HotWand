using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour {

	public string weaponName;
	public bool equippable;		//If true, this object can be stored in inventory
	//public string weaponType;		//This will tell what type of item this object is
	public string weaponType;
	public int weaponAmmo;
	private bool hasAmmo;

	void Start()
	{
		weaponType = null;
		if (weaponName != null)
		{
			switch (weaponName)
			{
				case "Wand":
					weaponType = "Ranged";
				break;

				case "Crossbow":
					weaponType = "Ranged";
				
				break;

				case "Sword":
					weaponType = "Melee";
				break;

				case "Axe":
					weaponType = "Melee";
				break;
			}
		}
	}
	public void PickedUp(){
		//Picked up and put in inventory
		gameObject.SetActive (false);
	}
}
