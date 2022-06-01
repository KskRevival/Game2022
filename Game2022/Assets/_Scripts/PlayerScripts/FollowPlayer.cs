using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector3 offset;
    private Player player;

    void Start() => player = GameManager.Instance.player;

    void LateUpdate() => transform.position = player.transform.position + offset;
}
