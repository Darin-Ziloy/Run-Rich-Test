using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UI ui;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameWin;

    public static GameManager instance { get; private set; }
    public GameObject playerInstance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LevelManager.instance.LoadLevel(PlayerPrefs.GetInt("Level", 1) - 1);
        SpawnPlayer();
        gameOverUI.SetActive(false);
        winUI.SetActive(false);

        ui.StartUI.currentLevelText.text = "Уровень " + PlayerPrefs.GetInt("Level", 1).ToString();
    }

    public void StartGame()
    {
        ui.StartUI.gameObject.SetActive(false);
        ui.GameUI.SetActive(true);
        OnGameStart?.Invoke();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player prefab or spawn point not set in GameManager.");
        }
    }

    public void GameOver()
    {
        gameOverUI.gameObject.SetActive(true);
        OnGameOver?.Invoke();

        UI.instance.GameOverUI.restartButton.onClick.AddListener(RestartGame);
    }

    public void WinGame()
    {
        int currettLevel = PlayerPrefs.GetInt("Level", 1);
        PlayerPrefs.SetInt("Level", currettLevel + 1);
        
        winUI.gameObject.SetActive(true);
        OnGameWin?.Invoke();

        UI.instance.GameVictoryUI.currentLevelText.text = "Уровень " + currettLevel.ToString();
        UI.instance.GameVictoryUI.nextLevelButton.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}