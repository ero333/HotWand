using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	GameObject player;
	PlayerMovement pm;
	public bool followPlayer = true;
	Vector3 mousePos;
	Camera cam;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pm = player.GetComponent<PlayerMovement> ();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			followPlayer = false;
			pm.setMoving (false);
		} else {
			followPlayer = true;
		}

		if (followPlayer == true) {
			camFollowPlayer ();
		} else {
			lookAhead ();
		}
	}

	public void setFollowPlayer(bool val)
	{
		followPlayer = val;
	}

	void camFollowPlayer()
	{
		Vector3 newPos = new Vector3 (player.transform.position.x, player.transform.position.y, this.transform.position.z);
		this.transform.position = newPos;
	}

	void lookAhead()
	{

		Vector3 camPos = cam.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y));
		camPos.z = -10;
		Vector3 dir = camPos-this.transform.position;
		if (player.GetComponent<SpriteRenderer> ().isVisible  == true) {
			transform.Translate(dir*2*Time.deltaTime);
		}
	}
}
