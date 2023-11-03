using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Stats", fileName = "Game Stats")]
public class GameStats : ScriptableObject
{
    #region Player Stats
    [Header("Player Stats")]

    [SerializeField] private float playerMoveSpeed = 5f;
    [SerializeField] private float playerTurnSpeed = 0.3f;
    [SerializeField] private float playerAttackDelayInSeconds = 0.2f;
    [SerializeField] private Bullet playerBulletPrefab;
    [SerializeField] private GameObject playerBulletEntityPrefab;
    [SerializeField] private float playerBulletSpeed = 7f;

    public float PlayerMoveSpeed => playerMoveSpeed;
    public float PlayerTurnSpeed => playerTurnSpeed;
    public float PlayerAttackSpeedInSeconds => playerAttackDelayInSeconds;
    public Bullet PlayerBullet => playerBulletPrefab;
    public GameObject PlayerBulletEntityPrefab => playerBulletEntityPrefab;
    public float PlayerBulletSpeed => playerBulletSpeed;
    #endregion

    #region Enemy Spawning
    [Header("Enemy Spawning")]

    [SerializeField] private float spawnRateInSeconds = 1.0f;
    [SerializeField] private int amountToSpawn = 100;
    [SerializeField] private bool spawnOnStart = false;
    [SerializeField] private bool spawnOverTime = false;
    [SerializeField] private Enemy enemyPrefab;

    public float SpawnRateInSeconds => spawnRateInSeconds;
    public int AmountToSpawn => amountToSpawn;
    public bool SpawnOnStart => spawnOnStart;
    public bool SpawnOverTime => spawnOverTime;
    public Enemy EnemyPrefab => enemyPrefab;
    #endregion

    #region Enemy Stats
    [Header("Enemy Stats")]

    [SerializeField] private float enemyMoveSpeed = 1.0f;

    public float EnemyMoveSpeed => enemyMoveSpeed;
    #endregion

    #region Modifiers
    [Header("Modifiers")]
    [SerializeField] private bool playerAutoAttack = false;
    public bool PlayerAutoAttack => playerAutoAttack;
    #endregion
}