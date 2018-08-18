using UnityEngine;
using System.Collections;
//using Pathfinding;
public class AIPathFind : MonoBehaviour {
	/*public Transform target;
	public float distanceToTarget;
	// How many times each second we will update our path
	public float updateRate = 5f;
	float speed1 = 1.5f;
	// Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;
	//The AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;
	Vector3 nextPoint;
	// The max distance from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistance = 0.7f;

	// The waypoint we are currently moving towards
	private int currentWaypoint = 0;
	GameObject player;

	public bool alerted = false;

	public bool stopped = false;
	void Start () {
		
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();


		// Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}



	IEnumerator UpdatePath () {
		if (target == null) {
			//TODO: Insert a player search here.
			return false;
		}

		// Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);
		yield return new WaitForSeconds ( 10f/updateRate );
		StartCoroutine (UpdatePath());
	}
		
	public void stopPathfinding()
	{
		StopCoroutine(UpdatePath());
	}

	public Vector3 returnNextPoint()
	{
		return nextPoint;
	}

	public void OnPathComplete (Path p) {
		//Debug.Log ("We got a path. Did it have an error? " + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void Update () {

		if (target == null) {
			//emergancyFindTarget ();
		}
			

		distanceToTarget = Vector3.Distance (this.transform.position, target.position);

		if (target == null) {
			//	target = GameObject.FindGameObjectWithTag ("MichaelTorso").transform;
			return;
		}

		//TODO: Always look at player?

		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;

			//Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;


		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;

		//Move the AI
		if (target.gameObject != this.gameObject) {
			this.transform.position += dir * Time.deltaTime * speed1;
		}


		if (currentWaypoint + 1 < path.GetTotalLength ()) {
			nextPoint = path.vectorPath [currentWaypoint + 1];
		} else {
			nextPoint = path.vectorPath [currentWaypoint];
		}

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		//cd.setObjectToFace(path.vectorPath[currentWaypoint]);
		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}*/
}
