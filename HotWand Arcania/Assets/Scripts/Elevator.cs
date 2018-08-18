using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	public GameObject[] enemies;
	public GameObject NextPlaceToGo;
	static bool canTravel = true;
	float timer=1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timerCountdown ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && areAllEnemiesDead()==true && canTravel==true) {
			other.transform.position = NextPlaceToGo.transform.position;
			canTravel = false;
		}
	}

	public bool areAllEnemiesDead()
	{
		for (int x = 0; x < enemies.Length; x++) {
			if (enemies [x].tag != "Dead") {
				return false;
			}
		}
		return true;
	}

	void timerCountdown()
	{
		if (canTravel == false) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				canTravel = true;
				timer = 1.0f;
			}
		}
	}
}
