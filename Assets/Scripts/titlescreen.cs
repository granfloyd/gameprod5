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

    public int peter = 0;

    private Vector3 spawn1 = new Vector3(500, 600, 2);
    private Vector3 spawn2 = new Vector3(1000, 500, 2);
    private Vector3 spawn3 = new Vector3(1000, 500, 2);

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(imsofunny);
        infoButton.onClick.AddListener(showinfo);
        if(backButton != null)
        backButton.onClick.AddListener(hideinfo);
        exitButton.onClick.AddListener(Exit);
    }
    void imsofunny()
    {
        peter += 1;
        
        if(newGameButton.transform.position == spawn1)
        {
            newGameButton.transform.position = spawn2;
        }
        else if(newGameButton.transform.position == spawn2)
        {
            newGameButton.transform.position = spawn3;
        }
        else if (newGameButton.transform.position == spawn3)
        {
            newGameButton.transform.position = spawn1;
        }
        else
        {
            newGameButton.transform.position = spawn1;
        }
        if(peter == 7)
        {
            SceneManager.LoadScene("Game1");
        }
    }
    // Update is called once per frame
    void Update()
    {
        TitleScreenBG.transform.Rotate(0, 0, 50 * Time.deltaTime);
        infoButton.transform.Rotate(0, 0, -50 * Time.deltaTime);
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
