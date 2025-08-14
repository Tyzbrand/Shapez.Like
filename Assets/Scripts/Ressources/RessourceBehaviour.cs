using UnityEngine;

public class RessourceBehaviour : MonoBehaviour
{
    [System.Serializable]
    public enum RessourceType
    {   
        Empty,
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
