using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    private int score = 0;
    private bool isGameOver = false;

    [Header("UI References")]
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text gameOverText;
    public UnityEngine.UI.Text healthText;
    public GameObject gameOverPanel;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
        
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            UpdateUI();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER\nScore: " + score + "\nPress R to Restart";
        }
        
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        
        if (healthText != null && player != null)
        {
            healthText.text = "Health: " + player.GetCurrentHealth();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public int GetScore()
    {
        return score;
    }
}
