using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DiggingGun : PlayerTool, IDig
{

    public InputActionReference useAction;
    XRGrabInteractable grabInteractable;

    [SerializeField]
    float range, forceOffset, force;

    [SerializeField]
    LayerMask sand;

    [SerializeField]
    bool digging;

    private void Update()
    {
        if (digging)
        {
            GetDigPoint();
        }
        
	}


    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        useAction.action.started += Dig; //equivalent to GetKeyDown()
        useAction.action.canceled += StopDigging; //Equivalent to GetKeyUp()

        ///Getting the equivalent of a GetKey() is a little bit more complicated.
        ///
        /// 1: Set a bool as true inside Dig
        /// 2: Set that bool to false inside StopDigging()
        /// 3: Check for that Bool on Update()
    }

    void GetDigPoint()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, (transform.forward * range) + transform.position, Color.red, 0.1f);
        Physics.Raycast(transform.position, transform.forward, out hit, range, sand);

            MeshDeformer deformer = hit.collider?.GetComponent<MeshDeformer>();
        if (deformer)
        {
            Vector3 point = hit.point;
            point += Vector3.down * forceOffset;
            deformer.AddDeformingForce(point, force);
        }
    }

    public void Dig()
    {
        GetDigPoint();
    }

    public void DigStart()
    {
        digging = true;
    }

    public void DigEnd()
    {
        digging = false;
    }

    void DrawTexture()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, (transform.forward * range) + transform.position, Color.red, 0.1f);
        Physics.Raycast(transform.position, transform.forward, out hit, range, sand);

    }

 
    public void Dig(InputAction.CallbackContext context)
    {
        if (grabInteractable.isSelected)
        {
            DigStart();
        }
    }

    public void StopDigging(InputAction.CallbackContext context)
    {
        DigEnd();
    }

    
 
}
