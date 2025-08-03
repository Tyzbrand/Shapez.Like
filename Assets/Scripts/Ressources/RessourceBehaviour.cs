using UnityEngine;

public class RessourceBehaviour : MonoBehaviour
{
    [System.Serializable]
    public enum RessourceType
    {
        Iron,
        Copper,
        Coal,
        IronIngot,
        CopperIngot
    }

    public RessourceType type;
}
