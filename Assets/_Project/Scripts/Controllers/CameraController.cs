using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : ControllerBaseModel
{

    [SerializeField] CinemachineVirtualCamera CinemachineCamera;

    public override void OnGameStateChange(GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.None:
                break;
            case GameStates.MainMenu:
                break;
            case GameStates.Gameplay:
                PlayerController playerController = GameController.Instance.GetController<PlayerController>();
                SetCameraTarget(playerController.Player.transform);
                break;
            case GameStates.LevelSuccess:
                break;
            case GameStates.LevelFail:
                SetCameraTarget(null);
                break;

        }
    }

    public void SetCameraTarget(Transform target)
    {
        CinemachineCamera.Follow = target;
        CinemachineCamera.LookAt = target;
    }
}
