using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth_Beast : MonoBehaviour {

    public bool boss;
	public int health;
	public bool dead = false;
	public Animator animator;
	private SpriteRenderer sprite;
	private GameObject main;
    private GameObject score;
    private GameObject player;

    //Getting Child's Sprite
    private Transform child_transform;
	private GameObject child_object;
	private SpriteRenderer child_sprite;
	private GameObject portal;
    public float TiempoDead;

    public void Start()
	{
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
		child_transform = gameObject.transform.GetChild(0);
		child_object = child_transform.gameObject;
		child_sprite = child_object.GetComponent<SpriteRenderer>();
		portal = GameObject.FindGameObjectWithTag("Portal");
		main = GameObject.FindGameObjectWithTag("Main");
        score = GameObject.FindGameObjectWithTag("Score");
        player = GameObject.FindGameObjectWithTag("Player");
        TiempoDead = 0;
	}

    public void Update()
    {
         if (dead && boss)
        {
            print(Time.time);
            if (Time.time - TiempoDead > 2)
            {
                SceneManager.LoadScene("FinalCut");
            }
        }
    }

    public void TakeDamage(int damage) {
		health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        if (health <= 0)
        {
            print("muerto");
            dead = true;
            animator.SetBool("Dead", true);
            if (sprite) sprite.sortingLayerName = "Dead";
            if (child_sprite) child_sprite.sortingLayerName = "Dead";

            if (portal != null) portal.GetComponent<NextLevel>().enemiesAlive -= 1;
            if (score != null)
            {
                if (score.GetComponent<Score>().lastWeaponUsed != player.GetComponent<Equipment>().equippedWeapon)
                {
                    score.GetComponent<Score>().score += 500;
                }
                else
                {
                    score.GetComponent<Score>().score += 100;
                }
                score.GetComponent<Score>().lastWeaponUsed = player.GetComponent<Equipment>().equippedWeapon;
                TiempoDead = Time.time;

        }
        }
		else
		if (health >= 1)
		{
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
			Debug.Log("Got Knocked.");
			transform.Translate(new Vector3(0,0,0));
		}
		
		Debug.Log("Got Hit.");
	}
}
