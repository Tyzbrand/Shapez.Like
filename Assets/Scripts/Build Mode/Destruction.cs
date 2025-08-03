using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Destruction : MonoBehaviour
{

    private PlayerVariables player;
    private PlayerSwitchMode buildSc;
    private Preview previewSc;
    private Camera playerCam;
    private GameObject destructionOverlay;
    private bool allowDestruction = false;


    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        buildSc = ReferenceHolder.instance.playerSwitchMode;
        previewSc = ReferenceHolder.instance.previewSC;
        playerCam = ReferenceHolder.instance.mainCamera;
        destructionOverlay = ReferenceHolder.instance.destructionOverlay;
    }



    private void DestructionSet()
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
                Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] objets = Physics2D.RaycastAll(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Conveyor", "Building", "Foundry", "Interactable"));

                if (objets.Length > 0)
                {
                    Destroy(objets[0].collider.gameObject);
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
        destructionOverlay.SetActive(true);


        if (previewSc.currentPreview != null)
        {
            previewSc.DestroyInstance();
        }
    }


    public void DestructionModeOff()
    {
        player.destructionMode = false;
        destructionOverlay.SetActive(false);
    }





}
