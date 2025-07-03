using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int score;
    public TMP_Text scoreText;
    public GameObject GameOverPanel;
    public TMP_Text currentText;
    public TMP_Text highScoreText;
    public Button restartButton;
    public Camera mainCam;
    public Image backgroundImage;
    private int randomIndex;
    public Color[] colorToChange;
    private bool isGameOver = false; // Flag to track if GameOver has already been triggered

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        randomIndex = Random.Range(0, colorToChange.Length);
        ChangeColor();
    }
    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        GameOverPanel.SetActive(false);
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartLevel);
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        randomIndex = Random.Range(0, colorToChange.Length);
        ApplyColor();
    }

    public void GameOver()
    {
        //FindObjectOfType<AudioManager>().Play("GameOver");
        if (isGameOver) return; // Prevent multiple calls

        isGameOver = true; // Set flag to true

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Stop("PlayerRunning"); // Stop only the running sound
            audioManager.Play("GameOver"); // Play game over sound once
        }

        // Optionally pause the game (if you want everything to freeze)
        Time.timeScale = 0f; // Stops all physics-based movements

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        PlayerPrefs.SetInt("Score", score);
        currentText.text = PlayerPrefs.GetInt("Score").ToString();
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        GameOverPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) // Ensure obstacles have the correct tag
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    public void RestartLevel()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    void ApplyColor()
    {
        if (score >= 2)
        {
            ChangeColor();
        }
    }
    public void ChangeColor()
    {
        mainCam.backgroundColor = colorToChange[randomIndex];
        backgroundImage.color = colorToChange[randomIndex];
    }
}