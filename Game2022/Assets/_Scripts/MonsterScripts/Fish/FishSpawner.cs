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

            LastFish = InitiatePrefab("Fish", Player.transform.position + GetRotatedVector());

            LastFishTraectory = InitiatePrefab("FishTraectory", Player.transform.position);

            RotateFishAndTraectory();
        }
    }

    private float GetRandomNumberInRange(float lowerBound, float upperBound) =>
        (float)random.NextDouble() * (upperBound - lowerBound) + lowerBound;

    private float GetAngleInDegrees(float radians) => radians * 180 / Mathf.PI;

    private Vector3 GetRotatedVector() => Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right * DistanceFromPlayer;

    private void DestroyLastFishGameObject()
    {
        Destroy(LastFish);
        Destroy(LastFishTraectory);
    }

    private GameObject InitiatePrefab(string prefabName, Vector3 position)
    {
        return Instantiate(
                GameManager.GameObjectResources(prefabName),
                position,
                Quaternion.identity
                );
    }

    private void RotateFishAndTraectory()
    {
        LastFish.transform.Rotate(new Vector3(0, 0, angle));
        LastFishTraectory.transform.Rotate(new Vector3(0, 0, angle));
    }

}
