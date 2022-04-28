using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

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
        var rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            var target = rangeChecks[0].transform;
            var position = transform.position;
            var directionToTarget = (target.position - position).normalized;
            var distanceToTarget = Vector3.Distance(position, target.position);
            canSeePlayer = 
                Vector3.Angle(transform.up, directionToTarget) < angle / 2
                &&
                !Physics2D.Raycast(
                    position,
                    directionToTarget, 
                    distanceToTarget, 
                    obstructionMask);
        }
        canSeePlayer = rangeChecks.Length != 0 && canSeePlayer;
    }
}
