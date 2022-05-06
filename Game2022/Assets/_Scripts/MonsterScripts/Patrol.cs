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

        if (!GetComponent<ChasePlayer>().isChasingPlayer && isReachedWaypoint)
        {
            RotateAndCheckForWayPoints(90);
            Debug.Log(moveSpots.Count);
            RotateAndCheckForWayPoints(-180);
            Debug.Log(moveSpots.Count);
            RotateAndCheckForWayPoints(90);
            Debug.Log(moveSpots.Count);
            if (moveSpots.Where(moveSpot => moveSpot != TargetWaypoint).Count() == 0)
                RotateAndCheckForWayPoints(180);
            Debug.Log(moveSpots.Count);
            isReachedWaypoint = false;
            GetRandomTargetWayPoint();
        }

        if (!GetComponent<ChasePlayer>().isChasingPlayer)
        {
            GetComponent<EnemyMovement>().enemyDirection = GetComponent<EnemyMovement>().GetMovePosition(TargetWaypoint.position).normalized;
            GetComponent<EnemyMovement>().MoveEnemy();
        }
        else
            TargetWaypoint = transform;
    }

    private void RotateAndCheckForWayPoints(float angle = 0)
    {
        GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, angle);

        foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask))
            moveSpots.Add(waypoint);
    }

    private Vector3 GetRotatedVector(Vector3 vector, float angle) => Quaternion.AngleAxis(angle, Vector3.forward) * vector;

    public void GetRandomTargetWayPoint()
    {
        var random = new System.Random();
        var moveSpotsArray = moveSpots.Where(moveSpot => moveSpot != TargetWaypoint).ToArray();
        TargetWaypoint = moveSpotsArray[random.Next(0, moveSpotsArray.Length)];
        moveSpots.Clear();
    }

    private bool IsTargetPointReached() => (transform.position - TargetWaypoint.position).sqrMagnitude < 0.1;
}