using UnityEngine;

public class Extractor : BuildingBH
{
    public float ejectTimer = 3f;
    public float timer = 0f;

    public Extractor(Vector2 worldPosition) : base(worldPosition)
    {

    }

    public override void BuidlingStart()
    {
        Debug.Log("Extractor !");
    }

    public override void BuildingOnDestroy()
    {
        Debug.Log("Extractor Détruit !");
    }

    public override void BuildingUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= ejectTimer)
        {   
            Vector2 spawnPos = worldPosition + Vector2.right;
            if (buildingManager.GetBuildingOnTile(spawnPos) is Conveyor && ItemManager.IsSpaceFree(spawnPos))
            {
                ItemManager.AddItem(new ItemBH(RessourceBehaviour.RessourceType.Iron, spawnPos), spawnPos);
                timer -= ejectTimer;
            }
            }

    }
}
