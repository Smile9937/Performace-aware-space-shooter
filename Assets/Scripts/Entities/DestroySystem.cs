using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;

[BurstCompile]
[UpdateInGroup(typeof(LateSimulationSystemGroup))]
public partial class DestroySystem : SystemBase
{
    private EndFixedStepSimulationEntityCommandBufferSystem ecbSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        ecbSystem = World.GetExistingSystemManaged<EndFixedStepSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        EntityCommandBuffer.ParallelWriter ecb = ecbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.WithAll<DestroyTag>().ForEach((Entity entity, int entityInQueryIndex) =>
        {
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).WithBurst().ScheduleParallel();
    }
}