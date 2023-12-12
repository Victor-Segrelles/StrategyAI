using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using System.Collections.Generic;

public class Unit : MonoBehaviour {


	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;
	public Transform target;
	private Transform targetaux;
	public float speed = 20;
	float turnSpeed=20;
	float turnDst=0;
	float stoppingDst = 2;
	public Transform[] waypoints;
	
	
	Path path;
	int currentIndex = 0;
	bool stuned = false;
	bool debug_recentstun=false;
    public AudioSource audio;
	public bool warned = false;
	
	bool chasing = false;
	public Transform hijo;
	public LayerMask ignored;
	public int heightoffset;
	MageBT mageBT;
	public int life;
    public Healthbar healthbar;
    public GameMaster gm;
	public float attackRange = 15;
	public Character attackTarget;
	public bool moved = false;
	public bool myturn=false;
	public bool isAI;
	public Vector3 lastPosition;
	public float moveDistance;
	public Character actionTarget;
	public int selectedAction;  //Desde aquí se llamará a character
	public Transform AOETarget;


	void Awake() {
       
    }
	void Start(){
		moveDistance=10f;
		gm = FindObjectOfType<GameMaster>();
		//PathRequestManager.RequestPath(transform.position, waypoints[currentIndex].position, OnPathFound);
		StartCoroutine (UpdatePath ());
		target=transform;
		targetaux=target;
		lastPosition=transform.position;
        healthbar.UpdateBar(life, 100);

    }

    private void Update() {


		Movement();
		//Action();


		Ray ray = new Ray(transform.position + new Vector3(0, 100, 0), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ~ignored))
        {
            var distanceToGround = hit.distance;
            //Debug.Log(distanceToGround);
            hijo.position = new Vector3(hijo.position.x, transform.position.y+100-distanceToGround+heightoffset, hijo.position.z);
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
			
			//debug
			if(debug_recentstun){
				debug_recentstun=false;
				target = waypoints[currentIndex];
			}
		}
        healthbar.UpdateBar(life, 100);
    }
	private void Movement(){
		moveLimited();
		if(myturn && !moved && isAI){
			ChangeTarget(targetaux);
			moved = true;
			Invoke("Action", 3.6f);
		}
	}
	private void Action(){
		Debug.Log("cafetera industrial");
		if (selectedAction != 0)
        {
			if (gameObject.GetComponent<Character>() is Cleric)
            {
				if (selectedAction == 1)
                {
					gameObject.GetComponent<Cleric>().Smite(actionTarget);
				}
				else if (selectedAction == 2)
                {
					gameObject.GetComponent<Cleric>().Heal(actionTarget);
				}
                else
                {
					gameObject.GetComponent<Cleric>().healingArea(AOETarget);
				}
            }
			else if (gameObject.GetComponent<Character>() is Mage)
            {

            }
        }
	}

	public void ChangeTarget(Transform newTarget) {
		target = newTarget;
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

	public void move2(Transform target2)
    {
		  //Esto hay que ponerlo a false al terminar el turno
		targetaux = target2;
    }

	public void moveLimited()
    {
		if (Vector3.Distance(lastPosition, transform.position) >= moveDistance)
        {
			//Debug.Log("Resultado: "+(Vector3.Distance(lastPosition, transform.position) >= moveDistance).ToString()+"Distancia movimiento: "+moveDistance.ToString()+"distancia a origen"+(Vector3.Distance(lastPosition, transform.position)).ToString());

			target = transform;
			targetaux=transform;//esto es imprescindible o explota
        }
    }

	public void endTurn()	//Se ha de llamar aqu� al final de cada turno
    {
		lastPosition = transform.position;
		moved = false;
		attackTarget = null;
		myturn=false;
		actionTarget = null;
		selectedAction = 0;
		AOETarget = null;
		//habrá que poner una bool de action a false, same as movement
    }
}