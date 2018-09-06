using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	public GameObject currentInteractable = null;
	public InteractionObject currentInteractableObjectScript = null;
	public Inventory inventory;

	void Update(){
		//Pick up item from the floor
		if(Input.GetMouseButtonDown(1) && currentInteractable){
			//Check to see if this object is to be stored in inventory
			if (currentInteractableObjectScript.inventory)
			{
				inventory.AddItem(currentInteractable);
				currentInteractable.SendMessage("Pickedup");
			}
		}

		//Use item
		if(Input.GetMouseButtonDown(0) && currentInteractable){
			//Check the inventory if we have something equipped

		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Interactable")){
			Debug.Log(other.name);
			currentInteractable = other.gameObject;
			currentInteractableObjectScript = currentInteractable.GetComponent<InteractionObject>();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Interactable")){
			if(other.gameObject == currentInteractable){
				currentInteractable = null;
			}
		}
	}
}
