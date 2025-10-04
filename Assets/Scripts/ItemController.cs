using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    protected GameObject player;
    protected float speed;
    protected int score;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        speed = 10.0f;  // �̵� �ӵ�
        score = 100;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);  // �Ʒ��� �̵�
    }

    protected void OnTriggerEnter2D(Collider2D collision)  // �浹�� (Tag: Player), Destroy, ItemGain() ���� ;;; (Tag: BlockCollider), Destroy
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            ItemGain();
        }
        if (collision.CompareTag("BlockCollider"))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void ItemGain()  // virtual : ����Ŭ���� ����� ����, �ڽ� Ŭ�������� �����Ұ�!
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }
}
