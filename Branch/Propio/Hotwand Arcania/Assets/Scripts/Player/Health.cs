using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public Animator anim;
	public bool dead;
	private int health;
    private SpriteRenderer sprite;
	public void Start()
	{
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0){
			Debug.Log("Dead");
			anim.SetBool("Dead", true);
			dead = true;
			if (sprite)
            sprite.sortingOrder = 3;		
		}
	}
}
