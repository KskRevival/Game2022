using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float MoveSpeed;
    public Vector3 PlayerPos;
    public Rigidbody2D FishRb;

    public Vector3 DirectionToPlayer;
    public AudioClip audioClip;
    private AudioSource audioSource;

    void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        FishRb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        StartCoroutine(PlaySound());
        SetDirectionToPlayer();
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0.8f);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update() =>
        FishRb.MovePosition(transform.position + DirectionToPlayer * (MoveSpeed * Time.fixedDeltaTime));

    private void SetDirectionToPlayer() => DirectionToPlayer = (PlayerPos - transform.position).normalized;
}
