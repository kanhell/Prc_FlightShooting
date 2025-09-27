using System;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float moveSpeed, rotateSpeed;
    float time;
    Rigidbody2D rg2D;
    GameObject player;


    void Start()
    {
        moveSpeed = 10.0f;
        rotateSpeed = 300.0f;
        time = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();

        FireBullet();
    }


    void Update()
    {
        RotateBullet();
        DestroyBullet();
    }

    private void FireBullet()
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;  // ¹æÇâ º¤ÅÍ°¡ ³ª¿È
        rg2D.linearVelocity = dir * moveSpeed;
    }

    private void RotateBullet()
    {
        transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
    }

    private void DestroyBullet()
    {
        time += Time.deltaTime;
        if(time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
