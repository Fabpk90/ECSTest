using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct MoveComponent : IComponentData
{
    public float movementSpeed;
}