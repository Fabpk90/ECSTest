using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

public class EnemyAttackSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        
        var job = Entities.ForEach((in Translation t, in EnemyComponent e) =>
        {
            
        }).Schedule(inputDeps);

        return job;
    }
}