using UnityEngine;
using System.Collections;

public class WeaponAttack : MonoBehaviour {
	public GameObject oneHandSpawn,twoHandSpawn,bullet,shotgunBullet;
	GameObject curWeapon;
	public bool gun = false;
	float timer = 0.1f,timerReset=0.1f;
	PlayerAnimate pa;
	SpriteContainer sc;

	float weaponChange = 0.5f;
	bool changingWeapon = false;
	bool oneHanded = false;



	//NEW STUFF FOR 16
	bool Shotgun = false;

	public GUIStyle text;
	float originalWidth = 1920.0f;
	float originalHeight = 1080.0f;
	Vector3 scale;
	public Texture2D bg;
	public WeaponPickup curWepScr;

	void Start () {
		pa = this.GetComponent<PlayerAnimate> ();
		sc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SpriteContainer> ();
	}
	

	void Update () {
		

		if (timer > 0) {
			timer -= Time.deltaTime;
		}

		if(Input.GetMouseButton(0) && timer<=0)
		{
			attack ();
		}
		if(Input.GetMouseButtonDown(0))
		{
			pa.resetCounter ();
		}
		if (Input.GetMouseButtonUp (0)) {
			pa.resetCounter ();
		}
		if (curWeapon == null) {

		} else {
			if (curWeapon.activeInHierarchy == false) {
				if (Input.GetMouseButtonDown (1) && changingWeapon == false) {
					dropWeapon ();
				}
			}
		}

		if(changingWeapon==true)
		{
			weaponChange -= Time.deltaTime;
			if(weaponChange<=0)
			{
				changingWeapon = false;
			}
		}
	}

	public void setWeapon(GameObject cur, string name, float fireRate,bool gun,bool oneHanded,bool shotgun)
	{
		this.GetComponent<AudioController> ().pickupWeapon ();
		changingWeapon = true;

		pa.setNewTorso (sc.getWeaponWalk(name),sc.getWeapon(name));
		this.gun = gun;
		timerReset = fireRate;
		timer = timerReset;
		this.oneHanded = oneHanded;
		Shotgun = shotgun;

		//changed to get rid of null reference errors
		if (cur == null) {

		} else {
			curWeapon = cur;
			curWepScr = curWeapon.GetComponent<WeaponPickup> ();//NEW STUFF FOR 16

		}
	}

	void decideSFX()
	{
		if (gun == true) {
			if (Shotgun == true) {
				this.GetComponent<AudioController> ().fireShotgun ();
			} else {
				this.GetComponent<AudioController> ().fireSmg ();
			}
		} else {
			this.GetComponent<AudioController> ().meleeAttack ();
		}
	}

