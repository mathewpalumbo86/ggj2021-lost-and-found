using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTool : MonoBehaviour
{
    protected virtual void Awake()
    {
        //GetComponentInParent<PlayerToolHolder>().AddTool(this);
    }

}
