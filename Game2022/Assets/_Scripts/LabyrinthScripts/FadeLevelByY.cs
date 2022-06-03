using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FadeLevelByY : MonoBehaviour
{
    public GameObject Fade;

    void Update()
    {
        var fadeSprite = Fade.GetComponent<SpriteRenderer>();
        Fade.GetComponent<SpriteRenderer>().color = new Color(fadeSprite.color.r,
            fadeSprite.color.g,
            fadeSprite.color.b,
            Mathf.Max(0, GameManager.Instance.player.transform.position.y) / 12);
    }
}
