using System;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //
    GameObject player;
    PlayerController playerController;
    // 체력바
    public float hp1;  // 초록색
    public float hp2;  // 빨간색
    //
    Animator animator;
    //
    bool onDead;
    bool isSpawn;
    //
    int score;
    //
    float time;
    //
    Transform spawnMovePos;
    float speed;


    void Awake()
    {
        hp1 = 150.0f;
        hp2 = 150.0f;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        spawnMovePos = GameObject.Find("BossSpawn").GetComponent<Transform>();

        animator = GetComponent<Animator>();

        onDead = false;
        isSpawn = true;

        score = 1000;

        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn)
            BossSpawn();
        if (onDead)
            time += Time.deltaTime;
        if (time > 0.6f)
            Destroy(gameObject);
    }

    private void OnDead()
    {
        onDead = true;
        UIManager.instance.isBossSpawn = false;
        if (gameObject.tag != "Untagged")
        {
            UIManager.instance.ScoreAdd(score);
            SoundManager.instance.enemyDeadSound.Play();
        }
        gameObject.tag = "Untagged";
    }

    private void BossSpawn()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnMovePos.position, Time.deltaTime * speed);
        if (transform.position == spawnMovePos.position)
            isSpawn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)  // 충돌시 (Tag: bullet or BoomMissaile), 죽는모션 (State -> 1), OnDead() 실행 ;;; (Tag: BlockCollider), OnDisapear() 실행
    {
        if (collision.CompareTag("bullet"))
        {
            if (hp1 > 0)
                hp1 = hp1 - playerController.Damage;
            else
                hp2 = hp2 - playerController.Damage;
        }
        if (collision.CompareTag("BoomMissile"))
        {
            if (hp1 > 0)
                hp1 = hp1 - playerController.BoomDamage;
            else
                hp2 = hp2 - playerController.BoomDamage;
        }
        if (hp2 <= 0)
        {
            animator.SetTrigger("Die");
            OnDead();
        }
    }

}
