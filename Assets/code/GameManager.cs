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
    public bool gameIsActive = false;  // ���� Ȱ��ȭ ����
    private int winningScore = 5;      // �¸� ���� ����

    public AudioSource playbutton;

    void Start()
    {
        InitializeGame();  // ���� �ʱ�ȭ
    }

    private void Playbutton()
    {
        playbutton.Play();
    }

    // ���� �ʱ�ȭ
    private void InitializeGame()
    {
        // �г� ���� �ʱ�ȭ
        gamestartpanel.SetActive(true);   // ���� �г� Ȱ��ȭ
        gameclearpanel.SetActive(false);  // �¸� �г� ��Ȱ��ȭ
        gameoverpanel.SetActive(false);   // ���� ���� �г� ��Ȱ��ȭ

        // ���� �ʱ�ȭ
        playerScore = 0;
        aiScore = 0;
        UpdateScoreUI();  // UI �ʱ�ȭ
        gameIsActive = false;  // ���� ��Ȱ��ȭ ���·� ����
        Time.timeScale = 0;  // ������ ���� ���·� ����
    }
    // Start ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void GameStartbutton()
    {
        gamestartpanel.SetActive(false);  
        gameclearpanel.SetActive(false);  
        gameoverpanel.SetActive(false);   
        playerScore = 0;                  
        aiScore = 0;                      
        UpdateScoreUI();                  
        gameIsActive = true;              
        Time.timeScale = 1; // ���� ���� (�ӵ��� �������� ����)
        Playbutton();
    }
    // ���� �� ������ ������Ʈ �� �� ȣ��
    public void UpdateScore(int playerIncrement, int aiIncrement)
    {
        if (!gameIsActive) return;  // ������ Ȱ��ȭ�� ���¿����� ���� ������Ʈ

        // ���� ������Ʈ
        playerScore += playerIncrement;
        aiScore += aiIncrement;
        UpdateScoreUI();
        // �¸� �Ǵ� �й� ���� üũ
        CheckGameEnd();
    }
    // ������ UI�� �ݿ�
    private void UpdateScoreUI()
    {
        PlayerscoreTxt.text = "Player Score : " + playerScore;
        AIScoreTxt.text = "Ai Score : " + aiScore;
    }
    // ���� ���� ���� üũ
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

    // �÷��̾� �¸� ó��
    public void GameWin()
    {
        gameIsActive = false;               
        gameclearpanel.SetActive(true);     
        PauseGame();
        Playbutton();
    }

    // ���� ���� ó��(AI �¸�)
    public void GameOver()
    {
        gameIsActive = false;               
        gameoverpanel.SetActive(true);      
        PauseGame();
        Playbutton();
    }
    // ������ �Ͻ������ϴ� �Լ�
    private void PauseGame()
    {
        Time.timeScale = 0;  // ������ �Ͻ����� ���·� ����
    }
    // ������ ������ϴ� �Լ� (�г� �ݱ� �� ȣ�� ����)
    public void ResumeGame()
    {
        Time.timeScale = 1;  // ������ �ٽ� ����
    }
    // ���� ���� ��ư
    public void ExitButton()
    {
        Debug.Log("exit");
        Application.Quit();
        Playbutton();
    }
}
