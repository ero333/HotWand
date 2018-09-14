using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothTime = 0.3F;
	public float posY;
	private Vector3 velocity = Vector3.zero;

	void Start(){
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
	}
	void Update () {
		Vector3 targetPosition = target.TransformPoint(new Vector3(0, posY, -10));
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);	
	}
}
