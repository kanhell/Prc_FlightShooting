using System;
using System.Threading;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public Transform[] enemySpawns;  // ������ġ
    public GameObject[] enemyGameObject;  // �� ������
    float time;
    float respawnTime;  // enemyGameObject ���� �ð�
    int enemyCount;  // enemyGameObject ���� ����
    int[] randomCount;  // ���� ���� ����
    int wave;  // ���̺�
    GameObject player;  // �÷��̾�

    
    void Start()
    {
        time = 3.5f;  // enemyGameObject ������ ��
        respawnTime = 4.0f;  // enemyGameObject ���� �ֱ�
        enemyCount = 5;  // �ѹ��� ������ enemyGameObject ��
        randomCount = new int[enemyCount];  // enemyGameObject ���� ��ġ (enemySpawns) ����
        wave = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Timer();  // respawnTime���� enemyCount��ŭ�� enemyGameObject ����, wave++
    }

    void Timer()  // respawnTime���� enemyCount��ŭ�� enemyGameObject ����, wave++
    {
        time += Time.deltaTime;
        if (time > respawnTime)
        {
            RandomPos();  // enemyCount��ŭ�� ���� ���� ��ġ(0-8) ���� -> randomCount[i]
            EnemyCreate();  // player�� ����ִٸ�, enemyCount��ŭ�� enemy ����
            wave++;
            time -= time;
        }
    }

    void RandomPos()  // enemyCount��ŭ�� ���� ��ġ ����
    {
        for (int i = 0; i < enemyCount; i++)
        {
            randomCount[i] = UnityEngine.Random.Range(0, 9);
        }
    }

    void EnemyCreate()  // player�� ����ִٸ�, enemyCount��ŭ�� enemyGameObject ����
    {
        if (player == null)
            return;
        for (int i = 0; i < enemyCount; i++)
        {
            int tmpCnt = UnityEngine.Random.Range(0, enemyGameObject.Length);  // ���� �� ����
            GameObject tmp = GameObject.Instantiate(enemyGameObject[tmpCnt]);  // ����
            tmp.transform.position = enemySpawns[randomCount[i]].position;  // ��ġ
            float tmpX = tmp.transform.position.x;  // ��¦�� ��ġ �ٸ���
            float result = UnityEngine.Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, transform.position.z);

        }
    }
}
