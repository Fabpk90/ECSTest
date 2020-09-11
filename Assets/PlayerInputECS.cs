using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Material = UnityEngine.Material;
using Collider = Unity.Physics.Collider;

public class PlayerInputECS : MonoBehaviour
{
    public static PlayerInputECS instance;
    
    private PlayerInput input;

    private EntityManager manager;
    private Entity _entity;

    public bool isRightClicking;
    public Vector2 movement;

    public Mesh playerMesh;
    public Material playerMaterial;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       /* input = GetComponent<PlayerInput>();
        
        input.currentActionMap["Move"].started += OnMoveStarted;
        input.currentActionMap["Move"].canceled += OnMoveCanceled;
        
        input.currentActionMap["RightClick"].started += OnRightClickStarted;
        input.currentActionMap["RightClick"].canceled += OnRightClickCanceled;*/

    }
    
    private void OnRightClickCanceled(InputAction.CallbackContext obj)
    {
        isRightClicking = false;
    }

    public void OnRightClickStarted(InputAction.CallbackContext obj)
    {
        isRightClicking = true;
    }

    public void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }

    public void OnMoveStarted(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }
}
