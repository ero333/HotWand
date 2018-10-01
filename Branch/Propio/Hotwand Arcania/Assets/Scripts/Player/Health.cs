using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	public Animator anim;
	public bool dead;
	public bool knocked;
	public int health;
	private float knockedTimer;
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
		//child_sprite = child_object.GetComponent<SpriteRenderer>();
	}

	public void Update()
	{
		if ((knocked) || (dead))
		{
			if (sprite) sprite.sortingLayerName = "Dead";
			if (child_object) child_object.SetActive(false);
			GetComponent<Animator>().SetBool("Knocked", true);
			GetComponent<RotateToCursor>().enabled = false;
			GetComponent<Movement>().enabled = false;
			GetComponent<PlayerInteract>().enabled = false;
		}
		else
		if (!knocked)
		{
			if (sprite) sprite.sortingLayerName = "Player";
			if (child_object) child_object.SetActive(true);
			GetComponent<Animator>().SetBool("Knocked", false);
			GetComponent<RotateToCursor>().enabled = true;
			GetComponent<Movement>().enabled = true;
			GetComponent<PlayerInteract>().enabled = true;
		}

		/////////////Knocked Logic
		if (knocked)
		{
			if (Time.time > knockedTimer + 2)
			{
				knocked = false;
				knockedTimer = Time.time;
			}
		}
	}
	public void TakeDamage(int damage)
	{
		if (health <= 0){
			Debug.Log("Dead");
			anim.SetBool("Dead", true);
			dead = true;
		}
		else
		if (health > 0)
		{
			health -= damage;
			knocked = true;
		}
	}
}
