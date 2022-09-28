using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelDataModel
{
    public int StaticPlatformCount;
    public int NumberOfCuts;

    public List<PlatformModel> StaticPlatforms;
    public List<PlatformModel> GeneratedPlatforms;
    public float NextLevelZOffset;

    public void Reset()
    {
        for (int i = 0; i < StaticPlatforms.Count; i++)
        {
            StaticPlatforms[i].ResetPlatform();
            StaticPlatforms[i].SetDeactive();
        }

        for (int i = 0; i < GeneratedPlatforms.Count; i++)
        {
            GeneratedPlatforms[i].ResetPlatform();
            GeneratedPlatforms[i].SetDeactive();
        }

    }

}
