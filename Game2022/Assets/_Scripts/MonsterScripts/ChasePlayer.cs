using System.Collections;
using System.Collections.Generic;
using LabyrinthScripts;
using MonsterScripts;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public bool isChasingPlayer;
    public bool isPlayerLost;
    public static Vector2 playerHitboxOffset = new Vector2(0, -0.25f);

    private FieldOfView monsterFieldOfView;

    void Start()
    {
        monsterFieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        if (GameManager.Instance.state == GameState.Fight) return;
        UpdatePlayerLocation();

        var enemyMovement = GetComponent<EnemyMovement>();

        if (isChasingPlayer)
        {
            enemyMovement.enemyDirection 
                = enemyMovement.GetMovePosition(enemyMovement.monsterTargetLocation + playerHitboxOffset).normalized;
            if (!isPlayerLost && !GetComponent<FieldOfView>().canSeePlayer) GetLastSeenPlayerArea();
        }

        if (isChasingPlayer && !IsMonsterReachedLastPlayerLocation())
            enemyMovement.MoveEnemy();

        if (IsMonsterReachedLastPlayerLocation())
            isChasingPlayer = false;
    }

    private void UpdatePlayerLocation()
    {
        var playerLocation = monsterFieldOfView.GetPlayerPositionInVision();
        if (playerLocation == default) return;
        isPlayerLost = false;
        isChasingPlayer = true;
        GetComponent<EnemyMovement>().monsterTargetLocation = playerLocation;
    }

    private void GetLastSeenPlayerArea()
    {
        isPlayerLost = true;
        GetComponent<EnemyMovement>().monsterTargetLocation += GetComponent<EnemyMovement>().enemyDirection.normalized / 2;
    }

    private bool IsMonsterReachedLastPlayerLocation() => (transform.position - (Vector3)GetComponent<EnemyMovement>().monsterTargetLocation).sqrMagnitude < 0.01;
}
