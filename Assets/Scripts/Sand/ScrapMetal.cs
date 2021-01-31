using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrapMetal : MonoBehaviour, IMetal
{

    XRGrabInteractable grab;
    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.enabled = false;
    }

    public IMetal Collect()
    {
        return this;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsHigher(float height)
    {
        return transform.position.y > height;
    }

    private void OnWillRenderObject()
    {
        if(grab && !grab.enabled)
            grab.enabled = true;
    }

    public bool CheckSeen()
    {
        if (grab)
            return grab.enabled;
        else return false;
    }
}
