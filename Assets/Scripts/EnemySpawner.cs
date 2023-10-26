using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameStats gameStats;

    private Camera mainCamera;

    private enum SpawnLocation
    {
        Top,
        Bottom,
        Left,
        Right
    }

    private void Start()
    {
        mainCamera = Camera.main;

        if(gameStats.SpawnOverTime)
            StartCoroutine(SpawnEnemy());

        if(gameStats.SpawnOnStart)
        {
            for(int i = 0; i < gameStats.AmountToSpawn; i++)
            {
                Spawn();
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(gameStats.SpawnRateInSeconds);
            Spawn();
        }
    }

    private void Spawn()
    {
        SpawnLocation location = GetSpawnLocation();
        Vector3 position = GetStartPosition(location);
        Quaternion rotation = GetStartRotation(location);

        Instantiate(gameStats.EnemyPrefab, position, rotation);
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