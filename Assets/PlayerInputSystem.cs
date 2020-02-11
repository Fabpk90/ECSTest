using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

[AlwaysSynchronizeSystem]
public class PlayerInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithoutBurst()
            .ForEach((ref Translation t, in PlayerComponentTag p) =>
            {
                t.Value.x += Input.GetAxis("Horizontal") * Time.DeltaTime;
                t.Value.y += Input.GetAxis("Vertical") * Time.DeltaTime;
            }).Run();
        return default;
    }
}