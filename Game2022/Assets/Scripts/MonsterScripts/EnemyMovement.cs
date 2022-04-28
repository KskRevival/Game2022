using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private bool isChasingPlayer;
    public bool isPlayerLost;
    public Vector3 lastSeenPlayerLocation;
    public Vector3 enemyDirection;
    public Rigidbody2D monsterRigidbody;
    public float moveSpeed = 10f;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        lastSeenPlayerLocation = transform.position;
        monsterRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastSeenPlayerLocation);
        if (isChasingPlayer) 
        {
            enemyDirection = lastSeenPlayerLocation - transform.position;
            if (!isPlayerLost && !GetComponent<FieldOfView>().canSeePlayer) GetLastSeenPlayerLocation();
        }
        angle = Mathf.Atan2(enemyDirection.y, enemyDirection.x);
        enemyDirection.Normalize();
    }

    private void FixedUpdate()
    {
        if (isChasingPlayer && !(transform.position == lastSeenPlayerLocation))
            MoveEnemy();
        else isChasingPlayer = false;
    }

    void MoveEnemy()
    {
        monsterRigidbody.MovePosition(transform.position + enemyDirection * (moveSpeed * Time.deltaTime));
    }

    public void UpdatePlayerLocation(Vector3 playerLocation)
    {
        isChasingPlayer = true;
        isPlayerLost = false;
        lastSeenPlayerLocation = playerLocation;
    }

    public void GetLastSeenPlayerLocation()
    {
        isPlayerLost = true;
        lastSeenPlayerLocation = enemyDirection + enemyDirection.normalized / 2;
    }
}
