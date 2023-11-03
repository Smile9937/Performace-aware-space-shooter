using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float speed;
    public float colliderSize;
}

public class BulletBaker : Baker<BulletAuthoring>
{
    public override void Bake(BulletAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Speed
        {
            value = authoring.speed
        });
        AddComponent(entity, new Collider
        {
            ColliderSize = authoring.colliderSize
        });
        AddComponent(entity, new BulletTag{});
    }
}