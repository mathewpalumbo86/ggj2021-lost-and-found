using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrapMetal : MonoBehaviour, IMetal
{

    XRGrabInteractable grab;

    bool isSeen, isFound;

    static int found, total;
    static float totalValue;

    float value;

    private void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        if(grab) grab.enabled = false;
        total++;

        value = ((float)Random.Range(0, 200))/100;
    }

    public IMetal Collect()
    {
        if (!isFound)
        {
            isFound = true;
            found++;
            totalValue += value;
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

    public static string GetFoundText()
    {
        return found + "/" + total;
    }

    public static string GetTotalValue()
    {
        return "$" + totalValue;
    }

}
