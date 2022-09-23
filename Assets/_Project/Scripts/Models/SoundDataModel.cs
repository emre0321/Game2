using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundDataModel
{
    public SoundIDs soundID;
    public AudioClip Audio;
    public bool Mute;
    public bool ByPassEffects;
    public bool ByPassListenerEffects;
    public bool ByPassReverbZones;
    public bool PlayOnAwake;
    public bool Loop;
    [Range((int) 0, (int)256)]
    public int Priorty;
    [Range(0, 1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;
    public float DefaultPitchValue;
    public float PitchIncreaseValue;
    public float CurrentPitchValue;
    [Range(-1, 1)]
    public float StereoPan;
    [Range(0, 1)]
    public float SpatialBlend;
    [Range(0, 1.1f)]
    public float ReverbZoneMix;
}

