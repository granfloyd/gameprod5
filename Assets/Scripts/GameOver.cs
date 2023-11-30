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
    public GameObject bg;
    public Image gameoverBG;
    //public Button continueButton;


    public Button exitButton;
    public GameObject Cursorgo;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {

        newGameButton.onClick.AddListener(LoadGame);
        continueButton.onClick.AddListener(Continue);
        
        exitButton.onClick.AddListener(Exit);
        // Get the CanvasGroup component
        canvasGroup = Cursorgo.GetComponent<CanvasGroup>();
        // If the CanvasGroup component doesn't exist, add one
        if (canvasGroup == null)
        {
            canvasGroup = Cursorgo.AddComponent<CanvasGroup>();
        }

        canvasGroup.blocksRaycasts = false;
    }
    void LoadGame()
    {
        SceneManager.LoadScene("Game1");
    }
    // Update is called once per frame
    void Update()
    {

        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Cursorgo.transform.position = Input.mousePosition;    

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

