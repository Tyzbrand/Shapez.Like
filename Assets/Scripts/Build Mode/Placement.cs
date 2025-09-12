
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Unity.VisualScripting;




//-------------------------------------Gère tout les input liés a la construction sur la map (Seulement si build mode = true)-------------------------------------
//utilisation de "PlayerVariables" pour: -buildMode et -rotation

public class Placement : MonoBehaviour
{


    private Camera playerCam;
    private PlayerVariables player;
    private OverlaySC OverlaySC;
    private Preview previewSC;
    private UIManager uIManager;
    private BuildMenuSC buildMenuSC;
    private BuildingLibrary buildingLibrary;
    private BuildingManager buildingManager;
    private MiscInput miscInput;
    private List<TileBase> restrictedTiles; //Liste des tuiles sur lesquelles je ne peux pas construire (NE FONCTIONNE PAS)

    [HideInInspector]
    public GameObject currentBuild;

    private Tilemap tilemap;
    private bool allowConstruciton = false;
    public bool hasPickup = false;
    private bool isHoldingBuild = false;
    public BuildingManager.buildingType currentBuildingType = BuildingManager.buildingType.None;
    public BuildingManager.buildingType pickupType = BuildingManager.buildingType.None;
    private RecipeParent actualRecipe = null;
    private Vector3Int lasTilePos;
    private BuildingLibrary prices;





    private void Start()
    {

        playerCam = ReferenceHolder.instance.mainCamera;
        restrictedTiles = new List<TileBase>(ReferenceHolder.instance.restrictedTiles);
        player = ReferenceHolder.instance.playervariable;
        buildingManager = ReferenceHolder.instance.buildingManager;
        OverlaySC = ReferenceHolder.instance.inGameOverlay;
        previewSC = ReferenceHolder.instance.previewSC;
        buildingLibrary = ReferenceHolder.instance.buildingLibrary;
        miscInput = ReferenceHolder.instance.miscInput;
        buildMenuSC = ReferenceHolder.instance.buildMenu;
        uIManager = ReferenceHolder.instance.uIManager;



        prices = ReferenceHolder.instance.buildingLibrary;

    }


    private void Update()
    {
        if (allowConstruciton && !isHoldingBuild)
        {

            if (currentBuildingType != BuildingManager.buildingType.None)
            {
                if (player.buildMode)
                {
                    tilemap = player.tilemap1;
                    Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    mousePos3D.z = 0;
                    Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

                    Vector3Int cellPos = tilemap.WorldToCell(mousePos2D);
                    TileBase underTile = tilemap.GetTile(cellPos);

                    if (!restrictedTiles.Contains(underTile) && underTile != null && !EventSystem.current.IsPointerOverGameObject())
                    {
                        if (hasPickup) BuildPickUp(mousePos2D, false);
                        else BuildCurrent(mousePos2D);
                    }
                }

            }
            allowConstruciton = false;
        }

        if (isHoldingBuild)
        {
            if (currentBuildingType != BuildingManager.buildingType.None)
            {
                if (player.buildMode)
                {
                    Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    mousePos3D.z = 0;
                    Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

                    tilemap = player.tilemap1;

                    Vector3Int cellPos = tilemap.WorldToCell(mousePos2D);
                    TileBase underTile = tilemap.GetTile(cellPos);

                    if (!restrictedTiles.Contains(underTile) && underTile != null && !EventSystem.current.IsPointerOverGameObject() && cellPos != lasTilePos)
                    {   
                        if (hasPickup) BuildPickUp(mousePos2D, true);
                        else BuildCurrent(mousePos2D);
                        lasTilePos = cellPos;
                    }


                    
                    
                }
            }
        }

        if (player.pickupMode && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PickupBuilding();
            }
    }







    private void Rotate(InputAction.CallbackContext context) //Permet la rotation des objets du preview et de la map
    {
        if (context.performed && player.buildMode == true)
        {
            player.rotation = (player.rotation + 90) % 360;
        }
    }



