using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct PlayerMovementAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<PlayerStats> playerStats;
    private readonly RefRW<LocalTransform> transform;

    public float3 SpawnPosition => transform.ValueRO.Position + 0.3f * transform.ValueRO.Up();
    public quaternion PlayerRotation => transform.ValueRO.Rotation;
    public Entity BulletPrefab => playerStats.ValueRO.prefab;
    public float AttackSpeed => playerStats.ValueRO.attackSpeedInSeconds;

    public void Move(Vector3 moveDirection, float deltaTime)
    {
        float3 movePosition = transform.ValueRO.Position + (float3)playerStats.ValueRO.speed * deltaTime * moveDirection;
        float3 clampedPosition = new float3(Mathf.Clamp(movePosition.x, -CameraEdges.ScreenEdges.x, CameraEdges.ScreenEdges.x), Mathf.Clamp(movePosition.y, -CameraEdges.ScreenEdges.y, CameraEdges.ScreenEdges.y), 0);

        transform.ValueRW.Position = clampedPosition;

        if(moveDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

            transform.ValueRW.Rotation = Quaternion.RotateTowards(transform.ValueRO.Rotation, rotation, playerStats.ValueRO.turnSpeed * deltaTime);
        }
    }
}
