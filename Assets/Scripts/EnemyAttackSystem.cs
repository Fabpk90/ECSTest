using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

public class EnemyAttackSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        CollisionWorld collisionWorld = World.GetExistingSystem<BuildPhysicsWorld>().PhysicsWorld.CollisionWorld;
        var job = Entities.ForEach((in Translation t, in EnemyComponent e) =>
        {
            OverlapAabbInput inputOverlap = new OverlapAabbInput() { Aabb = new Aabb() { }};
            //collisionWorld.OverlapAabb()
        }).Schedule(inputDeps);

        return job;
    }
}