using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Destruction : MonoBehaviour
{

    private PlayerVariables player;
    private Preview previewSc;
    private Camera playerCam;
    private BuildMenuSC buildMenuSC;
    private UIManager uIManager;
    private DestructionOverlaySC destructionOverlay;
    private BuildingManager buildingManager;
    private bool allowDestruction = false;


    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        previewSc = ReferenceHolder.instance.previewSC;
        playerCam = ReferenceHolder.instance.mainCamera;
        destructionOverlay = ReferenceHolder.instance.overlayManager;
        buildingManager = ReferenceHolder.instance.buildingManager;
        uIManager = ReferenceHolder.instance.uIManager;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
    }



    public void DestructionSet()
    {
        if (!player.destructionMode)
        {   
            DestructionModeOn();
            destructionOverlay.DestructionOverlayOn();
            uIManager.hideOpenPanel();

        }
        else if (player.destructionMode)
        {
            DestructionModeOff();
            destructionOverlay.DestructionOverlayOff();
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
        if(player.buildMode) player.buildMode = false;


        if (previewSc.currentPreview != null)
        {
            previewSc.DestroyInstance();
        }
    }


    private void DestructionModeOff()
    {
        player.destructionMode = false;
    }





}
