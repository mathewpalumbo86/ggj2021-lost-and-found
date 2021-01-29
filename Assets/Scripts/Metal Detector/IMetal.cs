using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMetal
{
    IMetal Collect();
    void Dig();

    Vector3 GetPosition();
    bool IsHigher(float height);
}
