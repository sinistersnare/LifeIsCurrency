using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform thingToTarget;
    public GameObject enemyPrefab;
    public float radius = 20;
    public float growthRate = 4;
    public float maxRadius = 69.420f;
    /// <summary>
    /// Every `spawnRate` seconds, enemies will spawn.
    /// </summary>
    public float spawnRate = 1f;
    private float lastSpawn;
    private FightController levelController;

    void Start()
    {
        this.lastSpawn = float.MinValue;
        this.levelController = FightController.FindMe();
    }

    void Update()
    {
        if (this.radius < this.maxRadius) 
        {
            this.radius += this.growthRate * Time.deltaTime;
        }

        float now = Time.time;
        if (now - this.lastSpawn >= this.spawnRate)
        {
            this.lastSpawn = now;
            // spawn enemies on circle.
            int numEnemies = (int) Mathf.Floor(this.radius) / 2;
            for (float angle = 0; angle <= 360; angle += (360 / numEnemies))
            {
                Vector3 enemyPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * this.radius, Mathf.Sin(angle * Mathf.Deg2Rad) * this.radius, 0);
                GameObject enemyObject = GameObject.Instantiate(this.enemyPrefab, enemyPos, Quaternion.identity, this.transform);
                enemyObject.name = "enemy-" + angle;
                EnemyController ctrl = enemyObject.GetComponent<EnemyController>();
                ctrl.target = thingToTarget.transform;
                ctrl.levelController = this.levelController;
            }
        }
    }
}
