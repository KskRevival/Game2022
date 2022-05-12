using System;
using System.Collections;
using System.Collections.Generic;
using LabyrinthScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static BattleState;

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject player;
    public GameObject enemy;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    private Unit playerUnit;
    private Unit enemyUnit;

    public TextMeshProUGUI dialogText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    void Start()
    {
        state = BattleState.Start;
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        playerUnit = Instantiate(player, playerBattleStation).GetComponent<Unit>();
        enemyUnit = Instantiate(enemy, enemyBattleStation).GetComponent<Unit>();

        dialogText.text = $@"A wild {enemyUnit.unitName} approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action";
        playerUnit.defence = Math.Max(playerUnit.minimalDefence, playerUnit.defence - 1);
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PlayerTurn) return;
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        var dead = enemyUnit.TakeDamage(playerUnit.damage - enemyUnit.defence);
        enemyHUD.SetHP(enemyUnit.health);
        dialogText.text = $@"{playerUnit.unitName} deals {playerUnit.damage - enemyUnit.defence} damage";
        yield return new WaitForSeconds(1.5f);

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
        yield return new WaitForSeconds(1.5f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = $@"{enemyUnit.unitName} attacks";
        enemyUnit.defence = Math.Max(enemyUnit.minimalDefence, enemyUnit.defence - 1);

        yield return new WaitForSeconds(1.5f);

        dialogText.text = $@"{enemyUnit.unitName} deals {enemyUnit.damage - playerUnit.defence} damage";
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

    // IEnumerator Turn(Unit attacker, Unit defender)
    // {
    //     
    // }

    private void EndBattle()
    {
        GameManager.Instance.State = GameState.Maze;
        if (state == Won)
        {
            SceneManager.LoadScene(GameManager.Instance.level + 1);
        }
        else SceneManager.LoadScene("DeathScene");
    }
}