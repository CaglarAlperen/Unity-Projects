using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas gameCanvas;
    [SerializeField] Canvas gameoverCanvas;
    [SerializeField] Canvas highscoreCanvas;
    [SerializeField] Text scoreText;
    [SerializeField] Text highscoreText;

    int activeScene;
    int highscore;

    private void Start()
    {
        Application.targetFrameRate = 60;
        gameCanvas.enabled = false;
        gameoverCanvas.enabled = false;
        highscoreCanvas.enabled = false;
        activeScene = 0;
        highscore = PlayerPrefs.GetInt("highscore",0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (activeScene == 2)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void StartGame()
    {
        startCanvas.enabled = false;
        gameCanvas.enabled = true;
        activeScene = 1;
    }

    public void FinishGame()
    {
        gameCanvas.enabled = false;
        gameoverCanvas.enabled = true;
        int score = GetComponent<Score>().GetScore();
        scoreText.text = score.ToString();
        activeScene = 2;
        if (score > highscore)
        {
            highscoreCanvas.enabled = true;
            PlayerPrefs.SetInt("highscore", score);
            highscore = score;
        }
        highscoreText.text = highscore.ToString();
        GetComponent<BonusSpawner>().StopSpawn();
    }

}
