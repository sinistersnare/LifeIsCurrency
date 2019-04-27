using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int health = 1;
    public float speed = 6;
    public GameObject[] gunPrefabs;
    public float arenaRadius;

    private int currentGun = 0;
    private GameObject[] gunObjects;
    private Vector2 movementVector = Vector2.zero;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private bool dying;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.gunObjects = new GameObject[this.gunPrefabs.Length];
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        for (int i=0; i < this.gunObjects.Length; i++)
        {
            this.gunObjects[i] = GameObject.Instantiate(this.gunPrefabs[i], this.transform.position, 
                                                        Quaternion.identity, this.transform);
            this.gunObjects[i].SetActive(i == this.currentGun); // just activate the first.
        }
    }

    private void FixedUpdate()
    {
        if (!this.dying)
        {
            this.MovePlayer();
        }
    }

    void Update()
    {
        if (this.health == 0)
        {
            this.StartCoroutine(this.DeathSequence(3));
        }
    }

    void MovePlayer()
    {
        this.movementVector.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.body.MovePosition(this.transform.position + ((Vector3)this.movementVector * this.speed * Time.deltaTime));
        if (((Vector2)this.transform.position).magnitude > this.arenaRadius)
        {
            Vector2 newPos = this.transform.position.normalized * this.arenaRadius;
            this.transform.position = newPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.health--;
        }
    }

    private IEnumerator DeathSequence(float deathTime)
    {
        this.dying = true;
        float timeLeft = deathTime;
        this.gunObjects[this.currentGun].SetActive(false);
        while (timeLeft > 0)
        {
            this.spriteRenderer.enabled = !this.spriteRenderer.enabled;
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        FightController.FindMe().EndLevel();
    }
}
