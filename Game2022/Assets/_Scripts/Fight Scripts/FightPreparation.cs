using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightPreparation
{
    public static GameObject fightPrefab;

    private static readonly GameObject[] fightPrefabs =
    {
        GameManager.GameObjectResources("Fight/Entity6InFight")
    };

    public static void SetFightPrefab(int index) => fightPrefab = fightPrefabs[index];
}
