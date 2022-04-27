using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 monsterDirection;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<FieldOfView>().canSeePlayer)
            monsterDirection = player.position - transform.position;
        angle = Mathf.Atan2(monsterDirection.y, monsterDirection.x);
    }
}
