using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemyProjectile : MonoBehaviour {
	/*
	public float speed;


	private Transform player;
	private Vector2 target;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;

		target = new Vector2(player.position.x, player.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
	*/
	public Vector3 direction;
	private Vector2 target;
	private Transform player;
	public float speed;
    

	// time for killing the projectile if it lasts for too long
	public float deathTimer = 10.0f;


	void Start () {
		direction.x = Vector2.right.x;
		direction.y = Vector2.right.y;
		direction.z = 0;
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Vector3 targetDir = player.position - transform.position;
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);

		transform.Translate (direction*8*Time.deltaTime);

		deathTimer -= Time.deltaTime;
		if(deathTimer<=0)
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			Destroy(this.gameObject);
		}
		else
		if(other.CompareTag("Wall")){
			Destroy(this.gameObject);
		}
	}
}
