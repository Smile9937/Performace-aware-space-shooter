using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollider : MonoBehaviour
{
    [SerializeField] private CollisionLayer collisionLayer;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float size;
    [SerializeField] private bool checkForCollision;

    public CollisionLayer CollisionLayer => collisionLayer;
    public LayerMask LayerMask => layerMask;
    public Vector2 Center => (Vector2)transform.position;
    public float Width => Center.x + size;
    public float Height => Center.y + size;

    private void Awake() => ColliderCollection.AddCollider(this);
    private void OnDestroy() => ColliderCollection.RemoveCollider(this);

    public bool CheckForCollision(CollisionLayer collisionLayer, out GameObject collisionObject)
    {
        if(ColliderCollection.GetColliders(collisionLayer) != null && checkForCollision == true)
        {
            foreach (CustomCollider otherCollider in ColliderCollection.GetColliders(collisionLayer))
            {
                if(otherCollider.Width >= Center.x &&
                otherCollider.Center.x <= Width &&
                otherCollider.Height >= Center.y &&
                otherCollider.Center.y <= Height)
                {
                    collisionObject = otherCollider.gameObject;
                    return true;
                }                
            }
        }

        collisionObject = null;
        return false;
    }
}