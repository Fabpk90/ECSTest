using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class MoverSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        var job = Entities.ForEach((ref Translation t, ref MoveComponent m) =>
        {
            t.Value.x += m.movementSpeed * deltaTime;
            if (t.Value.x > 10)
                m.movementSpeed = -m.movementSpeed ;
            else if(t.Value.x < 10)
                m.movementSpeed = m.movementSpeed;
        }).Schedule(inputDeps);

        return job;
    }
}