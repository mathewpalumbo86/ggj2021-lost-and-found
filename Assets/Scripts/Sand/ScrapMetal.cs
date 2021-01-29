using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMetal : MonoBehaviour, IMetal
{
    public IMetal Collect()
    {
        return this;
        //throw new System.NotImplementedException();
    }

    public void Dig()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsHigher(float height)
    {
        return transform.position.y > height;
    }
}
