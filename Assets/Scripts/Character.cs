using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isPlayerControlled;

	public Transform target;
    float speed = 5;
    float turnSpeed = 20;
	float turnDst = 0;
	float stoppingDst = 2;
    const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;
    Path path;

    const int MaxHealth = 100;
    const int MaxMovementAmount = 100;

    int health = MaxHealth;
    int movementAmountLeft = 0;

    GameMaster gm;
    private Renderer rend;
    Color highlightedColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
        gm = FindObjectOfType<GameMaster>();
        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < 0.3f)
        {
			yield return new WaitForSeconds (0.3f);
		}

		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true)
        {
			yield return new WaitForSeconds (minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
				PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
				targetPosOld = target.position;
			}
		}
	}

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful)
        {
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

    IEnumerator FollowPath() {
		bool followingPath = false;
		int pathIndex = 0;
		if (pathIndex<path.lookPoints.Length)
        {
			followingPath = true;
			transform.LookAt (path.lookPoints[0]);
		}


		float speedPercent = 1;

		while (followingPath)
        {
			if (pathIndex<path.lookPoints.Length)
            {
				Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
				while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D))
                {
					if (pathIndex == path.finishLineIndex)
                    {
						followingPath = false;
						break;
					} 
                    else
                    {
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

    public void StartTurn()
    {
        movementAmountLeft = MaxMovementAmount;
        if (isPlayerControlled)
        {
            // show control interface
        }
    }

    public void Move(Transform target) // should be limited by movementAmountLeft
    {
        this.target = target;
    }

    private void OnMouseEnter()
    {
        if (this != gm.activeCharacter)
        {
            Highlight();
        }
    }

    private void OnMouseExit() {
        Reset();
    }

    private void OnMouseDown()
    {
        gm.selectedCharacter = this;
    }

    public void Highlight()
    {
        rend.material.color = highlightedColor;
    }

    public void Reset()
    {
        rend.material.color = Color.white;
    }
}
