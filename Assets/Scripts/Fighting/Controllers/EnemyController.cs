using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // IDEA: flare gun??? sets the target away from player for a few seconds.
    public Transform target;
    public FightController levelController;

    // IDEA: slowgun??? makes things caught by it slower.
    // Do it like a coroutine, set all enemies in raycast slower, then yield for seconds, then re-set back to originals.
    public float speed = 1f;
    public int health = 3;

    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO maybe be a bit more descriptive than just 'not enemy' able to kill the enemy.
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            if (--this.health == 0)
            {
                GameObject.Destroy(this.gameObject);
                this.levelController.killed++;
            }
        }
    }


    void Update()
    {
        Vector3 direction = (target.position - this.transform.position).normalized;
        this.transform.position += direction * this.speed * Time.deltaTime;
    }
}
