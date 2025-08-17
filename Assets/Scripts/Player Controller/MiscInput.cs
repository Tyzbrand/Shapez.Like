using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MiscInput : MonoBehaviour
{

    private PlayerVariables player;

    private PauseMenuSC pauseMenu;
    private BuildMenuSC buildMenuSC;
    private Preview previewSC;
    private Destruction destructionSC;







    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        pauseMenu = ReferenceHolder.instance.pauseMenu;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
        previewSC = ReferenceHolder.instance.previewSC;
        destructionSC = ReferenceHolder.instance.destructionSC;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && !player.destructionMode)
        {
            pauseMenu.TogglePauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && player.buildMenu) buildMenuSC.BuildMenuOff();
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && player.buildMode)
        {
            buildMenuSC.BuildMenuOff();
            player.buildMode = false;
            previewSC.DestroyInstance();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode && player.destructionMode) destructionSC.DestructionSet();

        if (Input.GetMouseButtonDown(1) && !player.buildMenu && player.buildMode)
        {
            buildMenuSC.BuildMenuOn();
            player.buildMode = false;
            previewSC.DestroyInstance();
        }





    }
    

}
