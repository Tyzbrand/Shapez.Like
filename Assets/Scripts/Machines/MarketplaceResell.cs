using System.Linq;
using UnityEngine;

public class MarketplaceResell : MonoBehaviour
{

    private RessourceData data;
    private PlayerVariables player;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
        data = ReferenceHolder.instance.ressourceData;

    }


    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.gameObject.layer == LayerMask.NameToLayer("Ressource"))

        {

            if (player != null)
            {

                RessourceBehaviour itemData = item.GetComponent<RessourceBehaviour>();
                player.Money += data.GetPrice(itemData.type);;
                Destroy(item.gameObject);
            }
           
        }
    }
    
    
}