	public void attack()
	{
		
		if (gun == true && curWepScr.ammo > 0) {//NEW STUFF FOR 16
			pa.attack ();
			Bullet bl = bullet.GetComponent<Bullet> ();
			Vector3 dir;
			dir.x = Vector2.right.x;
			dir.y = Vector2.right.y;
			dir.z = 0;
			bl.setVals (dir,"Player");

				if (oneHanded == true) {
					if (Shotgun == false) {//new for new weapons
						Instantiate (bullet, oneHandSpawn.transform.position, this.transform.rotation);
					} else {
						Instantiate (shotgunBullet, oneHandSpawn.transform.position, this.transform.rotation);
					}
					curWeapon.GetComponent<WeaponPickup> ().ammo--;
					FindObjectOfType<LevelEscapeController> ().shotFired ();
				} else {
					if (Shotgun == false) {//new for new weapons
						Instantiate (bullet, twoHandSpawn.transform.position, this.transform.rotation);
					} else {
						Instantiate (shotgunBullet, twoHandSpawn.transform.position, this.transform.rotation);
					}
					curWeapon.GetComponent<WeaponPickup> ().ammo--;
					FindObjectOfType<LevelEscapeController> ().shotFired ();
				}
			decideSFX ();
			timer = timerReset;

			//if (Input.GetMouseButtonUp (0)) {
				//pa.resetCounter ();
			//}

		} else if(gun == true && curWepScr.ammo == 0)
		{
			//NEW STUFF FOR 16
		}
		else{
			
			pa.attack ();//NEW STUFF FOR 16
			//melee attack
			int layerMask = 1<<9;
			layerMask = ~layerMask;
			pa.attack ();
			RaycastHit2D ray = Physics2D.Raycast (new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(transform.right.x,transform.right.y),1.5f,layerMask);
			Debug.DrawRay (new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(transform.right.x,transform.right.y),Color.green);

			if (ray.collider == null) {

			} else {
				if (curWeapon == null && ray.collider.gameObject.tag == "Enemy") {

					if (ray.collider.isTrigger == true && ray.collider.gameObject.tag == "Enemy") {//new for execute
						ray.collider.gameObject.GetComponent<EnemyAttacked> ().execute ();
						decideSFX ();
					} else {
						EnemyAttacked ea = ray.collider.gameObject.GetComponent<EnemyAttacked> ();
						ea.knockDownEnemy ();
						decideSFX ();
					}
				} else if (curWeapon == null && ray.collider.gameObject.tag == "Dog") {////
					ray.collider.gameObject.GetComponent<DogHealth> ().killDog ();//
				}//
				else if (curWeapon == null && ray.collider.gameObject.tag == "Wall" && ray.collider.gameObject.GetComponent<Window> () != null) {
					ray.collider.gameObject.GetComponent<Window> ().breakWindow ();
				} else if (ray.collider != null) {
					Debug.Log (ray.collider.gameObject.tag);
					if (ray.collider.gameObject.tag == "Enemy") {
						
						if (ray.collider.isTrigger == true && ray.collider.gameObject.tag == "Enemy") {
							ray.collider.gameObject.GetComponent<EnemyAttacked> ().execute ();
						} else {
							EnemyAttacked ea = ray.collider.gameObject.GetComponent<EnemyAttacked> ();
							ea.killMelee ();
							decideSFX ();
						}
					} else if (ray.collider.gameObject.tag == "Dog") {
						ray.collider.gameObject.GetComponent<DogHealth> ().killDog ();
					}

					if (ray.collider.gameObject.tag == "Wall" && ray.collider.gameObject.GetComponent<Window> () != null) {
						ray.collider.gameObject.GetComponent<Window> ().breakWindow ();
					}
				}
			}
			timer = timerReset;
		}


	}

	public GameObject getCur()
	{
		return curWeapon;
	}

	public void dropWeapon()
	{
		if (curWeapon == null) {

		} else {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0));
			curWeapon.AddComponent<ThrowWeapon> ();
			Vector3 dir;
			dir.x = mousePos.x - this.transform.position.x;
			dir.y = mousePos.y - this.transform.position.y;
			dir.z = 0;
			curWeapon.GetComponent<Rigidbody2D> ().isKinematic = false;
			curWeapon.GetComponent<ThrowWeapon> ().setDirection (dir);
			curWeapon.transform.position = oneHandSpawn.transform.position;
			curWeapon.transform.eulerAngles = this.transform.eulerAngles;
			curWeapon.SetActive (true);
			setWeapon (null, "", 0.5f, false,false,false);
			pa.resetSprites ();
		}

	}

	void OnGUI() //NEW STUFF FOR 16
	{
		GUI.depth = 0;
		scale.x = Screen.width/originalWidth;
		scale.y = Screen.height/originalHeight;
		scale.z =1;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero,Quaternion.identity,scale);

		if (gun == true && CutsceneDisplay.anyCutsceneDisplaying==false) {//new condition for 20/21?
			Rect posForAmmo = new Rect (originalWidth-535,originalHeight-200,300,150);
			GUI.DrawTexture (posForAmmo, bg);
			posForAmmo = new Rect (originalWidth-500,originalHeight-150,300,150);
			GUI.Box (posForAmmo, "Ammo: " + curWeapon.GetComponent<WeaponPickup> ().ammo, text);
		}

		GUI.matrix = svMat;
	}
}
