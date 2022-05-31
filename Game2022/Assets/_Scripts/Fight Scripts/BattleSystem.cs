using System;
using System.Collections;
using System.Collections.Generic;
using LabyrinthScripts;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BattleState;
using Random = System.Random;

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    private Player player;
    
    public GameObject playerInFight;
    public GameObject enemy;

    private int damageAddition;
    private int armorAddition;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Unit playerUnit;
    public Unit enemyUnit;

    public TextMeshProUGUI dialogText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    private Random random = new Random();

    void Start()
    {
        state = BattleState.Start;
        enemy = FightPreparation.fightPrefab;
        StartCoroutine(SetUpBattle());
        SetUpPlayer();
    }

    private void SetUpPlayer()
    {
        player = GameManager.Instance.player;
        playerUnit.damage += player.GetWeaponDamage();
        playerUnit.health = player.health;
        playerUnit.maxHealth = player.maxHealth;
        playerUnit.minimalDefence = player.GetArmor();
        playerHUD.SetHP(playerUnit.health);
    }

    IEnumerator SetUpBattle()
    {
        playerUnit = Instantiate(playerInFight, playerBattleStation).GetComponent<Unit>();
        enemyUnit = Instantiate(enemy, enemyBattleStation).GetComponent<Unit>();

        dialogText.text = $@"A wild {enemyUnit.unitName} approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action";
        playerUnit.defence = Math.Max(playerUnit.minimalDefence, playerUnit.defence - 1);
        playerHUD.armorText.text = playerUnit.defence.ToString();
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn) return;
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        playerUnit.damage = 2 + player.GetWeaponDamage();
        if (playerUnit.damage >= 4) AmmoCounter.AmmoCount = Math.Max(0, AmmoCounter.AmmoCount - 1);
        var dead = enemyUnit.TakeDamage(playerUnit.damage - enemyUnit.defence);
        enemyHUD.SetHP(enemyUnit.health);
        dialogText.text = $@"{playerUnit.unitName} deals {playerUnit.damage - enemyUnit.defence} damage";
        yield return new WaitForSeconds(0.5f);

        if (dead)
        {
            state = Won;
            EndBattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnDefendButton()
    {
        if (state != BattleState.PlayerTurn) return;
        StartCoroutine(PlayerDefence());
    }

    IEnumerator PlayerDefence()
    {
        playerUnit.defence = Math.Min(enemyUnit.damage, playerUnit.defence + playerUnit.defenceStack);
        dialogText.text = $@"{playerUnit.unitName} have {playerUnit.defence} armor";
        playerHUD.armorText.text = playerUnit.defence.ToString();
        yield return new WaitForSeconds(0.5f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = $@"{enemyUnit.unitName} attacks";
        enemyUnit.defence = Math.Max(enemyUnit.minimalDefence, enemyUnit.defence - 1);

        yield return new WaitForSeconds(0.5f);

        var damage = Math.Max(0, enemyUnit.damage - playerUnit.defence);
        
        dialogText.text = $@"{enemyUnit.unitName} deals {damage} damage";
        var dead = playerUnit.TakeDamage(enemyUnit.damage - playerUnit.defence);
        playerHUD.SetHP(playerUnit.health);

        yield return new WaitForSeconds(1f);

        if (dead)
        {
            state = Lost;
            EndBattle();
        }
        else
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    public void TryLeave()
    {
        var chance = random.Next(0, 100);
        StartCoroutine(Leave(chance > 90));
        // Debug.Log(chance);
        // if (chance > 90)
        // {
        //     state = Left;
        //     EndBattle();
        // }
        //
        // StartCoroutine(EnemyTurn());
    }

    IEnumerator Leave(bool left)
    {
        if (left)
        {
            dialogText.text = $@"{playerUnit.unitName} ran away";
            
            yield return new WaitForSeconds(1f);

            state = Left;
            EndBattle();
        } 
        
        dialogText.text = $@"{playerUnit.unitName} failed to ran away";
        
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(EnemyTurn());
    }

    // IEnumerator Turn(Unit attacker, Unit defender)
    // {
    //     
    // }

    public void EndBattle()
    {
        GameManager.Instance.state = GameState.Maze;
        GameManager.Instance.player.health = playerUnit.health;
        if (state == Won || state == Left)
        {
            SceneManager.LoadScene(GameManager.Instance.level + 1);
        }
        else
        {
            
            SceneManager.LoadScene("DeathScene");
        }
    }
}