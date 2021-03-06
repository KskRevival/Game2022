using UnityEngine;
using System.Collections;
using PlayerScripts;
using UIScripts;
using InventoryScripts;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioClip audioClip;
    private AudioSource audioData;
    private Player player;

    void Start()
    {
        audioClip = AudioResourses.PlayerFootsteps[GameManager.Instance.level - 1];
        player = GameManager.Instance.player;
        audioData = gameObject.AddComponent<AudioSource>();
        audioData.clip = audioClip;
    }

    void Update()
    {
        if (player.md.movement != default 
            && GameManager.Instance.state == GameState.Maze 
            && !PauseScript.IsPaused && !audioData.isPlaying 
            && !InventoryHandler.IsInventoryActive)
        {
            audioData.volume = Random.Range(0.8f, 1);
            audioData.pitch = player.md.speed == MovementData.NormalSpeed ? Random.Range(0.8f, 1f) : Random.Range(1.1f, 1.4f);
            audioData.Play();
        }
    }
}