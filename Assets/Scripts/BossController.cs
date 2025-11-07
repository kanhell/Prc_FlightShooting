using System;
using System.Collections;
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

    // 총알
    public Transform LAttackPos;
    public Transform RAttackPos;
    public GameObject bossBullet;
    float fireDelay;

    // 애니메이션 상태
    int animNumber;  // -1: 대기.이동반복, 0: 대기.이동, 1: L공격, 2: R공격, 3: Die

    // v피격
    public SpriteRenderer spriteRenderer;
    Color currentColor;


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

        animNumber = 0;

        currentColor = spriteRenderer.color;
    }


    void Update()
    {
        if (isSpawn)
            BossSpawn();
        if (onDead)
            time += Time.deltaTime;
        if (time > 0.6f)
            Destroy(gameObject);
        if (player == null && GameManager.instance.lifeCount >= 0)
            PlayerFind();
        FireBullet();
        AnimationSystem();
    }

    public void PlayerFind()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent <PlayerController>();
    }

    void FireBullet()
    {
        // 페이즈 1 (초록 hp바)
        if (hp1 > 0 && isSpawn == false)
        {
            fireDelay += Time.deltaTime;
            if (fireDelay > 1.0f && animNumber != 1)
            {
                // L공격
                animNumber = 1;
                fireDelay -= fireDelay;
            }
        }
        // 페이즈 2 (빨강 hp바)
        if (hp1 <= 0)
        {
            fireDelay += Time.deltaTime;
            if (fireDelay > 1.0f && animNumber != 2)
            {
                // R공격
                animNumber = 2;
                fireDelay -= fireDelay;
            }
        }
    }

    void AnimationSystem()
    {
        if (animNumber == 0)
        {
            StartCoroutine(Co_Idle());
        }
        if (animNumber == 1)
        {
            StartCoroutine(Co_LAttack());
        }
        if (animNumber == 2)
        {
            StartCoroutine(Co_RAttack());
        }
    }

    IEnumerator Co_Idle()
    {
        animNumber = -1;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.6f);
    }

    IEnumerator Co_LAttack()
    {
        animNumber = -1;
        animator.SetTrigger("LAttack");
        yield return new WaitForSeconds(0.6f);
        animNumber = 0;
    }

    IEnumerator Co_RAttack()
    {
        animNumber = -1;
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animNumber = 0;
    }

    void LAttack()
    {
        if (player == null)
            return;
        Instantiate(bossBullet, LAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }

    void RAttack()
    {
        if (player == null)
            return;
        Instantiate(bossBullet, RAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
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
            StartCoroutine(OnDemageEffect());
        }
        if (collision.CompareTag("BoomMissile"))
        {
            if (hp1 > 0)
                hp1 = hp1 - playerController.BoomDamage;
            else
                hp2 = hp2 - playerController.BoomDamage;
            StartCoroutine(OnDemageEffect());
        }
        if (hp2 <= 0)
        {
            animator.SetTrigger("Die");
            OnDead();
        }
    }

    IEnumerator OnDemageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = currentColor;
    }

}