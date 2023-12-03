using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Unit : MonoBehaviour {


	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;
	public Transform target;
	public float speed = 20;
	float turnSpeed=20;
	float turnDst=0;
	float stoppingDst = 2;
	public Transform[] waypoints;
	public GameObject player;
	public GameObject[] enemies;
	Path path;
	int currentIndex = 0;
	bool stuned = false;
	bool debug_recentstun=false;
    public AudioSource audio;
	public bool warned = false;
	public List<GameObject> warnedEnemies;
	bool chasing = false;
	public Transform hijo;

	void Awake() {
       
    }
	void Start(){
		//PathRequestManager.RequestPath(transform.position, waypoints[currentIndex].position, OnPathFound);
		StartCoroutine (UpdatePath ());
	}

    private void Update() {

        Ray ray = new Ray(transform.position + new Vector3(0, 100, 0), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            var distanceToGround = hit.distance;
            Debug.Log(distanceToGround);
            hijo.position = new Vector3(hijo.position.x, transform.position.y+100-distanceToGround+1, hijo.position.z);
        }

        /*RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0,-1,0), -Vector3.up, out hit))
        {
            var distanceToGround = hit.distance;
			Debug.Log(distanceToGround);
			hijo.position = new Vector3(hijo.position.x, distanceToGround-1, hijo.position.z);
        }*/

        if (!stuned)
		{
			MoveEnemy();
			//debug
			if(debug_recentstun){
				debug_recentstun=false;
				target = waypoints[currentIndex];
			}
		}
	}

	public void ChangeTarget(Transform newTarget) {
		target = newTarget;
    }

	private void MoveEnemy()
	{
		if (warned || Vector3.Distance(transform.position, player.transform.position) < 8)
		{
			chasing = true;
			target = player.transform;
			if (!warned)
			{
				for (int i = 0; i < enemies.Length; i++)
				{
					if (Vector3.Distance(transform.position, enemies[i].transform.position) < 15)
					{
						enemies[i].GetComponent<Unit>().ChangeTarget(player.transform);
						if (!warnedEnemies.Contains(enemies[i]))
						{
							enemies[i].GetComponent<Unit>().warned = true;
							warnedEnemies.Add(enemies[i]);
						}
					}
				}
			}
		}
		else
		{
			foreach (GameObject enemy in warnedEnemies)
			{
				enemy.GetComponent<Unit>().warned = false;
			}
			warnedEnemies.Clear();
			if (chasing || (waypoints.Length > 0 && Vector3.Distance(transform.position, waypoints[currentIndex].position) < 1.9f))
			{
				chasing = false;
				if (currentIndex + 1 >= waypoints.Length)
				{
					currentIndex = 0;
				}
				else
				{
					currentIndex++;
				}
				target = waypoints[currentIndex];
			}
		}
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
	IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < 0.3f) {
			yield return new WaitForSeconds (0.3f);
		}
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath() {
		bool followingPath = false;
		int pathIndex = 0;
		if(pathIndex<path.lookPoints.Length){
			followingPath = true;
			transform.LookAt (path.lookPoints [0]);
		}


		float speedPercent = 1;

		while (followingPath) {
			if(pathIndex<path.lookPoints.Length){
				Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
				while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
					if (pathIndex == path.finishLineIndex) {
						followingPath = false;
						break;
					} else {
						pathIndex++;
					}
				}

				if (followingPath) {

					if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
						speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
						if (speedPercent < 0.01f) {
							followingPath = false;
						}
					}
					Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
					transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
					transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
				}
				yield return null;
			}

		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos();
			
		}
	}

	public void stun() {
		target = transform;
		stuned = true;
        audio.Play();

    }

	public void stunend() {
        Invoke("unstun", 5.0f);
		
    }

	public void unstun() {

		stuned = false;
		debug_recentstun=true;
		Debug.Log("cafetera");
	}
}