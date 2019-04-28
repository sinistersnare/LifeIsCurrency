using UnityEngine;
using System.Collections;

public class BombHolster : MonoBehaviour
{
    //darvis is a stinky boi
    public float rechargeTime = 5;
    public GameObject bombPrefab;

    private float lastBomb = float.MinValue;

    void Update()
    {
        if (Input.GetButtonDown("FireBomb") && (Time.time - this.lastBomb > this.rechargeTime))
        {
            this.lastBomb = Time.time;

            // Should activate and destroy itself.
            GameObject.Instantiate(this.bombPrefab, this.transform.position, Quaternion.identity);
        }
    }
}
