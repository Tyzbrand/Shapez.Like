using System.Linq;
using UnityEngine;

public class hubBehaviour : MonoBehaviour


{


    private PlayerVariables player;



    private void Start()
    {
        player = ReferenceHolder.instance.playervariable;
    }




    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.gameObject.layer == LayerMask.NameToLayer("Ressource"))
        {

            RessourceBehaviour data = item.GetComponent<RessourceBehaviour>();
            if (player != null && data != null)
            {
                player.inventory.Add(data.type, 1);
                Destroy(item.gameObject);
            }
        }
    }
}
