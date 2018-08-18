using UnityEngine;
using System.Collections;

public class DogHealth : MonoBehaviour {
	public Sprite dead;
	public GameObject bloodPool;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void killDog()
	{
		this.gameObject.tag = "Dead";//added for 20
		this.GetComponent<BoxCollider2D> ().enabled = false;
		this.GetComponent<DogAI> ().enabled = false;
		this.GetComponent<DogAnimate> ().enabled = false;
		this.GetComponent<SpriteRenderer> ().sprite = dead;
		this.GetComponent<AudioSource> ().enabled = false;
		this.enabled = false;
		Instantiate (bloodPool, this.transform.position, this.transform.rotation);
	}
}
