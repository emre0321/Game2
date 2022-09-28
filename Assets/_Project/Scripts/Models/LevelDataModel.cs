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
    public float NextLevelZOffset;

    public void Reset()
    {
        for (int i = 0; i < StaticPlatforms.Count; i++)
        {
            StaticPlatforms[i].ResetPlatform();
        }
    }
  
}
