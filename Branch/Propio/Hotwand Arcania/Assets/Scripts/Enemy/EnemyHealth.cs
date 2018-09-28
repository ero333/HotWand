using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public bool dead = false;
	public Animator animator;
	private SpriteRenderer sprite;

	//Getting Child's Sprite
	private Transform child_transform;
	private GameObject child_object;
	private SpriteRenderer child_sprite;
	public void Start()
	{
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
		child_transform = gameObject.transform.GetChild(0);
		child_object = child_transform.gameObject;
		child_sprite = child_object.GetComponent<SpriteRenderer>();
	}

	public void TakeDamage(int damage) {
		health -= damage;
		if (health <= 0)
		{
			dead = true;
			animator.SetBool("Dead", true);
            if (sprite) sprite.sortingLayerName = "Dead";
			if (child_sprite) child_sprite.sortingLayerName = "Dead";
		}
		else
		if (health == 1)
		{
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
			Debug.Log("Got Knocked.");
		}
		
		Debug.Log("Got Hit.");
	}
}
