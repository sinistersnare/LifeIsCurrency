using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int health = 1;
    public float speed = 6;
    public List<GameObject> gunPrefabs;
    public float arenaRadius;

    private int currentGun = 0;
    private List<GameObject> gunObjects;
    private Vector2 movementVector = Vector2.zero;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private bool dying;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.gunObjects = new List<GameObject>(this.gunPrefabs.Count);
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        foreach (GameObject gunPrefab in gunPrefabs)
        {
            gunObjects.Add(GameObject.Instantiate(gunPrefab, this.transform.position, Quaternion.identity, this.transform));
        }
        for (int i = 0; i < gunObjects.Count; i++)
        {
            gunObjects[i].SetActive(i == currentGun);
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) { this.ChangeGun(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { this.ChangeGun(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { this.ChangeGun(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { this.ChangeGun(3); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { this.ChangeGun(4); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { this.ChangeGun(5); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { this.ChangeGun(6); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { this.ChangeGun(7); }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { this.ChangeGun(8); }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { this.ChangeGun(9); }
    }

    private void ChangeGun(int idxPos)
    {
        if (idxPos < this.gunObjects.Count)
        {
            this.gunObjects[this.currentGun].SetActive(false);
            this.currentGun = idxPos;
            this.gunObjects[this.currentGun].SetActive(true);
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
