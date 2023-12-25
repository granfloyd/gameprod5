using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Unity.Netcode;

public class titlescreen : MonoBehaviour
{
    public Button playButton;
    public Button newGameButton;
    public Image TitleScreenBG;
    public Button hostButton;
    public Button serverButton;
    public Button clientButton;
    //public Button continueButton;
    public Button infoButton;
    public Button backButton;
    public Button playmainbackButton;
    public GameObject info;
    public GameObject playmain;
    public GameObject main;
    public Button exitButton;

    public GameObject Cursorgo;
    private CanvasGroup canvasGroup;

    [SerializeField] private string gameplaySceneName = "Game1";
    // Start is called before the first frame update
    void Start()
    {

        newGameButton.onClick.AddListener(LoadGame);
        infoButton.onClick.AddListener(showinfo);
        if(backButton != null)
        backButton.onClick.AddListener(hideinfo);
        exitButton.onClick.AddListener(Exit);
        playmainbackButton.onClick.AddListener(Exit2);
        playButton.onClick.AddListener(HideMain);

        hostButton.onClick.AddListener(Host);
        serverButton.onClick.AddListener(Server);
        clientButton.onClick.AddListener(Client);
        // Get the CanvasGroup component
        canvasGroup = Cursorgo.GetComponent<CanvasGroup>();
        // If the CanvasGroup component doesn't exist, add one
        if (canvasGroup == null)
        {
            canvasGroup = Cursorgo.AddComponent<CanvasGroup>();
        }

        canvasGroup.blocksRaycasts = false;
    }
    void Host()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    void Server()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    void Client()
    {
        NetworkManager.Singleton.StartClient();
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
    void HideMain()
    {
        playmain.SetActive(true);
    }
    void Exit2()
    {
        playmain.SetActive(false);
        main.SetActive(true);
    }
    void Exit()
    {
        Application.Quit();

    }
}
