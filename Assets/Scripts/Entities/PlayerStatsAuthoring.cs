using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerStatsAuthoring : MonoBehaviour
{
    public GameStats stats;
    public GameObject spawnPosition;
}

public class PlayerStatsBaker : Baker<PlayerStatsAuthoring>
{
    public override void Bake(PlayerStatsAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new PlayerStats
        {
            speed = authoring.stats.PlayerMoveSpeed,
            turnSpeed = authoring.stats.PlayerTurnSpeed,
            attackSpeedInSeconds = authoring.stats.PlayerAttackSpeedInSeconds,
            prefab = GetEntity(authoring.stats.PlayerBulletEntityPrefab, TransformUsageFlags.Dynamic)
        });
    }
}