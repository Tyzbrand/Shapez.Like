using UnityEngine;
using UnityEngine.InputSystem;

public class MiscInput : MonoBehaviour
{

    private PlayerVariables player;
    private Camera cam;
    private PlayerSwitchMode switchMode;
    private GameObject inventoryPanel;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        switchMode = ReferenceHolder.instance.playerSwitchMode;
        cam = ReferenceHolder.instance.mainCamera;
        inventoryPanel = ReferenceHolder.instance.inventoryMenu;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.buildMenu && !player.buildMode) //Selection UI batiment Ingame
        {
            Vector2 mousPos = cam.ScreenToWorldPoint(Input.mousePosition);



            RaycastHit2D hub = Physics2D.Raycast(mousPos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Hub"));

            if (hub.collider != null)
            {
                switchMode.InventoryUIToggle();
            }
            else if (hub.collider == null && inventoryPanel.activeSelf)
            {
                switchMode.InventoryUIToggle();
            }



            if (Input.GetKeyDown(KeyCode.Escape) && !player.buildMenu && !player.buildMode)
            {
                Application.Quit();
            }

            
            

                
            
        }
    }
}
