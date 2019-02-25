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

    //Flash Shader Vars
    public Material Default;
    public Material Hit;
    private float knockedTimer = 20.0f;
    public bool knocked;

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

    public void Update()
    {
        //Flash Shader when hit
        if (knocked) {
            knockedTimer -= Time.time;

            if (knockedTimer <= 0.0f)
            {
                knocked = false;
                child_object.GetComponent<SpriteRenderer>().material = Default;
                GetComponent<SpriteRenderer>().material = Default;
            }
        }
    }
    public void TakeDamage(Attack attack) {
        health -= attack.damage;
        child_object.GetComponent<SpriteRenderer>().material = Hit;
        GetComponent<SpriteRenderer>().material = Hit;

        if (health <= 0)
		{
            GetComponent<SpriteRenderer>().material = Default;
            legs.SetActive(false);          
            health = 0;
            if ( !dead ) {
                dead = true;

                Debug.Log("Evento Matar <"+
                " nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)+
                " tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)+
                " enemigo: " + (this.name)+
                " arma:" + attack.arma +
                " CordenadasX: " + GameObject.FindGameObjectWithTag("Player").transform.position.x+
                " CordenadasY: " + GameObject.FindGameObjectWithTag("Player").transform.position.y+">");

                Analytics.CustomEvent("Matar", new Dictionary<string, object> {
                         {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                         {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                         {"enemigo",  (this.name) },
                         {"arma", attack.arma },
                         { "CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
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
            //legs.SetActive(false);
            // Si sobrevivio al ataque, queda noqueado
            if (!knocked) {
                knockedTimer = 20.0f;
                knocked = true;
            }

 
            /*
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
            
			//Debug.Log("Got Knocked.");
			transform.Translate(new Vector3(0,0,0));

            //Knocked = true;
            Debug.Log("Evento Noqueado <" +"nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)+
            " tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)+
            " enemigo: " + (this.name)+
            " arma;"+ attack.arma +
            " CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x+
            " CordenadasY:" + GameObject.FindGameObjectWithTag("Enemy").transform.position.y+">");
            Analytics.CustomEvent("Noqueado", new Dictionary<string, object>
                {
                    {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"enemigo",  (this.name) },
                    {"arma", attack.arma },
                    {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                    {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                });
            */

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