using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text timerText; 
    public Text scoreText; 
    public GameObject endGamePanel; 
    public Text finalScoreText; 
    public Button restartButton; 

    private float timeRemaining = 60f;
    private int score = 0; 
    private bool isGameOver = false; 

    void Start()
    {
        
        endGamePanel.SetActive(false);

        
        restartButton.onClick.AddListener(RestartGame);

       
        UpdateScoreUI();
    }

    void Update()
    {
        if (!isGameOver)
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
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateTimerUI()
    {
      
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateScoreUI()
    {
        
        scoreText.text = "Pontok: " + score.ToString();
    }

    private void EndGame()
    {
        isGameOver = true;

       
        endGamePanel.SetActive(true);
        finalScoreText.text = "Pontszámod: " + score.ToString();
    }

    private void RestartGame()
    {
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
