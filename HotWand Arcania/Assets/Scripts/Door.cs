using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy"){
			if (Vector3.Distance (player.transform.position, this.transform.position) < 1.0) {
				EnemyAttacked ea = coll.gameObject.GetComponent<EnemyAttacked> ();
				ea.knockDownEnemy ();
			}
		}
	}
}
