using Unity.Entities;
using UnityEngine;

public struct ActorComponent : IComponentData
{
    public float health;
    public float atk;
    public float def;
}