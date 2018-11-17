using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour {

	public string weaponName;
	public bool equippable;		//If true, this object can be stored in inventory
	public int weaponAmmo;
	public Vector3 direction;
	private Vector2 target;
	public float attackDelay;
	public bool beingThrown;
    
	private Quaternion lastRotation;

	void Start()
	{
		direction.x = Vector2.right.x;
		direction.y = Vector2.right.y;
		direction.z = 0;
		lastRotation = transform.rotation;
	}
			
			
	void Update()
	{		
		if (beingThrown)
		{
			transform.Translate(direction*4*Time.deltaTime);
			//transform.rotation = Quaternion.Slerp(this.transform.rotation,new Quaternion(this.transform.rotation.x,this.transform.rotation.y,this.transform.rotation.z-1,this.transform.rotation.w), Time.deltaTime * 10);
			lastRotation = transform.rotation;
		}
		else
		{
			transform.Translate(new Vector3(0,0,0));
			transform.rotation = lastRotation;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if ((beingThrown) && (other.CompareTag("Enemy"))){
			//beingThrown = false;
			other.SendMessage("TakeDamage", new Attack(1, other.name), SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}
		else
		if ((beingThrown) && (other.CompareTag("Wall"))){
			//beingThrown = false;
			Destroy(this.gameObject);
		}
	}
	public void PickedUp(){
		//Picked up and put in inventory
		gameObject.SetActive (false);
	}
}
