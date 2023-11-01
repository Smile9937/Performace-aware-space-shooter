using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct EnemyAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<Speed> speed;
    private readonly RefRW<LocalTransform> transform;

    public void Move(ref SystemState state, float deltaTime, Vector3 position)
    {
        Vector3 newPos = transform.ValueRW.Position + speed.ValueRW.value * deltaTime * transform.ValueRO.Up();
        state.EntityManager.SetComponentData(entity, LocalTransform.FromPositionRotation(newPos, transform.ValueRO.Rotation));
    }
}