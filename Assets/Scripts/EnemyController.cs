using System;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyBullet;
    GameObject player;  // player 오브젝트
    PlayerController playerController;  // playerController 스크립트
    float fireDelay;

    Animator animator;
    bool onDead;
    float time;

    // 이동
    Rigidbody2D rg2D;
    float moveSpeed;

    // 아이템
    public GameObject[] item;
    int hp;

    // 태그 임시저장
    public string tagName;

    // 점수
    int score;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        onDead = false;
        time = 0.0f;  // 죽을때
        fireDelay = 2.5f;
        if (gameObject.CompareTag("ItemDropEnemy"))
            hp = 3;
        else
            hp = 1;
        tagName = gameObject.tag;  // 죽으면 Untagged로 태그가 바뀌니 item drop이 안됨 > 미리 저장
        // 이동
        rg2D = GetComponent<Rigidbody2D>();
        moveSpeed = UnityEngine.Random.Range(5.0f, 7.0f);
        Move();  // player 생존시, 방향: player, 속도: moveSpeed
        // 점수
        score = 10;
    }

    
    void Update()
    {
        OnDeadCheck();  // onDead가 True면 0.6뒤 Destroy / item drop
        FireBullet();  // player 생존시, 3초마다 enemyBullet 발사
    }

    public void Move()  // player 생존시, 방향: player, 속도: moveSpeed
    {
        if (player == null)
            return;
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.linearVelocity = dir * moveSpeed;
    }

    public void FireBullet()  // player 생존시, 3초마다 enemyBullet 발사
    {
        if (player == null)
            return;

        fireDelay += Time.deltaTime;
        if (fireDelay > 3f)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay -= 3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  // 충돌시 (Tag: bullet or BoomMissaile), 죽는모션 (State -> 1), OnDead() 실행 ;;; (Tag: BlockCollider), OnDisapear() 실행
    {
        if (collision.CompareTag("bullet"))
            hp = hp - playerController.Damage;
        if (collision.CompareTag("BoomMissile"))
            hp = hp - playerController.BoomDamage;
        if (hp <= 0)
        {
            animator.SetInteger("State", 1);
            OnDead();
        }
        if (collision.CompareTag("BlockCollider"))
        {
            OnDisapear();  // Destroy
        }
    }

    private void OnDead()  // OnDead -> true, 스코어 증가
    {
        onDead = true;
        if (gameObject.tag != "Untagged")
        {
            UIManager.instance.ScoreAdd(score);
        }
        gameObject.tag = "Untagged";  // 파괴애니메이션 중에는 총알이 닿아도 총알이 사라지지 않도록 태그를 Untagged로
    }
    private void OnDisapear()  // Destroy
    {
        Destroy(gameObject);
    }

    private void OnDeadCheck()  // onDead가 True면 0.6뒤 Destroy
    {
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);

            // item drop
            if (tagName == "ItemDropEnemy")
            {
                int temp = UnityEngine.Random.Range(0, 2);
                Instantiate(item[temp], transform.position, Quaternion.identity);
            }
        }
    }
}
