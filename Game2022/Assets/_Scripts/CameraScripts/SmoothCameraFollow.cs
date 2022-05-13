using System;
using UnityEngine;
using System.Collections;
using PlayerScripts;

public class SmoothCameraFollow : MonoBehaviour
{
    public float CameraYDelta = 0f;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera camera;

    public bool IsCameraLockedByX;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // if (IsCameraLockedByX = GameManeger.Instance.level == 7) destination.x = point.x + delta.x * 0.55f;
    }

    void Update()
    {
        if (!target) return;

        Vector3 point = camera.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f + CameraYDelta, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        if (IsCameraLockedByX) destination.x = point.x + delta.x * 0.55f;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

    }
}