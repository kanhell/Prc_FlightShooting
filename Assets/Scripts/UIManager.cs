using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] ui_Booms;

    // ����
    public Text scoreText;
    public int score;
    //������
    public GameObject[] ui_Lifes;
    // �ϸ�
    public Image blackOut_Curtain;
    float blackOut_Curtain_value;  // ��������
    float blackOut_Curtain_speed;


    private void Awake()  // start() ���� �����
    {
        if (instance == null)
        {
            instance = this;
        }
        else  // [�̱��� : �ϳ��� �����] Ȥ�� UIManager�� ���� ����� �ϳ��� ����� ���� ;; static ������ ����ϱ� ����
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  // �� �̵��� �ı����� �ʵ�����
    }

    void Start()
    {
        score = 0;
        blackOut_Curtain_value = 1.0f;
        blackOut_Curtain_speed = 0.5f;
    }

    private void Update()
    {
        if(blackOut_Curtain_value > 0)
            HideBlackOut_Curtain();
    }


    public void BoomCheck(int boomCount)  // boom ������ŭ ui_Booms Ȱ��ȭ�ϱ�
    {
        for (int i = 0; i < ui_Booms.Length; i++)
        {
            if (i+1 <= boomCount)
                ui_Booms[i].SetActive(true);
            else
                ui_Booms[i].SetActive(false);
        }
    }

    public void ScoreAdd(int _score)  // ���ھ� ����
    {
        // enemy ���̸� 10
        // ������ �ʰ� ȹ���ϸ� 100
        score += _score;
        scoreText.text = score.ToString();

    }

    public void LifeCheck(int lifeCount)  // life ������ŭ ui_Lifes Ȱ��ȭ�ϱ�
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
}
