using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isChasingPlayer;
    public bool isPlayerLost;

    public Vector3 monsterTargetLocation;
    public Vector3 enemyDirection;
    public Rigidbody2D monsterRigidbody;

    private float moveSpeed;
    public float standardSpeed;
    public float chaseSpeed;

    public float angle;
    private FieldOfView monsterFieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        monsterTargetLocation = transform.position;
        monsterRigidbody = GetComponent<Rigidbody2D>();
        monsterFieldOfView = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMovingSpeed();

        UpdatePlayerLocation();
        if (isChasingPlayer)
        {
            enemyDirection = GetMovePosition(monsterTargetLocation);
            if (!isPlayerLost && !GetComponent<FieldOfView>().canSeePlayer) GetLastSeenPlayerArea();
        }
        angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x);
        enemyDirection.Normalize();
    }

    private void FixedUpdate()
    {
        if (isChasingPlayer && !IsMonsterReachedLastPlayerLocation())
            MoveEnemy();

        if (IsMonsterReachedLastPlayerLocation())
            isChasingPlayer = false;
    }

    public void MoveEnemy() =>
        monsterRigidbody.MovePosition(transform.position + enemyDirection * (moveSpeed * Time.fixedDeltaTime));

    public Vector3 GetMovePosition(Vector3 target) => target - transform.position;

    private void UpdatePlayerLocation()
    {
        var playerLocation = monsterFieldOfView.GetPlayerPositionInVision();
        if (playerLocation != default)
        {
            isPlayerLost = false;
            isChasingPlayer = true;
            monsterTargetLocation = playerLocation;
        }
    }

    private void GetLastSeenPlayerArea()
    {
        isPlayerLost = true;
        monsterTargetLocation += enemyDirection.normalized / 2;
    }

    private bool IsMonsterReachedLastPlayerLocation() => (transform.position - monsterTargetLocation).sqrMagnitude < 0.01;

    private void SetMovingSpeed() => moveSpeed = isChasingPlayer ? chaseSpeed : standardSpeed;
}
