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
        time = 0;  // ������
        player = GameObject.FindGameObjectWithTag("Player");
        rg2D = GetComponent<Rigidbody2D>();

        FireBullet();  // ����: player, �ӵ� : moveSpeed
    }


    void Update()
    {
        RotateBullet();  // ȸ��
        DestroyBullet();  // 5�� �� Destroy
    }

    private void FireBullet()  // ����: player, �ӵ� : moveSpeed
    {
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;  // ���� ���Ͱ� ����
        rg2D.linearVelocity = dir * moveSpeed;
    }

    private void RotateBullet()  // ȸ��
    {
        transform.rotation = Quaternion.Euler(0, 0, time * rotateSpeed);
    }

    private void DestroyBullet()  // 5�� �� Destroy
    {
        time += Time.deltaTime;
        if(time > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  // �浹�� (Tag: Player), Destroy
    {
        if (collision.CompareTag("Player") || collision.CompareTag("BoomMissile"))
        {
            Destroy(gameObject);
        }
    }
}
