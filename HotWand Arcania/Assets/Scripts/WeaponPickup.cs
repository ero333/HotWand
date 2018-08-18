using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
	public string name;
	public float fireRate;
	WeaponAttack wa;
	public bool gun,oneHanded,shotugn;
	public int ammo;//NEW STUFF FOR 16
	//public AudioClip sfx;

	// Use this for initialization
	void Start () {
		wa = GameObject.FindGameObjectWithTag ("Player").GetComponent<WeaponAttack> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D coll) {
		//Debug.Log ("Collision");
		if (coll.gameObject.tag == "Player" && Input.GetMouseButtonDown(1)){
			//code to add weapon to player
			Debug.Log("Player picked up: " + name);
			if (wa.getCur () != null) {
				wa.dropWeapon ();
			}
			wa.setWeapon (this.gameObject,name,fireRate,gun,oneHanded,shotugn);//added one handed
			//Destroy (this.gameObject);
			this.gameObject.SetActive (false);
		}
		else if(coll.gameObject.tag=="Enemy" && coll.gameObject.GetComponent<EnemyWeaponController>().getCur()==null)//New for tut 7
		{
			Debug.Log("Enemy picked up: " + name);

			EnemyWeaponController ewc = coll.gameObject.GetComponent<EnemyWeaponController> ();
			ewc.setWeapon (this.gameObject,name,fireRate,gun,oneHanded,shotugn);//added one handed
			//Destroy (this.gameObject);
			this.gameObject.SetActive (false);
		}
	}
}
