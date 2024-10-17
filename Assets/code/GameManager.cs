using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    [SerializeField]
    private GameObject poop;
    private int score;
    private bool doubleScoreActive = false;
    [SerializeField]
    private Text scoreTxt;
    private Text AIScoreTxt;
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;
    void Start()
    {

    }

    public bool stopTrigger = true;
    public void GameOver()
    {
        stopTrigger = false;
        if (score >= PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);
    }

    public void GameStart() //���� ����
    {
        score = 0;
        scoreTxt.text = "Score : " + score;
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine()); //������ ���� �� �� �ڵ�
        StartCoroutine(CreateItemRoutine());
        panel.SetActive(false);

    }
    public void GameExit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}