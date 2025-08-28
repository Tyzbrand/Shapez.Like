using UnityEngine;

public class RessourceBehaviour : MonoBehaviour
{
    [System.Serializable]
    public enum RessourceType
    {   
        Empty,
        Stone,
        Coal,
        Iron,
        IronIngot,
        IronPlate,
        Copper,
        CopperIngot,
        CopperPlate,
        Silver,
        SilverIngot

    }

    public RessourceType type;
}
