using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDetector
{
    void Activate();
    void Deactivate();
    void DetectedNearby(float distance);
    event Action<float> Detected;

}
