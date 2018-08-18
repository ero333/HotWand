using UnityEngine;
using System.Collections;

public class HeavyAttack : MonoBehaviour {
	public GameObject blood;
	float timer = 0.1f,timerReset=0.1f;

	SpriteContainer sc;



	GameObject player;

	bool attacking = false;
	SpriteRenderer sr;
	HeavyAnimate ea;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		ea = this.GetComponent<HeavyAnimate> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerHealth.dead == false) {//new for 10
			if (timer > 0) {
				timer -= Time.deltaTime;
			}


			if (timer <= 0 && Vector3.Distance (this.transform.position, player.transform.position) <= 1.6f) {
				if (ea.tCounter == ea.attackingSpr.Length - 1) {
					attack ();
				}
				ea.setAttacking ();
			}
		}
	}

	public void attack()
	{
			int layerMask = 1<<8;
			layerMask = ~layerMask;
			//pa.attack ();
			RaycastHit2D ray = Physics2D.Raycast (new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(transform.right.x,transform.right.y),1.5f,layerMask);
			Debug.DrawRay (new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(transform.right.x,transform.right.y),Color.green);
			Debug.Log ("Attempting melee attack");


			if (ray.collider.gameObject.tag=="Player") {
				PlayerHealth.dead = true;
				Instantiate (blood, player.transform.position, player.transform.rotation);
				this.GetComponent<AudioController> ().meleeAttack ();
			} 

			timer = timerReset;
	}


}
