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

		/////////////Knocked Logic
		if (knocked)
		{
            Analytics.CustomEvent("Noquear", new Dictionary<string, object>
              {
                    {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                    {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                });
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
	public void TakeDamage(int damage)
	{
		if (health <= 0){

			anim.SetBool("Dead", true);
			dead = true;
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().muertes+=1;
            Analytics.CustomEvent("Morir", new Dictionary<string, object> {
                        {"nivel", (GameObject.FindGameObjectWithTag("Portal").GetComponent<NextLevel>().nextLevel - 1)},
                        {"tiempo", (Time.time - GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().tiempoLevel) },
                        {"CordenadasX", ( GameObject.FindGameObjectWithTag("Enemy").transform.position.x) },
                        {"CordenadasY",  (GameObject.FindGameObjectWithTag("Enemy").transform.position.y) },
                    });
        }
        else
		if (health > 0)
		{
			health -= damage;
			knocked = true;
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