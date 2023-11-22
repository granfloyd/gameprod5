using UnityEngine;

public class FirstStartManager : MonoBehaviour
{
    public static bool isFirstStart = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //private void Awake()
    //{
    //    // Check if the game is being started for the first time
    //    if (PlayerPrefs.GetInt("FirstStart", 1) == 1)
    //    {
    //        // If it is the first start, set FirstStart to 0
    //        PlayerPrefs.SetInt("FirstStart", 0);
    //        PlayerPrefs.SetInt("PlayerHealth", 10); // Set health to 10
    //        PlayerPrefs.SetInt("PlayerScore", 0); // Set score to 0
    //    }
    //}
}
