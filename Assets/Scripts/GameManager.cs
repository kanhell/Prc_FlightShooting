using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerController playerController;
    public Vector3 playerPos;
    public int lifeCount;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        lifeCount = 2;
        UIManager.instance.LifeCheck(lifeCount);
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        if (lifeCount >= 0)
        {
            GameObject player = Instantiate(playerPrefab);
            float x = UnityEngine.Random.Range(-9.0f, 9.0f);
            float y = -18.0f;
            playerPos = new Vector3 (x, y, 0);
            player.transform.position = playerPos;
            playerController = player.GetComponent<PlayerController>();
            UIManager.instance.BoomCheck(playerController.Boom);  // boom은 다시 0개.

        }
    }

    // 플레이어 라이프 감소
    public void PlayerLifeRemove()
    {
        lifeCount--;
    }
}
