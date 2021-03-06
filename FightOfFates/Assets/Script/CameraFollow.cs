﻿using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float smoothSpeed = 0.080f;
    public Vector3 offset;
    public SelectPlayerController selectPlayerController;
    private Transform target;

    void FixedUpdate()
    {

        if(target == null)
        {
            try
            {
                target = GameObject.Find("Player").transform;
            }
            catch (Exception e)
            {
                return;
            }
            
        }
        else
        {
            Vector3 desiredPosition = target.position + offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
        }
       
        
    }

  
    
}
