using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CustomCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CustomCollider customCollider;
    [SerializeField] private GameStats gameStats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        transform.position += gameStats.EnemyMoveSpeed * Time.deltaTime * transform.up;

        if (customCollider.CheckForCollision(CollisionLayer.PlayerBullet ,out GameObject collisionObject))
        {
            Destroy(collisionObject);
            Destroy(gameObject);
        }

        if (CameraEdges.CheckIfInsideScreenBounds(transform.position, 2))
        {
            Destroy(gameObject);
        }
    }
}