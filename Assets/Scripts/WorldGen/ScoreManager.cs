using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    public int savehighscore;

    public int highscore;

    public Text highscoreText;

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (FirstStartManager.isFirstStart)
        {
            score = 0;
            PlayerPrefs.SetInt("PlayerScore", score); // Save score to PlayerPrefs
        }
        else
        {
            // Load score from PlayerPrefs
            score = PlayerPrefs.GetInt("PlayerScore", 0);
        }
        
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void UpdateScore()
    {
        score++;
        PlayerPrefs.SetInt("PlayerScore", score); // Save score to PlayerPrefs
        UpdateScoreText();
    }
    

    void SaveHighScore()
    {
        using (StreamWriter writer = new StreamWriter("savefile.txt"))
        {
            writer.WriteLine(savehighscore);
        }
    }

    void LoadHighScore()
    {
        using (StreamReader sr = new StreamReader("savefile.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                // Parse the last line from txt file to savehighscore
                savehighscore = int.Parse(line);
            }
        }
    }

    public void UpdateHighScoreText()
    {
        highscoreText.text = savehighscore.ToString();
    }

    void Update()
    {
        UpdateScoreText();
        LoadHighScore();
        if (score > highscore)
        {
            highscore = score;
            if (highscore > savehighscore)
            {
                savehighscore = highscore;
                SaveHighScore();
            }
            UpdateHighScoreText();
        }
        //Debug.Log("SCORE: " + score);
    }
    
}