    private void Build(InputAction.CallbackContext context) //Permet de poser l'objet en question au clic Gauche
    {

        if (context.performed)
        {
            Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos3D.z = 0;
            Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

            int price = prices.GetBuildingPrice(currentBuildingType);

            bool isTileUsed = buildingManager.IsTileUsed(mousePos2D);


            if (!isTileUsed || (isTileUsed && buildingManager.GetBuildingOnTile(mousePos2D) is Conveyor)) allowConstruciton = true;
        }

        if (context.phase == InputActionPhase.Started && player.lineBuild) isHoldingBuild = true;
        else if (context.phase == InputActionPhase.Canceled && player.lineBuild) isHoldingBuild = false;

    }

    private BuildingBH GetCurrentType(Vector2 mousePos2D, Tilemap buildingTilemap, BuildingManager.buildingType typeSource)
    {
        switch (typeSource)
        {
            case BuildingManager.buildingType.Extractor:
                return new Extractor(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Conveyor:
                return new Conveyor(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Depot:
                return new Depot(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Foundry:
                return new Foundry(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.builder:
                return new Builder(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.CoalGenerator:
                return new CoalGenerator(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.AdvancedExtractor:
                return new AdvancedExtractor(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Junction:
                return new Junction(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Splitter:
                return new Splitter(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Merger:
                return new Merger(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Wall:
                return new Wall(mousePos2D, player.rotation, buildingTilemap);
        }
        return null;
    }

    public void PickupBuilding()
    {
        Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos3D.z = 0;
        Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

        var building = buildingManager.GetBuildingOnTile(mousePos2D);

        if (!buildingManager.IsTileUsed(mousePos2D) || building is Hub) { player.pickupMode = false; miscInput.ComeBackToBuildMenu(); }
        else
        {
            pickupType = building.buildingType;
            player.rotation = building.rotation;
            hasPickup = true;
            player.pickupMode = false;

            player.buildMode = true;
            uIManager.HidePanel(buildMenuSC.panel, () => buildMenuSC.BuildMenuOnHide());

            previewSC.DestroyInstance();
            previewSC.previewToUse = buildingLibrary.GetBuildingPreview(pickupType);
            previewSC.CreateInstance();


            if (building is Foundry foundry) { actualRecipe = foundry.currentRecipe; }
            else if (building is Builder builder) { actualRecipe = builder.currentRecipe; }

            if (pickupType is BuildingManager.buildingType.Conveyor) player.lineBuild = true;
        }


    }

    private void BuildPickUp(Vector2 mousePos2D, bool save)
    {
        BuildingBH pickupInstance = GetCurrentType(mousePos2D, tilemap, pickupType);

        if (buildingLibrary.GetBuildingPrice(pickupInstance.buildingType) >= player.Money) { Debug.Log("not enough Money !!!");  return; }

        if (actualRecipe == null) buildingManager.AddBuilding(mousePos2D, pickupInstance);
        else
        {
            buildingManager.AddBuilding(mousePos2D, pickupInstance, true, () =>
            {
                if (pickupInstance is Foundry foundry) foundry.currentRecipe = (Recipe11_1)actualRecipe;
                else if (pickupInstance is Builder builder) builder.currentRecipe = (Recipe1_1)actualRecipe;
            });
            if (!save) actualRecipe = null;
        }
        

        player.Money -= prices.GetBuildingPrice(pickupType);
        OverlaySC.UpdateMoneyText();
    }

    private void BuildCurrent(Vector2 mousePos2D)
    {       
        BuildingBH currentInstance = GetCurrentType(mousePos2D, tilemap, currentBuildingType);

        if (buildingLibrary.GetBuildingPrice(currentInstance.buildingType) >= player.Money) { Debug.Log("not enough Money !!!");  return; }
        buildingManager.AddBuilding(mousePos2D, currentInstance);
        player.Money -= prices.GetBuildingPrice(currentBuildingType);
        OverlaySC.UpdateMoneyText();
    }

}

           
        
    


