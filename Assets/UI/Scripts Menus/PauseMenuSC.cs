using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuSC : MonoBehaviour
{
    [SerializeField] private UIDocument uI;
    private VisualElement panel;

    private PlayerVariables player;

    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
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
        player.isInUI = true;

    }

    public void ClosePauseMenu()
    {
        panel.style.display = DisplayStyle.None;
        player.isInUI = false;

    }

    public void TogglePauseMenu()
    {
        if (panel.style.display == DisplayStyle.Flex)
        {
            ClosePauseMenu();

        }
        else
        {
            OpenPauseMenu();

        }
    }
}
