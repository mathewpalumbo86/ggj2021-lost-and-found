using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DiggingInput : MonoBehaviour
{
    public InputActionReference useAction;
    XRGrabInteractable grabInteractable;

    IDig diggingTool;

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        useAction.action.started += Dig; //equivalent to GetKeyDown()
        useAction.action.canceled += StopDigging; //Equivalent to GetKeyUp()

        diggingTool = GetComponentInChildren<IDig>();
    }

    public void Dig(InputAction.CallbackContext context)
    {
        if (grabInteractable.isSelected)
        {
            diggingTool.DigStart();
        }
    }

    public void StopDigging(InputAction.CallbackContext context)
    {
        diggingTool.DigEnd();
    }
}
