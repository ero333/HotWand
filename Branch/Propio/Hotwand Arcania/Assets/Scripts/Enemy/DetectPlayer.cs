using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectPlayer : MonoBehaviour
{
	public bool playerInRange;
	public float viewAngle;
	public GameObject player;
    public Transform target;
	private Animator animator;

	public LayerMask layersIWant;
	 void Start()
	 {
		playerInRange = false;
	 	animator = gameObject.GetComponent<Animator>();
	 }
    void Update()
    {
			Vector3 dirToTarget = target.transform.position - transform.position;
            Vector3 C = Quaternion.AngleAxis(90 + viewAngle / 2, -transform.forward) * -transform.right;
            if (SignedAngleBetween(transform.up, dirToTarget, transform.up) <= SignedAngleBetween(transform.up, C, transform.up) || viewAngle == 360)
            {
				RaycastHit2D enemySight = Physics2D.Raycast(transform.position, dirToTarget, 15.0f, layersIWant);

				if (enemySight.collider != null)
				{
					if (enemySight.collider.name == "Player")
					{
						playerInRange = true;
						animator.SetBool("Player Detected", true);
						Debug.DrawRay(transform.position, dirToTarget, Color.green);
					}
					else
					{
						playerInRange = false;
						animator.SetBool("Player Detected", false);
						Debug.DrawRay(transform.position, dirToTarget, Color.red);
					}
				}
				else
				{
					playerInRange = false;
					animator.SetBool("Player Detected", false);
					Debug.DrawRay(transform.position, dirToTarget, Color.red);
				}
			}
			else
			{
				playerInRange = false;
				animator.SetBool("Player Detected", false);
				Debug.DrawRay(transform.position, dirToTarget, Color.red);
			}

    }

	float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
	{
		float angle = Vector3.Angle(a, b);                                  // angle in [0,180]
		float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));       //Cross for testing -1, 0, 1
		float signed_angle = angle * sign;                                  // angle in [-179,180]
		float angle360 =  (signed_angle + 360) % 360;                       // angle in [0,360]
		return (angle360);
	}
}