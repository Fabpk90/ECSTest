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
            (ref ActorComponent a, ref Translation t, in EnemyComponent e) =>
            {
                float3 directionToPlayer = playerPos - t.Value;
               // if (math.length(directionToPlayer) < 1.25f)

              //t.Value = t.Value + math.normalize(directionToPlayer) * delta;
            }
            ).Schedule(inputDeps);
        
        /*Entities.WithoutBurst().ForEach((ref ActorComponent a, in PlayerComponentTag p) =>
        {
            a.health -= damage;
            if(a.health <= 0)
                Debug.Log("player dead");
        }).Run();*/
        
        return job;
    }
}