using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using LabyrinthScripts;
using MonsterScripts;
using Random = Unity.Mathematics.Random;


public class Patrol : MonoBehaviour
{
    public HashSet<Transform> moveSpots;
    public bool isReachedWaypoint;
    public LayerMask waypointsMask;
    public Transform TargetWaypoint;

    private System.Random random = new System.Random();

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
        if (GameManager.Instance.State == GameState.Fight) return;
        isReachedWaypoint = IsTargetPointReached();

        if (!GetComponent<ChasePlayer>().isChasingPlayer && isReachedWaypoint)
        {
            NewMethod();
        }

        if (!GetComponent<ChasePlayer>().isChasingPlayer)
        {
            GetComponent<EnemyMovement>().enemyDirection =
                GetComponent<EnemyMovement>().GetMovePosition(TargetWaypoint.position).normalized;
            GetComponent<EnemyMovement>().MoveEnemy();
        }
        else
            TargetWaypoint = transform;
    }

    private void NewMethod()
    {
        RotateAndCheckForWayPoints(90);
        Debug.Log(moveSpots.Count);
        RotateAndCheckForWayPoints(-180);
        Debug.Log(moveSpots.Count);
        RotateAndCheckForWayPoints(90);
        Debug.Log(moveSpots.Count);
        if (moveSpots.All(moveSpot => moveSpot == TargetWaypoint))
            RotateAndCheckForWayPoints(180);
        Debug.Log(moveSpots.Count);
        isReachedWaypoint = false;
        GetRandomTargetWayPoint();
    }

    private void RotateAndCheckForWayPoints(float angle = 0)
    {
        GetComponent<EnemyMovement>().enemyDirection =
            GetRotatedVector(GetComponent<EnemyMovement>().enemyDirection, angle);

        foreach (var waypoint in GetComponent<FieldOfView>().GetRangeChecks(waypointsMask))
            moveSpots.Add(waypoint);
    }

    private Vector3 GetRotatedVector(Vector3 vector, float angle) =>
        Quaternion.AngleAxis(angle, Vector3.forward) * vector;

    public void GetRandomTargetWayPoint()
    {
        var moveSpotsArray = moveSpots.Where(moveSpot => moveSpot != TargetWaypoint).ToArray();

        TargetWaypoint = moveSpotsArray[random.Next(0, moveSpotsArray.Length)];
        moveSpots.Clear();
    }

    private bool IsTargetPointReached() => (transform.position - TargetWaypoint.position).sqrMagnitude < 0.1;
}
