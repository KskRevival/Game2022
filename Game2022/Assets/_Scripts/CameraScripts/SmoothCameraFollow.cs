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
    public float maxY;
    public float minY;
    public bool isCameraLocked;

    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // if (IsCameraLockedByX = GameManeger.Instance.level == 7) destination.x = point.x + delta.x * 0.55f;
    }

    void Update()
    {
        if (!target) return;

        // if (GameManager.Instance.level == 4) target.position = new Vector3(target.position.x, 0.9f, target.position.z);
        var cameraYOffset = GameManager.Instance.level == 4 ? Vector3.up * target.position.y * 0.4f : Vector3.zero;
        Vector3 point = camera.WorldToViewportPoint(target.position);
        Vector3 delta = target.position + cameraYOffset - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        if (isCameraLocked)
        {
            destination.x = GameManager.Instance.level == 4 ? 0 : point.x + delta.x * 0.08f;
            destination.y = Mathf.Min(maxY, destination.y);
            destination.y = Mathf.Max(minY, destination.y);
        }
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

    }
}