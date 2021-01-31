using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrapPickup : MonoBehaviour
{

    AudioSource audioSource;

    public AudioClip clip;

    IMetal scrap;


    private void Start()
    {
        scrap = GetComponent<IMetal>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Pickup()
    {
        audioSource.PlayOneShot(clip);
        scrap.Collect();
        ObjectCollector.instance.SetIntemNumber(ScrapMetal.GetFoundText());
        ObjectCollector.instance.SetTotalValue(ScrapMetal.GetTotalValue());
    }

}
