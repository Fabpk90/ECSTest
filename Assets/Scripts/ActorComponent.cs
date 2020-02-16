using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct ActorComponent : IComponentData
{
    public float health;
    public float atk;
    public float def;

    public float atkSum;
}