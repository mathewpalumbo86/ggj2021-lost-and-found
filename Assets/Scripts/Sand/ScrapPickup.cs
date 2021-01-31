using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrapPickup : MonoBehaviour
{

    public int itemValue;

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
    }

}
