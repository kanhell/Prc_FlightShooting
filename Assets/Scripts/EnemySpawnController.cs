using System;
using System.Threading;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public Transform[] enemySpawns;  // 생성위치
    public GameObject[] enemyGameObject;  // 적 프리팹
    float time;
    float respawnTime;  // 적 생성 시간
    int enemyCount;  // 적 생성 숫자
    int[] randomCount;  // 랜덤 숫자 변수
    int wave;  // 웨이브
    GameObject player;  // 플레이어

    
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
            int tmpCnt = Random.Range(0, enemyGameObject.Length);  // 랜덤 적 선택
            GameObject tmp = GameObject.Instantiate(enemyGameObject[tmpCnt]);  // 생성
            tmp.transform.position = enemySpawns[randomCount[i]].position;  // 위치
            float tmpX = tmp.transform.position.x;  // 살짝씩 위치 다르게
            float result = Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, transform.position.z);

        }
    }
}
