using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReferenceHolder : MonoBehaviour
{
    //Référence principale
    public static ReferenceHolder instance;

    //Principaux éléments 
    [Header("Principaux éléments")]
    public Camera mainCamera;
    
    
    //Scripts
    [Header("Scripts")]
    public PlayerVariables playervariable;
    public PlayerSwitchMode playerSwitchMode;
    public Preview previewSC;
    public Placement placementSC;
    public Destruction destructionSC;
    public Inventory inventorySC;
    public PlayerMove playerMoveSC;
    public BuildingManager buildingManager;
    public ItemManager itemManager;
    



    //Tuiles
    [Header("Tuiles")]
    public TileBase groundTile;
    public TileBase borderTile;
    public TileBase restrictedTile;
    public TileBase ironTile;
    public TileBase copperTile;
    public TileBase coalTile;
    public List<TileBase> restrictedTiles;
    public List<TileBase> extradableTiles;


    //Prefabs
    [Header("Prefabs")]
    public GameObject extractorPrefab;
    public GameObject conveyorPrefab;
    public GameObject marketplacePrefab;
    public GameObject foundryPrefab;
    public GameObject itemPrefab;


    //Sprites de ressources
    [Header("Sprites")]
    public Sprite ironSprite;
    public Sprite copperSprite;
    public Sprite coalSprite;
    public Sprite ironIngotSprite;
    public Sprite copperIngotSprite;


    //Previews batiments
    [Header("Preview")]
    public GameObject extractorPreview;
    public GameObject conveyorPreview;
    public GameObject marketplacePreview;
    public GameObject foundryPreview;


    //Datas
    [Header("Données")]
    public ExtractorData extractorData;
    public FoundryData foundryData;
    public RessourceData ressourceData;


    //Recettes
    [Header("Recettes")]
    public FoundryRecipe foundryRecipe;


    //Overlay
    [Header("Overlay")]
    public GameObject destructionOverlay;


    //Dictionnaires
    [Header("Dictionnaires")]
    public RessourceDictionnary ressourceDictionnary;
    public BuildPriceDictionnary buildPriceDictionary;

    //Panel UI
    [Header("Panel UI")]
    public RectTransform hubInventoryUI;
    public GameObject buildMenu;
    public GameObject inventoryMenu;
    



    void Awake()
    {
        instance = this;
    }
}
