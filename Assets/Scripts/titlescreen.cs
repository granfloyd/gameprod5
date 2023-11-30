using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class titlescreen : MonoBehaviour
{
    public Button newGameButton;
    public Image TitleScreenBG;
    //public Button continueButton;
    public Button infoButton;
    public Button backButton;
    public GameObject info;
    public Button exitButton;

    public GameObject Cursorgo;
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {

        newGameButton.onClick.AddListener(LoadGame);
        infoButton.onClick.AddListener(showinfo);
        if(backButton != null)
        backButton.onClick.AddListener(hideinfo);
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
        mousePos.z = -10;
        Cursorgo.transform.position = Input.mousePosition; 
    }
    void NewGame()
    {
        SceneManager.LoadScene("Game1");
    }
    void showinfo()
    {
        info.SetActive(true);
    }
    void hideinfo()
    {
        info.SetActive(false);
    }
    void Exit()
    {
        Application.Quit();

    }
}
