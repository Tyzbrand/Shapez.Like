using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ConveyorBehaviour : MonoBehaviour
{
    private List<Transform> itemsOnBelt = new();
    private float speed = 2.0f;
    public Transform EndPoint;


    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.CompareTag("Ressource"))
        {
            itemsOnBelt.Add(item.transform);
        }

    }

    private void OnTriggerExit2D(Collider2D item)
    {
        if (item != null)
        {
            itemsOnBelt.Remove(item.transform);
        }
    }

    private void Update()
    {
        Vector3 direction = -transform.up;


        foreach (Transform item in itemsOnBelt)
        {

            if (item != null)

            {
                Vector3 nextPos = item.position + direction * speed * Time.deltaTime;

                Collider2D[] hits = Physics2D.OverlapCircleAll(nextPos, 0.15f, LayerMask.GetMask("Ressource"));
                Collider2D foundry = Physics2D.OverlapCircle(nextPos, 0.1f, LayerMask.GetMask("Foundry"));
                bool foundryBlocked = false;
                bool ressourceBlocked = false;


                if (foundry != null)
                {
                    foundryBlocked = true;
                }
                else foundryBlocked = false;


                foreach (var hit in hits)
                {
                    if (!hit.transform.IsChildOf(item))
                    {
                        ressourceBlocked = true;
                        break;
                    }
                }

                if (!ressourceBlocked && !foundryBlocked)
                {
                    float distanceToEnd = Vector3.Distance(item.position, EndPoint.position);

                    if (distanceToEnd > 0.05f)
                    {
                        item.position = nextPos;
                    }
                }
                

            }
        }
    }

}
        

    

