using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text gameStateText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseButton;
    
    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 60f;
    
    [Header("Audio")]
    [SerializeField] private AudioSource pointSound;
    [SerializeField] private AudioSource gameOverSound;
    
    private float timeRemaining;
    private int score = 0;
    private bool isGameOver = false;
    private bool isPaused = false;

    void Start()
    {
        timeRemaining = gameDuration;
        endGamePanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        pauseButton.onClick.AddListener(TogglePause);
        UpdateScoreUI();
        UpdateBestScoreUI();
        gameStateText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver && !isPaused)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        UpdateScoreUI();
        pointSound?.Play();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Pontok: {score}";
    }

    private void UpdateBestScoreUI()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = $"Legjobb pontszám: {bestScore}";
    }

    private void EndGame()
    {
        isGameOver = true;
        gameOverSound?.Play();
        endGamePanel.SetActive(true);
        finalScoreText.text = $"Pontszámod: {score}";

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            PlayerPrefs.Save();
            bestScoreText.text = $"Új rekord! {score}";
        }

        gameStateText.gameObject.SetActive(true);
        gameStateText.text = "Játék vége!";
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        gameStateText.gameObject.SetActive(isPaused);
        gameStateText.text = isPaused ? "Szünet" : "";
    }
}
