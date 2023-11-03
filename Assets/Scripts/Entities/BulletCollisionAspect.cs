using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct BulletCollisionAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<BulletTag> bullet;
    private readonly RefRW<Collider> collider;
    private readonly RefRW<LocalTransform> transform;

    public Entity Entity => entity;
    public Vector2 Center => (Vector3)transform.ValueRO.Position;
    public float Width => Center.x + collider.ValueRO.ColliderSize;
    public float Height => Center.y + collider.ValueRO.ColliderSize;
}