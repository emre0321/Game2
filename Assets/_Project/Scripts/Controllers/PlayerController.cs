using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ControllerBaseModel
{
    public PlayerModel Player;
    [Header("TARGET PLATFORM")]
    public PlatformModel TargetPlatform;
    [Header("MOVEMENT")]
    [SerializeField] Vector3 PlayerStartPos;
    [SerializeField] float ForwardSpeed;
    [SerializeField] float SideSpeed;

    public override void ControllerUpdate(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.None:
                break;
            case GameStates.MainMenu:
                break;
            case GameStates.Gameplay:
                PlayerMovement();
                break;
            case GameStates.LevelSuccess:
                break;
            case GameStates.LevelFail:
                break;
        }
    }

    public override void OnGameStateChange(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.MainMenu:
                Player.ChangeAnimation(AnimationNames.Idle);
                break;
            case GameStates.Gameplay:
                Player.Die(false);
                Player.transform.position = LevelController.Instance.CurrentLevel.StaticPlatforms[0].transform.position + new Vector3(0, 0.5f, 0);
                LevelController.Instance.SetPlayerTargetPlatform(LevelController.Instance.CurrentLevel.StaticPlatforms[0]);
                Player.ChangeAnimation(AnimationNames.Run);
                break;
            case GameStates.LevelSuccess:
                Player.ChangeAnimation(AnimationNames.Dance);
                break;
            case GameStates.LevelFail:
                Player.Die(true);
                break;

        }
    }

    public void PlayerMovement()
    {
        if (TargetPlatform == null)
            return;

        Player.transform.Translate(Vector3.forward * Time.deltaTime * ForwardSpeed);
        Vector3 playerCurrentPos = Player.transform.position;
        float lerpedXPos = Mathf.Lerp(playerCurrentPos.x, TargetPlatform.transform.position.x, Time.deltaTime * SideSpeed);
        playerCurrentPos.x = lerpedXPos;
        Player.transform.position = playerCurrentPos;

    }

    public void SetPlayerPosition(Vector3 pos)
    {
        Player.transform.position = pos;
    }
}
