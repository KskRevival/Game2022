using InventoryScripts;
using System.Collections;
using System.Collections.Generic;
using UIScripts;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    [SerializeField]
    public LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float viewDistance;
    private float fov;
    private const int rayCount = 50;
    private AudioSource audioSource;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 110f;
        viewDistance = 3f;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PauseScript.IsPaused 
            || InventoryHandler.IsInventoryActive 
            || GameManager.Instance.state == GameState.Fight 
            || TutorialHandler.IsTutorialOpened) 
            return;

        var playerPos = GameManager.Instance.player.transform.position + Vector3.up * 0.7f;

        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
            SetFOV(fov == 110f ? 30f : 110f);
            SetViewDistance(viewDistance == 3f ? 7f : 3f);
        }

        SetAimDirection((Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerPos.z))
            - playerPos).normalized);
        SetOrigin(playerPos);
    }

    private void LateUpdate()
    {
        var angle = startingAngle;
        var angleIncrease = fov / rayCount;

        var verticies = new Vector3[rayCount + 2];
        var uv = new Vector2[verticies.Length];
        var triangles = new int[rayCount * 3];

        verticies[0] = origin;

        var vertexIndex = 1;
        var triangleIndex = 0;
        for (var i = 0; i <= rayCount; i++)
        {
            var raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            var vertex = raycastHit2D.collider == null
                ? origin + GetVectorFromAngle(angle) * viewDistance
                : (Vector3)raycastHit2D.point;

            verticies[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }



        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        // angle 0 -> 360
        var angleRad = angle * (Mathf.PI / 180);

        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < -180) angle += 360;

        return angle;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
        var raycastHit2D = Physics2D.Raycast(origin, new Vector2(0, -1), viewDistance, layerMask);
        if (raycastHit2D.collider != null && (raycastHit2D.collider.transform.position - this.origin).magnitude < 1)
        {
            Debug.Log(raycastHit2D.collider.transform.position - transform.up * 0.51f);
            this.origin.y = raycastHit2D.collider.transform.position.y - transform.up.y * 0.51f;
        }
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFOV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
