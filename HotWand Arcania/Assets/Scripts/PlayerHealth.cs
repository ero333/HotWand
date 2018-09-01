using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	public static bool dead=false;

	float originalWidth = 1920.0f; //turn these to floats to fix placement issue
	float originalHeight = 1080.0f;
	Vector3 scale;
	public GUIStyle text;
	public Texture2D bg;
	public Sprite deadSpr;

	void awake()
	{
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (dead == true) {
			killPlayer ();

			if (Input.GetKeyDown (KeyCode.R)) {
				
				revivePlayer ();

				SceneManager.LoadScene (SceneManager.GetActiveScene().name);//remember to mention new scene manager using thing

			

			}
		}
	}

	void killPlayer()
	{
		if (this.GetComponent<PlayerAnimate> ().enabled == true) {
			PlayerAnimate pa = this.GetComponent<PlayerAnimate> ();
			PlayerMovement pm = this.GetComponent<PlayerMovement> ();
			RotateToCursor rot = this.GetComponent<RotateToCursor> ();
			WeaponAttack wa = this.GetComponent<WeaponAttack> ();
			legDir ld = this.GetComponentInChildren<legDir> ();
			wa.dropWeapon ();
			pa.legs.sprite = null;
			pa.legs.enabled = false;
			ld.enabled = false;

			pa.torso.sprite = deadSpr;
			pa.enabled = false;

			rot.enabled = false;
			wa.enabled = false;

			pm.enabled = false;
			CircleCollider2D col = this.GetComponent<CircleCollider2D> ();
			col.enabled = false;
		}
	}

	void revivePlayer(){
		PlayerAnimate pa = this.GetComponent<PlayerAnimate> ();
		PlayerMovement pm = this.GetComponent<PlayerMovement> ();
		RotateToCursor rot = this.GetComponent<RotateToCursor> ();
		WeaponAttack wa = this.GetComponent<WeaponAttack> ();
		legDir ld = this.GetComponentInChildren<legDir> ();
		wa.dropWeapon ();
		pa.legs.enabled = true;
		ld.enabled = true;

		pa.enabled = true;

		rot.enabled = true;
		wa.enabled = true;

		pm.enabled = true;
		CircleCollider2D col = this.GetComponent<CircleCollider2D> ();
		col.enabled = true;
		dead = false;
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
			Rect posForRestart = new Rect (100,originalHeight-200,500,150);
			GUI.DrawTexture (posForRestart,bg);
			GUI.Box (posForRestart,"Presione 'R' para re-iniciar nivel",text);
		}

		GUI.matrix = svMat;
	}
}
