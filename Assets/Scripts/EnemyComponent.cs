using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct EnemyComponent : IComponentData
{
    public float cooldownToAttack;
    public float lastAttack;
}