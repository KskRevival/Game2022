using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabyrinthScripts;

public class FishSpawner : MonoBehaviour
{
    public GameObject Player;
    public float FishSpawnTime;
    public GameObject LastFish;
    public GameObject LastFishTraectory;
    public float DistanceFromPlayer;
    public float angle;

    private System.Random random = new System.Random();


    void Start()
    {
        // PlayerPos = GameManager.Instance.player.transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnFish());
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(FishSpawnTime);

            angle = GetAngleInDegrees(GetRandomNumberInRange(-Mathf.PI, Mathf.PI));
            if (LastFish != null) DestroyLastFishGameObject();

            LastFish = Instantiate(
                GameManager.GameObjectResources("Fish"),
                Player.transform.position + GetRotatedVector() * DistanceFromPlayer,
                Quaternion.identity
                );

            LastFishTraectory = Instantiate(
                GameManager.GameObjectResources("FishTraectory"),
                Player.transform.position,
                Quaternion.identity
                );

            RotateFishAndTraectory();
        }
    }

    private float GetRandomNumberInRange(float lowerBound, float upperBound) =>
        (float)random.NextDouble() * (upperBound - lowerBound) + lowerBound;

    private float GetAngleInDegrees(float radians) => radians * 180 / Mathf.PI;

    private Vector3 GetRotatedVector() => Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

    private void DestroyLastFishGameObject()
    {
        Destroy(LastFish);
        Destroy(LastFishTraectory);
    }

    private void RotateFishAndTraectory()
    {
        if (Mathf.Abs(angle) > 90) MirrorFish();
        Debug.Log(angle);

        LastFish.transform.Rotate(new Vector3(0, 0, angle));
        LastFishTraectory.transform.Rotate(new Vector3(0, 0, angle));
    }

    private float GetEntitiesRotationAngle()
    {
        return Mathf.Abs(angle) <= Mathf.PI / 2 ? angle : angle + Mathf.PI / 2;
    }

    private void MirrorFish()
    {
        var newFishScale = LastFish.transform.localScale;
        newFishScale.x *= -1;
        LastFish.transform.localScale = newFishScale;
        angle += 180;
    }
}
