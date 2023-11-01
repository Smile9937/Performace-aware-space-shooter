using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionLayer
{
    Enemy,
    PlayerBullet
}

public class ColliderCollection
{
    private static Dictionary<CollisionLayer, HashSet<CustomCollider>> colliderDictionary = new Dictionary<CollisionLayer, HashSet<CustomCollider>>();
    public static void AddCollider(CustomCollider collider)
    {
        if(colliderDictionary.ContainsKey(collider.CollisionLayer))
        {
            colliderDictionary[collider.CollisionLayer].Add(collider);
        }
        else
        {
            colliderDictionary.Add(collider.CollisionLayer, new HashSet<CustomCollider>());
            colliderDictionary[collider.CollisionLayer].Add(collider);
        }
    }

    public static void RemoveCollider(CustomCollider collider)
    {
        if (colliderDictionary.ContainsKey(collider.CollisionLayer))
        {
            colliderDictionary[collider.CollisionLayer].Remove(collider);
        }
    }

    public static HashSet<CustomCollider> GetColliders(CollisionLayer layer)
    {
        if(colliderDictionary.ContainsKey(layer))
        {
            return colliderDictionary[layer];
        }

        return null;
    }
}