using UnityEngine;
using System.Collections;

public class DoorMovement : MonoBehaviour {

	public float startRot = 0;
	float timer=0.01f;
	public int mod = 3;
	public bool beingOpened=false;
	//increasing the rotation on the z axis makes the door move anticlockwise
	void awake()
	{
		

	}
	// Use this for initialization
	void Start () {
		startRot = this.transform.eulerAngles.z;

	}
	
	// Update is called once per frame
	void Update () {
		/*if (this.transform.eulerAngles.z > startRot && beingOpened==false) {
			rotateAntiClockwiseMethod ();
		} else if(this.transform.eulerAngles.z < startRot && beingOpened==false){
			rotateClockwiseMethod ();
		}*/
	}

	public void rotateClockwiseMethod()
	{
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Vector3 rot = new Vector3 (0, 0, 0);
			float z = this.transform.eulerAngles.z;
			rot.z = z+= mod;
			this.transform.eulerAngles = rot;
			timer = 0.01f;


		}
	}

	public void rotateAntiClockwiseMethod()
	{
		timer -= Time.deltaTime;
		if (timer <= 0) {
			Vector3 rot = new Vector3 (0, 0, 0);
			float z = this.transform.eulerAngles.z;
			rot.z = z-= mod;
			this.transform.eulerAngles = rot;
			timer = 0.01f;


		}
	}




}
