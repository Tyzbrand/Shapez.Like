using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuSC : MonoBehaviour
{
    [SerializeField] private UIDocument uI;
    private VisualElement panel;

    private PlayerVariables player;
    private DestructionOverlaySC overlayManager;
    private TimeManager timeManager;

    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        overlayManager = ReferenceHolder.instance.overlayManager;
        timeManager = ReferenceHolder.instance.timeManager;
        panel = uI.rootVisualElement.Q<VisualElement>("PauseMenu");


        var resumeBtn = panel.Q<Button>("Resume");
        var quitBtn = panel.Q<Button>("Quit");

        resumeBtn.clicked -= ClosePauseMenu;
        quitBtn.clicked -= Application.Quit;


        resumeBtn.clicked += ClosePauseMenu;
        quitBtn.clicked += Application.Quit;
    }




    //-------Getters et Setters-------

    public void OpenPauseMenu()
    {
        panel.style.display = DisplayStyle.Flex;
        player.isInPauseUI = true;
        timeManager.SetPause(false);


    }

    public void ClosePauseMenu()
    {
        panel.style.display = DisplayStyle.None;
        player.isInPauseUI = false;
        timeManager.SetPlay();

    }

    public void TogglePauseMenu()
    {
        if (panel.resolvedStyle.display == DisplayStyle.Flex)
        {
            ClosePauseMenu();

        }
        else
        {
            OpenPauseMenu();

        }
    }
}
