using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class EnemyHealth : MonoBehaviour {

	public int health;
	public bool dead = false;
	public Animator animator;
    public GameObject legs;
	private SpriteRenderer sprite;
	
	public WeaponPickup weaponPickup;
	
	private GameObject player;
	private GameObject main;
    private GameObject score;

    //Getting Child's Sprite
    private Transform child_transform;
	private GameObject child_object;
	private SpriteRenderer child_sprite;
	private GameObject portal;
	public void Start()
	{
		weaponPickup = gameObject.GetComponent<WeaponPickup>();
		dead = false;
		sprite = GetComponent<SpriteRenderer>();
		child_transform = gameObject.transform.GetChild(0);
		child_object = child_transform.gameObject;
		child_sprite = child_object.GetComponent<SpriteRenderer>();

		portal = GameObject.FindGameObjectWithTag("Portal");
		main = GameObject.FindGameObjectWithTag("Main");
		score = GameObject.FindGameObjectWithTag("Score");
		player = GameObject.FindGameObjectWithTag("Player");
    }


    public void TakeDamage(Attack attack) {
        health -= attack.damage;

        if (health <= 0)
		{
            legs.SetActive(false);
            health = 0;
            if ( !dead ) {
                dead = true;

                Debug.Log("Evento Matar <"+
                " nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)+
                " tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)+
                " enemigo: " + (this.name)+
                " CordenadasX: " + GameObject.FindGameObjectWithTag("Player").transform.position.x+
                " CordenadasY: " + GameObject.FindGameObjectWithTag("Player").transform.position.y+">");

                Analytics.CustomEvent("Matar", new Dictionary<string, object> {
                         {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                         {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                         {"enemigo",  (this.name) },
                         {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                         {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                });
            }
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

       
            }
            if ((GetComponent<WeaponPickup>()) != null)
			{
				if (GetComponent<WeaponPickup>().weaponEquipped != null)
				{
					weaponPickup.weaponEquipped.transform.position = transform.position;
					weaponPickup.weaponEquipped.SetActive(true);
					weaponPickup.weaponEquipped = null;
					animator.SetBool("has weapon", false);
				}
			}

        }
		else
		{
            // Si sobrevivio al ataque, queda noqueado
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
			//Debug.Log("Got Knocked.");
			transform.Translate(new Vector3(0,0,0));

            //knocked = true;
            Debug.Log("Evento Noqueado <" +"nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)+
            " tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)+
            " enemigo: " + (this.name)+
            " CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x+
            " CordenadasY:" + GameObject.FindGameObjectWithTag("Enemy").transform.position.y+">");
            Analytics.CustomEvent("Noqueado", new Dictionary<string, object>
                {
                    {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"enemigo",  (this.name) },
                    {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                    {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                });

            if (GetComponent<WeaponPickup>().weaponEquipped != null)
			{
				weaponPickup.weaponEquipped.transform.position = transform.position;
				weaponPickup.weaponEquipped.SetActive(true);
				weaponPickup.weaponEquipped = null;
				animator.SetBool("has weapon", false);
			}
		}




		//Debug.Log("Got Hit.");
	}
}