using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class upanddown : MonoBehaviour
{
    AudioSource source;
    public static int FFTSIZE = 1024; // https://en.wikipedia.org/wiki/Fast_Fourier_transform
    public static float[] samples = new float[FFTSIZE];
    public static float audioAmp = 0f;
    public float smoothingFactor = 0.1f; // added smoothing factor
    public float minY = 0f; // minimum Y value
    public float maxY = 10f; // maximum Y value

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        // The source (time domain) transforms into samples in frequency domain 
        GetComponent<AudioSource>().GetSpectrumData(samples, 0, FFTWindow.Hanning);
        // Empty first, and pull down the value.
        float newAudioAmp = 0f;
        for (int i = 0; i < FFTSIZE; i++)
        {
            newAudioAmp += samples[i];
        }

        // Smooth it out
        audioAmp = Mathf.Lerp(audioAmp, newAudioAmp, smoothingFactor);

        // Change the Y value of the object's position, bounded by minY and maxY
        float newY = Mathf.Clamp(audioAmp * 1.5f, minY, maxY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}