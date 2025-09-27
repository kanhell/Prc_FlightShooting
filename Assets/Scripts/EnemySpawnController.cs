using System;
using System.Threading;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public Transform[] enemySpawns;  // 생성위치
    public GameObject[] enemyGameObject;  // 적 프리팹
    float time;
    float respawnTime;  // enemyGameObject 생성 시간
    int enemyCount;  // enemyGameObject 생성 숫자
    int[] randomCount;  // 랜덤 숫자 변수
    int wave;  // 웨이브
    GameObject player;  // 플레이어

    
    void Start()
    {
        time = 3.5f;  // enemyGameObject 생성할 때
        respawnTime = 4.0f;  // enemyGameObject 생성 주기
        enemyCount = 5;  // 한번에 생성할 enemyGameObject 수
        randomCount = new int[enemyCount];  // enemyGameObject 랜덤 위치 (enemySpawns) 설정
        wave = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Timer();  // respawnTime마다 enemyCount만큼의 enemyGameObject 생성, wave++
    }

    void Timer()  // respawnTime마다 enemyCount만큼의 enemyGameObject 생성, wave++
    {
        time += Time.deltaTime;
        if (time > respawnTime)
        {
            RandomPos();  // enemyCount만큼의 랜덤 스폰 위치(0-8) 설정 -> randomCount[i]
            EnemyCreate();  // player가 살아있다면, enemyCount만큼의 enemy 생성
            wave++;
            time -= time;
        }
    }

    void RandomPos()  // enemyCount만큼의 랜덤 위치 설정
    {
        for (int i = 0; i < enemyCount; i++)
        {
            randomCount[i] = UnityEngine.Random.Range(0, 9);
        }
    }

    void EnemyCreate()  // player가 살아있다면, enemyCount만큼의 enemyGameObject 생성
    {
        if (player == null)
            return;
        for (int i = 0; i < enemyCount; i++)
        {
            int tmpCnt = UnityEngine.Random.Range(0, enemyGameObject.Length);  // 랜덤 적 선택
            GameObject tmp = GameObject.Instantiate(enemyGameObject[tmpCnt]);  // 생성
            tmp.transform.position = enemySpawns[randomCount[i]].position;  // 위치
            float tmpX = tmp.transform.position.x;  // 살짝씩 위치 다르게
            float result = UnityEngine.Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, transform.position.z);

        }
    }
}
