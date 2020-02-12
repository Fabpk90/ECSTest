using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EnemySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float3 playerPos = float3.zero;
        Entities.WithoutBurst().ForEach((in PlayerComponentTag p, in Translation t) => { playerPos = t.Value; }).Run();

        float delta = Time.DeltaTime;
        
        var job = Entities.ForEach(
            (ref ActorComponent a, ref Translation t, in EnemyComponentTag e) =>
            {
                t.Value = t.Value + math.normalize(playerPos - t.Value) * delta;
            }
            ).Schedule(inputDeps);
        
        return job;
    }
}