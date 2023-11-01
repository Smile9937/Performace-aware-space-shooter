using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct MovingSystemBase : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        /*foreach(RefRW<Speed> speed in SystemAPI.Query<RefRW<Speed>>())
        {
            state.EntityManager.SetComponentData(speed.ValueRW.entity, LocalTransform.FromPosition());
        }*/

        //NativeArray<Entity> asteroidArray = EntityManager.Instantiate(asteroidGeneratorComponent.asteroidPrefab, spawnAmount, Allocator.Temp);

        foreach(EnemyAspect enemy in SystemAPI.Query<EnemyAspect>())
        {
            enemy.Move(ref state, SystemAPI.Time.DeltaTime, new Vector3(1, 1, 1));
        }

        foreach (LocalTransform transform in SystemAPI.Query<LocalTransform>())
        {
            Debug.Log(transform);
            //transform.Position += math.up;
            //EntityManager.SetComponentData(transform, new LocalTransform { Position = new Vector3(1, 1, 1) });
        }

        
    }
}