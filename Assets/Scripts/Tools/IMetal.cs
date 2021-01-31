using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMetal
{
    IMetal Collect();
    bool CheckSeen();

    Vector3 GetPosition();
    bool IsHigher(float height);
}
