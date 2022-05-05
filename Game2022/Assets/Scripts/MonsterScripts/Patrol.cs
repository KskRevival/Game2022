using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


public class Patrol : MonoBehaviour
{
    public HashSet<Transform> moveSpots;
    public bool isReachedWaypoint;
    public LayerMask waypointsMask;
    public Transform TargetWaypoint;

    void Start()
    {
        moveSpots = new HashSet<Transform>();
        GetComponent<EnemyMovement>().enemyDirection = transform.right;
        isReachedWaypoint = true;
        TargetWaypoint = transform;
        // StartCoroutine(DoPatrol());
    }

    void Update()
    {
        if (IsTargetPointReached()) isReachedWaypoint = true;

        if (!GetComponent<EnemyMovement>().isChasingPlayer && isReachedWaypoint)
        {
            foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask))
                moveSpots.Add(waypoint);

            GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 90);

            foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask))
                moveSpots.Add(waypoint);

            GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, -180);

            foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask))
                moveSpots.Add(waypoint);

            GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 90);

            if (moveSpots.Count == 0)
            {
                GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 180);

                foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask))
                    moveSpots.Add(waypoint);
            }

            isReachedWaypoint = false;
            GetRandomTargetWayPoint();
        }

        if (!GetComponent<EnemyMovement>().isChasingPlayer)
        {
            Debug.Log("Penis");
            GetComponent<EnemyMovement>().enemyDirection = GetComponent<EnemyMovement>().GetMovePosition(TargetWaypoint.position).normalized;
            GetComponent<EnemyMovement>().MoveEnemy();
        }
        else
            TargetWaypoint = transform;
    }

    private Vector3 GetRotatedVector(Vector3 vector, float angles) => Quaternion.Euler(angles, 0, 0) * vector;

    public void GetRandomTargetWayPoint()
    {
        var random = new System.Random();
        var moveSpotsArray = moveSpots.Where(moveSpot => moveSpot != TargetWaypoint).ToArray();
        TargetWaypoint = moveSpotsArray[random.Next(0, moveSpotsArray.Length)];
    }

    private bool IsTargetPointReached() => (transform.position - TargetWaypoint.position).magnitude < 0.1;
}