using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : ControllerBaseModel
{
    public static LevelController Instance;

    public List<LevelDataModel> Levels;
    public LevelDataModel CurrentLevel;

    [Header("GECICI OLARAK LEVEL INDEX")]
    public int LevelIndex;
    public int FakeLevelIndex;

    [Header("PLATFORMS OFFSET VALUES")]
    [SerializeField] float MovingPlatformXOffset;
    [SerializeField] float PlatformsZOffset;
    [SerializeField] float XPerfectToleranceValue;
    [SerializeField] float XFailScaleToleranceValue;

    [Header("PLATFORM POOL")]
    [SerializeField] PoolModel PlatformPool;
    [SerializeField] PoolModel FinishPlatformPool;

    [Header("PLAYER CONTROLLER")]
    [SerializeField] PlayerController playerController;

    [Header("IZLENEN DEGERLER")]
    [SerializeField] PlatformModel ReferancePlatform;
    [SerializeField] PlatformModel MovingPlatform;
    [SerializeField] int NumberOfCuts;


    public override void Initialize()
    {
        Instance = this;
        GenerateLevel(Levels[LevelIndex], 0);
    }

    public void NextLevel()
    {
        LevelIndex++;
        if (LevelIndex == Levels.Count)
            LevelIndex = 0;
        float tempZOffset = CurrentLevel.NextLevelZOffset;

        GenerateLevel(Levels[LevelIndex], tempZOffset);
    }

    public void RetryLevel()
    {
        float tempZOffset = CurrentLevel.NextLevelZOffset;

        GenerateLevel(Levels[LevelIndex], tempZOffset);
    }

   

    public void GenerateLevel(LevelDataModel level , float zOffset)
    {
        if (CurrentLevel != null)
            CurrentLevel.Reset();

        CurrentLevel = level;
        NumberOfCuts = 0;

        // STATIC PLATFORMLARI OLUSTURMA
        for (int i = 0; i < level.StaticPlatformCount; i++)
        {
            PlatformModel tempPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
            tempPlatform.SetActive();
            tempPlatform.transform.position = new Vector3(0, 0, (i * PlatformsZOffset)+ zOffset);
            CurrentLevel.StaticPlatforms.Add(tempPlatform);
        }

        // FINISH PLATFORMU OLUSTURMA
        PlatformModel tempFinishPlatform = FinishPlatformPool.GetDeactiveItem<PlatformModel>();
        tempFinishPlatform.transform.position = new Vector3(0, 0.5f, (((level.StaticPlatformCount + level.NumberOfCuts) * PlatformsZOffset) - 0.5f) + zOffset);
        tempFinishPlatform.SetActive();
        CurrentLevel.GeneratedPlatforms.Add(tempFinishPlatform);

        //// PLAYER POSITION SETLEME
        //playerController.SetPlayerPosition(CurrentLevel.StaticPlatforms[0].transform.position + new Vector3(0, 0.5f, 0));

        CurrentLevel.NextLevelZOffset = tempFinishPlatform.transform.position.z + 2.5f;

        CurrentLevel.StaticPlatforms[CurrentLevel.StaticPlatforms.Count - 1].IsReferancePlatform = true;
        ReferancePlatform = CurrentLevel.StaticPlatforms[CurrentLevel.StaticPlatforms.Count - 1];
    }


    public override void ControllerUpdate(GameStates gameState)
    {
        if (gameState == GameStates.Gameplay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // SOL TUSLA KESME ISLEMI
                CalculateCutProcess();
            }
        }

    }
    public void GenerateMovingPlatform()
    {
        MovingPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        int ran = UnityEngine.Random.Range(0, 2);
        if (ran == 0)
        {
            // Left
            MovingPlatform.MovementType = PlatformMovementType.Left;
            MovingPlatform.transform.position = ReferancePlatform.transform.position + new Vector3(-MovingPlatformXOffset, 0, 3);

        }
        else
        {
            // Right
            MovingPlatform.MovementType = PlatformMovementType.Right;
            MovingPlatform.transform.position = ReferancePlatform.transform.position + new Vector3(MovingPlatformXOffset, 0, 3);


        }
        MovingPlatform.transform.localScale = ReferancePlatform.transform.localScale;
        MovingPlatform.SetActive();
        MovingPlatform.SetPlatformMovement(true);

    }

    public void CalculateCutProcess()
    {
        if (ReferancePlatform == null || MovingPlatform == null)
            return;

        if (!IsPlatformsCloseEnough(ReferancePlatform, MovingPlatform))
        {
            Debug.Log("MESAFE UZAK");
            //GameController.ChangeGameState(GameStates.LevelFail);
            return;
        }

        if (IsPerfect(ReferancePlatform, MovingPlatform))
        {

            Debug.Log("PERFECT !");
        }
        else
        {
            Debug.Log("NOT PERfect");

        }

        MovingPlatform.SetDeactive();



        bool isLeft = ReferancePlatform.transform.position.x - MovingPlatform.transform.position.x < 0 ? true : false;

        float xPositionDiff = Mathf.Abs(ReferancePlatform.transform.position.x - MovingPlatform.transform.position.x);
        float overOffset = (MovingPlatform.transform.localScale.x / 2) - xPositionDiff;
        float newXPivot = xPositionDiff / 2 + (isLeft == true ? ReferancePlatform.transform.position.x : -ReferancePlatform.transform.position.x);
        float newXScale = xPositionDiff + (2 * overOffset);

        // SAHNEDE KALAN PARCA
        PlatformModel newReferancePlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        newReferancePlatform.SetActive();
        newReferancePlatform.transform.position = new Vector3(isLeft == true ? newXPivot : -newXPivot, 0, MovingPlatform.transform.position.z);
        newReferancePlatform.transform.localScale = new Vector3(newXScale, 1, 3);
        newReferancePlatform.IsPlatformMoving = false;


        // ASAGI DUSEN PARCA
        PlatformModel dropPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        dropPlatform.MainCollider.enabled = false;
        dropPlatform.SetActive();
        dropPlatform.Drop();

        float dropPlatformScaleX = Mathf.Abs(ReferancePlatform.transform.localScale.x - newReferancePlatform.transform.localScale.x);
        float dropPlatformPositionX = isLeft == false ? -(ReferancePlatform.transform.localScale.x / 2 + dropPlatformScaleX / 2) + ReferancePlatform.transform.position.x : ((ReferancePlatform.transform.localScale.x / 2) + (dropPlatformScaleX / 2)) + ReferancePlatform.transform.position.x;
        dropPlatform.transform.localScale = new Vector3(dropPlatformScaleX, 1, 3);
        dropPlatform.transform.position = new Vector3(dropPlatformPositionX, 0, MovingPlatform.transform.position.z);

        if (IsPlatformThin(newXScale))
        {
            newReferancePlatform.IsPlatformThin = true;
        }

        NumberOfCuts++;
        if (IsNumberOfCutsDone())
        {
            newReferancePlatform.IsLastPlatform = true;
        }


        CurrentLevel.GeneratedPlatforms.Add(newReferancePlatform);
        CurrentLevel.GeneratedPlatforms.Add(dropPlatform);

        newReferancePlatform.IsReferancePlatform = true;
        ReferancePlatform = newReferancePlatform;
        MovingPlatform = null;

        #region METHOD 1 ( OLD )

        //    float xPositionDiff = ReferancePlatform.transform.position.x - MovingPlatform.transform.position.x; // iki kubun arasindaki total mesafe farki

        //    float spaceOffset = xPositionDiff - (MovingPlatform.transform.localScale.x / 2);

        //    float calculatedPosX = ((ReferancePlatform.transform.position.x + spaceOffset) + (MovingPlatform.transform.position.x - spaceOffset)) / 2;

        //    PlatformModel tempPlatform = PlatformPool.GetDeactiveItem<PlatformModel>(); // SAHNEDE KALACAK OLAN KUP
        //    tempPlatform.SetActive();
        //    tempPlatform.transform.position = new Vector3(calculatedPosX, 0, MovingPlatform.transform.position.z);
        //    tempPlatform.transform.localScale = new Vector3(calculatedPosX, 1, 3);

        //    PlatformModel secondPlatform = PlatformPool.GetDeactiveItem<PlatformModel>();
        //    secondPlatform.SetActive();
        //    secondPlatform.transform.localScale = new Vector3(MovingPlatform.transform.localScale.x - calculatedPosX, 1, 3);
        //    float secondPlatformX = (MovingPlatform.transform.position.x + calculatedPosX) / 2;
        //    secondPlatform.transform.position = new Vector3(calculatedPosX + secondPlatformX, 0, MovingPlatform.transform.position.z);

        //    MovingPlatform.SetDeactive();

        // secondCube.AddComponent<Rigidbody>();

        #endregion

    }

    public void SetPlayerTargetPlatform(PlatformModel platform)
    {
        playerController.TargetPlatform = platform;

        if(platform.MovementType == PlatformMovementType.Finish)
        {
            playerController.SetPlayerPosition(platform.transform.position);
            GameController.ChangeGameState(GameStates.LevelSuccess);
        }
    }

    public bool IsPlatformsCloseEnough(PlatformModel referancePlatform, PlatformModel movingPlatform)
    {
        float offset = Mathf.Abs(referancePlatform.transform.position.x - movingPlatform.transform.position.x) - (referancePlatform.transform.localScale.x);
        Debug.Log("OFFSET = " + offset);
        if (offset <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPerfect(PlatformModel referancePlatform, PlatformModel movingPlatform)
    {
        float offset = Mathf.Abs(referancePlatform.transform.position.x - movingPlatform.transform.position.x);
        Debug.Log("OFFSET = " + offset);
        if (offset <= XPerfectToleranceValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPlatformThin(float xScale)
    {
        if (xScale < XFailScaleToleranceValue)
        {
            return true;
        }
        return false;
    }

    public bool IsNumberOfCutsDone()
    {
        if (NumberOfCuts >= CurrentLevel.NumberOfCuts)
        {
            return true;
        }

        return false;
    }
}
