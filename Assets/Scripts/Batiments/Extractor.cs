
using UnityEngine;

public class Extractor : BuildingBH
{
    public float ejectTimer = 3f;
    public float timer = 0f;

    public Extractor(Vector2 worldPosition, int rotation) : base(worldPosition, rotation)
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
            Vector2 spawnPos = new Vector2(Mathf.Floor(worldPosition.x) + 0.5f, Mathf.Floor(worldPosition.y) + 0.5f) + GetDirection();
            if (buildingManager.GetBuildingOnTile(spawnPos) is Conveyor && ItemManager.IsSpaceFree(spawnPos))
            {
                ItemManager.AddItem(new ItemBH(RessourceBehaviour.RessourceType.Iron, spawnPos), spawnPos);
                timer -= ejectTimer;
            }
        }

    }


}
