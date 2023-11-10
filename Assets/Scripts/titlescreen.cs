using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class titlescreen : MonoBehaviour
{
    public Button newGameButton;

    //public Button continueButton;

    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(NewGame);
        //continueButton.onClick.AddListener(Continue);
        
        exitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Continue()
    {
        SceneManager.LoadScene("Game1");
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
