using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UIScripts;
using UnityEngine;

public class MonsterSounds : MonoBehaviour
{
    public float MonsterRadius;
    public AudioClip MonsterFootsteps;
    public AudioClip MonsterIdle;
    public AudioClip[] MonsterChase;
    private int previousIndex = -1;
    private Player player;
    private AudioSource footstepsSourse;
    private AudioSource monsterSoundsSourse;

    void Start() 
    {
        footstepsSourse = gameObject.AddComponent<AudioSource>();
        footstepsSourse.clip = MonsterFootsteps;

        monsterSoundsSourse = gameObject.AddComponent<AudioSource>();

        player = GameManager.Instance.player;

        StartCoroutine(FootSteps());
        StartCoroutine(MonsterWorldSounds());
    }

    private IEnumerator FootSteps()
    {
        while (true)
        {
            var isChasingPlayer = GetComponent<ChasePlayer>().isChasingPlayer;

            if (IsPlayerInMonsterRadius())
            {
                var soundsVolume = GetSoundsVolume();

                footstepsSourse.volume = Random.Range(0.8f * soundsVolume, soundsVolume);
                footstepsSourse.pitch = isChasingPlayer ? Random.Range(1.1f, 1.3f) : Random.Range(0.9f, 1.1f);

                footstepsSourse.Stop();
                footstepsSourse.Play();
            }

            yield return new WaitForSeconds(isChasingPlayer ? footstepsSourse.pitch - 0.75f : Random.Range(0.8f, 0.9f));
        }
    }

    private IEnumerator MonsterWorldSounds()
    {
        while (true)
        {
            var isChasingPlayer = GetComponent<ChasePlayer>().isChasingPlayer;
            monsterSoundsSourse.clip = isChasingPlayer ? GetRandomRoar() : MonsterIdle;

            if (IsPlayerInMonsterRadius())
            {
                var soundsVolume = GetSoundsVolume();

                monsterSoundsSourse.volume = Random.Range(0.8f * soundsVolume, soundsVolume);

                monsterSoundsSourse.Stop();
                monsterSoundsSourse.Play();
            }

            yield return new WaitForSeconds(monsterSoundsSourse.clip.length + Random.Range(1.5f, 1.8f));
        }
    }

    private AudioClip GetRandomRoar()
    {
        var randomIndex = Random.Range(0, MonsterChase.Length - 1);
        if (randomIndex == previousIndex) randomIndex = (previousIndex + 1) % MonsterChase.Length;
        previousIndex = randomIndex;

        return MonsterChase[randomIndex];
    }

    private float GetDistanceToPlayer() => (transform.position - player.transform.position).magnitude;

    private bool IsPlayerInMonsterRadius() => GameManager.Instance.state == GameState.Maze && !PauseScript.IsPaused && GetDistanceToPlayer() <= MonsterRadius;

    private float GetSoundsVolume() => Mathf.Max(0, MonsterRadius - GetDistanceToPlayer()) / MonsterRadius;
}
