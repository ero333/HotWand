using UnityEngine;
using System.Collections;

public class WeaponWindowDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.LogError ("Trigga enter " + coll.gameObject.tag);
		if (coll.gameObject.tag == "Wall" && coll.gameObject.GetComponent<Window> () != null) {
			coll.gameObject.GetComponent<Window> ().breakWindow ();
		}
	}
}
