using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct EnemyCollisionAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<EnemyTag> enemy;
    private readonly RefRW<Collider> collider;
    private readonly RefRW<LocalTransform> transform;

    Vector2 Center => (Vector3)transform.ValueRO.Position;
    float Width => Center.x + collider.ValueRO.ColliderSize;
    float Height => Center.y + collider.ValueRO.ColliderSize;
    public void CheckCollision(BulletCollisionAspect bullet, EntityCommandBuffer ecb)
    {
        if (bullet.Width >= Center.x &&
        bullet.Center.x <= Width &&
        bullet.Height >= Center.y &&
        bullet.Center.y <= Height)
        {
            ecb.AddComponent<DestroyTag>(bullet.Entity);
            ecb.AddComponent<DestroyTag>(entity);
        }
    }
}