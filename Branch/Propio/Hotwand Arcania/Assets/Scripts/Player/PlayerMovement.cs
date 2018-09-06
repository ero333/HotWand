using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public  bool moving = true;
	public float speed = 5.0f;

	public Animator animator;
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moving==true)
		{
			movement ();
			animator.SetBool("Moving", true);
		}
		movementCheck ();
	}

	public void setMoving(bool val)
	{
		moving = val;
	}

	void movement()
	{
		if(Input.GetKey(KeyCode.W))
		{
			transform.Translate (Vector3.up * speed * Time.deltaTime,Space.World);
		}

		if(Input.GetKey(KeyCode.S))
		{
			transform.Translate (Vector3.down * speed * Time.deltaTime,Space.World);
		}

		if (Input.GetKey(KeyCode.A)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime,Space.World);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime,Space.World);
		}


	}

	void movementCheck()//added in ep 2, talk about changes and why
	{
		if (Input.GetKey (KeyCode.D) != true && Input.GetKey (KeyCode.A) != true && Input.GetKey (KeyCode.S) != true && Input.GetKey (KeyCode.W) != true) {
			moving = false;
			animator.SetBool("Moving", false);
		} else {
			moving = true;
		}
	}
}
