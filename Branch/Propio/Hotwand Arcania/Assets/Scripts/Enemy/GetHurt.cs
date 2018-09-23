using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHurt : MonoBehaviour {

	public int health;
	public bool dead = false;
	public Animator animator;

	public void TakeDamage(int damage) {
		health -= damage;
		if (health <= 0)
		{
			dead = true;
			animator.SetBool("Dead", true);
		}
		Debug.Log("Got Hit.");
	}
}
