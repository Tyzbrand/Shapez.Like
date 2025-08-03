using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

//-------------------------------------Gère la génération procédurale-------------------------------------

public class ProceduralGeneration : MonoBehaviour
{
    private int height = 200;
    private int width = 200;
    public Tilemap tilemap;
    public Tile tile;
    public Tile tileBorder;
    public Tile tileIron;
    public Tile tileCopper;
    public Tile tileRestricted;




    public GameObject hub;
    public Vector3Int hubPos;


    //Gestion des zones restreintes
    private int veinCountRestrictedArea = 25;
    private int veinMinTilesRestrictedArea = 60;
    private int veinMaxTilesRestrictedArea = 100;
    private float expansionChanceRestrictedArea = 0.9f;


    //Gestion du fer
    private int veinCountIron = 10;
    private int veinMaxTilesIron = 14;
    private float expansionChanceIron = 0.7f;

    //Gestion du cuivre
    private int veinCountCopper = 11;
    private int veinMaxTilesCopper = 20;
    private float expansionChanceCopper = 0.7f;
 




    void Start()
    {
        //GENERATION PAR DEFAUT DU TERRAIN
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    tilemap.SetTile(pos, tileBorder);   //Définit les limites de la map

                }
                else tilemap.SetTile(pos, tile);    //Pose des tuiles "basiques" partout sauf sur les bordures

            }
        }



        for (int i = 0; i < 50; i++) //Essaye 50 fois de placer le hub a un emplacement valide
        {
            Vector3Int baseCellPos = new Vector3Int(Random.Range(0, width - 1), Random.Range(0, height - 1), 0); //Pose aléatoire du HUB dans la map en vérifianrt si les tuiles sont adaptées
            Vector3 baseHubPos = tilemap.CellToWorld(baseCellPos);
            Vector3 cellSize = tilemap.cellSize;



            Vector3Int pos1 = baseCellPos;  // Verification des 4 tuiles pour savoir si elles sont libres
            Vector3Int pos2 = baseCellPos + new Vector3Int(1, 0, 0);
            Vector3Int pos3 = baseCellPos + new Vector3Int(0, 1, 0);
            Vector3Int pos4 = baseCellPos + new Vector3Int(1, 1, 0);

            Vector3 hubPos = baseHubPos + new Vector3(cellSize.x, cellSize.y, 0);

            if (tilemap.GetTile(pos1) == tile && tilemap.GetTile(pos2) == tile && tilemap.GetTile(pos3) == tile && tilemap.GetTile(pos4) == tile)
            {
                Instantiate(hub, hubPos, Quaternion.identity);
                break;

            }
        }
        




        //GENERATION DES ZONES RESTREINTES


            for (int boucle = 0; boucle < veinCountRestrictedArea; boucle++)
            {
                Vector3Int tilepos = new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0); //récupere une position aléatoire sur la map

                if (tilemap.GetTile(tilepos) == tile) //vérification que c'est une tuile de base avant de poser
                {
                    tilemap.SetTile(tilepos, tileRestricted); //Pose une tuile de fer

                    for (int i = 0; i < Random.Range(veinMinTilesRestrictedArea, veinMaxTilesRestrictedArea); i++)
                    {
                        int dir = Random.Range(0, 4);
                        Vector3Int current = Vector3Int.zero;

                        if (dir == 0) current += new Vector3Int(1, 0, 0);
                        if (dir == 1) current += new Vector3Int(-1, 0, 0);
                        if (dir == 2) current += new Vector3Int(0, 1, 0);
                        if (dir == 3) current += new Vector3Int(0, -1, 0);

                        Vector3Int newPos = tilepos + current;

                        if (Random.value < expansionChanceRestrictedArea && (tilemap.GetTile(newPos) == tile || tilemap.GetTile(newPos) == tileRestricted)) //vérifications que c'est une tuile de base avant de poser
                        {
                            tilemap.SetTile(newPos, tileRestricted);

                            if (tilemap.GetTile(newPos + new Vector3Int(1, 0, 0)) == tile || tilemap.GetTile(newPos + new Vector3Int(1, 0, 0)) == tileRestricted) //Pose 5 tuiles au lie de 1 pour donner  un effet de "gros paté"
                            {
                                tilemap.SetTile(newPos + new Vector3Int(1, 0, 0), tileRestricted);
                            }
                            if (tilemap.GetTile(newPos + new Vector3Int(-1, 0, 0)) == tile || tilemap.GetTile(newPos + new Vector3Int(-1, 0, 0)) == tileRestricted)
                            {
                                tilemap.SetTile(newPos + new Vector3Int(-1, 0, 0), tileRestricted);
                            }
                            if (tilemap.GetTile(newPos + new Vector3Int(0, 1, 0)) == tile || tilemap.GetTile(newPos + new Vector3Int(0, 1, 0)) == tileRestricted)
                            {
                                tilemap.SetTile(newPos + new Vector3Int(0, 1, 0), tileRestricted);
                            }
                            if (tilemap.GetTile(newPos + new Vector3Int(0, -1, 0)) == tile || tilemap.GetTile(newPos + new Vector3Int(0, -1, 0)) == tileRestricted)
                            {
                                tilemap.SetTile(newPos + new Vector3Int(0, -1, 0), tileRestricted);
                            }
                            tilepos = newPos;
                        }

                    }

                }
            }









        //GENERATION DE FILONS DE FER


        for (int boucle = 0; boucle < veinCountIron; boucle++)
        {
            Vector3Int tilepos = new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0); //récupere une position aléatoire sur la map

            if (tilemap.GetTile(tilepos) == tile) //vérification que c'est une tuile de base avant de poser
            {
                tilemap.SetTile(tilepos, tileIron); //Pose une tuile de fer

                for (int i = 0; i < veinMaxTilesIron; i++) //Permet e creer des voisin selon un pourcentage de chance 
                {
                    int dir = Random.Range(0, 4);
                    Vector3Int current = Vector3Int.zero;

                    if (dir == 0) current += new Vector3Int(1, 0, 0);
                    if (dir == 1) current += new Vector3Int(-1, 0, 0);
                    if (dir == 2) current += new Vector3Int(0, 1, 0);
                    if (dir == 3) current += new Vector3Int(0, -1, 0);

                    Vector3Int newPos = tilepos + current;

                    if (Random.value < expansionChanceIron && tilemap.GetTile(newPos) == tile) //vérification que c'est une tuile de base avant de poser
                    {
                        tilemap.SetTile(newPos, tileIron);
                        tilepos = newPos;
                    }

                }

            }
        }


        //GENERATION DE FILONS DE CUIVRE


        for (int boucle = 0; boucle < veinCountCopper; boucle++)
        {
            Vector3Int tilepos = new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0); //récupere une position aléatoire sur la map

            if (tilemap.GetTile(tilepos) == tile) //vérification que c'est une tuile de base avant de poser
            {
                tilemap.SetTile(tilepos, tileCopper); //Pose une tuile de cuivre

                for (int i = 0; i < veinMaxTilesCopper; i++)    //Permet e creer des voisin selon un pourcentage de chance 
                {
                    int dir = Random.Range(0, 4);
                    Vector3Int current = Vector3Int.zero;


                    if (dir == 0) current += new Vector3Int(1, 0, 0);
                    if (dir == 1) current += new Vector3Int(-1, 0, 0);
                    if (dir == 2) current += new Vector3Int(0, 1, 0);
                    if (dir == 3) current += new Vector3Int(0, -1, 0);

                    Vector3Int newPos = tilepos + current;

                    if (Random.value < expansionChanceCopper && tilemap.GetTile(newPos) == tile) //vérification que c'est une tuile de base avant de poser
                    {
                        tilemap.SetTile(newPos, tileCopper);
                        tilepos = newPos;
                    }
                }

            }
        }













    }
}

