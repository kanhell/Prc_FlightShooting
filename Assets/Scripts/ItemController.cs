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
        speed = 10.0f;  // 이동 속도
        score = 100;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);  // 아래로 이동
    }

    protected void OnTriggerEnter2D(Collider2D collision)  // 충돌시 (Tag: Player), Destroy, ItemGain() 실행 ;;; (Tag: BlockCollider), Destroy
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

    protected virtual void ItemGain()  // virtual : 가상클래스 만들어 놓고, 자식 클래스에서 구현할게!
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }
}
