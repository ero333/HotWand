using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public Animator anim;
	public bool dead;
	public bool knocked;
	public int health;
    private SpriteRenderer sprite;
		
	//Getting Child's Sprite
	private Transform child_transform;
	private GameObject child_object;
	private SpriteRenderer child_sprite;
	public void Start()
	{
		knocked = false;
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
		child_transform = gameObject.transform.GetChild(0);
		child_object = child_transform.gameObject;
		child_sprite = child_object.GetComponent<SpriteRenderer>();
	}
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0){
			Debug.Log("Dead");
			anim.SetBool("Dead", true);
			dead = true;
			if (sprite) sprite.sortingLayerName = "Dead";		
			if (child_sprite) child_sprite.sortingLayerName = "Dead";
		}
		else
		if (health == 1)
		{
			knocked = true;
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
		}
	}
}
