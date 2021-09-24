using UnityEngine;
using TMPro;

public class UIManager : MBSingleton<UIManager>
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject panel2048;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI restartText;

    private void Awake()
    {
        GameManager.OnGameStarted += GameStarted;
        GameManager.OnGameLost += GameLost;
        GameManager.On2048 += On2048;
        GameManager.OnScoreChanged += UpdateScore;
        GameManager.OnHighScoreChanged += UpdateHighScore;

        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            InputController.OnSpace += CheckRestart;
        }
    }

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            restartText.text = "Tap to replay";
        }
        else
        {
            restartText.text = "Tap space to replay";
        }
    }

    private void GameStarted()
    {
        gameOverPanel.SetActive(false);
    }

    private void GameLost()
    {
        gameOverPanel.SetActive(true);
    }

    private void On2048()
    {
        panel2048.SetActive(true);
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateHighScore(int score)
    {
        highscoreText.text = score.ToString();
    }

    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }

    public void CheckRestart()
    {
        if (gameOverPanel.activeSelf == true)
        {
            Restart();
        }
    }

    public void Continue()
    {
        panel2048.SetActive(false);
    }
}
