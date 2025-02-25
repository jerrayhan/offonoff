// Unity Audio Spectrum data analysis
// IMDM Course Material 
// Author: Myungin Lee
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class AudioSpectrum : MonoBehaviour
{
    AudioSource source;
    //change 1
    public Light lighty;
    public static int FFTSIZE = 1024; //
    public static float[] samples = new float[FFTSIZE];
    public static float audioAmp = 0f;
    public float smoothingFactor = 0.1f; //added smoothing factor 
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

        //smooth it out
        audioAmp = Mathf.Lerp(audioAmp, newAudioAmp, smoothingFactor);

        lighty.intensity = audioAmp*3;
    }
}
