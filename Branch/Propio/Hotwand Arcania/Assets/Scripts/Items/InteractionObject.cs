using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

	public bool inventory;		//If true, this object can be stored in inventory
	public string itemType;		//This will tell what type of item this object is

	public void Pickedup(){
		//Picked up and put in inventory
		gameObject.SetActive (false);
	}
}
