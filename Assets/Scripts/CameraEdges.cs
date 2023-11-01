using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEdges : MonoBehaviour
{
    private Camera myCamera;
    private static Vector3 screenEdges;

    public static Vector3 ScreenEdges => screenEdges;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
        SetCollisionEdges();
    }

    private void SetCollisionEdges()
    {
        screenEdges = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, myCamera.nearClipPlane));
    }

    public static bool CheckIfInsideScreenBounds(Vector2 position, float padding)
    {
        if(position.x > screenEdges.x + padding || 
           position.x < -screenEdges.x - padding ||
           position.y > screenEdges.y + padding || 
           position.y < -screenEdges.y - padding)
        {
            return true;
        }

        return false;
    }
}