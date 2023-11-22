using UnityEngine;

public class FirstStartManager : MonoBehaviour
{
    public static bool isFirstStart = true;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
