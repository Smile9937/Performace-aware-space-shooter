using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float speed;
    public float colliderSize;
}

public class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
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
        AddComponent(entity, new EnemyTag{});
    }
}