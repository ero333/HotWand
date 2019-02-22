using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLegs : MonoBehaviour {
	private Animator animator;
	private Transform parentTransform;
	private Rigidbody2D parentRigidbody2D;
	private Vector3 curPos;
	private Vector3 lastPos;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		parentTransform = GetComponentInParent<Transform>();
		parentRigidbody2D = GetComponentInParent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
     curPos = parentTransform.position;
     if(curPos != lastPos) {
         animator.SetBool("Moving", true);
     }
	 else
	 {
		 animator.SetBool("Moving", false);
	 }
     lastPos = curPos;
	}
}
