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
    private int playerScore;
    [SerializeField]
    public int aiScore;
    [SerializeField]
    private Text PlayerscoreTxt;
    [SerializeField]
    public Text AIScoreTxt;
    [SerializeField]
    private GameObject gamestartpanel;
    [SerializeField]
    private GameObject gameclearpanel;
    [SerializeField]
    private GameObject gameoverpanel;
    public bool gameIsActive = false;  // 게임 활성화 상태
    private int winningScore = 5;      // 승리 조건 점수

    public AudioSource playbutton;

    void Start()
    {
        InitializeGame();  // 게임 초기화
    }

    private void Playbutton()
    {
        playbutton.Play();
    }

    // 게임 초기화
    private void InitializeGame()
    {
        // 패널 상태 초기화
        gamestartpanel.SetActive(true);   // 시작 패널 활성화
        gameclearpanel.SetActive(false);  // 승리 패널 비활성화
        gameoverpanel.SetActive(false);   // 게임 오버 패널 비활성화

        // 점수 초기화
        playerScore = 0;
        aiScore = 0;
        UpdateScoreUI();  // UI 초기화
        gameIsActive = false;  // 게임 비활성화 상태로 시작
        Time.timeScale = 0;  // 게임을 정지 상태로 시작
    }
    // Start 버튼을 눌렀을 때 호출되는 함수
    public void GameStartbutton()
    {
        gamestartpanel.SetActive(false);  
        gameclearpanel.SetActive(false);  
        gameoverpanel.SetActive(false);   
        playerScore = 0;                  
        aiScore = 0;                      
        UpdateScoreUI();                  
        gameIsActive = true;              
        Time.timeScale = 1; // 게임 시작 (속도를 정상으로 돌림)
        Playbutton();
    }
    // 게임 중 점수가 업데이트 될 때 호출
    public void UpdateScore(int playerIncrement, int aiIncrement)
    {
        if (!gameIsActive) return;  // 게임이 활성화된 상태에서만 점수 업데이트

        // 점수 업데이트
        playerScore += playerIncrement;
        aiScore += aiIncrement;
        UpdateScoreUI();
        // 승리 또는 패배 조건 체크
        CheckGameEnd();
    }
    // 점수를 UI에 반영
    private void UpdateScoreUI()
    {
        PlayerscoreTxt.text = "Player Score : " + playerScore;
        AIScoreTxt.text = "Ai Score : " + aiScore;
    }
    // 게임 종료 조건 체크
    private void CheckGameEnd()
    {
        if (playerScore >= winningScore)
        {
            GameWin();
        }
        if (aiScore >= winningScore)
        {
            GameOver();
        }
    }

    // 플레이어 승리 처리
    public void GameWin()
    {
        gameIsActive = false;               
        gameclearpanel.SetActive(true);     
        PauseGame();
        Playbutton();
    }

    // 게임 오버 처리(AI 승리)
    public void GameOver()
    {
        gameIsActive = false;               
        gameoverpanel.SetActive(true);      
        PauseGame();
        Playbutton();
    }
    // 게임을 일시정지하는 함수
    private void PauseGame()
    {
        Time.timeScale = 0;  // 게임을 일시정지 상태로 설정
    }
    // 게임을 재시작하는 함수 (패널 닫기 시 호출 가능)
    public void ResumeGame()
    {
        Time.timeScale = 1;  // 게임을 다시 진행
    }
    // 게임 종료 버튼
    public void ExitButton()
    {
        Debug.Log("exit");
        Application.Quit();
        Playbutton();
    }
}
