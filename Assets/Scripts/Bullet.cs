using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float movementSpeed = 1f;

    public void Initialize(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    private void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * transform.up;

        if(CameraEdges.CheckIfInsideScreenBounds(transform.position, 2))
        {
            Destroy(gameObject);
        }
    }
}