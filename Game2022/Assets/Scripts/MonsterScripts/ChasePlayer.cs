using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public bool isChasingPlayer;
    public bool isPlayerLost;

    private FieldOfView monsterFieldOfView;

    void Start()
    {
        monsterFieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        UpdatePlayerLocation();

        if (isChasingPlayer)
        {
            GetComponent<EnemyMovement>().enemyDirection 
                = GetComponent<EnemyMovement>().GetMovePosition(GetComponent<EnemyMovement>().monsterTargetLocation).normalized;
            if (!isPlayerLost && !GetComponent<FieldOfView>().canSeePlayer) GetLastSeenPlayerArea();
        }

        if (isChasingPlayer && !IsMonsterReachedLastPlayerLocation())
            GetComponent<EnemyMovement>().MoveEnemy();

        if (IsMonsterReachedLastPlayerLocation())
            isChasingPlayer = false;
    }

    private void UpdatePlayerLocation()
    {
        var playerLocation = monsterFieldOfView.GetPlayerPositionInVision();
        if (playerLocation != default)
        {
            isPlayerLost = false;
            isChasingPlayer = true;
            GetComponent<EnemyMovement>().monsterTargetLocation = playerLocation;
        }
    }

    private void GetLastSeenPlayerArea()
    {
        isPlayerLost = true;
        GetComponent<EnemyMovement>().monsterTargetLocation += GetComponent<EnemyMovement>().enemyDirection.normalized / 2;
    }

    private bool IsMonsterReachedLastPlayerLocation() => (transform.position - GetComponent<EnemyMovement>().monsterTargetLocation).sqrMagnitude < 0.01;
}
