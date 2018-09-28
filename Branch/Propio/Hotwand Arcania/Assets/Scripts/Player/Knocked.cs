using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Knocked : MonoBehaviour {

	private bool knocked;
	private float knockedTimer;
	// Use this for initialization
	public void Start () {
		knocked = gameObject.GetComponent<Health>().knocked;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > knockedTimer + 3)
		{
			gameObject.GetComponent<Animator>().SetBool("Knocked", false);
			gameObject.GetComponent<Health>().knocked = false; 
			knockedTimer = Time.time;
		}
	}
}
