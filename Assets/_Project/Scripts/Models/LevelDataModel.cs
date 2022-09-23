using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelDataModel
{
    public List<PlatformMovementType> MoveablePlatformCount;
    public List<PlatformModel> GeneratedPlatforms;

    public void Reset()
    {
        for (int i = 0; i < GeneratedPlatforms.Count; i++)
        {
            GeneratedPlatforms[i].ResetPlatform();
            GeneratedPlatforms[i].SetDeactive();
        }
    }

}
