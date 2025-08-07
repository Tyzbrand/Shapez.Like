using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMapTeleport : MonoBehaviour
{

    private void Start()
    {
        SceneManager.LoadScene(1);
        Application.targetFrameRate = 165;
        transform.position = new Vector2(45, 106);
    }
}
