using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class EnemyHealth_Beast : MonoBehaviour {

    public bool boss;
	public int health;
	public bool dead = false;
	public Animator animator;
	private SpriteRenderer sprite;
	private GameObject main;
    private GameObject score;
    private GameObject player;

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

        //Flash Shader when hit
        if (knocked) {
            knockedTimer -= Time.time;

            if (knockedTimer <= 0.0f)
            {
                knocked = false;
                GetComponent<SpriteRenderer>().material = Default;
            }
        }
    }

    public void TakeDamage(Attack attack) {
		health -= attack.damage;
 
        if (health <= 0)
        {
            print("muerto");
            health = 0;
            if (!dead)
            {
                dead = true;

                Debug.Log("Evento Matar <" +
                " nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1) +
                " tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) +
                " enemigo: " + "Beast" +
                " arma:" + attack.arma +
                " CordenadasX: " + GameObject.FindGameObjectWithTag("Player").transform.position.x +
                " CordenadasY: " + GameObject.FindGameObjectWithTag("Player").transform.position.y + ">");

                Analytics.CustomEvent("Matar", new Dictionary<string, object> {
                         {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                         {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                         {"enemigo", "Beast" },
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
                TiempoDead = Time.time;

        }
        }
		else
		if (health > 0)
		{
            if (!knocked) {
                knockedTimer = 20.0f;
                knocked = true;
            }

            GetComponent<SpriteRenderer>().material = Hit;
            /*
			gameObject.GetComponent<Animator>().SetBool("Knocked", true);
			Debug.Log("Got Knocked.");
			transform.Translate(new Vector3(0,0,0));
            //knocked = true;
            Debug.Log("Evento Noqueado <" + "nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1) +
            " tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) +
            " enemigo: " + "Beast" +
            " arma;" + attack.arma +
            " CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x +
            " CordenadasY:" + GameObject.FindGameObjectWithTag("Enemy").transform.position.y + ">");
            Analytics.CustomEvent("Noqueado", new Dictionary<string, object>
                {
                    {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"enemigo",  "Beast" },
                    {"arma", attack.arma },
                    {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                    {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                });
            */
        }
		Debug.Log("Got Hit.");
	}
}
