using UnityEngine;
using UnityEngine.InputSystem;

public class TimeManager : MonoBehaviour
{
    private DestructionOverlaySC overlayManager;
    private PlayerVariables player;
    private OverlaySC overlay;

    private float counter;

    void Start()
    {
        overlayManager = ReferenceHolder.instance.overlayManager;
        player = ReferenceHolder.instance.playervariable;
        overlay = ReferenceHolder.instance.inGameOverlay;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= 1f)
        {
            player.seconds++;
            counter -= 1f;
            overlay.UpdateTimeText();
        }

        if (player.seconds >= 60)
        {
            player.minutes++;
            player.seconds -= 60;
            overlay.UpdateTimeText();

        }

        if (player.minutes >= 1 && player.seconds >= 30)
        {
            player.day++;
            player.seconds = 0;
            player.minutes = 0;
            overlay.UpdateDayText();
        }

    }

    public void SetPause(bool overlay)
    {
        Time.timeScale = 0f;
        if (overlay) overlayManager.PauseOverlayOn();
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

    //keyBinds

    public void KeyTogglePause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (Time.timeScale == 0f) SetPlay();
        else SetPause(true);
    }

    public void KeySetPlay(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SetPlay();
    }

    public void KeySetX2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        SetX2();
    }

    public void KeySetX3(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Setx3();
    }
    
    
}
