using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightPreparation
{
    private static GameObject fightPrefab;

    private static readonly GameObject[] fightPrefabs =
    {
        GameManager.GameObjectResources("Fight/Entity6InFight")
    };

    public static GameObject GetFightPrefab() => fightPrefab;

    public static void SetFightPrefab(int index) => fightPrefab = fightPrefabs[index];
}
