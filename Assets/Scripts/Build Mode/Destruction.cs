using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Destruction : MonoBehaviour
{

    private PlayerVariables player;
    private PlayerSwitchMode buildSc;
    private Preview previewSc;
    private Camera playerCam;
    private DestructionOverlaySC destructionOverlay;
    private BuildingManager buildingManager;
    private bool allowDestruction = false;


    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        buildSc = ReferenceHolder.instance.playerSwitchMode;
        previewSc = ReferenceHolder.instance.previewSC;
        playerCam = ReferenceHolder.instance.mainCamera;
        destructionOverlay = ReferenceHolder.instance.overlayManager;
        buildingManager = ReferenceHolder.instance.buildingManager;
    }



    public void DestructionSet()
    {
        if (!player.destructionMode)
        {
            DestructionModeOn();
        }
        else if (player.destructionMode)
        {
            DestructionModeOff();
        }
    }

    private void Update()
    {
        if (allowDestruction)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && player.destructionMode)
            {
                Vector2 mousePos2D = playerCam.ScreenToWorldPoint(Input.mousePosition);

                if (buildingManager.IsTileUsed(mousePos2D))
                {   
                    if(!(buildingManager.GetBuildingOnTile(mousePos2D) is Hub)) buildingManager.RemoveBuilding(mousePos2D);
                    
                }
                allowDestruction = false;

            }
        }

        
    }



    private void Destroy(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            allowDestruction = true;
        }

    }



    private void DestructionModeOn()
    {
        player.destructionMode = true;
        buildSc.BuildGuiClose();
        buildSc.BuildModeOff();
        destructionOverlay.DestructionOverlayToggle();


        if (previewSc.currentPreview != null)
        {
            previewSc.DestroyInstance();
        }
    }


    public void DestructionModeOff()
    {
        player.destructionMode = false;
        destructionOverlay.DestructionOverlayToggle();
    }





}
