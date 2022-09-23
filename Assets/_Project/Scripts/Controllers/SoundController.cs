using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : ControllerBaseModel
{
    [SerializeField] List<SoundDataModel> Sounds;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        SetAudio(SoundIDs.NoteSound);
    }

    public void SetAudio(SoundIDs soundIDs)
    {
        SoundDataModel tempDataModel = new SoundDataModel();
        for (int i = 0; i < Sounds.Count; i++)
        {
            if (Sounds[i].soundID == soundIDs)
            {
                tempDataModel = Sounds[i];
            }
        }

        audioSource.clip = tempDataModel.Audio;
        audioSource.mute = tempDataModel.Mute;
        audioSource.bypassEffects = tempDataModel.ByPassEffects;
        audioSource.bypassListenerEffects = tempDataModel.ByPassListenerEffects;
        audioSource.bypassReverbZones = tempDataModel.ByPassReverbZones;
        audioSource.playOnAwake = tempDataModel.PlayOnAwake;
        audioSource.loop = tempDataModel.Loop;
        audioSource.priority = tempDataModel.Priorty;
        audioSource.volume = tempDataModel.Volume;
        audioSource.pitch = tempDataModel.DefaultPitchValue;
        audioSource.panStereo = tempDataModel.StereoPan;
        audioSource.spatialBlend = tempDataModel.SpatialBlend;
        audioSource.reverbZoneMix = tempDataModel.ReverbZoneMix;


    }
}
