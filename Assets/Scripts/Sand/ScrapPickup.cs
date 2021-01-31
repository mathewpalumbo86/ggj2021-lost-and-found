using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrapPickup : MonoBehaviour
{

    AudioSource audioSource;

    public AudioClip clip;

    IMetal scrap;

    XRGrabInteractable interactable;

    Rigidbody rb;

    bool isPicked = false;


    private void Start()
    {
        scrap = GetComponent<IMetal>();
        audioSource = GetComponent<AudioSource>();
        ObjectCollector.instance.SetIntemNumber(ScrapMetal.GetFoundText());
        ObjectCollector.instance.SetTotalValue(ScrapMetal.GetTotalValue());
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(interactable.isSelected && !isPicked)
        {
            isPicked = true;
            Pickup();
            rb.useGravity = true;
        }

        
    }


    public void Pickup()
    {
        audioSource.PlayOneShot(clip);
        scrap.Collect();
        ObjectCollector.instance.SetIntemNumber(ScrapMetal.GetFoundText());
        ObjectCollector.instance.SetTotalValue(ScrapMetal.GetTotalValue());
    }

}
