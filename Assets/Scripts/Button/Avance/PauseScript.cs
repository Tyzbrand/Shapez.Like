using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private void SetPause()
    {
        Time.timeScale = 0f;
    }

    private void SetPlay()
    {
        Time.timeScale = 1f;
    }

    private void SetX2()
    {
        Time.timeScale = 2f;
    }
    
    private void Setx3()
    {
        Time.timeScale = 3f;
    }
}
