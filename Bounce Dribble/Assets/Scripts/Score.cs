using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    [SerializeField] Text scoreText;
    [SerializeField] int scoreToLevelUp = 10;
    [SerializeField] float horizontalPush = 0.5f;
    int startingScore = 0;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = startingScore;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score++;
        UpdateDisplay();
        if (score % scoreToLevelUp == 0)
        {
            GetComponent<SpikeSpawner>().IncreaseDifficulty();
        }
        if (score % 2 == 0)
        {
            GetComponent<SpikeSpawner>().UpdateSpikes();
        }
        if (score > 5*scoreToLevelUp && score % scoreToLevelUp == 0)
        {
            FindObjectOfType<Ball>().AddHorizontalVel(horizontalPush);
        }
    }

    public int GetScore()
    {
        return score;
    }
}
