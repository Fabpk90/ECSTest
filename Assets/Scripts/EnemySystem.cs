using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class EnemySystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities.ForEach(
            (ref ActorComponent a, in EnemyComponentTag e) =>
            {
                
            }
            ).Schedule(inputDeps);
        
        return inputDeps;
    }
}