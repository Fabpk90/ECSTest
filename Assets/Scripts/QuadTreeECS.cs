using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class QuadTreeECS : MonoBehaviour
{
    public Mesh _mesh;
    public Material _material;
    
    
    private NativeArray<Entity> array;

    public int elements;

    private void Awake()
    {
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var archetype = manager.CreateArchetype(typeof(Translation), typeof(RenderMesh), typeof(LocalToWorld));
        
        array = new NativeArray<Entity>(elements, Allocator.Temp);

        manager.CreateEntity(archetype, array);
    }
}