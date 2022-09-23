using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : ControllerBaseModel
{
    public List<LevelDataModel> Levels;
    public LevelDataModel CurrentLevel;

    [Header("PLATFORM POOL")]
    [SerializeField] PoolModel PlatformPool;
    [SerializeField] PlatformModel FinishPlatformPrefab;

    [Header("LEVEL GENERATE PARAMS")]
    public int StaticPlatformCount;
    public float PlatformsXOffset;
    public float PlatformsZOffset;

    public override void Initialize()
    {
        GenerateLevel(Levels[0]);
    }

    public void GenerateLevel(LevelDataModel level)
    {
        if (CurrentLevel != null)
            CurrentLevel.Reset();
        CurrentLevel = level;


        for (int i = 0; i < StaticPlatformCount; i++)
        {
            PlatformModel tempPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
            tempPlatform.SetActive();
            tempPlatform.transform.position = new Vector3(0, 0, i * PlatformsZOffset);
            CurrentLevel.GeneratedPlatforms.Add(tempPlatform);
        }


        for (int i = 0; i < level.MoveablePlatformCount.Count; i++)
        {
            PlatformModel tempPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
            tempPlatform.MovementType = level.MoveablePlatformCount[i];
            tempPlatform.SetActive();
            tempPlatform.transform.position = new Vector3(tempPlatform.MovementType == PlatformMovementType.Left ? -PlatformsXOffset : PlatformsXOffset, 0, CurrentLevel.GeneratedPlatforms[CurrentLevel.GeneratedPlatforms.Count - 1].transform.position.z + PlatformsZOffset);
            CurrentLevel.GeneratedPlatforms.Add(tempPlatform);

        }
    }

}
