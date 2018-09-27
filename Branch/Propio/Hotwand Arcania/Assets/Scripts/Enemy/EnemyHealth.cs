using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public bool dead = false;
	public Animator animator;
	private SpriteRenderer sprite;
	public void Start()
	{
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
	}

	public void TakeDamage(int damage) {
		health -= damage;
		if (health <= 0)
		{
			dead = true;
			animator.SetBool("Dead", true);
            sprite.sortingOrder = 3;
		}
		Debug.Log("Got Hit.");
	}
}
