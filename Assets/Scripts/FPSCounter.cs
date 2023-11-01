using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private WaitForSeconds waitTime;
    private float count;

    private IEnumerator Start()
    {
        GUI.depth = 2;

        waitTime = new WaitForSeconds(0.1f);

        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return waitTime;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 40, 100, 25), "FPS: " + Mathf.Round(count));
    }
}
