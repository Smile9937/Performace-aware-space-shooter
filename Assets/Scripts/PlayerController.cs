using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameStats gameStats;
    [SerializeField] private Transform attackPosition;

    [SerializeField] private Rigidbody2D rb;

    private PlayerInput playerInput;
    private InputAction move;
    private InputAction shoot;

    private Vector2 moveDirection;

    private float attackTimer = 0f;

    private Vector2 screenEdges;

    private void Awake()
    {
        playerInput = new PlayerInput();

        Camera camera = Camera.main;
        screenEdges = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.nearClipPlane));
    }

    private void OnEnable()
    {
        move = playerInput.Player.Move;
        move.Enable();

        shoot = playerInput.Player.Shoot;
        shoot.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        shoot.Disable();
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (attackTimer >= gameStats.PlayerAttackSpeedInSeconds)
        {
            if (gameStats.PlayerAutoAttack || shoot.IsPressed())
            {
                Bullet bullet = Instantiate(gameStats.PlayerBullet, attackPosition.position, transform.rotation);
                bullet.Initialize(gameStats.PlayerBulletSpeed);
                attackTimer = 0;
            }
        }
        else
        {
            attackTimer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        HandleMoving();
    }

    private void HandleMoving()
    {
        Vector2 movementDirection = moveDirection;

        Vector2 movePosition = (Vector2)transform.position + gameStats.PlayerMoveSpeed * Time.deltaTime * movementDirection;
        Vector2 clampedPosition = new Vector2(Mathf.Clamp(movePosition.x, -screenEdges.x, screenEdges.x), Mathf.Clamp(movePosition.y, -screenEdges.y, screenEdges.y));

        rb.MovePosition(clampedPosition);

        if(movementDirection != Vector2.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, gameStats.PlayerTurnSpeed * Time.deltaTime);
        }
    }
}