using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : ControllerBaseModel
{

    [SerializeField] CinemachineVirtualCamera CinemachineCamera;

    [Header("PLAYER FOLLOW OFFSETS")]
    [SerializeField] Vector3 PlayerFollowOffset;

    [Header("ORBITAL CAMERA OFFSETS")]
    [SerializeField] Vector3 OrbitalCameraOffset;
    [SerializeField] float OrbitalCameraSpeed;

    public override void Initialize()
    {
        ChangeCameraType(CameraTypes.PlayerFollow);
    }


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
                ChangeCameraType(CameraTypes.PlayerFollow);

                break;
            case GameStates.LevelSuccess:
                ChangeCameraType(CameraTypes.OrbitalMove);
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

    public void ChangeCameraType(CameraTypes cameraType)
    {
        switch (cameraType)
        {
            case CameraTypes.PlayerFollow:
                var tempTransposer = CinemachineCamera.AddCinemachineComponent<CinemachineTransposer>();
                tempTransposer.m_FollowOffset = PlayerFollowOffset;
                tempTransposer.m_XDamping = 0;
                break;
            case CameraTypes.OrbitalMove:
                var tempOrbital = CinemachineCamera.AddCinemachineComponent<CinemachineOrbitalTransposer>();
                tempOrbital.m_FollowOffset = OrbitalCameraOffset;
                tempOrbital.m_XAxis.m_InputAxisName = "";
                tempOrbital.m_XAxis.m_InputAxisValue = OrbitalCameraSpeed;
                tempOrbital.m_XDamping = 0;
                break;
         
        }
    }

}
