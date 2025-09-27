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
        time = 0;  // 죽을때
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();

        FireBullet();  // 방향: player, 속도 : moveSpeed
    }


    void Update()
    {
        RotateBullet();  // 회전
        DestroyBullet();  // 5초 뒤 Destroy
    }

    private void FireBullet()  // 방향: player, 속도 : moveSpeed
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;  // 방향 벡터가 나옴
        rg2D.linearVelocity = dir * moveSpeed;
    }

    private void RotateBullet()  // 회전
    {
        transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
    }

    private void DestroyBullet()  // 5초 뒤 Destroy
    {
        time += Time.deltaTime;
        if(time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  // 충돌시 (Tag: Player), Destroy
    {
        if (collision.CompareTag("Player") || collision.CompareTag("BoomMissile"))
        {
            Destroy(gameObject);
        }
    }
}
