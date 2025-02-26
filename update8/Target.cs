using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class Target : MonoBehaviour
{
    public bool isTargetPractice;
    public float health = 10f;
    public float defaultHealth;
    public static int score = 0;
    private int comboMultiplier = 1;
    private float lastHitTime;
    private float comboResetTime = 2f;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText; 
    public GameObject restartButton; 
    public AudioSource hitSound;
    public ParticleSystem hitEffect;
    
    private float timer = 10f;
    private bool isGameOver = false;
    
    void Start()
    {
        defaultHealth = health;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        
        if (timerText == null)
            timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        if (restartButton != null)
        {
            restartButton.SetActive(false);
            restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
        }
        
        UpdateScoreUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime; 
            UpdateTimerUI();

            if (timer <= 0)
                EndGame();
        }
    }

    public void TakeDamage(float amount, bool isHeadshot)
    {
        if (Time.time - lastHitTime <= comboResetTime)
            comboMultiplier++;
        else
            comboMultiplier = 1;
        
        lastHitTime = Time.time;
        
        health -= amount;
        if (hitEffect != null)
            hitEffect.Play();
        
        if (hitSound != null)
            hitSound.Play();
        
        if (health <= 0)
            Die(isHeadshot);
    }

    void Die(bool isHeadshot)
    {
        int baseScore = 2;
        if (isHeadshot)
            baseScore *= 2;
        
        AddScore(baseScore * comboMultiplier);

        if (isTargetPractice)
        {
            health = defaultHealth;
            gameObject.transform.position = new Vector3(Random.Range(2, -30), 5, Random.Range(13, 65));
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Target Broken");
    }

    void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Pontszám: " + score;
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = $"Idő: {minutes:D2}:{seconds:D2}";
        }
    }

    void EndGame()
    {
        isGameOver = true;
        if (restartButton != null)
            restartButton.SetActive(true); 

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void RestartGame()
    {
        score = 0;
        timer = 10f;
        isGameOver = false;
        comboMultiplier = 1;

        UpdateScoreUI();
        UpdateTimerUI();

        if (restartButton != null)
            restartButton.SetActive(false); 

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("Játék újraindult");
    }
}
