using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	public GameObject currentInteractable = null;

	void Update(){
		if(Input.GetButtonDown("Interact") && currentInteractable){
			//Do something with the object
			currentInteractable.SendMessage("DoInteraction");
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Interactable")){
			currentInteractable = other.gameObject;
			Debug.Log(other.name);
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
