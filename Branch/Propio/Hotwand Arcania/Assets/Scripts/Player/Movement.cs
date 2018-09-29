using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed;
	public Animator animator;

	// Update is called once per frame
	void Update () {
		{
			float horizontal = Input.GetAxisRaw ("Horizontal");
			float vertical	 = Input.GetAxisRaw ("Vertical");
			bool moving = false;

			//Moving the character
			transform.Translate(horizontal * Time.deltaTime * speed, vertical * Time.deltaTime * speed, 0f, Space.World);


			//Tell the animator which animation to play
			if ((Mathf.Abs(horizontal) + Mathf.Abs(vertical)) > 0){
				moving = true;
			}
			else
			{
				moving = false;
			}
			
			//Animate the player walking
			animator.SetBool("Moving", moving);
		}
	}
}
