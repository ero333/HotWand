using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	GameObject player;
	public bool patrol = true, gaurd = false,clockwise=false;
	public bool moving = true;
	public bool pursuingPlayer = false, goingToLastLoc=false;
	Vector3 target;
	Rigidbody2D rid;
	public Vector3 playerLastPos;
	RaycastHit2D hit;
	float speed = 2.0f; 
	int layerMask = 1<<8;




	ObjectManager obj;
	GameObject[] weapons;
	EnemyWeaponController ewc;
	public GameObject weaponToGoTo;
	public bool goingToWeapon = false;
	public bool hasGun=false;



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerLastPos = this.transform.position;
		obj = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ObjectManager> ();
		ewc = this.GetComponent<EnemyWeaponController> ();
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

		if(moving==true)
		{
			if (hasGun == false) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} else {
				if (Vector3.Distance (this.transform.position, player.transform.position) < 5 && pursuingPlayer == true) {

				} else {
					transform.Translate (Vector3.right * speed * Time.deltaTime);
				}
			}
		}


		if(patrol==true)
		{
			
			speed = 2.0f;

			if (hit2.collider != null)
			{
				
				if (hit2.collider.gameObject.tag == "Wall")
				{

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
			speed=3.5f;
			rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);


			if (hit.collider.gameObject.tag == "Player") {
				playerLastPos = player.transform.position;
			} 



		}

		if(goingToLastLoc==true)
		{
			speed = 3.0f;
			rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((playerLastPos.y - transform.position.y), (playerLastPos.x - transform.position.x)) * Mathf.Rad2Deg);
			if (Vector3.Distance (this.transform.position, playerLastPos) < 1.5f) {
				patrol=true;
				goingToLastLoc = false;
			}



		}
		if(goingToWeapon==true )
		{
			speed = 3.0f;
			rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((weaponToGoTo.transform.position.y - transform.position.y), (weaponToGoTo.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);

			if (ewc.getCur()!=null) {
				weaponToGoTo=null;
				patrol=true;
				goingToWeapon = false;
				pursuingPlayer = false;
				goingToLastLoc = false;
			}

			if (weaponToGoTo == null) {
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
			
				if (distance < 10) {
					Vector3 dir = weapons[x].transform.position - transform.position;
					RaycastHit2D wepCheck = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(dir.x,dir.y ), distance,layerMask);
				
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


		if(hit.collider!=null)
		{
			if (hit.collider.gameObject.tag == "Player" && pos.x > 1.2f && Vector3.Distance(this.transform.position,player.transform.position)<9) {
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
			}
		}
	}

	public float getSpeed()
	{
		return speed;
	}



}
