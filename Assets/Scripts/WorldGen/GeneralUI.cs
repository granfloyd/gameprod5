using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour
{
    public static int score;

    public static int totalKeys;

    public int savehighscore;

    public int highscore;

    public Text highscoreText;

    public Text scoreText;

    public Text keyCountText;

    

    public GameObject imageObject;//display item to ui 
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    //public GameObject pauseMenuUI;

    //UI stuff//
    public Canvas genUIcanvan;
    public Image SelectorIMG;//selctor square
    public Image image;//for ui slots
    public Image SelectorIMGPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SelectorIMGPrefab = SelectorIMG.GetComponent<Image>();
        SelectorIMG.transform.SetParent(genUIcanvan.transform, false);
        SelectorIMG.transform.position = Slot1.transform.position;
        if (FirstStartManager.isFirstStart)
        {
            score = 0;
            totalKeys = 0;
            PlayerPrefs.SetInt("PlayerScore", score); // Save score to PlayerPrefs
            PlayerPrefs.SetInt("PlayerKeys", totalKeys); // Save totalkeys to PlayerPrefs
        }
        else
        {
            // Load score from PlayerPrefs
            score = PlayerPrefs.GetInt("PlayerScore", 0);
            totalKeys = PlayerPrefs.GetInt("PlayerKeys", 0);
        }
        
    }

    public void UpdateKey(int addkey)
    {
        //audioSource.Play();
        totalKeys += addkey;
        PlayerPrefs.SetInt("PlayerKeys", totalKeys); // Save keys to PlayerPrefs
        keyCountText.text = totalKeys.ToString();
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
        using (StreamWriter writer = new StreamWriter("ForestKidSaveInfo.txt"))
        {
            writer.WriteLine(savehighscore);
        }
    }

    void LoadHighScore()
    {
        using (StreamReader sr = new StreamReader("ForestKidSaveInfo.txt"))
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
