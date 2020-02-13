using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;
using Material = UnityEngine.Material;

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
        input = GetComponent<PlayerInput>();
        
        input.currentActionMap["Move"].started += OnMoveStarted;
        input.currentActionMap["Move"].canceled += OnMoveCanceled;
        
        input.currentActionMap["RightClick"].started += OnRightClickStarted;
        input.currentActionMap["RightClick"].canceled += OnRightClickCanceled;
        
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        var playerArchetype = manager.CreateArchetype( typeof(Translation), typeof(RenderMesh),
            typeof(LocalToWorld), typeof(MoveComponent), typeof(ActorComponent), typeof(PlayerComponentTag));
        
       _entity = manager.CreateEntity(playerArchetype);
       
       manager.SetComponentData(_entity, new Translation() { Value = new float3( 10, 0, 0)});
       manager.SetComponentData(_entity, new ActorComponent() { atk = 2, def = 3, health = 50});
       
       manager.SetSharedComponentData(_entity, new RenderMesh()
       {
           mesh = playerMesh,
           material = playerMaterial
       });
        
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
