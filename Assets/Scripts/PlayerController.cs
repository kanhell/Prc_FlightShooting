using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{

    float x, y;

    public Vector3 limitMax, limitMin;
    Vector3 temp;

    public GameObject[] prefabBullet;
    float time;
    public float speed;

    float fireDelay;
    Animator animator;
    bool onDead;

    // 아이템
    public int Damage, Boom;
    // 폭탄
    public GameObject BoomMissile;
    public int BoomPosY;
    public int BoomDamage;

    private void Start()
    {
        time = 0;  // 죽을때
        fireDelay = 0;
        speed = 10.0f;  // 이동속도

        animator = GetComponent<Animator>();
        onDead = false;

        // 아이템
        Damage = 1;
        Boom = 0;
        // 폭탄
        BoomPosY = -30;
        BoomDamage = 30;

    }

    void Update()
    {
        Move();  // 키보드 입력으로 움직이기, 화면 밖으로 못 나가게
        FireBullet();  // 0.3초마다 prefabBullet 발사, log "Fire"
        OnDeadCheck();  // onDead가 True면 0.6초 뒤 Destroy
        FireBoom();  // Space 누르면 Boom!!
    }

    public void Move()  // 키보드 입력으로 움직이기, 화면 밖으로 못 나가게
    {
        // 키보드 입력
        x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // 이동
        transform.Translate(new Vector3(x, y, 0));

        // 화면 밖으로 못 나가게
        if (transform.position.x > limitMax.x)
        {
            temp.x = limitMax.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.x < limitMin.x)
        {
            temp.x = limitMin.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y > limitMax.y)
        {
            temp.x = transform.position.x;
            temp.y = limitMax.y;
            transform.position = temp;
        }
        if (transform.position.y < limitMin.y)
        {
            temp.x = transform.position.x;
            temp.y = limitMin.y;
            transform.position = temp;
        }
    }

    public void FireBullet()  // 0.3초마다 prefabBullet 발사
    {
        fireDelay += Time.deltaTime;
        if (fireDelay > 0.3f)
        {
            // object 생성 : Instantiate(생성할 대상, 위치, ?)
            Instantiate(prefabBullet[Damage-1], transform.position, Quaternion.identity);
            fireDelay -= 0.3f;
        }
    }  

    public void FireBoom()  // Space 누르면 Boom!! BoomMissile 생성
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Boom >= 1)
            {
                GameObject go = Instantiate(BoomMissile, transform.position, Quaternion.identity);  // 출발 위치는 플레이어 x위치에서 아랫쪽
                go.transform.position = new Vector3(transform.position.x, BoomPosY, transform.position.z);
                Boom--;
                UIManager.instance.BoomCheck(Boom);
            }
        }
    }

    private void OnDrawGizmos()  // 경계선 시각화
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));

    }

    private void OnTriggerEnter2D(Collider2D collision)  // 충돌시 (Tag: enemyBullet), 죽는 모션 (animator State -> 1), onDead True
    {
        if (collision.CompareTag("enemyBullet"))
        {
            animator.SetInteger("State", 1);
            onDead = true;
        }
    }

    private void OnDeadCheck()  // onDead가 True면 0.6초 뒤 Destroy
    {
        if (onDead)
        {
            if(SoundManager.instance.playerDeadSound.isPlaying == false)
                SoundManager.instance.playerDeadSound.Play();
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);
            GameManager.instance.PlayerLifeRemove();
            GameManager.instance.CreatePlayer();
            UIManager.instance.LifeCheck(GameManager.instance.lifeCount);
        }
    }

}
