using UnityEngine;

public class BuildMenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerSwitchMode modSwitch;
    private PlayerVariables variable;
    private Destruction destructionSc;


    private void Start()
    {
        modSwitch = ReferenceHolder.instance.playerSwitchMode;
        variable = ReferenceHolder.instance.playervariable;
        destructionSc = ReferenceHolder.instance.destructionSC;
    }


    private void BuildMenuToogle()
    {

        if (variable.buildMenu == false)
        {
            modSwitch.BuildGuiOpen();

        }
        else if (variable.buildMenu == true)
        {
            modSwitch.BuildGuiClose();


        }

        destructionSc.DestructionModeOff();
        
    }
}
