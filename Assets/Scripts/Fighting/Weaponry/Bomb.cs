using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public float timeToLive = 4;
    public float explosionRadius = 5;
    public int damage = 5;

    private float liveTime;

    private void Explode()
    {
        // mask API makes no sense.
        Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, this.explosionRadius, ~0);
        foreach (Collider2D enemyCollider in enemies)
        {
            // if the collider has an EnemyController, damage it.
            enemyCollider.gameObject.GetComponent<EnemyController>()?.TakeHit(this.damage);
        }
        GameObject.Destroy(this.gameObject);
    }

    void Update()
    {
        if (this.liveTime > this.timeToLive)
        {
            this.Explode();
        }
        this.liveTime += Time.deltaTime;
    }
}
