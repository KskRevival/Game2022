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
        StartCoroutine(DoPatrol());
    }

    IEnumerator DoPatrol()
    {
        var wait = new WaitForSeconds(0.5f);

        if (!GetComponent<EnemyMovement>().isChasingPlayer && isReachedWaypoint)
        {
            yield return wait;

            foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask).Select(waypoint => waypoint.transform))
                moveSpots.Add(waypoint);

            GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 90);

            yield return wait;

            foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask).Select(waypoint => waypoint.transform))
                moveSpots.Add(waypoint);

            GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 180);

            yield return wait;

            foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask).Select(waypoint => waypoint.transform))
                moveSpots.Add(waypoint);

            GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 90);

            if (moveSpots.Count == 0)
            {
                yield return wait;

                GetComponent<EnemyMovement>().enemyDirection = GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, 180);

                foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask).Select(waypoint => waypoint.transform))
                    moveSpots.Add(waypoint);
            }

            Debug.Log(moveSpots.Count);

            isReachedWaypoint = false;
        }

        if (!GetComponent<EnemyMovement>().isChasingPlayer)
        {
            GetComponent<EnemyMovement>().enemyDirection = GetComponent<EnemyMovement>().GetMovePosition(TargetWaypoint.position).normalized;
            GetComponent<EnemyMovement>().MoveEnemy();
        }
    }

    private Vector3 GetRotatedVector(Vector3 vector, float angles) => Quaternion.Euler(0, 0, angles) * vector;

    public void GetRandomTargetWayPoint()
    {
        var random = new System.Random();
        var moveSpotsArray = moveSpots.ToArray();
        TargetWaypoint = moveSpotsArray[random.Next(0, moveSpotsArray.Length)];
    }
}