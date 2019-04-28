using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;
    public float speed = 2;
    public float timeToLive;
    public int objectsToHit = 2;

    private Vector3 direction;
    private float liveTime;

    void Start()
    {
        // cast to Vector2 to zero z-part.
        this.direction = ((Vector2)target - (Vector2) this.transform.position).normalized;
    }



    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
        if (this.liveTime > this.timeToLive)
        {
            GameObject.Destroy(this.gameObject);
        }
        this.liveTime += Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (--this.objectsToHit == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
