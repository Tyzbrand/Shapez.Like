using UnityEngine;

public class BuildMenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerSwitchMode modSwitch;
    private PlayerVariables player;
    private Destruction destructionSc;


    private void Start()
    {
        modSwitch = ReferenceHolder.instance.playerSwitchMode;
        player = ReferenceHolder.instance.playervariable;
        destructionSc = ReferenceHolder.instance.destructionSC;
    }


    public void BuildMenuToogle()
    {

        if (!player.buildMenu)
        {
            modSwitch.BuildGuiOpen();

        }
        else if (player.buildMenu)
        {
            modSwitch.BuildGuiClose();


        }

        destructionSc.DestructionModeOff();
        
    }
}
