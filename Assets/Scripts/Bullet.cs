using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float movementSpeed = 1f;

    //public Vector3 Size => spriteRenderer.bounds.extents;
    //public Vector3 Center => spriteRenderer.bounds.center;

    public void Initialize(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + movementSpeed * Time.deltaTime * transform.up);
    }

    private void Update()
    {
        /*if(GameManager.CheckForCollision(Size, Center))
        {
            Destroy(gameObject);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}