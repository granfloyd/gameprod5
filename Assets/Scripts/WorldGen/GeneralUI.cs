using Microsoft.Win32.SafeHandles;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GeneralUI : MonoBehaviour
{
    public GameObject playerGO;
    public PlayerCollision PC;
    public static int score;
    public static int totalKeys;
    public static int cost;
    public static int shootSpread;
    public static float newShootCD;
    public static float crackSpeed;
    public int savehighscore;
    public int highscore;
    public Text highscoreText;
    public Text scoreText;
    public Text keyCountText;
    public TMP_Text drinkText;
    public TMP_Text pp69Text;
    public TMP_Text spreadText;
    //UI stuff//
    public GameObject generalUIGO;
    public GameObject imageObject;//display item to ui 
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    //public GameObject pauseMenuUI;
    public GameObject page;
    public Canvas genUIcanvan;
    public Image SelectorIMG;//selctor square
    public Image image;//for ui slots
    public Image SelectorIMGPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PC = playerGO.GetComponent<PlayerCollision>();
        SelectorIMGPrefab = SelectorIMG.GetComponent<Image>();
        SelectorIMG.transform.SetParent(genUIcanvan.transform, false);
        SelectorIMG.transform.position = Slot1.transform.position;
        if (FirstStartManager.isFirstStart)
        {
            score = 0;
            totalKeys = 0;
            shootSpread = 0;
            cost = 25;
            newShootCD = 0.7f;
            crackSpeed = 1.5f;
            PlayerPrefs.SetInt("PlayerScore", score); // Save score to PlayerPrefs
            PlayerPrefs.SetInt("PlayerKeys", totalKeys); // Save totalkeys to PlayerPrefs
            PlayerPrefs.SetInt("PlayerShootSpread", shootSpread);
            PlayerPrefs.SetInt("Cost", cost);
            PlayerPrefs.SetFloat("NewShootCD", newShootCD);
            PlayerPrefs.SetFloat("CrackSpeed", crackSpeed);
        }
        else
        {
            // Load score from PlayerPrefs
            score = PlayerPrefs.GetInt("PlayerScore", 0);
            totalKeys = PlayerPrefs.GetInt("PlayerKeys", 0);
            shootSpread = PlayerPrefs.GetInt("PlayerShootSpread", 0);
            cost = PlayerPrefs.GetInt("Cost", 0);
            newShootCD = PlayerPrefs.GetFloat("NewShootCD", 0.7f);
            crackSpeed = PlayerPrefs.GetFloat("CrackSpeed", 1.5f);
        }
        UpdateKey(10);
    }

    public void UpdateKey(int addkey)
    {
        //audioSource.Play();
        totalKeys += addkey;
        PlayerPrefs.SetInt("PlayerKeys", totalKeys); // Save keys to PlayerPrefs
        keyCountText.text = totalKeys.ToString();
        PC.shopkeyCountText.text = totalKeys.ToString();
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

    public void UpdatePermishPowerUpsUI()
    {
        drinkText.text = newShootCD.ToString();
        pp69Text.text = crackSpeed.ToString();
        spreadText.text = shootSpread.ToString();
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
        //Debug.Log("SCORE: " + newShootCD);
    }
    
}
