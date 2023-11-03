using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial class MovementSystem : SystemBase
{
    private EndFixedStepSimulationEntityCommandBufferSystem ecbSystem;

    protected override void OnCreate()
    {
        base.OnCreate();

        ecbSystem = World.GetExistingSystemManaged<EndFixedStepSimulationEntityCommandBufferSystem>();
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        EntityCommandBuffer ecb = ecbSystem.CreateCommandBuffer();

        foreach(MovementAspect movingEntity in SystemAPI.Query<MovementAspect>())
        {
            movingEntity.MoveForward(SystemAPI.Time.DeltaTime, ecb);
        }

        foreach(EnemyCollisionAspect enemy in SystemAPI.Query<EnemyCollisionAspect>())
        {
            foreach(BulletCollisionAspect bullet in SystemAPI.Query<BulletCollisionAspect>())
            {
                enemy.CheckCollision(bullet, ecb);
            }
        }
    }
}