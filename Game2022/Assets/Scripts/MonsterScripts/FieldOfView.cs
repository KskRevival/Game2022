using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)] public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    public void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        var wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FeildOfViewCheck();
        }
    }

    private void FeildOfViewCheck()
    {
        var rangeChecksForPlayer = GetRangeChecks(targetMask);

        if (rangeChecksForPlayer.Length != 0)
        {
            var target = rangeChecksForPlayer[0].transform;
            var position = transform.position;
            var directionToTarget = (target.position - position).normalized;
            var distanceToTarget = Vector3.Distance(position, target.position);
            canSeePlayer = InView(directionToTarget) && CanSee(position, directionToTarget, distanceToTarget);
        }

        canSeePlayer = rangeChecksForPlayer.Length != 0 && canSeePlayer;
    }

    private bool InView(Vector3 directionToTarget)
        => Vector3.Angle(GetComponent<EnemyMovement>().enemyDirection.normalized, directionToTarget) < angle / 2;

    private bool CanSee(Vector3 position, Vector3 directionToTarget, float distanceToTarget)
        => !Physics2D.Raycast(
            position,
            directionToTarget,
            distanceToTarget,
            obstructionMask);

    public Vector3 GetPlayerPositionInVision() => canSeePlayer ? playerRef.transform.position : default;

    public Collider2D[] GetRangeChecks(LayerMask layerMask) 
        => Physics2D.OverlapCircleAll(transform.position, radius, layerMask);
}