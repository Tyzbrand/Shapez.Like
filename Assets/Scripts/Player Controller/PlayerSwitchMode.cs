using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//-------------------------------------Gère le changement entre les différents mods du jeu-------------------------------------
//utilisation de "PlayerVariables" pour: -buildMode, -rotation

public class PlayerSwitchMode : MonoBehaviour
{


    private GameObject buildPanel;
    private PlayerVariables player;
    private Preview previewSC;
    private GameObject inventoryPanel;


    private void Start()
    {
        buildPanel = ReferenceHolder.instance.buildMenu;
        player = ReferenceHolder.instance.playervariable;
        previewSC = ReferenceHolder.instance.previewSC;
        inventoryPanel = ReferenceHolder.instance.inventoryMenu;
    }




    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && player.buildMode == true)
        {
            BuildGuiOpen();
            BuildModeOff();
            previewSC.DestroyInstance();

        }
        if (Input.GetKeyDown(KeyCode.Escape) && player.buildMode == true)
        {
            BuildModeOff();
            previewSC.DestroyInstance();

        }

        if (Input.GetKeyDown(KeyCode.Escape) && player.buildMenu == true)
        {
            BuildGuiClose();
        }
    }






    public void BuildGuiOpen()
    {
        player.buildMenu = true;
        player.movementBlock = true;
        buildPanel.SetActive(true);

    }

    public void BuildGuiClose()
    {
        buildPanel.SetActive(false);
        player.buildMenu = false;
        player.movementBlock = false;
    }

    public void BuildModeOn()
    {
        GetComponent<PlayerVariables>().buildMode = true;
        player.rotation = 0;
    }

    public void BuildModeOff()
    {
        GetComponent<PlayerVariables>().buildMode = false;
    }






    public void InventoryUIToggle()
    {
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
            inventoryPanel.SetActive(true);

    }


}

