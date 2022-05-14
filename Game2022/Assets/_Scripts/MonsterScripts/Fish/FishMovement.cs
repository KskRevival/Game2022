using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Vector3 PlayerPos;
    public Rigidbody2D FishRb;

    public Vector3 DirectionToPlayer;

    void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        FishRb = GetComponent<Rigidbody2D>();
        SetDirectionToPlayer();
    }

    // Update is called once per frame
    void Update() =>
        FishRb.MovePosition(transform.position + DirectionToPlayer * (MoveSpeed * Time.fixedDeltaTime));

    private void SetDirectionToPlayer() => DirectionToPlayer = (PlayerPos - transform.position).normalized;
}
