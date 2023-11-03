using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MovementAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<Speed> speed;
    private readonly RefRW<LocalTransform> transform;

    public void MoveForward(float deltaTime, EntityCommandBuffer ecb)
    {
        Vector3 newPos = transform.ValueRO.Position + speed.ValueRO.value * deltaTime * transform.ValueRO.Up();
        transform.ValueRW.Position = newPos;

        if (CameraEdges.CheckIfInsideScreenBounds((Vector3) transform.ValueRO.Position, 2))
        {
            ecb.AddComponent<DestroyTag>(entity);
        }
    }
}