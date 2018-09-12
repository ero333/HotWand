using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour {

	public Vector3 direction;
	private Vector2 target;
	public float speed;
    

	// time for killing the projectile if it lasts for too long
	public float deathTimer = 10.0f;


	void Start () {
		direction.x = Vector2.right.x;
		direction.y = Vector2.right.y;
		direction.z = 0;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (direction*8*Time.deltaTime);

		deathTimer -= Time.deltaTime;
		if(deathTimer<=0)
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Enemy")){
			Destroy(this.gameObject);
		}
		else
		if(other.CompareTag("Wall")){
			Destroy(this.gameObject);
		}
	}
}
