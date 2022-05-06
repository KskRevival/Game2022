using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        canSeePlayer = rangeChecksForPlayer.Length != 0;
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

    public Transform[] GetRangeChecks(LayerMask layerMask)
        => Physics2D.OverlapCircleAll(transform.position, radius, layerMask)
        .Select(collider => collider.transform)
        .Where(target => InView((target.position - transform.position).normalized)
        && CanSee(transform.position, (target.position - transform.position).normalized, Vector3.Distance(transform.position, target.position)))
        .ToArray();
}