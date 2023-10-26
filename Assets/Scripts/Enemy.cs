using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameStats gameStats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + gameStats.EnemyMoveSpeed * Time.deltaTime * transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}