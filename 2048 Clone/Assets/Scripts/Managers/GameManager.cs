using System;
using UnityEngine;

public class GameManager : MBSingleton<GameManager>
{
    private int _score;
    private int _highScore;
    public static event Action OnGameStarted;
    public static event Action OnGameLost;
    public static event Action On2048;
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnHighScoreChanged;

    private bool _reached2048;
    private bool _controlOn = true;
    public bool ControlOn => _controlOn;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _controlOn = true;
        _score = 0;
        _reached2048 = false;
        _highScore = PlayerPrefs.GetInt("highScore", 0);
        OnScoreChanged?.Invoke(_score);
        OnHighScoreChanged?.Invoke(_highScore);
        OnGameStarted?.Invoke();
    }

    public void RestartGame()
    {
        NumberCell[] cells = FindObjectsOfType<NumberCell>();
        foreach (NumberCell cell in cells)
        {
            Destroy(cell.gameObject);
        }
        GridManager.Instance.ClearGrid();
        StartGame();
    }

    public void GameLost()
    {
        _controlOn = false;
        if (_score > _highScore)
        {
            _highScore = _score;
            OnHighScoreChanged?.Invoke(_highScore);
            PlayerPrefs.SetInt("highScore", _score);
        }
        OnGameLost?.Invoke();
    }

    public void AddScore(int score)
    {
        _score += score;
        OnScoreChanged?.Invoke(_score);

        if (_score == 2048 && !_reached2048)
        {
            _reached2048 = true;
            On2048?.Invoke();
        }
    }
}
