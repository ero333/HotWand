using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Health : MonoBehaviour {
    
    float originalWidth = 1280.0f; //turn these to floats to fix placement issue
	float originalHeight = 720.0f;
	Vector3 scale;
	public GUIStyle text;
	public Texture2D bg;
    public GameObject RestartButton;

    
	public Animator anim;
	public bool dead;
	public bool knocked;
	public int health;
	private float knockedTimer;
    private SpriteRenderer sprite;
	public Equipment equipment;
	public PlayerInteract interact;



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

		///Knocked Logic
		if (knocked)
		{
            if (Time.time > knockedTimer + 2)
			{
				knocked = false;
				knockedTimer = Time.time;

			}
			equipment.DropWeapon();
		}



		if (dead == true) {
            RestartButton.gameObject.SetActive(true);
            Time.timeScale = 0f;
            if (Input.GetKeyDown(KeyCode.R)) {
                Debug.Log("Evento ReiniciarNivel <"
                +" nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)
                +" tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)
                +" puntos: " + GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score
                +" muertes: " + GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes+" >"); 
                Analytics.CustomEvent("ReiniciarNivel", new Dictionary<string, object>
                {
                    {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"puntos", (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score) },
                    {"muertes", GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes }
                });
                SceneManager.LoadScene (SceneManager.GetActiveScene().name);//remember to mention new scene manager using thing
                Time.timeScale = 1f;
            }
           
		}

        if (dead == false)
        {
            RestartButton.gameObject.SetActive(false);
        }
        

    }


	public void TakeDamage(Attack attack)
	{
		if (health > 0)
		{
			health -= attack.damage;

            if (health > 0) { 

                if(!knocked)
                {                    
                    knocked = true;
                    Debug.Log("Evento Noqueado <"
                    +" nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)
                    +" tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)
                    +" enemigo: " + attack.creator
                    +" CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x
                    +" CordenadasY: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.y+" >");
                    Analytics.CustomEvent("Noqueado", new Dictionary<string, object>
                    {
                        {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                        {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                        {"enemigo", attack.creator },
                        {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                        {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                    });

                }
            }
            else
            {
                health = 0;
                Debug.Log("oh por Dios he muerto, necesito reiniciar!");
                anim.SetBool("Dead", true);
                if (!dead)
                {
                    Debug.Log("Evento Morir <"
                    +" nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)
                    +" tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)
                    +" enemigo: " + attack.creator
                    +" CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x
                    +" CordenadasY: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.y+" >");
                    
                    Analytics.CustomEvent("Morir", new Dictionary<string, object> {
                        {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                        {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                        {"enemigo", attack.creator },
                        {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                        {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                    });
                }
                dead = true;
                GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes += 1;
            }
        }
    }

	void OnGUI()
	{
		GUI.depth = 0;
		scale.x = Screen.width/originalWidth;
		scale.y = Screen.height/originalHeight;
		scale.z =1;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero,Quaternion.identity,scale);

		if (dead == true) {
            //Rect posForRestart = new Rect (0,0,originalWidth,originalHeight);
            //GUI.DrawTexture (posForRestart,bg);
            //SceneManager.LoadScene (SceneManager.GetActiveScene().name);
            
        }

		GUI.matrix = svMat;
        
    }
}