using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerPositionChunkComponent : IComponentData
{
        public float3 position;
}