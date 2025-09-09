using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking.PlayerConnection;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;



//-------------------------------------Gère tout les input liés au mouvement du joueur-------------------------------------
//utilisation de "PlayerVariables" pour: -buildMode



public class PlayerMove : MonoBehaviour
{
    private UnityEngine.Vector2 moveInput;
    private float scrollInput;
    public float speed = 7f;
    private float finalSpeed;
    private float baseZoom = 2.5f;
    private int ppu = 64;
    public float zoomSpeed = 0.5f;
    public float minZoom = 3f;
    public float maxZoom = 30f;

    private Camera cam;
    private PlayerVariables player;

    //Drag and move variables
    private UnityEngine.Vector3 mousePosition;
   public bool isDragging = false;




    




    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        cam = ReferenceHolder.instance.mainCamera;
        player = ReferenceHolder.instance.playervariable;
    }
    private void Update()
    {
        //Scroll
        if (!player.isInUI && !player.isInPauseUI && !player.isInBuildingUI)
        {
            cam.orthographicSize -= scrollInput * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            scrollInput = 0f;
            finalSpeed = math.sqrt(cam.orthographicSize / baseZoom);
        }

        //Drag and move
        if (isDragging)
        {
            UnityEngine.Vector3 currentMousPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position += mousePosition - currentMousPos;
        }

        //XY
        if (!player.isInUI && !player.isInPauseUI)
        {
            Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f) * speed * finalSpeed * Time.fixedDeltaTime;
            transform.position += movement;
        }
    }

    //Fix de la caméra pour eviter les déformations des sprites
    private void LateUpdate()
    {
        Vector3 camFix = cam.transform.position;
        cam.transform.position = new Vector3(math.round(camFix.x * ppu) / ppu, math.round(camFix.y * ppu) / ppu, camFix.z);
    }

    //Input mouvements X Y
    private void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //Input mouvement de zoom
    private void Scroll(InputAction.CallbackContext context)
    {
        scrollInput = context.ReadValue<Vector2>().y;
    }

    //Input Drag and Move avec la souris
    private void DragMove(InputAction.CallbackContext context)
    {
        if (context.performed && !player.buildMode && !player.isInUI && !player.isInPauseUI)
        {
            isDragging = true;
            mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if (context.canceled)
        {
            isDragging = false;
        }
    }


}

