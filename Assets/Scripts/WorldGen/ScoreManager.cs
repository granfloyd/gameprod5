using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public int savehighscore;

    public int highscore = 0;

    public Text highscoreText;

    public Text scoreText;

    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    public void UpdateScore()
    {
        UpdateScoreText();
        score++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
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

    }
}
