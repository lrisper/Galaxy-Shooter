using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] livess;
    public Image livesImageDisplay;
    public GameObject titleScreen;

    public Text scoreText;
    public int score;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = livess[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
     }

     public void ShowTitleScreen()
     {
        titleScreen.SetActive(true);
     }

     public void HideTitleScreen()
     {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";
     }
}
