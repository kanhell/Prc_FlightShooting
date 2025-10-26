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

    // ������
    public int Damage, Boom;
    // ��ź
    public GameObject BoomMissile;
    public int BoomPosY;
    public int BoomDamage;

    private void Start()
    {
        time = 0;  // ������
        fireDelay = 0;
        speed = 10.0f;  // �̵��ӵ�

        animator = GetComponent<Animator>();
        onDead = false;

        // ������
        Damage = 1;
        Boom = 0;
        // ��ź
        BoomPosY = -30;
        BoomDamage = 30;

    }

    void Update()
    {
        Move();  // Ű���� �Է����� �����̱�, ȭ�� ������ �� ������
        FireBullet();  // 0.3�ʸ��� prefabBullet �߻�, log "Fire"
        OnDeadCheck();  // onDead�� True�� 0.6�� �� Destroy
        FireBoom();  // Space ������ Boom!!
    }

    public void Move()  // Ű���� �Է����� �����̱�, ȭ�� ������ �� ������
    {
        // Ű���� �Է�
        x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // �̵�
        transform.Translate(new Vector3(x, y, 0));

        // ȭ�� ������ �� ������
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

    public void FireBullet()  // 0.3�ʸ��� prefabBullet �߻�
    {
        fireDelay += Time.deltaTime;
        if (fireDelay > 0.3f)
        {
            // object ���� : Instantiate(������ ���, ��ġ, ?)
            Instantiate(prefabBullet[Damage-1], transform.position, Quaternion.identity);
            fireDelay -= 0.3f;
        }
    }  

    public void FireBoom()  // Space ������ Boom!! BoomMissile ����
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Boom >= 1)
            {
                GameObject go = Instantiate(BoomMissile, transform.position, Quaternion.identity);  // ��� ��ġ�� �÷��̾� x��ġ���� �Ʒ���
                go.transform.position = new Vector3(transform.position.x, BoomPosY, transform.position.z);
                Boom--;
                UIManager.instance.BoomCheck(Boom);
            }
        }
    }

    private void OnDrawGizmos()  // ��輱 �ð�ȭ
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));

    }

    private void OnTriggerEnter2D(Collider2D collision)  // �浹�� (Tag: enemyBullet), �״� ��� (animator State -> 1), onDead True
    {
        if (collision.CompareTag("enemyBullet"))
        {
            animator.SetInteger("State", 1);
            onDead = true;
        }
    }

    private void OnDeadCheck()  // onDead�� True�� 0.6�� �� Destroy
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
