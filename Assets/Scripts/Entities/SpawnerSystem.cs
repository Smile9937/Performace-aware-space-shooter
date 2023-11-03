using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public partial class SpawnerSystem : SystemBase
{
    private enum SpawnLocation
    {
        Top,
        Bottom,
        Left,
        Right
    }

    bool spawned;
    private Camera mainCamera;

    protected override void OnCreate()
    {
        mainCamera = Camera.main;
    }

    protected override void OnDestroy() { }

    [BurstCompile]
    protected override void OnUpdate()
    {
        if(!spawned)
        {
            foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
            {
                for(int i = 0; i < 10000; i++)
                {
                    Spawn(spawner);
                }
                //ProcessSpawner(ref state, spawner);
            }
            spawned = true;
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
    {
        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.prefab);
            quaternion rotation = Quaternion.Euler(0, 0, 180);
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPositionRotation(spawner.ValueRO.spawnPosition, rotation));
            spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
        }
    }

    private void Spawn(RefRW<Spawner> spawner)
    {
        SpawnLocation location = GetSpawnLocation();
        Vector3 position = GetStartPosition(location);
        Quaternion rotation = GetStartRotation(location);

        Entity newEntity = EntityManager.Instantiate(spawner.ValueRO.prefab);
        EntityManager.SetComponentData(newEntity, LocalTransform.FromPositionRotation(position, rotation));
        //spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;

        //Instantiate(gameStats.EnemyPrefab, position, rotation);
    }

    private SpawnLocation GetSpawnLocation()
    {
        int randomNum = Random.Range(0, 4);

        return randomNum switch
        {
            1 => SpawnLocation.Bottom,
            2 => SpawnLocation.Left,
            3 => SpawnLocation.Right,
            _ => SpawnLocation.Top
        };
    }

    private Vector3 GetStartPosition(SpawnLocation spawnLocation)
    {
        Vector3 pos = new Vector3 { z = Mathf.Abs(mainCamera.transform.position.z) };

        const float padding = 5f;
        switch (spawnLocation)
        {
            case SpawnLocation.Top:
                pos.x = Random.Range(0f, Screen.width);
                pos.y = Screen.height + padding;
                break;
            case SpawnLocation.Bottom:
                pos.x = Random.Range(0f, Screen.width);
                pos.y = 0f - padding;
                break;
            case SpawnLocation.Left:
                pos.x = 0f - padding;
                pos.y = Random.Range(0f, Screen.height);
                break;
            case SpawnLocation.Right:
                pos.x = Screen.width + padding;
                pos.y = Random.Range(0f, Screen.height);
                break;
        }

        return mainCamera.ScreenToWorldPoint(pos);
    }

    private Quaternion GetStartRotation(SpawnLocation spawnLocation) => spawnLocation switch
    {
        SpawnLocation.Top => Quaternion.Euler(0, 0, 180f),
        SpawnLocation.Bottom => Quaternion.Euler(0, 0, 0),
        SpawnLocation.Left => Quaternion.Euler(0, 0, 270),
        SpawnLocation.Right => Quaternion.Euler(0, 0, 90),
        _ => Quaternion.identity,
    };
}
