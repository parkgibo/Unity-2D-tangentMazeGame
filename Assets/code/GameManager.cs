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

    public void GameStart() //게임 시작
    {
        score = 0;
        scoreTxt.text = "Score : " + score;
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine()); //아이템 생성 및 적 코드
        StartCoroutine(CreateItemRoutine());
        panel.SetActive(false);

    }
    public void GameExit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}