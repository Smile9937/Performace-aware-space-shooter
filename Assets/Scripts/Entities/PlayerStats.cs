using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerStats : IComponentData
{
    public float speed;
    public float turnSpeed;
    public float attackSpeedInSeconds;
    public Entity prefab;
}
