using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : ControllerBaseModel
{
    [SerializeField] List<SoundDataModel> Sounds;
    [SerializeField] NoteSoundDataModel NoteSoundData;
    [SerializeField] AudioSource audioSource;

    public override void Initialize()
    {
        SetAudio(SoundIDs.NoteSound);
    }


    public void PlayCurrentAudio(bool isCombo)
    {
        if (isCombo)
        {
            NoteSoundData.CurrentPitchValue = NoteSoundData.DefaultPitchValue;
            NoteSoundData.PitchComboCounter++;
            NoteSoundData.CurrentPitchValue += NoteSoundData.PitchComboCounter * NoteSoundData.IncreaseValue;
            audioSource.pitch = NoteSoundData.CurrentPitchValue;
        }
        else
        {
            NoteSoundData.CurrentPitchValue = NoteSoundData.DefaultPitchValue;
            NoteSoundData.PitchComboCounter = 0;
            audioSource.pitch = NoteSoundData.CurrentPitchValue;
        }

        audioSource.Play();

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
