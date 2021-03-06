﻿using System.Collections;
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
	
    private SpriteRenderer sprite;
	public Equipment equipment;
	public PlayerInteract interact;

    //Flash Shader Vars
    public Material Default;
    public Material Hit;
    private float knockedTimer = 40.0f;

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

        //Flash Shader when hit
        if (knocked) {
            knockedTimer -= Time.time;

            if (knockedTimer <= 0.0f)
            {
                knocked = false;
                child_object.GetComponent<SpriteRenderer>().material = Default;
                GetComponent<SpriteRenderer>().material = Default;
            }
            equipment.DropWeapon();
        }
        ///Knocked Logic
        /*
		if (knocked)
		{
            if (knockedTimer + 300f < Time.time)
			{
				knocked = false;
				knockedTimer = Time.time;
                Debug.Log("Fui Noqueado!");
                //GetComponent<SpriteRenderer>().material = Default;
			}

			
		}
        */


		if (dead == true) {
            RestartButton.gameObject.SetActive(true);
            Time.timeScale = 0f;


            if (Input.GetKeyDown(KeyCode.R)) {
                Globales.muertes++;
                Debug.Log("Evento ReiniciarNivel <"
                +" nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)
                +" tiempo: " + GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel
                +" puntos: " + GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score
                +" muertes: " + Globales.muertes+" >"); 
                Analytics.CustomEvent("ReiniciarNivel", new Dictionary<string, object>
                {
                    {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                    {"tiempo", (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                    {"puntos", (GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().score) },
                    {"muertes", Globales.muertes }
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

            if (health > 0)
            { 
                if (!knocked)
                {
                    knockedTimer = 40.0f;
                    knocked = true;
                    HealthBar.health -= 40.0f;
                }

                child_object.GetComponent<SpriteRenderer>().material = Hit;
                GetComponent<SpriteRenderer>().material = Hit;
                /* Quitado del master branch para evitar mayores bugs
                if(!knocked)
                {                    
                    knocked = true;
                    Debug.Log("Evento Noqueado <"
                    +" nivel: " + (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)
                    +" tiempo: " + (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel)
                    +" enemigo: " + attack.creator
                    + " arma: " + attack.arma
                    + " CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x
                    +" CordenadasY: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.y+" >");
                    Analytics.CustomEvent("Noqueado", new Dictionary<string, object>
                    {
                        {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                        {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                        {"enemigo", attack.creator },
                        {"arma", attack.arma },
                        {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                        {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) }
                    });

                }
                */
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
                    + " arma: " + attack.arma
                    + " CordenadasX: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.x
                    +" CordenadasY: " + GameObject.FindGameObjectWithTag("Enemy").transform.position.y+" >");
                    
                    Analytics.CustomEvent("Morir", new Dictionary<string, object> {
                        {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                        {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                        {"enemigo", attack.creator },
                        {"arma", attack.arma },
                        { "CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
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