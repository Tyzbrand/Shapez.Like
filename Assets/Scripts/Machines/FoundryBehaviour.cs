using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class FoundryBehaviour : MonoBehaviour
{
    private FoundryData data;

    //Références aux data
    private int capacity;
    private RessourceDictionnary ressourceDictionnary;

    //variables dynamiques
    private int currentStorageInput1 = 0;
    private int currentStorageInput2 = 0;
    private int currentStorageOutput = 0;
    private float ejectInteval = 0.5f;
    private float ejectTimer = 0f;




    //Autres variables
    private Vector2 outputDirection;
    private Recipe currentRecipe;
    private PlayerVariables player;
    private FoundryRecipe recipeDatabase;
    private bool isProducing = false;
    public Transform inputPoint;
    public Transform inputPoin2;
    public Transform outputPoint;
    private GameObject outputItem;



    private void Start() //Assignation des données
    {

        data = ReferenceHolder.instance.foundryData;
        player = ReferenceHolder.instance.playervariable;
        recipeDatabase = ReferenceHolder.instance.foundryRecipe;
        ressourceDictionnary = ReferenceHolder.instance.ressourceDictionnary;




        capacity = data.capacity;
        

        if (data != null && recipeDatabase.foundryRecipes.Count > 0)
        {
            currentRecipe = recipeDatabase.foundryRecipes[0];
        }

        


        outputDirection = outputPoint.transform.up.normalized;

    }


    private void OnTriggerEnter2D(Collider2D item)
    {
        CatchRessource(item);

    }


    private void CatchRessource(Collider2D item)
    {
        if (item.gameObject.layer == LayerMask.NameToLayer("Ressource"))
        {
            RessourceBehaviour behaviour = item.gameObject.GetComponent<RessourceBehaviour>();

            if (player != null && behaviour != null)
            {
                if (behaviour.type == currentRecipe.input1 && currentStorageInput1 < 1)
                {

                    currentStorageInput1 += 1;
                    Destroy(item.gameObject);

                }
                else if (behaviour.type == currentRecipe.input2 && currentStorageInput2 < 1)
                {

                    currentStorageInput2 += 1;
                    Destroy(item.gameObject);
                }
            }
        }

    }








    private void Update()
    {
        if (currentStorageInput1 > 0 && currentStorageInput2 > 0 && !isProducing && capacity > currentStorageOutput)
        {
            StartCoroutine(ProduceCoroutine());
        }

        ejectTimer += Time.deltaTime;

        if (currentStorageOutput > 0 && ejectTimer >= ejectInteval)
        {
            Vector2 outputPos = outputDirection + (Vector2)outputPoint.transform.position;

            Collider2D interactableFOund = Physics2D.OverlapCircle(outputPos, 0.2f, LayerMask.GetMask("Conveyor", "Interactable"));
            Collider2D hit = Physics2D.OverlapCircle(outputPos, 0.1f, LayerMask.GetMask("Ressource"));

            outputItem = ressourceDictionnary.GetRessourcePrefab(currentRecipe.output);

            if (interactableFOund != null && hit == null && outputItem != null)
            {
                Instantiate(outputItem, outputPos, quaternion.identity);
                currentStorageOutput -= 1;
                ejectTimer = 0f;
            }

        }
    }


    private IEnumerator ProduceCoroutine()
    {
        isProducing = true;

        yield return new WaitForSeconds(currentRecipe.craftSpeed);


        currentStorageInput1 -= 1;
        currentStorageInput2 -= 1;

        currentStorageOutput += 1;

        isProducing = false;

        ChekingForRessources();
    }


    private void ChekingForRessources()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(inputPoint.position, 0.05f);
        Collider2D[] hits2 = Physics2D.OverlapCircleAll(inputPoin2.position, 0.05f);

        foreach (var hit in hits)
        {
            CatchRessource(hit);
        }
        foreach (var hit in hits2)
        {
            CatchRessource(hit);
        }
    }






}
