using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
   
    public Image gameoverBG;
    //public Button continueButton;

    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(imsofunny);
        continueButton.onClick.AddListener(Continue);
        
        exitButton.onClick.AddListener(Exit);
    }
    void imsofunny()
    {
        SceneManager.LoadScene("Game1");
    }
    // Update is called once per frame
    void Update()
    {
        gameoverBG.transform.Rotate(50 * Time.deltaTime, 0, 0 );
    }

    void Continue()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    void NewGame()
    {
        SceneManager.LoadScene("Game1");
    }

    
    void Exit()
    {
        Application.Quit();

    }
}

