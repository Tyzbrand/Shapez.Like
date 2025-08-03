using TMPro;
using UnityEngine;

public class SlotUI : MonoBehaviour
{

    public TextMeshProUGUI type;
    public TextMeshProUGUI qte;


    public void SetText(RessourceBehaviour.RessourceType ressourceType, int quantity)
    {
        type.text = ressourceType.ToString();
        qte.text = quantity.ToString();
    }
}
