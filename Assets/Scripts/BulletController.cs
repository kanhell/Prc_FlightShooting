using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    float time;

    void Start()
    {
        speed = 30.0f;
        time = 0;  // ������
    }

    void Update()
    {
        FireBullet();  // �Ѿ� �߻�
        DestroyBullet();  // 5�� �� Destroy
    }

    private void DestroyBullet()  // 5�� �� Destroy
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void FireBullet()  // �Ѿ� �߻�
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)  // �浹�� (Tag: enemy or ItemDropEnemy), Destroy
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("ItemDropEnemy"))
        {
            Destroy(gameObject);

        }
    }

}
