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
    public float afterMaxRadiusSpawnRateIncreaseRate = 5;

    private bool hitTop;
    private float lastSpawn;
    private FightController levelController;

    void Start()
    {
        this.lastSpawn = float.MinValue;
        this.levelController = FightController.FindMe();
    }

    void Update()
    {
        float now = Time.time;

        // TODO probably a minRadius?
        if (this.radius > this.maxRadius || this.radius < 10)
        {
            this.growthRate *= -1;
            this.hitTop = true;
        }

        this.radius += this.growthRate * Time.deltaTime;

        if (now - this.lastSpawn >= this.spawnRate)
        {
            this.lastSpawn = now;
            this.MakeCircleOfEnemies(this.radius);
            if (this.hitTop) { this.MakeCircleOfEnemies(this.maxRadius); }
        }
    }

    void MakeCircleOfEnemies(float enemyRadius)
    {
        int numEnemies = (int)Mathf.Floor(enemyRadius) / 2;
        for (float angle = 0; angle <= 360; angle += (360 / numEnemies))
        {
            Vector3 enemyPos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * enemyRadius, Mathf.Sin(angle * Mathf.Deg2Rad) * enemyRadius, 0);
            GameObject enemyObject = GameObject.Instantiate(this.enemyPrefab, enemyPos, Quaternion.identity, this.transform);
            enemyObject.name = "enemy-" + angle;
            EnemyController ctrl = enemyObject.GetComponent<EnemyController>();
            ctrl.target = thingToTarget.transform;
            ctrl.levelController = this.levelController;
        }
    }
}
