﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

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
	private Vector3 targetDir;
	private Transform player;
	public int damage;
    public string creator_name;
    public float speed;
	public string arma;

	// time for killing the projectile if it lasts for too long
	public float deathTimer = 10.0f;


    void Start()
    {
        direction.x = Vector2.right.x;
        direction.y = Vector2.right.y;
        direction.z = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        damage = 1;
		targetDir = player.position - transform.position;
        //creator_name = "enemy";
    }

	// Update is called once per frame
	void Update () {
		targetDir = player.position - transform.position;
		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 2f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 2 * Time.deltaTime);
		
		transform.Translate (direction*speed*Time.deltaTime);

		deathTimer -= Time.deltaTime;
		if(deathTimer<=0)
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			print(creator_name + " me ataco");
			print("El proyectil vino de una " + arma);
			other.SendMessage("TakeDamage", new Attack(damage, creator_name, arma), SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}
		else
		if(other.CompareTag("Wall")){
			Destroy(this.gameObject);
		}
	}
}
