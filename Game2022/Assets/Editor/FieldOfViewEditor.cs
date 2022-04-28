using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        var fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.up, 360, fov.radius);
        var rotationAngle = fov.transform.GetComponent<EnemyMovement>().angle;
        Debug.Log(rotationAngle);

        var viewAngleRight = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        var viewAngleLeft = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            // Debug.Log($"{fov.transform.position} - {fov.playerRef.transform.position}");

            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
