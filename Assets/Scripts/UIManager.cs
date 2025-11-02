using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] ui_Booms;

    // 점수
    public Text scoreText;
    public Text highScoreText;
    public int score;
    public int highScore;

    //라이프
    public GameObject[] ui_Lifes;
    // 암막
    public Image blackOut_Curtain;
    float blackOut_Curtain_value;  // 불투명도값
    float blackOut_Curtain_speed;
    // 게임오버
    public Image gameOverImage;



    private void Awake()  // start() 전에 실행됨
    {
        if (instance == null)
        {
            instance = this;
        }
        else  // [싱글톤 : 하나만 남기기] 혹시 UIManager이 여러 개라면 하나만 남기고 삭제 ;; static 오류를 대비하기 위함
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  // 씬 이동시 파괴되지 않도록함
    }

    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        blackOut_Curtain_value = 1.0f;
        blackOut_Curtain_speed = 0.5f;
    }

    private void Update()
    {
        if(blackOut_Curtain_value > 0)
            HideBlackOut_Curtain();
    }


    public void BoomCheck(int boomCount)  // boom 갯수만큼 ui_Booms 활성화하기
    {
        for (int i = 0; i < ui_Booms.Length; i++)
        {
            if (i+1 <= boomCount)
                ui_Booms[i].SetActive(true);
            else
                ui_Booms[i].SetActive(false);
        }
    }

    public void ScoreAdd(int _score)  // 스코어 증가
    {
        // enemy 죽이면 10
        // 아이템 초과 획득하면 100
        score += _score;
        scoreText.text = score.ToString();

    }

    public void LifeCheck(int lifeCount)  // life 갯수만큼 ui_Lifes 활성화하기
    {
        for (int i = 0; i < ui_Lifes.Length; i++)
        {
            if (i + 1 <= lifeCount)
                ui_Lifes[i].SetActive(true);
            else
                ui_Lifes[i].SetActive(false);
        }
    }

    public void HideBlackOut_Curtain()
    {
        blackOut_Curtain_value -= Time.deltaTime * blackOut_Curtain_speed;
        blackOut_Curtain.color = new Color(0.0f, 0.0f, 0.0f, blackOut_Curtain_value);
    }

    public void GameOver()
    {
        gameOverImage.gameObject.SetActive(true);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }
        highScoreText.text = highScore.ToString();
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");

        Destroy(UIManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(SoundManager.instance.gameObject);
    }
}
