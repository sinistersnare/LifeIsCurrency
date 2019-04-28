using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject autoGunPrefab;
    public GameObject bombWeaponPrefab;
    public GameObject enemySpawnerPrefab;
    public Transform arenaTransform;
    public Text currentRunText;

    [HideInInspector]
    public int killed;

    private float currentRunTime = 0;


    private void Start()
    {
        float arenaRadius = this.arenaTransform.lossyScale.x / 2;
        // Create combatants.
        GameObject playerObject = GameObject.Instantiate(this.playerPrefab, Vector3.zero, Quaternion.identity);
        playerObject.name = "Player";
        PlayerController pc = playerObject.GetComponent<PlayerController>();
        pc.health = SaveData.Health;
        // Make Automatic Gun first, so players dont have to switch.
        if (SaveData.HasAuto) { pc.gunPrefabs.Insert(0, this.autoGunPrefab); }
        if (SaveData.HasBomber) { pc.bombHolsterPrefab = this.bombWeaponPrefab; }
        pc.arenaRadius = arenaRadius;

        GameObject spawnerObject = GameObject.Instantiate(this.enemySpawnerPrefab, Vector3.zero, Quaternion.identity);
        spawnerObject.name = "Enemy Spawner";
        EnemySpawner spawner = spawnerObject.GetComponent<EnemySpawner>();
        spawner.maxRadius = arenaRadius;
        spawner.thingToTarget = pc.transform;

        Camera.main.GetComponent<CameraController>().player = pc;


    }

    private void Update()
    {
        this.currentRunTime += Time.deltaTime;
        this.currentRunText.text = "Fight Length: " + this.currentRunTime.ToString("0.00");

    }

    public void EndLevel()
    {
        SaveData.money += killed;
        SaveData.highScore = Mathf.Max(this.currentRunTime, SaveData.highScore);

        SceneManager.LoadScene("ShopScene");
    }

    public static FightController FindMe()
    {
        return GameObject.Find("LevelController").GetComponent<FightController>();
    }
}
