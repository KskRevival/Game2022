using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AmmoCollider : MonoBehaviour
{
    public Random random = new Random();
    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        if (!collidedObject.transform.CompareTag("Player")) return;
        var ammo = random.Next(1, 5);
        AmmoCounter.AmmoCount += ammo;
        Destroy(gameObject);
    }
}
