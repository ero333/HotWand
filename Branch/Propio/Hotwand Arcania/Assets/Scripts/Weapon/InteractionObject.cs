using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

	public void DoInteraction(){
		//Picked up and put in inventory
		gameObject.SetActive (false);
	}
}
