using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public GameObject inventory;

	public void AddItem(GameObject item)
	{
		//Is inventory empty?
		if (inventory == null) {
			//Add item
			inventory = item;
		}
		else
		//Is the item we have equipped different from the one we want to pick up?
		if (inventory != item)
		{
			inventory.transform.position = item.transform.position;
			inventory.SetActive(true);
			inventory = null;
			inventory = item;
		}

	}
}
