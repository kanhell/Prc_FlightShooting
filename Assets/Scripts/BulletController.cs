using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    float time;

    void Start()
    {
        speed = 30.0f;
        time = 0;  // 죽을때
    }

    void Update()
    {
        FireBullet();  // 총알 발사
        DestroyBullet();  // 5초 뒤 Destroy
    }

    private void DestroyBullet()  // 5초 뒤 Destroy
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void FireBullet()  // 총알 발사
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)  // 충돌시 (Tag: enemy or ItemDropEnemy), Destroy
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("ItemDropEnemy"))
        {
            Destroy(gameObject);

        }
    }

}
