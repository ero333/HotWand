using UnityEngine;
using System.Collections;

public class legDir : MonoBehaviour {
	Vector3 rot;
	// Use this for initialization
	void Start () {
		rot = new Vector3 (0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W))
		{
			rot = new Vector3 (0, 0, 90);
			transform.eulerAngles = rot;
		}

		if(Input.GetKey(KeyCode.S))
		{
			rot = new Vector3 (0, 0, 270);
			transform.eulerAngles = rot;
		}

		if (Input.GetKey(KeyCode.A)) {
			rot = new Vector3 (0, 0, 180);
			transform.eulerAngles = rot;
		}

		if (Input.GetKey (KeyCode.D)) {
			rot = new Vector3 (0, 0, 0);
			transform.eulerAngles = rot;
		}
	}
}
