using UnityEngine;
using System.Collections;

public class HeavyAI : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerLastPos = this.transform.position;
		obj = GameObject.FindGameObjectWithTag ("GameController").GetComponent<ObjectManager> ();
		rid = this.GetComponent<Rigidbody2D> ();
		layerMask = ~layerMask;

	}

	void Update () {

		if (PlayerHealth.dead == false) {
			movement ();
			playerDetect ();

		} else {
			this.GetComponent<HeavyAnimate> ().enabled = false;
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
			if (Vector3.Distance (this.transform.position, player.transform.position) > 1) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} else {
				
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

			/*if (weaponToGoTo != null) {
				patrol = false;
				goingToWeapon = true;
			}*/


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

			} else {
				if(pursuingPlayer==true)
				{
					goingToLastLoc = true;
					pursuingPlayer = false;

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
