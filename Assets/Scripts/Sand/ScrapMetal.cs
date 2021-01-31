using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMetal : MonoBehaviour, IMetal
{

    Collider col;

    private void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;
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
        if(col && !col.enabled)
            col.enabled = true;
    }

    public bool CheckSeen()
    {
        if (col)
            return col.enabled;
        else return false;
    }
}
