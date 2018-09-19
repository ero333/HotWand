using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public Animator anim;
	public bool dead;
	private int health;

	public void Start()
	{
		dead = false;
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0){
			Debug.Log("Dead");
			anim.SetBool("Dead", true);
			dead = true;			
		}
	}
}
