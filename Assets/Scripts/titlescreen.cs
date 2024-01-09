using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Titlescreen : MonoBehaviour
{
    public Button newGameButton;
    public Image TitleScreenBG;
    public Button howtoplayButton;
    public Button backButton;
    public Button nextButton;
    public GameObject[] howtoplay = new GameObject[3];
    public Button exitButton;
    public Button creditsButton;
    public Button closecreditsButton;
    public GameObject creditsUI;

    public GameObject howtoplayui;
    public GameObject Cursorgo;
    public GameObject Cursorgo2;
    public GameObject Cursorgo3;
    public int Index = 0;
    public bool isInMain = true;
    // Start is called before the first frame update
    void Start()
    {
        creditsUI.SetActive(false);
        HideHowToPlay();
        newGameButton.onClick.AddListener(NewGame);
        howtoplayButton.onClick.AddListener(HowToPlay);
        exitButton.onClick.AddListener(Exit);
        backButton.onClick.AddListener(GoBack);
        nextButton.onClick.AddListener(GoNext);
        creditsButton.onClick.AddListener(DisplayCredits);
        closecreditsButton.onClick.AddListener(CloseCredits);
    }

    void Update()
    {
        if(isInMain)
        {
            HideHowToPlay();
            Cursor.visible = false;
        }
        if(!isInMain)
        {
            Cursor.visible = false;
        }
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -10;
        Cursorgo.transform.position = Input.mousePosition;
        Cursorgo2.transform.position = Input.mousePosition;
        Cursorgo3.transform.position = Input.mousePosition;
    }
    void DisplayCredits()
    {
        creditsUI.SetActive(true);
    }
    void CloseCredits()
    {
        creditsUI.SetActive(false);
    }
    void HowToPlay()
    {
        isInMain = false;
        howtoplayui.SetActive(true);
        Index = 0;
        howtoplay[Index].SetActive(true);
    }
    void GoBack()
    {
        if (Index > 0)
        {
            howtoplay[Index].SetActive(false);
            Index--;
            howtoplay[Index].SetActive(true);
        }
        else
        {
            isInMain = true;
        }
    }
    void GoNext()
    {
        if (Index < howtoplay.Length - 1)
        {
            howtoplay[Index].SetActive(false);// Set old one false
            Index++;
            howtoplay[Index].SetActive(true);
        }
    }
    void NewGame()
    {
        SceneManager.LoadScene("OverWorld");
    }
    void HideHowToPlay()
    {
        howtoplayui.SetActive(false);
    }


    void Exit()
    {
        Application.Quit();

    }
}
