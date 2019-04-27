using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemySpawnerPrefab;
    public Transform arenaTransform;

    public int killed;

    private void Start()
    {
        float arenaRadius = this.arenaTransform.lossyScale.x / 2;
        // Create combatants.
        GameObject playerObject = GameObject.Instantiate(this.playerPrefab, Vector3.zero, Quaternion.identity);
        playerObject.name = "Player";
        PlayerController pc = playerObject.GetComponent<PlayerController>();
        pc.health = SaveData.Health;
        pc.arenaRadius = arenaRadius;
        // TODO fill out PC from saved info when level starts.

        GameObject spawnerObject = GameObject.Instantiate(this.enemySpawnerPrefab, Vector3.zero, Quaternion.identity);
        spawnerObject.name = "Enemy Spawner";
        EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
        spawner.maxRadius = arenaRadius;
        spawner.thingToTarget = pc.transform;

        Camera.main.GetComponent<CameraController>().player = pc;
    }

    public void EndLevel()
    {
        SaveData.money += killed;

        SceneManager.LoadScene("ShopScene");
    }

    public static FightController FindMe()
    {
        return GameObject.Find("LevelController").GetComponent<FightController>();
    }
}
