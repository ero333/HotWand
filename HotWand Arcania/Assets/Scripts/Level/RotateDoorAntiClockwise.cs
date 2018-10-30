﻿using UnityEngine;
using System.Collections;

public class RotateDoorAntiClockwise : MonoBehaviour {

	DoorMovement dm;
	GameObject player;
	// Use this for initialization
	void Start () {
		dm = this.GetComponentInParent<DoorMovement>();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (Vector3.Distance (player.transform.position, this.gameObject.transform.position) < 1.1f &&other.gameObject.tag == "Enemy") {

            other.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
        }
	}

	void OnTriggerStay2D(Collider2D other)
	{
		dm.rotateAntiClockwiseMethod ();
		dm.beingOpened = true;
		dm.mod = 3;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		dm.beingOpened = false;
		dm.mod = 1;
	}
}
