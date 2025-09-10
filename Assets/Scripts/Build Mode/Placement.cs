
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;




//-------------------------------------Gère tout les input liés a la construction sur la map (Seulement si build mode = true)-------------------------------------
//utilisation de "PlayerVariables" pour: -buildMode et -rotation

public class Placement : MonoBehaviour
{


    private Camera playerCam;
    private PlayerVariables player;
    private OverlaySC OverlaySC;
    private BuildingManager buildingManager;
    private List<TileBase> restrictedTiles; //Liste des tuiles sur lesquelles je ne peux pas construire (NE FONCTIONNE PAS)

    [HideInInspector]
    public GameObject currentBuild;

    private GameObject Extractor;
    private GameObject conveyor;
    private GameObject marketPlace;
    private GameObject foundry;
    private Tilemap tilemap;
    private bool allowConstruciton = false;
    public BuildingManager.buildingType currentBuildingType = BuildingManager.buildingType.None;


    //Dictionnaire
    private BuildingLibrary prices;





    private void Start()
    {

        playerCam = ReferenceHolder.instance.mainCamera;
        restrictedTiles = new List<TileBase>(ReferenceHolder.instance.restrictedTiles);
        player = ReferenceHolder.instance.playervariable;
        buildingManager = ReferenceHolder.instance.buildingManager;
        OverlaySC = ReferenceHolder.instance.inGameOverlay;


        prices = ReferenceHolder.instance.buildingLibrary;

    }


    private void Update()
    {
        if (allowConstruciton)
        {

            if (currentBuildingType != BuildingManager.buildingType.None)
            {
                if (player.buildMode)
                {
                    tilemap = player.tilemap1;
                    Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    mousePos3D.z = 0;
                    Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

                    int décalage = player.rotation;
                    Vector3Int cellPos = tilemap.WorldToCell(mousePos2D);
                    TileBase underTile = tilemap.GetTile(cellPos);

                    if (!restrictedTiles.Contains(underTile) && underTile != null && !EventSystem.current.IsPointerOverGameObject())
                    {
                        BuildingBH currentInstance = GetCurrentType(mousePos2D, tilemap);

                        buildingManager.AddBuilding(mousePos2D, currentInstance);
                        player.Money -= prices.GetBuildingPrice(currentBuildingType);
                        OverlaySC.UpdateMoneyText();
                    }
                }

            }
            allowConstruciton = false;
        }
    }







    private void Rotate(InputAction.CallbackContext context) //Permet la rotation des objets du preview et de la map
    {
        if (context.performed && player.buildMode == true)
        {
            player.rotation = (player.rotation + 90)% 360;
        }
    }



    private void Build(InputAction.CallbackContext context) //Permet de poser l'objet en question au clic Gauche
    {   

        if (context.performed)
        {   
            Vector3 mousePos3D = playerCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos3D.z = 0;
            Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);

            if (prices.GetBuildingPrice(currentBuildingType) <= player.Money && !buildingManager.IsTileUsed(mousePos2D))
            {
                allowConstruciton = true;

            }
            else if (player.buildMode)
            {
                Debug.Log("Pas Assez d'argent");
            }

        }

    }

    private BuildingBH GetCurrentType(Vector2 mousePos2D, Tilemap buildingTilemap)
    {
        switch (currentBuildingType)
        {
            case BuildingManager.buildingType.Extractor:
                return new Extractor(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.Conveyor:
                return new Conveyor(mousePos2D, player.rotation, buildingTilemap);
            case BuildingManager.buildingType.marketplace:
                return new Marketplace(mousePos2D, player.rotation, buildingTilemap);
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
}

           
        
    


