using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Collider = Unity.Physics.Collider;
using Material = UnityEngine.Material;
using Random = UnityEngine.Random;

public class TestECS : MonoBehaviour
{
    public Mesh _mesh;
    public Material _material;

    public GameObject prefab;
    
    private NativeArray<Entity> array;
    // Start is called before the first frame update
    void Start()
    {
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var archetype = manager.CreateArchetype(typeof(Translation), typeof(RenderMesh),
            typeof(LocalToWorld), typeof(MoveComponent), typeof(ActorComponent), typeof(EnemyComponent),
            typeof(PhysicsCollider), typeof(PhysicsMass), typeof(Rotation), typeof(PhysicsVelocity)
            , typeof(PhysicsDamping));

        array = new NativeArray<Entity>(5, Allocator.Persistent);
        
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, new BlobAssetStore()); // Error Here
        var p = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);
        manager.AddComponent(p, typeof(RenderMesh));
        manager.AddComponent(p, typeof(LocalToWorld));

        manager.Instantiate(p, array);
        
        foreach (var t in array)
        {
            manager.SetComponentData(t, new MoveComponent() { movementSpeed = 2 * Random.value});
            var physicMass = PhysicsMass.CreateDynamic(MassProperties.UnitSphere, 1f);
            manager.SetComponentData(t, physicMass);
            manager.SetComponentData(t,
                new PhysicsCollider()
                    {Value = BlobAssetReference<Collider>.Create(new Collider() {Filter = CollisionFilter.Default})});
            manager.SetComponentData(t, new ActorComponent() { atk = 2, def = 2, health = 5});
            manager.SetComponentData(t,
                new Translation() {Value = new float3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4))});
            manager.SetSharedComponentData(t, new RenderMesh()
            {
                mesh = _mesh,
                material = _material
            });
        }

    }

    private void OnDestroy()
    {
        print("destroying array");

        array.Dispose();
    }
}
