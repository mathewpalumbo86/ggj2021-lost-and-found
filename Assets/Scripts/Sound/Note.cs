using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour, INote
{
    int sampleFreq = 44000;


    float frequency = 900;

    float soundTime = 0.01f;

    float timeBetween = 1;

    float maxTime = 0.5f, minTime = 0.001f;
    float maxPitch = 3, minPitch = 1f;

    [SerializeField]
    AnimationCurve timeCurve, pitchShift;

    float currentFrequency;

    float[] samples;

    AudioSource source;
    AudioClip clip;

    float timer = 0;

    [SerializeField]
    [Range(0, 1)]
    float strength;

    private void Start()
    {
        GetOrAddSource();
        source.clip = CreateClip();
    }

    void GetOrAddSource()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
            source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
    }

    AudioClip CreateClip()
    {
        samples = new float[(int)(soundTime * sampleFreq)];
        for (int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI * 2 * i * (frequency / sampleFreq));
        }

        clip = AudioClip.Create("Test", samples.Length, 1, sampleFreq, false);
        clip.SetData(samples, 0);

        currentFrequency = frequency;

        return clip;
    }

    void CalculateStrength(float strength)
    {
        //source.pitch = Mathf.Lerp(minPitch, maxPitch, strength);
        source.pitch = minPitch + (pitchShift.Evaluate(strength) * (maxPitch - minPitch));

        timeBetween = minTime + (timeCurve.Evaluate(strength) * (maxTime - minTime));

    }

    private void Update()
    {
        //CalculateStrength(strength);

        if (timeBetween >= maxTime)
        {
            timer = 0;
            return;
        }

        timer += Time.deltaTime;
        if(timer >= timeBetween)
        {
            if(currentFrequency != frequency)
                source.clip = CreateClip();

            source.Play();
            timer = 0;
        }
    }

    private void LateUpdate()
    {
        timeBetween = maxTime;
    }

    public void SetStrength(float strength)
    {
        CalculateStrength(strength);
    }

    public void ChangeFrequency(float frequency)
    {

    }
}
