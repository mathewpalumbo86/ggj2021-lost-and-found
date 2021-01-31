using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrapMetal : MonoBehaviour, IMetal
{

    XRGrabInteractable grab;

    bool isSeen, isFound;

    static int found, total;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        if(grab) grab.enabled = false;
        total++;
    }

    public IMetal Collect()
    {
        if (!isFound)
        {
            isFound = true;
            found++;
        }

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

        isSeen = true;
    }

    public bool CheckSeen()
    {
        return isSeen;
    }

    public static int GetTotalCount()
    {
        return total;
    }

    public static int GetFound()
    {
        return found;
    }
}
