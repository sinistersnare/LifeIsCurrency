using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float refreshTime = 1;
    private float lastShotTime;

    void Update()
    {
        this.RotateArm();
        if (Input.GetButtonDown("Fire1") && ((Time.time - lastShotTime) >= refreshTime))
        {
            lastShotTime = Time.time;
            GameObject bullet = GameObject.Instantiate(this.bulletPrefab, this.bulletSpawnPoint.position, this.transform.rotation);
            Bullet b = bullet.GetComponent<Bullet>();
            b.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void RotateArm()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 armPos = this.transform.position;
        Vector2 direction = mouse - armPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
