using UnityEngine;
using System.Collections;

public class EnemyAnimate : MonoBehaviour {
	public SpriteRenderer torso,legs;
	EnemyAI eai;
	SpriteContainer sc;
	public Sprite[] torsoSpr,attackingSpr,legsSpr;

	float torsoTimer=0.15f,legsTimer=0.15f,legReset=0.15f,torsoReset=0.15f;
	public int tCounter = 0,lCounter = 0;
	string weapon;
	EnemyWeaponController ewc;
	public bool attacking = false;
	// Use this for initialization
	void Start () {
		eai = this.GetComponent<EnemyAI> ();
		sc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<SpriteContainer>();

			torsoSpr = sc.getEnemyWalk ("");
			attackingSpr = sc.getEnemyWeapon ("");

		ewc = this.GetComponent<EnemyWeaponController> ();
		legsSpr = sc.getPlayerLegs ();
	}


	
	// Update is called once per frame
	void Update () {
		if (eai.moving == true) {
			animateLegs ();
		}

		if (attacking == true) {
			animateAttack ();
		} else {
			animateWalk ();
		}
		legResetSpeed ();
		//animateTorso ();
		valueChecks ();
	}
	//add checks for death and counter being above array length
	void animateWalk()
	{
		if (tCounter > torsoSpr.Length - 1) {
			tCounter = 0;
		}
		//Debug.Log ("WALK ANIMATE " + tCounter);
		torsoTimer -= Time.deltaTime;
		if (torsoSpr.Length > 1) {
			torso.sprite = torsoSpr [tCounter];
		} else {
			torso.sprite = torsoSpr [0];
		}

		if(torsoTimer<=0)
		{
			if (tCounter < torsoSpr.Length - 1) {
				tCounter++;
			} else {
				tCounter = 0;
			}
			torsoTimer = torsoReset;
		}
	}

	void animateAttack()
	{
		if (tCounter > attackingSpr.Length - 1) {
			tCounter = 0;
		}

		//Debug.Log ("ATTACK ANIMATE " + tCounter);
		torsoTimer -= Time.deltaTime;
		torso.sprite=attackingSpr[tCounter];
		if(torsoTimer<=0)
		{
			if (tCounter < attackingSpr.Length - 1) {
				tCounter++;
			} else {
				attacking = false;
				tCounter = 0;
			}
			torsoTimer = torsoReset;
		}

	}
		

	void animateLegs()
	{
		legs.sprite=legsSpr[lCounter];
		legsTimer -= Time.deltaTime;

		if (legsTimer <= 0) {
			if (lCounter < legsSpr.Length - 1) {
				lCounter++;
			} else {
				lCounter = 0;
			}

			legsTimer = legReset;
		}

	}

	void legResetSpeed()
	{
		if (eai.getSpeed () > 2.1f) {
			legReset = 0.03f;
			torsoReset = 0.03f;
		} else {
			legReset = 0.05f;
			torsoReset = 0.05f;
		}
	}

	public void setAttacking()
	{
		attacking = true;
	}

	void valueChecks()
	{

		if (this.gameObject.tag == "Dead") {
			legs.enabled = false;
		}

		if (eai.enabled == false) {
			legs.enabled = false;
		} else {
			legs.enabled = true;
		}
	}

	public void setTorsoSpr(string name)
	{
		torsoSpr = sc.getEnemyWalk (name);
		attackingSpr = sc.getEnemyWeapon (name);
		tCounter = 0;
	}
	public void resetCounter()
	{
		tCounter = 0;
	}

	public void disableLegs()
	{
		legs.sprite = null;
		legs.enabled = false;
	}

	public void enableLegs()
	{
		legs.enabled = true;
	}
}
