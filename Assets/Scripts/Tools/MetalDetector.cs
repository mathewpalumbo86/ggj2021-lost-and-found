using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDetector : PlayerTool, IDetector
{
    Vector3 detectionBoxSize;

    IMetal detected;

    public event Action<float> Detected;

    LayerMask metalLayer;

    INote note;

    private void Start()
    {
        note = GetComponentInChildren<INote>();
    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        IMetal metal = other.GetComponent<IMetal>();
        if (metal != null)
        {
            if (detected != null && metal != detected)
            {
                if (!detected.IsHigher(metal.GetPosition().y))
                {
                    detected = metal;
                }
                else return;
            }

            Vector3 closestPoint = other.ClosestPoint(transform.position);
            DetectedNearby(Vector3.Distance(closestPoint, transform.position));
        }
    }

    public void DetectedNearby(float distance)
    {
        float minDistance = 0.2f;
        float maxDistance = 1f;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        distance = (distance - minDistance) / (maxDistance - minDistance);
        distance = 1 - distance;
        Debug.Log(distance);
        note.SetStrength(distance);
        //Detected?.Invoke(distance);
    }
}
