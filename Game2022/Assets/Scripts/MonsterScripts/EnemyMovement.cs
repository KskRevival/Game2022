using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isChasingPlayer;
    public bool isPlayerLost;
    public Vector3 monsterTargetLocation;
    public Vector3 enemyDirection;
    public Rigidbody2D monsterRigidbody;
    public float moveSpeed;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        monsterTargetLocation = transform.position;
        monsterRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasingPlayer) 
        {
            enemyDirection = monsterTargetLocation - transform.position;
            if (!isPlayerLost && !GetComponent<FieldOfView>().canSeePlayer) GetLastSeenPlayerLocation();
        }
        angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x);
        enemyDirection.Normalize();
    }

    private void FixedUpdate()
    {
        if (isChasingPlayer && !IsMonsterReachedLastPlayerLocation())
            MoveEnemy();
        else isChasingPlayer = false;
    }

    void MoveEnemy()
    {
        monsterRigidbody.MovePosition(transform.position + enemyDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void UpdatePlayerLocation(Vector3 playerLocation)
    {
        isChasingPlayer = true;
        isPlayerLost = false;
        monsterTargetLocation = playerLocation;
    }

    private void GetLastSeenPlayerLocation()
    {
        isPlayerLost = true;
        monsterTargetLocation += enemyDirection.normalized / 2;
    }

    private bool IsMonsterReachedLastPlayerLocation() => (transform.position - monsterTargetLocation).sqrMagnitude < 0.1;
}
