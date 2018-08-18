using UnityEngine;
using System.Collections;

public class NonAstarVersion : MonoBehaviour {

	GameObject player;
	public bool patrol = true, gaurd = false,clockwise=false;
	public bool moving = true;
	public bool pursuingPlayer = false, goingToLastLoc=false;
	Vector3 target;
	Rigidbody2D rid;
	public Vector3 playerLastPos;
	RaycastHit2D hit;
	float speed = 2.0f; //changed bullets to be kenimatic
	int layerMask = 1<<8; //explain layermask for tutorial (how it works + changes to weapon attack)




	ObjectManager obj;
	GameObject[] weapons;
	EnemyWeaponController ewc;
	public GameObject weaponToGoTo;
	public bool goingToWeapon = false;
	public bool hasGun=false;//new for enemy weapons



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerLastPos = this.transform.position;
		obj = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ObjectManager> ();//new 7
		ewc = this.GetComponent<EnemyWeaponController> ();//new 7
		rid = this.GetComponent<Rigidbody2D> ();
		layerMask = ~layerMask;

	}

	void Update () {



		if (PlayerHealth.dead == false) {
			movement ();
			playerDetect ();
			canEnemyFindWeapon ();
		} else {
			this.GetComponent<EnemyAnimate> ().enabled = false;
			this.enabled = false;
		}



	}

	void movement()
	{
		float dist = Vector3.Distance (player.transform.position,this.transform.position);
		Vector3 dir = player.transform.position - transform.position;
		hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x, dir.y),dist,layerMask);
		Debug.DrawRay(transform.position, dir, Color.red);

		Vector3 fwt = this.transform.TransformDirection(Vector3.right);

		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y), 1.0f,layerMask);

		Debug.DrawRay (new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(fwt.x, fwt.y),Color.cyan);

		if(moving==true)//new for tut 7
		{
			if (hasGun == false) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} else {
				if (Vector3.Distance (this.transform.position, player.transform.position) < 5 && pursuingPlayer == true) {//new enemy weapon

				} else {
					transform.Translate (Vector3.right * speed * Time.deltaTime);
				}
			}
		}


		if(patrol==true)
		{
			//Debug.Log ("Patrolling normally");
			speed = 2.0f;

			if (hit2.collider != null)
			{
				// Debug.LogError(hit2.collider.tag);
				if (hit2.collider.gameObject.tag == "Wall")
				{
					//Quaternion rot = this.transform.rotation;

					if (clockwise == false)
					{
						transform.Rotate(0, 0, 90);
					}
					else
					{
						transform.Rotate(0, 0, -90);
					}
				}

			}

			if (weaponToGoTo != null) {
				patrol = false;
				goingToWeapon = true;
			}


		}

		if(pursuingPlayer==true)
		{
			//transform.Translate(Vector3.right * speed * Time.deltaTime);
			//Debug.Log("Pursuing Player");
			speed=3.5f;
			rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);


			if (hit.collider.gameObject.tag == "Player") {
				playerLastPos = player.transform.position;
			} 



		}

		if(goingToLastLoc==true)
		{
			//Debug.Log ("Checking last known player location");
			speed = 3.0f;
			//	transform.Translate(Vector3.right * 4 * Time.deltaTime);
			rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
			if (Vector3.Distance (this.transform.position, playerLastPos) < 1.5f) {
				//not found player, return to patrol
				patrol=true;
				goingToLastLoc = false;
			}



		}
		//&& pursuingPlayer==false && goingToLastLoc==false && patrol==false
		if(goingToWeapon==true )
		{
			//Debug.Log("going weapon");
			speed = 3.0f;
			//	transform.Translate(Vector3d.right * 4 * Time.deltaTime);
			rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((weaponToGoTo.transform.position.y - transform.position.y), (weaponToGoTo.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);

			if (ewc.getCur()!=null) {
				//not found player, return to patrol
				weaponToGoTo=null;
				patrol=true;
				goingToWeapon = false;
				pursuingPlayer = false;
				goingToLastLoc = false;
			}

			if (weaponToGoTo == null) { //re wrote this to fix bugs
				weaponToGoTo=null;
				patrol=true;
				goingToWeapon = false;
				pursuingPlayer = false;
				goingToLastLoc = false;
			} else {
				if(weaponToGoTo.active==false)
				{
					weaponToGoTo=null;
					patrol=true;
					goingToWeapon = false;
					pursuingPlayer = false;
					goingToLastLoc = false;
				}

			}


		}
	}

	void setWeaponToGoTo(GameObject weapon)
	{
		//Debug.Log ("Set weapon to go to");
		weaponToGoTo = weapon;
		goingToWeapon = true;
		patrol = false;
		pursuingPlayer = false;
		goingToLastLoc = false;

	}

	void canEnemyFindWeapon()
	{
		if (ewc.getCur()==null && weaponToGoTo==null&&goingToWeapon==false) {
			weapons = obj.getWeapons ();
			for(int x = 0;x<weapons.Length;x++)
			{
				float distance = Vector3.Distance (this.transform.position, weapons [x].transform.position);
				//Debug.Log ("Weapon " + weapons[x].name + " Distance " + distance );
				if (distance < 10) {
					Vector3 dir = weapons[x].transform.position - transform.position;
					RaycastHit2D wepCheck = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x,dir.y ), distance,layerMask);
					//Debug.DrawRay(transform.position, dir, Color.magenta);

					if (wepCheck.collider.gameObject.tag == "Weapon") {
						setWeaponToGoTo (weapons [x]);
					}


				}
			}
		}
	}

	public void playerDetect()
	{
		Vector3 pos = this.transform.InverseTransformPoint(player.transform.position);
		//Debug.Log (pos.x);//more than 1.2


		if(hit.collider!=null)
		{
			//			 Debug.LogError(hit.collider.tag);
			if (hit.collider.gameObject.tag == "Player" && pos.x > 1.2f && Vector3.Distance(this.transform.position,player.transform.position)<9) {
				//playerLastPos = player.transform.position;
				patrol=false;
				pursuingPlayer = true;
				goingToWeapon = false;
			} else {
				if(pursuingPlayer==true)
				{
					goingToLastLoc = true;
					pursuingPlayer = false;
					goingToWeapon = false;
				}
				//pursuingPlayer = false;
			}
		}
	}

	public float getSpeed()//new for enemy animate
	{
		return speed;
	}


}
