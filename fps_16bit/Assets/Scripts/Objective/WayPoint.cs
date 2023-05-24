using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public Image image;
    public Transform target;
    private Vector3 targetLocation;

    private void Update()
    {
        targetLocation = Camera.main.WorldToScreenPoint(target.position);
        image.transform.position = targetLocation;
    }
    

    private void OnGUI()
    {
        if (targetLocation.z >= 0f)
        {
            GUI.color = Color.green;
            GUI.Label(new Rect(targetLocation.x + 6, Screen.height - targetLocation.y, 200, 20), "OVER HERE");
            GUI.Label(new Rect(targetLocation.x - 6, Screen.height - targetLocation.y, 200, 20), "Waypoint");
        }
        
    }
}
