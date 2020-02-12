using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestECS : MonoBehaviour
{
    public Mesh _mesh;
    public Material _material;
    
    private NativeArray<Entity> array;
    // Start is called before the first frame update
    void Start()
    {
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var archetype = manager.CreateArchetype( typeof(Translation), typeof(RenderMesh),
            typeof(LocalToWorld), typeof(MoveComponent), typeof(ActorComponent), typeof(EnemyComponentTag));

        // manager.SetChunkComponentData()

        array = new NativeArray<Entity>(50000, Allocator.Persistent);
        
        manager.CreateEntity(archetype, array);
        
        for (int i = 0; i < array.Length; i++)
        {
            manager.SetComponentData(array[i], new MoveComponent() { movementSpeed = 2 * Random.value});
            manager.SetComponentData(array[i],
                new Translation() {Value = new float3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4))});
            manager.SetSharedComponentData(array[i], new RenderMesh()
            {
                mesh = _mesh,
                material = _material
            });
        }

    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        print("destroying array");

        array.Dispose();
    }
}
