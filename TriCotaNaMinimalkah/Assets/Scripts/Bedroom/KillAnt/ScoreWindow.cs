using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreWindow : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score;
    public void IncreaseScore()
    {
        score = int.Parse(scoreText.text);
        score++;
        scoreText.text = score.ToString();
        
    }
}
