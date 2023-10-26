using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdges : MonoBehaviour
{
    Camera myCamera;
    EdgeCollider2D edgeCollider;

    float padding = 2f;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();

        SetCollisionEdges();
    }

    private void SetCollisionEdges()
    {
        if (!myCamera.orthographic)
        {
            Debug.LogError("The main camera needs to be set to ortographic");
            return;
        }

        Vector2 leftBottom = myCamera.ScreenToWorldPoint(new Vector3(0, 0, myCamera.nearClipPlane));
        leftBottom.x -= padding;
        leftBottom.y -= padding;

        Vector2 leftTop = myCamera.ScreenToWorldPoint(new Vector3(0, myCamera.pixelHeight, myCamera.nearClipPlane));
        leftTop.x -= padding;
        leftTop.y += padding;

        Vector2 rightTop = myCamera.ScreenToWorldPoint(new Vector3(myCamera.pixelWidth, myCamera.pixelHeight, myCamera.nearClipPlane));
        rightTop.x += padding;
        rightTop.y += padding;

        Vector2 rightBottom = myCamera.ScreenToWorldPoint(new Vector3(myCamera.pixelWidth, 0, myCamera.nearClipPlane));
        rightBottom.x += padding;
        rightBottom.y -= padding;

        Vector2[] edgePoints = new[] { leftBottom, leftTop, rightTop, rightBottom, leftBottom };

        edgeCollider.points = edgePoints;
        edgeCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}