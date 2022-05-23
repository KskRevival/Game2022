using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    private Mesh mesh;
    private const int rayCount = 50;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        var fov = 90f;
        var origin = Vector3.zero;
        var angle = 0f;
        var angleIncrease = fov / rayCount;
        var viewDistance = 10f;

        var verticies = new Vector3[rayCount + 2];
        var uv = new Vector2[verticies.Length];
        var triangles = new int[rayCount * 3];

        verticies[0] = origin;

        var vertexIndex = 1;
        var triangleIndex = 0;
        for (var i = 0; i <= rayCount; i++)
        {
            var raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance);

            var vertex = raycastHit2D.collider == null
                ? origin + GetVectorFromAngle(angle) * viewDistance
                : (Vector3)raycastHit2D.point;

            verticies[i] = vertex;

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
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        // angle 0 -> 360
        var angleRad = angle * (Mathf.PI / 180);

        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
