using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private DestructionOverlaySC overlayManager;
    void Start()
    {
        overlayManager = ReferenceHolder.instance.overlayManager;
    }

    public void SetPause()
    {
        Time.timeScale = 0f;
        overlayManager.PauseOverlayOn();
    }

    public void SetPlay()
    {
        Time.timeScale = 1f;
        overlayManager.PauseOverlayOff();
    }

    public void SetX2()
    {
        Time.timeScale = 2f;
        overlayManager.PauseOverlayOff();
    }

    public void Setx3()
    {
        Time.timeScale = 3f;
        overlayManager.PauseOverlayOff();
    }
}
