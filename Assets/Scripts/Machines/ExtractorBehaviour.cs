
/*using UnityEngine;
using UnityEngine.Tilemaps;

public class ExtractorBehaviour : MonoBehaviour
{
    private ExtractorData data;
    [HideInInspector]
    public Tilemap tilemap; //Attention, PASSSSSSS de référence a la tilemap chosie dans "PlayerTeleport.cs"

    private List<TileBase> extractableRessources; //Liste des tuiles qui permettron a l'extracteur de produire


    //Reference aux data    
    private float ressourcesPerSecond;
    private int capacity;
    [HideInInspector]
    public int price = 100;

    //variables dynamiques
    private int currentStorage = 0;
    private bool canExtract = false;
    private float storageBuffer = 0.0f;
    private float ejectInteval = 0.5f;
    private float ejectTimer = 0f;

    //Variables autres
    private Vector2 outputDirection;
    private GameObject ressourceType;


    //Variables de ressources
    private GameObject iron;
    private GameObject copper;
    private GameObject coal;
    private TileBase coalTile;
    private TileBase ironTile;
    private TileBase copperTile;
    private RessourceDictionnary ressourceDictionnary;





    private void Start()
    {

        data = ReferenceHolder.instance.extractorData;
        extractableRessources = new List<TileBase>(ReferenceHolder.instance.extradableTiles);
        iron = ReferenceHolder.instance.ironPrefab;
        copper = ReferenceHolder.instance.copperPrefab;
        coal = ReferenceHolder.instance.coalPrefab;
        coalTile = ReferenceHolder.instance.coalTile;
        ironTile = ReferenceHolder.instance.ironTile;
        copperTile = ReferenceHolder.instance.copperTile;
        ressourceDictionnary = ReferenceHolder.instance.ressourceDictionnary;






        if (data != null) //Assignation des données depuis les data
        {
            ressourcesPerSecond = data.ressourcesPerSecond;
            capacity = data.capacity;
        }


        //Verification de la ressource en dessous

        tilemap = FindFirstObjectByType<Tilemap>();

        Vector3Int cellPos = tilemap.WorldToCell(transform.position);

        TileType underTile = tilemap.GetTile<TileType>(cellPos);



        outputDirection = -transform.up;


        if (underTile != null)
        {
            ressourceType = ressourceDictionnary.GetRessourcePrefab(underTile.tileType);

            if (ressourceType != null)
            {
                canExtract = true;
            }
        }



    }

    private void Update()
    {
        if (canExtract == true && currentStorage < capacity && ressourceType != null)
        {
            storageBuffer += ressourcesPerSecond * Time.deltaTime;

            int unitToAdd = Mathf.FloorToInt(storageBuffer);
            if (storageBuffer >= 1.0f)
            {
                int availableStorage = capacity - currentStorage;
                int Amount = Mathf.Min(unitToAdd, availableStorage);


                storageBuffer -= Amount;
                currentStorage += Amount;
            }

        }

        ejectTimer += Time.deltaTime;

        if (currentStorage >= 1 && ejectTimer >= ejectInteval)
        {
            Vector2 outputPos = outputDirection + (Vector2)transform.position;

            Collider2D conveyorFound = Physics2D.OverlapCircle(outputPos, 0.2f, LayerMask.GetMask("Conveyor", "Interactable"));
            Collider2D hit = Physics2D.OverlapCircle(outputPos, 0.1f, LayerMask.GetMask("Ressource"));

            if (conveyorFound != null && hit == null && ressourceType != null)
            {
                Instantiate(ressourceType, outputPos, Quaternion.identity);
                currentStorage -= 1;
                ejectTimer = 0f;
            }
        }


    }
    */
    
















