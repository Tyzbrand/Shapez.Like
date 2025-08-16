using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuSC : MonoBehaviour
{
    [SerializeField] private UIDocument pauseMenu;
    private VisualElement root;

    private PlayerVariables player;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        pauseMenu.gameObject.SetActive(false);
       
    }


    private void OnEnable()
    {   
        player = ReferenceHolder.instance.playervariable;

        root = pauseMenu.rootVisualElement;

        var resumeBtn = root.Q<Button>("Resume");
        var quitBtn = root.Q<Button>("Quit");

        resumeBtn.clicked -= ClosePauseMenu;
        quitBtn.clicked -= Application.Quit;


        resumeBtn.clicked += ClosePauseMenu;
        quitBtn.clicked += Application.Quit;


    }



    //-------Getters et Setters-------

    public void OpenPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        player.isInUI = true;

    }

    public void ClosePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
        player.isInUI = false;

    }

    public void TogglePauseMenu()
    {
        if (pauseMenu.gameObject.activeSelf)
        {
            ClosePauseMenu();

        }
        else
        {
            OpenPauseMenu();

        }
    }
}
