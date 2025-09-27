using System;
using System.Threading;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public Transform[] enemySpawns;  // ������ġ
    public GameObject[] enemyGameObject;  // �� ������
    float time;
    float respawnTime;  // �� ���� �ð�
    int enemyCount;  // �� ���� ����
    int[] randomCount;  // ���� ���� ����
    int wave;  // ���̺�
    GameObject player;  // �÷��̾�

    
    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCount = new int[enemyCount];
        wave = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        time += Time.deltaTime;
        if (time > respawnTime)
        {
            RandomPos();
            EnemyCreate();
            wave++;
            time -= time;
        }
    }

    void RandomPos()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            randomCount[i] = Random.Range(0, 9);
        }
    }

    void EnemyCreate()
    {
        if (player == null)
            return;
        for (int i = 0; i < enemyCount; i++)
        {
            int tmpCnt = Random.Range(0, enemyGameObject.Length);  // ���� �� ����
            GameObject tmp = GameObject.Instantiate(enemyGameObject[tmpCnt]);  // ����
            tmp.transform.position = enemySpawns[randomCount[i]].position;  // ��ġ
            float tmpX = tmp.transform.position.x;  // ��¦�� ��ġ �ٸ���
            float result = Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, transform.position.z);

        }
    }
}
