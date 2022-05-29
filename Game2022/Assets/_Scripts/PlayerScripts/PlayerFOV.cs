using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    [SerializeField]
    public LayerMask[] layerMasks;
    private LayerMask finalLayerMask;
    private Mesh mesh;
    private Vector3 origin;
    private float startingAngle;
    private float viewDistance;
    private float fov;
    private const int rayCount = 50;

    private void Start()
    {
        finalLayerMask = layerMasks[0] | layerMasks[1];
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        fov = 90f;
        viewDistance = 3f;
    }

    private void Update()
    {
        var playerPos = GameManager.Instance.player.transform.position + new Vector3(0, 0.8f);

        if (Input.GetMouseButtonDown(0))
        {
            SetFOV(fov == 90f ? 40f : 90f);
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
            var raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, finalLayerMask);

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
