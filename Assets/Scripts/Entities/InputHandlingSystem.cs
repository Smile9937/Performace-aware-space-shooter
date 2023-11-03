using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

[BurstCompile]
public partial class InputHandlingSystem : SystemBase
{
    private PlayerInput playerInput;
    private InputAction move;
    private InputAction shoot;

    private float attackTimer = 0f;

    protected override void OnCreate()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();

        move = playerInput.Player.Move;
        shoot = playerInput.Player.Shoot;
    }

    protected override void OnUpdate()
    {
        InputSystem.Update();
        Vector3 moveDirection = move.ReadValue<Vector2>();

        foreach(PlayerMovementAspect player in SystemAPI.Query<PlayerMovementAspect>())
        {
            player.Move(moveDirection, SystemAPI.Time.DeltaTime);

            if (attackTimer >= player.AttackSpeed)
            {
                if (shoot.IsPressed())
                {
                    Entity bullet = EntityManager.Instantiate(player.BulletPrefab);
                    EntityManager.SetComponentData(bullet, LocalTransform.FromPositionRotation(player.SpawnPosition, player.PlayerRotation));
                    attackTimer = 0;
                }
            }
            else
            {
                attackTimer += SystemAPI.Time.DeltaTime;
            }
        }
    }
}
